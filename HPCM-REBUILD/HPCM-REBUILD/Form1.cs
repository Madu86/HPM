using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using System.Collections;
using System.IO;
using System.Threading;
using System.Windows;
using System.Media;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Diagnostics;



namespace HPCM_REBUILD
{
    public partial class frmMainHPCM : Form
    {
        #region -- Class Level variables--
        //Markers...
        GMapMarkerCircle releasePointMarker1;
        GMapMarkerCircle releasePointMarker2;
        GMapMarkerCircle releasePointMarker3;
        GMapMarkerCircle releasePointMarker4;

        //public static GMapMarkerCircle metGridOriginMarker;
        GMapMarkerCircle metStationMarker;
        
        //A marker variable to facilitate UI events...
        GMapMarker currentMarker;


        
        public PointLatLng currentMarkerPosition {

            get { return currentMarker.Position; }
            set { currentMarker.Position = value; }
        
        }
        
        //A varialbe to note mouse key press event...

        bool isMouseDown;

        //layers...
        GMapOverlay middleLayer;
        GMapOverlay metMarkerLayer;
        
        //A route declaration for line sources...
        GMapRoute lineSource;


        //Marker panel variables.. 

        public static List<GMapMarkerCircle> metStationList;
        public static List<PointLatLng> sourcePointList;
  

   

        //A supporting variable declaration for map navigation using mouse events...
        int navigatingDirection;

        //Indicates the type of the source...

        public static string shapeOfTheSource;

        //Variables for graphic events...

        Pen drawingPen;
        SolidBrush fillBrush;     
        public static string graphicItem;
        
        //Delegate declarations for thread safe satus strip updates...
        private delegate void StatusStripDelegate(string text);

        //Delgate declarations for project progress and completion events...

        private delegate void UpdateStatusDelegate(string text);
        private delegate void CloseProgessWindowDelegate();


        //A variable to check the validation status...
        public static bool validated;

        //Some variables for database operations...
        OleDbConnection con = new OleDbConnection();
        OleDbDataAdapter da;
        DataSet ds = new DataSet();


        #endregion

        public frmMainHPCM()
        {
            InitializeComponent();

           SplashForm splashForm = new SplashForm();
            splashForm.Show();

            splashForm.UpdateSplashForm("Initializing components...");
            splashForm.Update();
            Thread.Sleep(1000);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |ControlStyles.UserPaint | ControlStyles.DoubleBuffer,true);


            try
            {
                splashForm.UpdateSplashForm("Checking network connections...");
                splashForm.Update();
                Thread.Sleep(600);

                System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("www.Google.com");

                splashForm.UpdateSplashForm("Connecting to internet succeeded...");
                splashForm.Update();
                Thread.Sleep(600);
            }
            catch
            {
                mapViewer.Manager.Mode = AccessMode.CacheOnly;

                splashForm.UpdateSplashForm("Failed to access internet...");
                splashForm.Update();

                MessageBox.Show("No internet connection available, switching to cache mode...", "Hazard Prediction Model", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            splashForm.UpdateSplashForm("Configuring user interface...");
            splashForm.Update();

            Thread.Sleep(1000);

            // config map 
            mapViewer.MapProvider = GMapProviders.GoogleHybridMap;


            mapViewer.Position = new PointLatLng(7.565, 80.43);
              //mapViewer.Position = new PointLatLng(39.8, -86.2);
            mapViewer.MinZoom = 1;
            mapViewer.MaxZoom = 17;
            mapViewer.Zoom = 6;
           

            //Adding custom layers...

            middleLayer = new GMapOverlay("middleLayer");
            middleLayer = new GMapOverlay();
            
            mapViewer.Overlays.Add(middleLayer);

            metMarkerLayer = new GMapOverlay("metMarkerLayer");
            mapViewer.Overlays.Add(metMarkerLayer);

            drawingPen=new Pen(Color.Red,15);
            fillBrush=new SolidBrush(Color.Red);


            //Adding markers...
            releasePointMarker1 = new GMapMarkerCircle(mapViewer.Position, drawingPen, fillBrush, 4, "Release Point");
            releasePointMarker1.IsVisible = false;
            releasePointMarker1.Tag = new MarkerTag("Untitled","ReleasePoint");

            PointLatLng p = new PointLatLng(mapViewer.Position.Lat, mapViewer.Position.Lng + 0.1);
            releasePointMarker2 = new GMapMarkerCircle(p, drawingPen, fillBrush, 4, "Release Point");
            releasePointMarker2.Tag = new MarkerTag("Untitled", "ReleasePoint");
            releasePointMarker2.IsVisible = false;

            releasePointMarker3 = new GMapMarkerCircle(mapViewer.Position, drawingPen, fillBrush, 4, "Release Point");
            releasePointMarker3.Tag = new MarkerTag("Untitled", "ReleasePoint");
            releasePointMarker3.IsVisible = false;

            releasePointMarker4 = new GMapMarkerCircle(mapViewer.Position, drawingPen, fillBrush, 4, "Release Point");
            releasePointMarker4.Tag = new MarkerTag("Untitled", "ReleasePoint");
            releasePointMarker4.IsVisible = false;

            drawingPen=new Pen(Color.White,15);
            
            //metGridOriginMarker = new GMapMarkerCircle(mapViewer.Position,drawingPen,fillBrush,4,"");
            //metGridOriginMarker.Tag = new MarkerTag("Untitled", "MetGridMarker");
            //metGridOriginMarker.IsVisible = false;

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "University of Colombo/Hazard Prediction Model/dbHpm.mdb");

           con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+filePath+";Persist Security Info=True;Jet OLEDB:Database Password=keevamalamute";
            con.Open();
            da = new OleDbDataAdapter("SELECT * FROM metStations", con);
            con.Close();
            da.Fill(ds, "metStations");


            LoadSLMetStations();




            metStationList = new List<GMapMarkerCircle>();
            sourcePointList = new List<PointLatLng>();

            //Configuring on map navigation buttons...

            navigatingDirection = 0;
            this.btnNavigateUp.Parent = this.mapViewer;
            this.btnNavigateUp.BackColor = Color.Transparent;

            this.btnNavigateDown.Parent = this.mapViewer;
            this.btnNavigateDown.BackColor = Color.Transparent;

            this.btnNavigateLeft.Parent = this.mapViewer;
            this.btnNavigateLeft.BackColor = Color.Transparent;

            this.btnNavigateRight.Parent = this.mapViewer;
            this.btnNavigateRight.BackColor = Color.Transparent;

            mapViewer.OnMarkerEnter += new MarkerEnter(mapViewer_OnMarkerEnter);
            mapViewer.MouseUp += new MouseEventHandler(mapViewer_MouseUp);

            splashForm.UpdateSplashForm("Initialization completed...");
            splashForm.Update();
            Thread.Sleep(1000);
            splashForm.Close();

            

        }

        #region --Supporting Methods --

        //populates meteorological checked list box...

        private void LoadSLMetStations() {


            int maxRows = ds.Tables[0].Rows.Count;

            for (int i = 0; i < maxRows; i++)
            {

                chkLstBoxSLMetStations.Items.Add(ds.Tables[0].Rows[i].ItemArray[1]);

            }
        
        
        }



        //Reads *.hpm files...

        public static string dataInputString;
        public static string projectDataString;
        public static bool isFileMode = false;
        

        private void openFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "HPM Data File|*.hpm";
            

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

       
                StreamReader sr = new StreamReader(ofd.OpenFile());
                string wholeContent= sr.ReadToEnd();
                var subContents = Regex.Split(wholeContent,"\r\n\r\n");

                string[] locationString = subContents[1].Split(';');
                dataInputString = subContents[2];
                projectDataString = subContents[3];

                this.defaultSettings();

                foreach (string s in locationString) {

                    string[] location = s.Split(',');

                    if (location[1] == "ReleasePoint") {

                        if (!releasePointMarker1.IsVisible)
                        {

                            releasePointMarker1.Position =new PointLatLng(Convert.ToDouble(location[2]),Convert.ToDouble(location[3]));
                            middleLayer.Markers.Add(releasePointMarker1);

                            ((MarkerTag)releasePointMarker1.Tag).Name = location[0];
                            ((MarkerTag)releasePointMarker1.Tag).Type = location[1];

                            releasePointMarker1.IsVisible = true;

                        }
                        shapeOfTheSource = "Point";
                        toolStripButtonPointSource.Enabled = false;

                    }
                    else if (location[1] == "MetMarker") {

                        drawingPen = new Pen(Color.YellowGreen, 15);
                        fillBrush = new SolidBrush(Color.YellowGreen);
                        PointLatLng metMarkerPosition = new PointLatLng(Convert.ToDouble(location[2]), Convert.ToDouble(location[3]));
                        metStationMarker = new GMapMarkerCircle(metMarkerPosition, drawingPen, fillBrush, 4, "");
                        metStationMarker.Tag = new MarkerTag(location[0], location[1]);
                        metMarkerLayer.Markers.Add(metStationMarker);
                        metStationMarker.IsVisible = true;
                        metStationList.Add(metStationMarker);
                    
                    }
                    

                
                }

                isFileMode = true;
                toolStripButtonOpen.Enabled = false;
                openProjectToolStripMenuItem.Enabled = false;
                sr.Close();
            
            }

        }


        //Writes data input of the current project into *.hpm files
        private void saveFile() {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "HPM Data File|*.hpm";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

               

                //Write initial content...

                StreamWriter sw = new StreamWriter(sfd.FileName);

                var initalContent = BuildString(new string[] { "~", "Hazard Prediction Model Version 1.0.0.0", Convert.ToString(System.DateTime.Now), "~" }, ' ');

                sw.WriteLine(initalContent);
                sw.WriteLine();
                sw.Flush();


                //Creates a string of locations................

                List<string> positionStringList=new List<string>();                

                var s = BuildString(new double[] { sourcePointList.ElementAt(0).Lat,sourcePointList.ElementAt(0).Lng},',');

                var s1=BuildString(new string[] {"Release_Point","ReleasePoint",s},',');

                positionStringList.Add(s1);

                foreach(GMapMarkerCircle m in metStationList){

                    positionStringList.Add(BuildString(new String[] { ((MarkerTag)m.Tag).Name, ((MarkerTag)m.Tag).Type ,Convert.ToString(m.Position.Lat), Convert.ToString(m.Position.Lng) }, ','));
                    
               
                }



                for (int i = 0; i < positionStringList.Count; i++)
                {

                    if (i % 2 != 0)
                    {
                        positionStringList.Insert(i, ";");
                    }

                }

                var positionStringArray = positionStringList.ToArray();

               var positionString=string.Concat(positionStringArray);

                sw.WriteLine(positionString);
                sw.WriteLine();
                sw.Flush();



                //Writes datainput to the binary...


                //Date and time...
                var s2 = BuildString(new string[] { Convert.ToString(frmDataInput.month), Convert.ToString(frmDataInput.date), Convert.ToString(frmDataInput.year) }, ','); //Date String...

               var s3= BuildString(new string[] {Convert.ToString(frmDataInput.hour),Convert.ToString(frmDataInput.minute),Convert.ToString(frmDataInput.second)},','); // Time String...


               var dateTimeString = BuildString(new string[] {s2,s3}, ';');

               sw.WriteLine(dateTimeString);
               sw.Flush();

                //Source details...

               sw.WriteLine(frmDataInput.typeOfTheSource);
               sw.Flush();

               if (frmDataInput.typeOfTheSource == "Instantaneous Source") {

                   var sourceDataString = BuildString(new string[] { Convert.ToString(frmDataInput.heatOfDetonation), Convert.ToString(frmDataInput.weightOfExplosive), Convert.ToString(frmDataInput.effectiveHeatPerentage), frmDataInput.chemicalAgent, Convert.ToString(frmDataInput.agentWeight), Convert.ToString(frmDataInput.effectiveAgentPercentage) }, ',');
                   sw.WriteLine(sourceDataString);
                   sw.Flush();
               }
               else if (frmDataInput.typeOfTheSource == "Semi-Continuous Source")
               {

                   var sourceDataString = BuildString(new string[] { Convert.ToString(frmDataInput.sourceTemperature), Convert.ToString(frmDataInput.releaseRate), Convert.ToString(frmDataInput.sourceRadius), frmDataInput.chemicalAgent, Convert.ToString(frmDataInput.agentWeight), Convert.ToString(frmDataInput.effectiveAgentPercentage) }, ',');
                   sw.WriteLine(sourceDataString);
                   sw.Flush();
               }


                //Geographical data...

               var geoDataString = BuildString(new double[] { frmDataInput.surfaceRoughnessLength, frmDataInput.albedoValue, frmDataInput.surfaceMoistureAvailability }, ',');
               sw.WriteLine(geoDataString);
               sw.Flush();

                //Meteorological data...

               var windDataString = BuildString(frmDataInput.windSpeed.ToArray(), ',');
               sw.WriteLine(windDataString);
               sw.Flush();

               var wdDataString = BuildString(frmDataInput.windDirection.ToArray(), ',');
               sw.WriteLine(wdDataString);
               sw.Flush();

               var tempDataString = BuildString(frmDataInput.DryBulbTemperature.ToArray(), ',');
               sw.WriteLine(tempDataString);
               sw.Flush();

               var pressureDataString = BuildString(frmDataInput.pressure.ToArray(), ',');
               sw.WriteLine(pressureDataString);
               sw.Flush();

               var relHumDataString = BuildString(frmDataInput.relativeHumidity.ToArray(), ',');
               sw.WriteLine(relHumDataString);
               sw.Flush();

               var ccDataString = BuildString(frmDataInput.cloudCover.ToArray(), ',');
               sw.WriteLine(ccDataString);
               sw.Flush();

               var mixingHeightDataString = BuildString(frmDataInput.mixingHeight, ',');
               sw.WriteLine(mixingHeightDataString);
               sw.Flush();

               var rainFallDataString = BuildString(frmDataInput.precipitationIntensity.ToArray(), ',');
               sw.WriteLine(rainFallDataString);
               sw.WriteLine();
               sw.Flush();
               
                //Project options...

               var gridOptionsDataString = BuildString(new string[] { Convert.ToString(Meteorology.NumberOfMetCellsX), Convert.ToString(Meteorology.NumberOfMetCellsY), Convert.ToString(Meteorology.NumberOfMetCellsZ), Convert.ToString(Meteorology.DomainHeight), Convert.ToString(Meteorology.HorizontalDimensions), Convert.ToString(dispersion.NumOfHorizontalConcCells) }, ','); // Grid variables...
               sw.WriteLine(gridOptionsDataString);
               sw.Flush();

               var metOptionsDataString = BuildString(new string[] { Convert.ToString(Meteorology.TimeZone), Convert.ToString(Meteorology.ConsiderHeightOfInfluence), Convert.ToString(Meteorology.HeightOfInfluence),Convert.ToString(Meteorology.ConsiderRadiusOfInfluence),Convert.ToString(Meteorology.RadiusOfInfluence),Convert.ToString(Base2.MinimumDivergence) }, ','); // Grid variables...
               sw.WriteLine(metOptionsDataString);
               sw.Flush();

               var samplingOptionsDataString = BuildString(new string[] { Convert.ToString(dispersion.ConcentrationLayerHeight),Convert.ToString(dispersion.SamplingTime),Convert.ToString(dispersion.IsStochasticApproach), Convert.ToString(dispersion.IsBayersDispersion),Convert.ToString(dispersion.CorrectionFactorOfCloudRise)}, ','); // Grid variables...
               sw.WriteLine(samplingOptionsDataString);
               sw.Flush();


                sw.Close();

            
        }

        

        
        }


        //Builds data in string format to write in to files...

        private string BuildString(double[] arrVariables, char separator)
        {


            StringBuilder sb = new StringBuilder();

            int i = 0;
            foreach (double d in arrVariables)
            {

                if (i != 0)
                {

                    sb.Append(separator);
                }

                var st = Convert.ToString(d);
                sb.Append(st);

                i++;
            }

            return sb.ToString();
        }


        private string BuildString(string[] arrVariables, char separator)
        {


            StringBuilder sb = new StringBuilder();

            int i = 0;
            foreach (string d in arrVariables)
            {

                if (i != 0)
                {

                    sb.Append(separator);
                }

                var st = Convert.ToString(d);
                sb.Append(st);

                i++;
            }

            return sb.ToString();
        }



        private void printReport() {

            PrintDialog printReport = new PrintDialog();
            printReport.ShowDialog();
        }


        private void dataInput() 
        {

            progressWindow pw = new progressWindow();
            pw.Opacity = 0.75;

            //PictureBox pb = new PictureBox();
            //pb.Image = Image.FromFile("aniprogress.gif");
            //pb.Size = new Size(50, 50);
    
  
            //pw.Controls.Add(pb);

            //pb.Location = new Point(((this.ClientSize.Width / 2) - (pb.Size.Width / 2)), ((this.ClientSize.Height / 2) - (pb.Size.Height / 2)));


             if (frmMainHPCM.metStationList.Count == 0 )
            {
                throw new System.Exception("Please mark the locations of meteorological stations on the map..!");
            }
            else if (releasePointMarker1.IsVisible == false) {

                throw new System.Exception("Please add a release point on to the map..!");
            }

            else
            {
                frmDataInput.numberOfMetStations = frmMainHPCM.metStationList.Count;
                
                
                //If loop to obtain exact release point location inserted through property text boxes...
                if (shapeOfTheSource == "Point" )
                  {

                    sourcePointList.Clear();
                    sourcePointList.Add(releasePointMarker1.Position);
                 }


                pw.Show();
                //pw.TopMost = true;
              
            
                //Loads geographical data and forms grid...
                Geography g = new Geography();
                g.CalculateGeography();



                Form dataInput = new frmDataInput(this);
                toolStripButtonMetMarker.Enabled = false;
                toolStripButtonLineSource.Enabled = false;
                toolStripButtonPointSource.Enabled = false;
                chkLstBoxSLMetStations.Enabled = false;
                toolStripButtonConfigurations.Enabled = false;
                configurationsToolStripMenuItem.Enabled = false;

                toolStripButtonEreser.Enabled = false;
                cmbBoxMapType.Enabled = false;
                btnReloadMap.Enabled = false;

                pw.Close();

                dataInput.ShowDialog();


            }

        }


        //Default settings for the program...
        private void defaultSettings() {

            cmbBoxMapType.SelectedIndex = 2;
            txtBoxCustomLat.Text = Convert.ToString(mapViewer.Position.Lat);
            txtBoxCustomLng.Text = Convert.ToString(mapViewer.Position.Lng);
            toolStripButtonRun.Enabled = false;
            runProjectToolStripMenuItem.Enabled = false;

            toolStripButtonDrawWind.Enabled = false;
            isFileMode = false;
            toolStripButtonConfigurations.Enabled = true;
            configurationsToolStripMenuItem.Enabled = true;

            toolStripButtonEreser.Enabled = true;
            
            

            ClearMapViewer();

            

           

        }


        //Map navigation using on screen buttons...
        private void offsetMap(int offsetSize) {


            if (navigatingDirection == 1) {
                mapViewer.Offset(0, offsetSize);
            }
            if (navigatingDirection == 2) {
                mapViewer.Offset(0, -offsetSize);
            }
            if (navigatingDirection == 3) {

                mapViewer.Offset(offsetSize, 0);
            }
            if (navigatingDirection == 4) {

                mapViewer.Offset(-offsetSize, 0);
            }
        }
       

        //changes marker colour on mouse move over the marker...
        private void ChangeMarkerColor(GMapMarkerCircle marker, Color newColor, Color defaultColor)
        {

            if (marker.IsMouseOver)
            {

                marker.ToolTipText = ((MarkerTag)marker.Tag).Name;
                GMapToolTip toolTip = new GMapToolTip(marker);

                marker.ToolTip = toolTip;

                drawingPen = new Pen(newColor, 15);
                marker.OuterPen = drawingPen;
            }
            else
            {

                drawingPen = new Pen(defaultColor, 15);
                marker.OuterPen = drawingPen;
            }

        }

        //Helper method for displaying map viewer status on the status bar...

        private void ShowMapViewerStatus(string message)
        {

            toolStripStatusLabelPos.Text = message;

        }

        private void ClearMapViewer()
        {

            try
            {
                mapViewer.Paint -= new PaintEventHandler(WindPaintEventHandler);
            }
            catch {
            
            }

            try
            {
                mapViewer.Paint -= new PaintEventHandler(ConPaintEventHandler);
            }
            catch { }


            try
            {
                releasePointMarker1.IsVisible = false;
                releasePointMarker2.IsVisible = false;
                shapeOfTheSource = null;


                sourcePointList.Clear();
                middleLayer.Routes.Clear();
                //metGridOriginMarker.IsVisible = false;

                metStationList.Clear();
                metMarkerLayer.Markers.Clear();
                

                mapViewer.Refresh();
                toolStripButtonPointSource.Enabled = true;
                //toolStripButtonMetGridMarker.Enabled = true;
                //toolStripButtonLineSource.Enabled = true;

                chkLstBoxSLMetStations.Items.Clear();
                LoadSLMetStations();

            }
            catch
            {

                throw new System.Exception("Some items on the map viewer cannot be removed...!");
            }


        }

        #endregion

        #region --Map events--

        private void mapViewer_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (!validated)
            {
                if (e.Button == MouseButtons.Right)
                {
                    markerMenu.Show(mapViewer, e.X, e.Y);

                    if (currentMarker != null)
                    {
                        txtBoxMarkerName.Text = ((MarkerTag)currentMarker.Tag).Name;
                        txtBoxMarkerLat.Text = Convert.ToString(currentMarker.Position.Lat);
                        txtBoxMarkerLng.Text = Convert.ToString(currentMarker.Position.Lng);
                    }

                }
            }
            
        }


        private void mapViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (!validated)
            {
                isMouseDown = true;
            }
        }


        void mapViewer_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
                //currentMarker = null;
            }
        }

        void mapViewer_OnMarkerEnter(GMapMarker item)
        {
            currentMarker = item;
        }


#endregion

        #region --User Interface Events --

        #region--Status strip updating events...
        
        //Displays latitude and the longitude on the status bar...

        private void CurrentMouseCoordinates(MouseEventArgs e) {
            PointLatLng p = mapViewer.FromLocalToLatLng(Cursor.Position.X, Cursor.Position.Y);

            this.Invoke(new StatusStripDelegate(ShowMapViewerStatus),new object[] {(Convert.ToString(Math.Round(p.Lat, 4)) + "°N  " + Convert.ToString(Math.Round(p.Lng, 4)) + "°E")});
         
            
        }
       

        private void mapViewer_MouseMove(object sender, MouseEventArgs e)
        {
       
            CurrentMouseCoordinates(e);

            if (!validated)
            {

                if (e.Button == MouseButtons.Left && isMouseDown)
                {
                    if (currentMarker != null)
                    {
                        if (currentMarker.IsVisible)
                        {

                            currentMarker.Position = mapViewer.FromLocalToLatLng(e.X, e.Y);

                            if (shapeOfTheSource == "Line" && ((MarkerTag)currentMarker.Tag).Type == "ReleasePoint")
                            {

                                sourcePointList.Clear();
                                middleLayer.Routes.Clear();
                                sourcePointList.Add(releasePointMarker1.Position);
                                sourcePointList.Add(releasePointMarker2.Position);
                                lineSource = new GMapRoute(sourcePointList, "lineSource");
                                middleLayer.Routes.Add(lineSource);

                            }
                            else if (shapeOfTheSource == "Point" && ((MarkerTag)currentMarker.Tag).Type == "ReleasePoint")
                            {

                                sourcePointList.Clear();
                                sourcePointList.Add(releasePointMarker1.Position);
                            }
                            txtBoxMarkerName.Text = ((MarkerTag)currentMarker.Tag).Name;
                            txtBoxMarkerLat.Text = Convert.ToString(currentMarker.Position.Lat);
                            txtBoxMarkerLng.Text = Convert.ToString(currentMarker.Position.Lng);

                        }
                    }


                }


                ChangeMarkerColor(releasePointMarker1, Color.Yellow, Color.Red);
                ChangeMarkerColor(releasePointMarker2, Color.Yellow, Color.Red);
                //ChangeMarkerColor(metGridOriginMarker, Color.Yellow, Color.White);


                foreach (GMapMarkerCircle metStationMarker in metStationList)
                {

                    ChangeMarkerColor(metStationMarker, Color.Yellow, Color.YellowGreen);

                }
            }
        }


        //Updating status strip label for tile loading events...
        private void mapViewer_OnTileLoadStart()
        {
            timerHPCM.Start();

        }

        private void timerHPCM_Tick(object sender, EventArgs e)
        {

      
            this.Invoke(new StatusStripDelegate(ShowMapViewerStatus), new object[] { "Loading Maps..." });
 
        }

        private void mapViewer_OnTileLoadComplete(long ElapsedMilliseconds)
        {
            timerHPCM.Stop();
            this.Invoke(new StatusStripDelegate(ShowMapViewerStatus), new object[] { "Map loading completed in " + Convert.ToString(ElapsedMilliseconds) + " miliseconds" });


        }

#endregion


        #region --Map Navigation and Zooming--

        //Handles navigation through maps using arrow keys...
    
        private void frmMainHPCM_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up) {

                navigatingDirection = 1;
                offsetMap(22);
            }
            else if (e.KeyCode == Keys.Down) {

                navigatingDirection = 2;
                offsetMap(22);
            }
            else if (e.KeyCode == Keys.Left) {

                navigatingDirection = 3;
                offsetMap(22);
            }
            else if (e.KeyCode == Keys.Right) {

                navigatingDirection = 4;
                offsetMap(22);
            }
        }

        //Handles navigation through maps using num pad...

        private void frmMainHPCM_KeyPress(object sender, KeyPressEventArgs e)
        {
           

            if (e.KeyChar == '+') {

                mapViewer.Zoom += 1;
            }

            if (e.KeyChar == '-') {

                mapViewer.Zoom -= 1;
            }
            else if (e.KeyChar == '8')
            {
                navigatingDirection = 1;
                offsetMap(22);
            }
            else if (e.KeyChar == '2') {

                navigatingDirection = 2;
                offsetMap(22); 
            }

            else if (e.KeyChar== '4')
            {

                navigatingDirection = 3;
                offsetMap(22);
            }
            else if (e.KeyChar == '6')
            {

                navigatingDirection = 4;
                offsetMap(22);
            }
        
        }


        //Handles map zooming on scroll event...
        private void mapViewer_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.SmallIncrement)
            {
                mapViewer.Zoom += 1;
            }
        }

        #region --Map navigation on onscreen buttons...

        private void btnNavigateUp_MouseEnter(object sender, EventArgs e)
        {
            navigatingDirection = 1;
            timerMapNavigation.Start();
        }

        private void timerMapNavigation_Tick(object sender, EventArgs e)
        {
            offsetMap(22);
        }

        private void btnNavigateUp_MouseLeave(object sender, EventArgs e)
        {
            timerMapNavigation.Stop();
        }

        private void btnNavigateDown_MouseEnter(object sender, EventArgs e)
        {
            navigatingDirection = 2;
            timerMapNavigation.Start();
        }

        private void btnNavigateDown_MouseLeave(object sender, EventArgs e)
        {
            timerMapNavigation.Stop();
        }



        private void btnNavigateLeft_MouseEnter(object sender, EventArgs e)
        {
            navigatingDirection = 3;
            timerMapNavigation.Start();

        }

        private void btnNavigateLeft_MouseLeave(object sender, EventArgs e)
        {
            timerMapNavigation.Stop();
        }

        private void btnNavigateRight_MouseEnter(object sender, EventArgs e)
        {
            navigatingDirection = 4;
            timerMapNavigation.Start();
        }

        private void btnNavigateRight_MouseLeave(object sender, EventArgs e)
        {
            timerMapNavigation.Stop();
        }

        #endregion 

        #endregion

        //Handles menu and tool strip buttons...
        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            printReport();
        }

        private void printReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printReport();
        }
      
        private void toolStripButtonInput_Click(object sender, EventArgs e)
        {
            try
            {



                dataInput();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }






        //Changes the map type of the map viewer...
        //private void cmbBoxMapType_DropDownClosed(object sender, EventArgs e)
        //{


        //    mapViewer.ReloadMap();
        //    mapViewer.Focus();

        //}

        //Handles default program properties...
        private void frmMainHPCM_Load(object sender, EventArgs e)
        {

            defaultSettings();

            //AddMedaStation();

          
        }

        

        private void btnCustomNavigate_Click(object sender, EventArgs e)
        {
            mapViewer.Position=new PointLatLng(Convert.ToDouble(txtBoxCustomLat.Text),Convert.ToDouble(txtBoxCustomLng.Text));
        }


        //Reloads the current map on map viewer
        private void btnReloadMap_Click(object sender, EventArgs e)
        {

            if (cmbBoxMapType.SelectedIndex == 0)
            {
                mapViewer.MapProvider = GMapProviders.GoogleMap;

            }
            else if (cmbBoxMapType.SelectedIndex == 1)
            {

                mapViewer.MapProvider = GMapProviders.GoogleSatelliteMap;

            }

            else if (cmbBoxMapType.SelectedIndex == 2)
            {

                mapViewer.MapProvider = GMapProviders.GoogleHybridMap;
            }

            else if (cmbBoxMapType.SelectedIndex == 3)
            {

                mapViewer.MapProvider = GMapProviders.GoogleTerrainMap;
            }
            else if (cmbBoxMapType.SelectedIndex == 4)
            {

                mapViewer.MapProvider = GMapProviders.OpenStreetMap;
            }

            mapViewer.ReloadMap();
            mapViewer.Focus();
        }

       
        //Handles marker panel button click events...

       
        private void toolStripButtonMetMarker_Click(object sender, EventArgs e)
        {
            //setMarkerPosition(toolStripButtonMetMarker);
            drawingPen = new Pen(Color.YellowGreen, 15);
            fillBrush = new SolidBrush(Color.YellowGreen);
            metStationMarker = new GMapMarkerCircle(mapViewer.Position, drawingPen, fillBrush, 4, "");
            metStationMarker.Tag = new MarkerTag("Untitled", "MetMarker");
            metMarkerLayer.Markers.Add(metStationMarker);
            metStationMarker.IsVisible = true;
            metStationList.Add(metStationMarker);

   
        }


        private void toolStripButtonPointSource_Click(object sender, EventArgs e)
        {
            if (shapeOfTheSource=="Line")
            {
                releasePointMarker2.IsVisible = false;
                middleLayer.Routes.Clear();

                //sourcePointList.Clear();
            }
            if (!releasePointMarker1.IsVisible)
            {

                releasePointMarker1.Position = mapViewer.Position;
                middleLayer.Markers.Add(releasePointMarker1);
                releasePointMarker1.IsVisible = true;
                
            }
            shapeOfTheSource = "Point";
            toolStripButtonPointSource.Enabled = false;
        }

 

        private void toolStripButtonLineSource_Click(object sender, EventArgs e)
        {
            if (!releasePointMarker1.IsVisible)
            {
                releasePointMarker1.Position = mapViewer.Position;
                middleLayer.Markers.Add(releasePointMarker1);
                releasePointMarker1.IsVisible = true;
               
            }
            PointLatLng p = new PointLatLng(mapViewer.Position.Lat, mapViewer.Position.Lng + 0.1);
            releasePointMarker2.Position = p;
            middleLayer.Markers.Add(releasePointMarker2);
            releasePointMarker2.IsVisible = true;

            if (!(sourcePointList.Count == 0))
            {
                sourcePointList.Clear();            
            }

            sourcePointList.Add(releasePointMarker1.Position);
            sourcePointList.Add(releasePointMarker2.Position);

            lineSource = new GMapRoute(sourcePointList, "lineSource");
            middleLayer.Routes.Add(lineSource);
            lineSource.IsVisible = true;

            shapeOfTheSource = "Line";

            toolStripButtonLineSource.Enabled = false;
            
        }

        private void toolStripButtonAreaSource_Click(object sender, EventArgs e)
        {
            //setMarkerPosition(toolStripButtonAreaSource);
        }
        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            mapViewer.Zoom += 1;
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            mapViewer.Zoom -= 1;
        }

      
        private void toolStripButtonEreser_Click(object sender, EventArgs e)
        {
            ClearMapViewer();
        }






        private void toolStripButtonConfigurations_Click(object sender, EventArgs e)
        {
            projectOptions frmProjectOptions = new projectOptions();
            frmProjectOptions.ShowDialog();

        }




        //Updates progress status during the project is running...
        private void UpdateProgressStatus(string text) {

            lblProgressStatus.Text = text;
        }

        //Performs actions on the main form at the end of running...
        private void CloseProgressWindow() {

           
            toolStripButtonRun.Enabled = false;
            toolStripButtonDrawWind.Enabled = true;
          
            tbCreateTerrain.Enabled = false;
            tbCreateReport.Enabled = false;

            pnlWrkInPrgrs.Visible = false;
        }

        //StopWatch.StopWatch st = new StopWatch.StopWatch();
        Stopwatch st = new Stopwatch();
        
        private void toolStripButtonRun_Click(object sender, EventArgs e)
        {
            //progressWindow pw = new progressWindow();
            //pw.ShowDialog();
            toolStripButtonRun.Enabled = false;
            runProjectToolStripMenuItem.Enabled = false;

           toolStripButtonConfigurations.Enabled = false;
           configurationsToolStripMenuItem.Enabled = false;

            toolStripButtonSave.Enabled = false;
            saveProjectToolStripMenuItem.Enabled = false;

            toolStripButtonOpen.Enabled = false;
            openProjectToolStripMenuItem.Enabled = false;
            toolStripButtonNew.Enabled = false;
            newProjectToolStripMenuItem.Enabled = false;

            btnToImage.Enabled = false;
            pnlWrkInPrgrs.Location = new Point(((this.ClientSize.Width / 2) - (pnlWrkInPrgrs.Size.Width / 2)), ((this.ClientSize.Height / 2) - (pnlWrkInPrgrs.Size.Height / 2)));
            pnlWrkInPrgrs.Visible = true;
            pnlWrkInPrgrs.BringToFront();
            
            timerRunProject.Enabled = true;
            
           
            st.Start();

            bckGrndWrkrRun.RunWorkerAsync();

            //pw.Close();

        }



        private void bckGrndWrkrRun_DoWork(object sender, DoWorkEventArgs e)
        {

            Calculator c = new Calculator(mapViewer);           
            c.RunProject();

        }


        private void bckGrndWrkrRun_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke(new CloseProgessWindowDelegate(CloseProgressWindow));
            timerRunProject.Enabled = false;
            //MessageBox.Show(st.GetElapsedTimeString());
            //MessageBox.Show(Convert.ToString(st.ElapsedMilliseconds / 1000));
            
            MessageBox.Show("Project Completed...");

            toolStripButtonSave.Enabled = true;
            saveProjectToolStripMenuItem.Enabled = true;
        
            //toolStripButtonNew.Enabled = true;

            btnToImage.Enabled = true;

            st.Stop();
        }



        private void writeValues() {

            string valueOfIndex;
            string valueOfU;
            string valueOfV;
            string valueOfW;
            string valueOfTemperatureField;

            StreamWriter indexWriter = new StreamWriter("arrayIndex.txt", true);
            StreamWriter componentUWriter = new StreamWriter("U.txt", true);
            StreamWriter componentVWriter = new StreamWriter("V.txt", true);
            StreamWriter componentWWriter = new StreamWriter("W.txt", true);
            StreamWriter componentTemperatureWriter = new StreamWriter("Temperature.txt", true);

            for (int k = 1; k < Meteorology.NumberOfMetCellsZ + 1; k++)
            {

                for (int j = 1; j < Meteorology.NumberOfMetCellsY + 1; j++)
                {

                    for (int i = 1; i < Meteorology.NumberOfMetCellsX + 1; i++)
                    {

                        valueOfIndex = Convert.ToString(j) + "," + Convert.ToString(i) + "," + Convert.ToString(k) + "\n";
                        indexWriter.WriteLine(valueOfIndex);

                        valueOfU = Convert.ToString(Meteorology.U[j, i, k]) + "\n";
                        componentUWriter.WriteLine(valueOfU);

                        valueOfV = Convert.ToString(Meteorology.V[j, i, k]) + "\n";
                        componentVWriter.WriteLine(valueOfV);

                        valueOfW = Convert.ToString(Meteorology.W[j, i, k]) + "\n";
                        componentWWriter.WriteLine(valueOfW);

                        valueOfTemperatureField = Convert.ToString(Meteorology.temperatureField[j, i, k]) + "\n";
                        componentTemperatureWriter.WriteLine(valueOfTemperatureField);

                    }
                }

            }

            indexWriter.Close();
            componentUWriter.Close();
            componentVWriter.Close();
            componentWWriter.Close();
            componentTemperatureWriter.Close();
        }



       public static frmDrawWind dw;

        private void btnDrawWind_Click(object sender, EventArgs e)
        {
            if (dw == null)
            {

                graphicItem = "Wind";
                dw = new frmDrawWind(this);
                dw.ShowDialog();
            }
            else {

                dw.Focus();
            }
            

        }

        void mapViewer_Paint(object sender, PaintEventArgs e)
        {
            //throw new NotImplementedException();

            
        }




        private void toolStripButtonDrawWind_MouseEnter(object sender, EventArgs e)
        {
            if (dw != null)
            {

                dw.Show();
            }
        }



        private void markerMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == deleteToolStripMenuItem) {

                if (currentMarker != null) {
                    
                    
                    foreach (GMapMarkerCircle m in metStationList) {

                        if (m == currentMarker) {


                            try
                            {
                                MarkerTag mtag = (MarkerTag)m.Tag;

                                string itemName = mtag.Name;
                                chkLstBoxSLMetStations.Items.Add(itemName);
                                chkLstBoxSLMetStations.Refresh();
                                
                            }
                            catch { }



                            metStationList.Remove(m);
                            m.IsVisible = false;
                             break;
                        }
                    }

                    //currentMarker.Dispose();
                    //currentMarker = null;
                    mapViewer.Cursor = Cursors.Default;
                    mapViewer.CanDragMap = true;
           


                }
            
            }
            else if (e.ClickedItem == propertiesToolStripMenuItem) {
                xPanderPanel2.Expand = false;
                xPanderPanel1.Expand = true;
            }

        }
        




        #endregion

        private void txtBoxMarkerName_TextChanged(object sender, EventArgs e)
        {
            ((MarkerTag)currentMarker.Tag).Name = txtBoxMarkerName.Text;
        }


        private void txtBoxMarkerLat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)13)
            {
                try
                {

                    currentMarker.Position = new PointLatLng(Convert.ToDouble(txtBoxMarkerLat.Text), Convert.ToDouble(txtBoxMarkerLng.Text));
                   
                }
                catch { 
                
                }

            }
        }


        private void txtBoxMarkerLng_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {

                    currentMarker.Position = new PointLatLng(Convert.ToDouble(txtBoxMarkerLat.Text), Convert.ToDouble(txtBoxMarkerLng.Text));

                }
                catch
                {

                }

            }
        }






        private void panel1_PanelExpanding(object sender, BSE.Windows.Forms.XPanderStateChangeEventArgs e)
        {

            btnNavigateRight.Size = new Size(238, 514);
        }

        private void panel1_PanelCollapsing(object sender, BSE.Windows.Forms.XPanderStateChangeEventArgs e)
        {
            
            btnNavigateRight.Size = new Size(52, 514);
        }

        private void operationPanel_PanelExpanding(object sender, BSE.Windows.Forms.XPanderStateChangeEventArgs e)
        {
            btnNavigateLeft.Size = new Size(86, 514); 
        }

        private void operationPanel_PanelCollapsing(object sender, BSE.Windows.Forms.XPanderStateChangeEventArgs e)
        {
            btnNavigateLeft.Size = new Size(52, 514);
        }

        private void timerRunProject_Tick(object sender, EventArgs e)
        {

            lblProgressStatus.Text = Convert.ToString(st.ElapsedMilliseconds / 1000);

        }

        private void toolStripButtonDrawConc_Click(object sender, EventArgs e)
        {

            frmDrawConc dc = new frmDrawConc(this.mapViewer, this);

            dc.ShowDialog(this);

        }



        //Wind paint methods...

        #region Wind Paint Methods...

        private int GetWindAngle(int i, int j, int k)
        {

            int angle;

            angle = Convert.ToInt32(Math.Round((Math.Atan2(Meteorology.V[j, i, k], Meteorology.U[j, i, k]) * 180 / Math.PI), 0));

            return angle;
        }


        public void WindPaintEventHandler(object sender, PaintEventArgs e)
        {
            if (frmDrawWind.drawWind)
            {
                Pen windDrawingPen = new Pen(frmDrawWind.windColor, 1);
                PointLatLng currentLocation;
                int x;
                int y;
                float fx;
                float fy;
                float fax1;
                float fax2;
                float fay1;
                float fay2;
                double p = 10;
                double q = 5;
                int theeta;
                int alpha;
                int beeta = 45;


                for (int j = Meteorology.NumberOfMetCellsY - 1; j >= 0; j--)
                {
                    for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
                    {
                        theeta = GetWindAngle(i, j, frmDrawWind.drawWindOfLayer);
                        alpha = 360 - theeta;
                        currentLocation = Geography.gridPoints[j, i];
                        x = Convert.ToInt32(mapViewer.FromLatLngToLocal(currentLocation).X);
                        y = Convert.ToInt32(mapViewer.FromLatLngToLocal(currentLocation).Y);

                        fx = (float)(x + p * Math.Cos(alpha * Math.PI / 180));
                        fy = (float)(y + p * Math.Sin(alpha * Math.PI / 180));
                        e.Graphics.DrawLine(windDrawingPen, x, y, fx, fy);

                        fax1 = (float)(fx - q * Math.Cos((alpha - beeta) * Math.PI / 180));
                        fax2 = (float)(fx - q * Math.Sin((90 - (alpha + beeta)) * Math.PI / 180));
                        fay1 = (float)(fy - q * Math.Sin((alpha - beeta) * Math.PI / 180));
                        fay2 = (float)(fy - q * Math.Cos((90 - (alpha + beeta)) * Math.PI / 180));

                        e.Graphics.DrawLine(windDrawingPen, fx, fy, fax1, fay1);
                        e.Graphics.DrawLine(windDrawingPen, fx, fy, fax2, fay2);


                    }

                }

                if (toolStripButtonDrawConc.Enabled == false) {

                    toolStripButtonDrawConc.Enabled = true;
                
                }
            }

           mapViewer.Invalidate();

        }





        #endregion

        //Con paint methods...

        # region - Con Paint methods... -

        public static List<PointLatLng> lstCurvePoints = new List<PointLatLng>();
        public static List<PointLatLng> lstCurvePoints_2 = new List<PointLatLng>();
        public static List<PointLatLng> lstCurvePoints_3 = new List<PointLatLng>();
        public static List<PointLatLng> lstCurvePoints_4 = new List<PointLatLng>();

        public double conc_Level_4 = 1000;
        public double conc_Level_3 = 600;
        public double conc_Level_2 = 300;
        public double conc_Level_1 = 1;

        public Color color_Level_4 = Color.OrangeRed;
        public Color color_Level_3 = Color.Yellow;
        public Color color_Level_2 = Color.YellowGreen;
        public Color color_Level_1 = Color.Blue;
        PathGradientBrush pgb;

        public void ConPaintEventHandler(object sender, PaintEventArgs e)
        {




            if (frmDrawConc.drawCon)
            {

                mapViewer.Invalidate();

                int k = 0;

                Point[] arr_P4 = new Point[lstCurvePoints_4.Count];
                foreach (PointLatLng p in lstCurvePoints_4)
                {

                    int x1 = Convert.ToInt32(mapViewer.FromLatLngToLocal(p).X);
                    int y1 = Convert.ToInt32(mapViewer.FromLatLngToLocal(p).Y);
                    Point point_1 = new Point(x1, y1);

                    arr_P4.SetValue(point_1, k);
                    k += 1;


                }

                if (arr_P4.Length > 3)
                {
                    pgb = new PathGradientBrush(arr_P4);

                    pgb.CenterPoint = new Point(Convert.ToInt32(mapViewer.FromLatLngToLocal(frmMainHPCM.sourcePointList[0]).X), Convert.ToInt32(mapViewer.FromLatLngToLocal(frmMainHPCM.sourcePointList[0]).Y));
                    pgb.CenterColor = Color.YellowGreen;
                    pgb.SurroundColors = new Color[] { color_Level_1 };


                    e.Graphics.FillClosedCurve(pgb, arr_P4);



                }

                //Paint loop for maxcon/3


                k = 0;

                Point[] arr_P3 = new Point[lstCurvePoints_3.Count];
                foreach (PointLatLng p in lstCurvePoints_3)
                {

                    int x1 = Convert.ToInt32(mapViewer.FromLatLngToLocal(p).X);
                    int y1 = Convert.ToInt32(mapViewer.FromLatLngToLocal(p).Y);
                    Point point_1 = new Point(x1, y1);

                    arr_P3.SetValue(point_1, k);
                    k += 1;


                }

                if (arr_P3.Length > 3)
                {

                    pgb = new PathGradientBrush(arr_P3);

                    pgb.CenterPoint = new Point(Convert.ToInt32(mapViewer.FromLatLngToLocal(frmMainHPCM.sourcePointList[0]).X), Convert.ToInt32(mapViewer.FromLatLngToLocal(frmMainHPCM.sourcePointList[0]).Y));
                    pgb.CenterColor = Color.Yellow;
                    pgb.SurroundColors = new Color[] { color_Level_2 };
                    e.Graphics.FillClosedCurve(pgb, arr_P3);
                }


                //Paint loop for maxcon/2


                k = 0;

                Point[] arr_P2 = new Point[lstCurvePoints_2.Count];
                foreach (PointLatLng p in lstCurvePoints_2)
                {

                    int x1 = Convert.ToInt32(mapViewer.FromLatLngToLocal(p).X);
                    int y1 = Convert.ToInt32(mapViewer.FromLatLngToLocal(p).Y);
                    Point point_1 = new Point(x1, y1);

                    arr_P2.SetValue(point_1, k);
                    k += 1;


                }

                if (arr_P2.Length > 3)
                {
                    pgb = new PathGradientBrush(arr_P2);

                    pgb.CenterPoint = new Point(Convert.ToInt32(mapViewer.FromLatLngToLocal(frmMainHPCM.sourcePointList[0]).X), Convert.ToInt32(mapViewer.FromLatLngToLocal(frmMainHPCM.sourcePointList[0]).Y));
                    pgb.CenterColor = Color.Orange;
                    pgb.SurroundColors = new Color[] { color_Level_3 };
                    e.Graphics.FillClosedCurve(pgb, arr_P2);
                }



                //Paint loop for highest concentrations

                k = 0;

                Point[] arr_P = new Point[lstCurvePoints.Count];

                foreach (PointLatLng p in lstCurvePoints)
                {

                    int x1 = Convert.ToInt32(mapViewer.FromLatLngToLocal(p).X);
                    int y1 = Convert.ToInt32(mapViewer.FromLatLngToLocal(p).Y);
                    Point point_1 = new Point(x1, y1);

                    arr_P[k] = point_1;
                    k += 1;


                }

                if (arr_P.Length > 3)
                {



                    pgb = new PathGradientBrush(arr_P);
                    pgb.CenterPoint = new Point(Convert.ToInt32(mapViewer.FromLatLngToLocal(frmMainHPCM.sourcePointList[0]).X), Convert.ToInt32(mapViewer.FromLatLngToLocal(frmMainHPCM.sourcePointList[0]).Y));
                    pgb.CenterColor = Color.Red;
                    pgb.SurroundColors = new Color[] { color_Level_4 };
                    e.Graphics.FillClosedCurve(pgb, arr_P);
                }

            }



        }




        #endregion


        SaveFileDialog imageSaveFileDialog = new SaveFileDialog();

        private void btnToImage_Click(object sender, EventArgs e)
        {

            Bitmap bmp = new Bitmap(1280, 655);

            mapViewer.DrawToBitmap(bmp, mapViewer.ClientRectangle);

         

            imageSaveFileDialog.Filter = " Untitled (*.png)\" |*.png";

            DialogResult result;

           result = imageSaveFileDialog.ShowDialog();

           if (result == DialogResult.OK) {

               bmp.Save(imageSaveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            
           }

          
        }

        private void tbCreateTerrain_Click(object sender, EventArgs e)
        {
            Visualizer vs = new Visualizer();
            vs.ShowDialog();

        }




        private void tbCreateReport_Click(object sender, EventArgs e)
        {

            //frmReporter frmRpt = new frmReporter(mapViewer, this);
            //frmRpt.ShowDialog();

        }

        private void txtBoxMarkerName_Enter(object sender, EventArgs e)
        {


            ((MarkerTag)currentMarker.Tag).Name = txtBoxMarkerName.Text;
            currentMarker.ToolTipText = txtBoxMarkerName.Text;

        }

        private void btnMarkerOk_Click(object sender, EventArgs e)
        {
            ((MarkerTag)currentMarker.Tag).Name = txtBoxMarkerName.Text;

                try
                {

                    currentMarker.Position = new PointLatLng(Convert.ToDouble(txtBoxMarkerLat.Text), Convert.ToDouble(txtBoxMarkerLng.Text));

                }
                catch
                {

                }

            
            currentMarker.ToolTipText = txtBoxMarkerName.Text;

            xPanderPanel2.Expand = true;
            xPanderPanel1.Expand = false;
        }



        private void frmMainHPCM_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you really want to quit Hazard Prediction Model ?  ", "Hazard Prediction Model", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {

                return;

            }
            else
            {

                e.Cancel = true;
            }
        }


        bool checkChanged = false;

        

        private void chkLstBoxSLMetStations_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {


                //setMarkerPosition(toolStripButtonMetMarker);
                drawingPen = new Pen(Color.YellowGreen, 15);
                fillBrush = new SolidBrush(Color.YellowGreen);

                string stationName = chkLstBoxSLMetStations.Items[e.Index].ToString();
                double lat=0;
                double lng=0;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    if (ds.Tables[0].Rows[i].ItemArray[1] == stationName)
                    {

                        lat = Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[2]);
                        lng = Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[3]);


                    }


                }

                metStationMarker = new GMapMarkerCircle(new PointLatLng(lat, lng), drawingPen, fillBrush, 4, "");
                metStationMarker.Tag = new MarkerTag(stationName, "MetMarker");
                metMarkerLayer.Markers.Add(metStationMarker);
                metStationMarker.IsVisible = true;
                metStationList.Add(metStationMarker);

            }
            else if (e.NewValue == CheckState.Unchecked)
            {

           
            }

        }

        private void chkLstBoxSLMetStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox clb = (CheckedListBox)sender;
            int index = clb.SelectedIndex;

            if (index != -1 && clb.GetItemCheckState(index) == CheckState.Checked)
            {
                clb.Items.RemoveAt(index);
            }

        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            defaultSettings();
            isFileMode = false;

            toolStripButtonOpen.Enabled = true;
            openProjectToolStripMenuItem.Enabled = true;

            toolStripButtonDrawConc.Enabled = false;
            chkLstBoxSLMetStations.Enabled = true;
            toolStripButtonMetMarker.Enabled = true;
            toolStripButtonInput.Enabled = true;
            inputToolStripMenuItem.Enabled = true;

            tbCreateReport.Enabled = false;
            cmbBoxMapType.Enabled = true;
            btnReloadMap.Enabled = true;

        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            defaultSettings();
            isFileMode = false;

            toolStripButtonOpen.Enabled = true;
            openProjectToolStripMenuItem.Enabled = true;

            toolStripButtonDrawConc.Enabled = false;
            chkLstBoxSLMetStations.Enabled = true;
            toolStripButtonMetMarker.Enabled = true;
            toolStripButtonInput.Enabled = true;
            inputToolStripMenuItem.Enabled = true;

            tbCreateReport.Enabled = false;
            cmbBoxMapType.Enabled = true;
            btnReloadMap.Enabled = true;
        }

        private void configurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            projectOptions frmProjectOptions = new projectOptions();
            frmProjectOptions.ShowDialog();

        }

        private void runProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonRun.Enabled = false;
            runProjectToolStripMenuItem.Enabled = false;

            toolStripButtonConfigurations.Enabled = false;
            configurationsToolStripMenuItem.Enabled = false;

            toolStripButtonSave.Enabled = false;
            saveProjectToolStripMenuItem.Enabled = false;

            toolStripButtonOpen.Enabled = false;
            openProjectToolStripMenuItem.Enabled = false;
            toolStripButtonNew.Enabled = false;
            newProjectToolStripMenuItem.Enabled = false;

            btnToImage.Enabled = false;
            pnlWrkInPrgrs.Location = new Point(((this.ClientSize.Width / 2) - (pnlWrkInPrgrs.Size.Width / 2)), ((this.ClientSize.Height / 2) - (pnlWrkInPrgrs.Size.Height / 2)));
            pnlWrkInPrgrs.Visible = true;
            pnlWrkInPrgrs.BringToFront();

            timerRunProject.Enabled = true;

            st.Start();

            bckGrndWrkrRun.RunWorkerAsync();

        }

        private void aboutHPCMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout abt = new frmAbout();
            abt.ShowDialog();
        }

        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            //frmHelp fh = new frmHelp();
            //fh.ShowDialog();
        }







        

    }
}
