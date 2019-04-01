using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using System.IO;
using System.Text.RegularExpressions;


namespace HPCM_REBUILD
{

    public partial class frmDataInput : Form
    {

        #region--Class level static variables--


        // Date, time and month variables...
        public static int year ;
        public static int month ;
        public static int date;
        public static int hour;
        public static int minute;
        public static int second;


        //meteorological variables...
        public static int numberOfMetStations;


        public static List<double> windSpeed=new List<double>();
        public static List<double> windDirection = new List<double>();
        public static List<double> cloudCover=new List<double>();
        public static List<double> DryBulbTemperature=new List<double>();
        public static List<double> pressure=new List<double>();
        public static List<double> relativeHumidity=new List<double>();
        public static List<double> elevationOfTheStation = new List<double>();
        public static double[] mixingHeight ;
        public static List<double> samplingTime = new List<double>();
        public static List<double> precipitationIntensity = new List<double>();

        public static string terrainType;
        public static double surfaceRoughnessLength;
        public static string surfaceType;
        public static double albedoValue;
        public static double surfaceMoistureAvailability;


        public static string chemicalAgent;
        public static double agentWeight;
        public static double heatOfDetonation;
        public static string typeOfTheSource;
        public  static double effectiveAgentPercentage;
        public static double effectiveHeatPerentage;
        //public  static double releaseDuration;
        public static double weightOfExplosive;
        public static double sourceTemperature;
        public static double releaseRate;
        public static double sourceRadius;



        public  static double referenceHeightForWindMeasurents = 10;
        public static double maximumRadialDistance = 40000;


        //Some variables for database operations...
        OleDbConnection con = new OleDbConnection();
        OleDbDataAdapter da;
        DataSet ds = new DataSet();
        DataTable dt;
  
        //Map viewer variable for point type conversions...
        GMap.NET.WindowsForms.GMapControl mapViewer;

        //mainForm variable to access controls in the main form.. 
        frmMainHPCM mainForm;

        #endregion

        public frmDataInput(frmMainHPCM mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.mapViewer = mainForm.mapViewer ;
        }



        //Configuring initial form interface and status on form loading...

        List<string> lstDataInputFromFile = new List<string>();

        private void meteorolgicalData_Load(object sender, EventArgs e)
        {

            // Reads and poppulates data input fields if loaded from file.. 



            if (frmMainHPCM.isFileMode == true)
            {

                var dif = Regex.Split(frmMainHPCM.dataInputString, "\r\n");

                for (int i = 0; i < dif.Length; i++) {

                    lstDataInputFromFile.Add(dif[i]);
                
                }
            }

            // Configure interface...

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "University of Colombo/Hazard Prediction Model/dbHpm.mdb");


            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+filePath+";Persist Security Info=True;Jet OLEDB:Database Password=keevamalamute";
            con.Open();
            da = new OleDbDataAdapter("SELECT * FROM explosives", con);
            da.Fill(ds, "explosivesDS");
            fillComboBoxHOD(cmbBoxHOD);
            CreateMeteorologicalTabs();
            da = new OleDbDataAdapter("SELECT * FROM chemicalAgents", con);
            dt = new DataTable("chemicalAgents");
            da.Fill(dt);
            ds.Tables.Add(dt);
            FillComboBoxChemicalAgent();
            con.Close();

            this.grpBoxInstantaneous.Hide();
            this.grpBoxSemiContinuous.Hide();
            this.grpBoxContinuous.Hide();

            if (frmMainHPCM.isFileMode == true)
            {

                if (lstDataInputFromFile.ElementAt(1) == "Instantaneous Source")
                {

                    radioButton1.Checked = true;
                    var sourceDataFromFile = (lstDataInputFromFile.ElementAt(2)).Split(',');
                    cmbBoxHOD.Text = sourceDataFromFile[0];

                    //Should manually assign heat of detonation.. bcoz the normal calculation is done upon the cmbbox drop down closing event..
                    heatOfDetonation = Convert.ToDouble(sourceDataFromFile[0]);

                    txtBoxWeightOfExplosive.Text = sourceDataFromFile[1];
                    tbarEffectiveHeatPercentage.Value = Convert.ToInt32(sourceDataFromFile[2])/5;
                    cmbBoxChemicalAgent.SelectedText = sourceDataFromFile[3];
                    txtBoxAgentWeight.Text = sourceDataFromFile[4];
                    tBarEffectiveAgentPercentage.Value = Convert.ToInt32(sourceDataFromFile[5])/5;

                }
                else if (lstDataInputFromFile.ElementAt(1) == "Semi-Continuous Source")
                {

                    radioButton2.Checked = true;

                    var sourceDataFromFile = (lstDataInputFromFile.ElementAt(2)).Split(',');
                    txtBoxSourceTemp.Text = sourceDataFromFile[0];
                    txtBoxReleaseRate.Text = sourceDataFromFile[1];
                    txtBoxSourceRadius.Text = sourceDataFromFile[2];
                    cmbBoxChemicalAgent.SelectedText = sourceDataFromFile[3];
                    txtBoxAgentWeight.Text = sourceDataFromFile[4];
                    tBarEffectiveAgentPercentage.Value = Convert.ToInt32(sourceDataFromFile[5]) / 5;
                }

                var dateTimeFromFile = (lstDataInputFromFile.ElementAt(0)).Split(';');
                var dateFromFile = dateTimeFromFile[0].Split(',');
                var timeFromFile = dateTimeFromFile[1].Split(',');

                DateTime dateTime = new DateTime(Convert.ToInt32(dateFromFile[2]), Convert.ToInt32(dateFromFile[0]), Convert.ToInt32(dateFromFile[1]), Convert.ToInt32(timeFromFile[0]), Convert.ToInt32(timeFromFile[1]), Convert.ToInt32(timeFromFile[2]));

                dateTimePickerMetData.Value = dateTime;


                //Populate geographical tab....

                var geoDataFromFile=(lstDataInputFromFile.ElementAt(3)).Split(',');

                chkBoxSurfaceRoughnessLength.CheckState = CheckState.Checked;
                txtBoxSurfaceRoughnessLength.Text = geoDataFromFile[0];
                chkBoxAlbedoValue.CheckState = CheckState.Checked;
                txtBoxAlbedo.Text = geoDataFromFile[1];
                txtBoxMoistureAvailability.Text = geoDataFromFile[2];



           }
           else
            {

                radioButton1.Checked = true;

            }

            mixingHeight = new double[numberOfMetStations];


            for (int i = 0; i < numberOfMetStations; i++)
            {

                if (frmMainHPCM.isFileMode == true)
                {
                    var mixingHeightsFromFile = (lstDataInputFromFile.ElementAt(10)).Split(',');

                    mixingHeight.SetValue(Convert.ToDouble(mixingHeightsFromFile[i]), i);
                }
                else
                {
                    mixingHeight.SetValue(0, i);
                }
            }


        }


        #region Supporting methods...


        //Creates meteorological tabs...
        private void CreateMeteorologicalTabs()
        {
            string stationName;
            
            for (int i = 0; i < numberOfMetStations; i++)
            {
                stationName =((MarkerTag) frmMainHPCM.metStationList.ElementAt<GMapMarkerCircle>(i).Tag).Name;

                this.dataInputTab.TabPages.Add("tabMet" + Convert.ToString(i + 1), "Meteorological input - : " + stationName);
            }

            int j = 1;
            foreach (TabPage tabMet in this.dataInputTab.TabPages)
            {

                tabMet.BackColor = Color.White;

                if (tabMet.Name == "tabMet" + Convert.ToString(j))
                {


                    //creating controls for wind information...
                    GroupBox grpBoxWindData = new GroupBox();
                    Label lblWindData = new Label();
                    TextBox txtBoxWindSpeed = new TextBox();
                    Label lblWindSpeedUnits = new Label();
                    Label lblWindDirection = new Label();
                    TextBox txtBoxWindDirection = new TextBox();
                    Label lblWindDirectionUnits = new Label();

                    grpBoxWindData.Text = "10-meter Wind Data";
                    grpBoxWindData.Name = "grpBoxWindData" + Convert.ToString(j);

                    lblWindData.Text = "Wind Speed :";
                    lblWindData.Name = "lblWindData" + Convert.ToString(j);
                    grpBoxWindData.Controls.Add(lblWindData);
                    lblWindData.SetBounds(24, 37, 89, 17);

                    txtBoxWindSpeed.Name = "txtBoxWindSpeed" + Convert.ToString(j);
                    grpBoxWindData.Controls.Add(txtBoxWindSpeed);
                    txtBoxWindSpeed.SetBounds(147, 34, 64, 25);

                    lblWindSpeedUnits.Text = "(m/s)";
                    lblWindSpeedUnits.Name = "lblWindSpeedUnits" + Convert.ToString(j);
                    grpBoxWindData.Controls.Add(lblWindSpeedUnits);
                    lblWindSpeedUnits.SetBounds(237, 37, 45, 17);

                    lblWindDirection.Text = "Wind Direction :";
                    lblWindDirection.Name = "lblWindDirection" + Convert.ToString(j);
                    grpBoxWindData.Controls.Add(lblWindDirection);
                    lblWindDirection.SetBounds(24, 97, 111, 17);

                    txtBoxWindDirection.Name = "txtBoxWindDirection" + Convert.ToString(j);
                    grpBoxWindData.Controls.Add(txtBoxWindDirection);
                    txtBoxWindDirection.SetBounds(147, 94, 64, 25);

                    lblWindDirectionUnits.Text = "(Degrees)";
                    lblWindDirectionUnits.Name = "lblWindDirectionUnits" + Convert.ToString(j);
                    grpBoxWindData.Controls.Add(lblWindDirectionUnits);
                    lblWindDirectionUnits.SetBounds(237, 97, 70, 17);

                    tabMet.Controls.Add(grpBoxWindData);
                    grpBoxWindData.SetBounds(25, 23, 390, 139);




                    //creating controls for temperature information...
                    GroupBox grpBoxTempData = new GroupBox();
                    Label lblDryBulbTemp = new Label();
                    TextBox txtBoxDryBulbTemp = new TextBox();
                    Label lblDryBulbTempUnits = new Label();

                    grpBoxTempData.Text = "2-meter Temperature Data";
                    grpBoxTempData.Name = "grpBoxTempData" + Convert.ToString(j);
                    tabMet.Controls.Add(grpBoxTempData);
                    grpBoxTempData.SetBounds(25, 189, 390, 78);

                    lblDryBulbTemp.Text = "Dry Bulb Temperature :";
                    lblDryBulbTemp.Name = "lblDryBulbTemp" + Convert.ToString(j);
                    grpBoxTempData.Controls.Add(lblDryBulbTemp);
                    lblDryBulbTemp.SetBounds(24, 38, 155, 17);

                    txtBoxDryBulbTemp.Name = "txtBoxDryBulbTemp" + Convert.ToString(j);
                    grpBoxTempData.Controls.Add(txtBoxDryBulbTemp);
                    txtBoxDryBulbTemp.SetBounds(185, 35, 71, 25);

                    lblDryBulbTempUnits.Text = " (°C)";
                    lblDryBulbTempUnits.Name = "lblDryBulbTempUnits" + Convert.ToString(j);
                    grpBoxTempData.Controls.Add(lblDryBulbTempUnits);
                    lblDryBulbTempUnits.SetBounds(273, 38, 37, 17);

                    //Creating controls for other meteorological information...
                    GroupBox grpBoxOtherMetParameters = new GroupBox();
                    Label lblPressure = new Label();
                    TextBox txtBoxAtmosphericPressure = new TextBox();
                    Label lblPressureUnits = new Label();
                    Label lblRelativeHumidity = new Label();
                    TextBox txtBoxRelativeHumidity = new TextBox();
                    Label lblRelativeHumidityUnits = new Label();
                    Label lblCloudCover = new Label();
                    TextBox txtBoxCloudCover = new TextBox();
                    Label lblCloudCoverUnits = new Label();

                    grpBoxOtherMetParameters.Text = "Other Meteorological Parameters";
                    grpBoxOtherMetParameters.Name = "grpBoxOtherMetParameters" + Convert.ToString(j);
                    tabMet.Controls.Add(grpBoxOtherMetParameters);
                    grpBoxOtherMetParameters.SetBounds(25, 293, 390, 193);

                    lblPressure.Text = "Pressure :";
                    lblPressure.Name = "lblPressure" + Convert.ToString(j);
                    grpBoxOtherMetParameters.Controls.Add(lblPressure);
                    lblPressure.SetBounds(24, 39, 70, 17);

                    txtBoxAtmosphericPressure.Name = "txtBoxAtmosphericPressure" + Convert.ToString(j);
                    grpBoxOtherMetParameters.Controls.Add(txtBoxAtmosphericPressure);
                    txtBoxAtmosphericPressure.SetBounds(185, 36, 71, 25);

                    lblPressureUnits.Text = "(mmHg)";
                    lblPressureUnits.Name = "lblPressureUnits" + Convert.ToString(j);
                    grpBoxOtherMetParameters.Controls.Add(lblPressureUnits);
                    lblPressureUnits.SetBounds(273, 39, 61, 17);

                    lblRelativeHumidity.Text = "Relative Humidity :";
                    lblRelativeHumidity.Name = "lblRelativeHumidity" + Convert.ToString(j);
                    grpBoxOtherMetParameters.Controls.Add(lblRelativeHumidity);
                    lblRelativeHumidity.SetBounds(24, 96, 128, 17);

                    txtBoxRelativeHumidity.Name = "txtBoxRelativeHumidity" + Convert.ToString(j);
                    grpBoxOtherMetParameters.Controls.Add(txtBoxRelativeHumidity);
                    txtBoxRelativeHumidity.SetBounds(185, 93, 71, 25);

                    lblRelativeHumidityUnits.Text = "(%)";
                    lblRelativeHumidityUnits.Name = "lblRelativeHumidityUnits" + Convert.ToString(j);
                    grpBoxOtherMetParameters.Controls.Add(lblRelativeHumidityUnits);
                    lblRelativeHumidityUnits.SetBounds(286, 96, 33, 17);

                    lblCloudCover.Text = "Cloud Cover :";
                    lblCloudCover.Name = "lblCloudCover" + Convert.ToString(j);
                    grpBoxOtherMetParameters.Controls.Add(lblCloudCover);
                    lblCloudCover.SetBounds(24, 148, 91, 17);

                    txtBoxCloudCover.Name = "txtBoxCloudCover" + Convert.ToString(j);
                    grpBoxOtherMetParameters.Controls.Add(txtBoxCloudCover);
                    txtBoxCloudCover.SetBounds(185, 145, 71, 25);

                    lblCloudCoverUnits.Text = "(%)";
                    lblCloudCoverUnits.Name = "lblCloudCoverUnits" + Convert.ToString(j);
                    grpBoxOtherMetParameters.Controls.Add(lblCloudCoverUnits);
                    lblCloudCoverUnits.SetBounds(286, 148, 33, 17);

                    //Creating controls for other meteorological parameter inputs.. 
                    GroupBox grpBoxMetParameters = new GroupBox();
                    Label lblMixingHeight = new Label();
                    CheckBox chkBoxIsMixingHeightAvailable = new CheckBox();

                    Label lblPrecipitationIntensity = new Label();
                    TextBox txtBoxPrecipitationIntensity = new TextBox();
                    Label lblPrecipitationIntensityUnits = new Label();

                    //Label lblGroundElevation = new Label();
                    //TextBox txtBoxElevationOfTheStation = new TextBox();
                    //Label lblGroundElevatioUnits = new Label();
                    //Label lblSamplingTime = new Label();
                    //TextBox txtBoxMetSamplingTime = new TextBox();
                    //Label lblSamplingTimeUnits = new Label();


                    grpBoxMetParameters.Text = "Other Meteorological Parameters";
                    grpBoxMetParameters.Name = "grpBoxMetParameters" + Convert.ToString(j);
                    tabMet.Controls.Add(grpBoxMetParameters);
                    grpBoxMetParameters.SetBounds(514, 23, 390, 150);

                    lblMixingHeight.Text = "Mixing Height Observation Available :";
                    lblMixingHeight.Name = "lblMixingHeight" + Convert.ToString(j);
                    grpBoxMetParameters.Controls.Add(lblMixingHeight);
                    lblMixingHeight.SetBounds(30, 37, 241, 17);

                    chkBoxIsMixingHeightAvailable.Name = "chkBoxIsMixingHeightAvailable" + Convert.ToString(j);
                    grpBoxMetParameters.Controls.Add(chkBoxIsMixingHeightAvailable);
                    chkBoxIsMixingHeightAvailable.SetBounds(277, 40, 15, 14);
                    chkBoxIsMixingHeightAvailable.CheckedChanged += new EventHandler(chkBoxIsMixingHeightAvailable_CheckedChanged);

                    lblPrecipitationIntensity.Text = "Rain Fall :";
                    lblPrecipitationIntensity.Name = "lblPrecipitationIntensity" + Convert.ToString(j);
                    grpBoxMetParameters.Controls.Add(lblPrecipitationIntensity);
                    lblPrecipitationIntensity.SetBounds(30, 97, 68, 17);

                    txtBoxPrecipitationIntensity.Name = "txtBoxPrecipitationIntensity" + Convert.ToString(j);
                    grpBoxMetParameters.Controls.Add(txtBoxPrecipitationIntensity);
                    txtBoxPrecipitationIntensity.SetBounds(164, 94, 56, 25);

                    lblPrecipitationIntensityUnits.Name = "lblPrecipitationIntensityUnits" + Convert.ToString(j);
                    lblPrecipitationIntensityUnits.Text = "(mm)";
                    grpBoxMetParameters.Controls.Add(lblPrecipitationIntensityUnits);
                    lblPrecipitationIntensityUnits.SetBounds(250, 97, 44, 17);


                   //Populates meteorological fields if loaded from file...

                    if (frmMainHPCM.isFileMode == true) {

                        var wsFromFile = (lstDataInputFromFile.ElementAt(4)).Split(',');
                        txtBoxWindSpeed.Text = wsFromFile[j-1];

                        var wdFromFile = (lstDataInputFromFile.ElementAt(5)).Split(',');
                        txtBoxWindDirection.Text = wdFromFile[j-1];

                        var tempFromFile = (lstDataInputFromFile.ElementAt(6)).Split(',');
                        txtBoxDryBulbTemp.Text = tempFromFile[j-1];

                        var pressureFromFile = (lstDataInputFromFile.ElementAt(7)).Split(',');
                        txtBoxAtmosphericPressure.Text = pressureFromFile[j-1];

                        var relHumFromFile = (lstDataInputFromFile.ElementAt(8)).Split(',');
                        txtBoxRelativeHumidity.Text = relHumFromFile[j-1];

                        var ccFromFile = (lstDataInputFromFile.ElementAt(9)).Split(',');
                        txtBoxCloudCover.Text = ccFromFile[j-1];

                        var rainFallFromFile = (lstDataInputFromFile.ElementAt(11)).Split(',');
                        txtBoxPrecipitationIntensity.Text = rainFallFromFile[j-1];




                    
                    }





                    //lblGroundElevation.Text = "Ground Elevation :";
                    //lblGroundElevation.Name = "lblGroundElevation" + Convert.ToString(j);
                    //grpBoxMetParameters.Controls.Add(lblGroundElevation);
                    //lblGroundElevation.SetBounds(30, 151, 125, 17);

                    //txtBoxElevationOfTheStation.Name = "txtBoxElevationOfTheStation" + Convert.ToString(j);
                    //grpBoxMetParameters.Controls.Add(txtBoxElevationOfTheStation);
                    //txtBoxElevationOfTheStation.SetBounds(164, 148, 56, 25);

                    //lblGroundElevatioUnits.Text = "(m)";
                    //lblGroundElevatioUnits.Name = "lblGroundElevatioUnits" + Convert.ToString(j);
                    //grpBoxMetParameters.Controls.Add(lblGroundElevatioUnits);
                    //lblGroundElevatioUnits.SetBounds(250, 151, 32, 17);


                    //lblSamplingTime.Text = "Sampling Time :";
                    //lblSamplingTime.Name = "lblSamplingTime" + Convert.ToString(j);
                    //grpBoxMetParameters.Controls.Add(lblSamplingTime);
                    //lblSamplingTime.SetBounds(30, 199, 107, 17);

                    //txtBoxMetSamplingTime.Name = "txtBoxMetSamplingTime" + Convert.ToString(j);
                    //grpBoxMetParameters.Controls.Add(txtBoxMetSamplingTime);
                    //txtBoxMetSamplingTime.SetBounds(164, 196, 56, 25);

                    //lblSamplingTimeUnits.Text = "(minutes)";
                    //lblSamplingTimeUnits.Name = "lblSamplingTimeUnits" + Convert.ToString(j);
                    //grpBoxMetParameters.Controls.Add(lblSamplingTimeUnits);
                    //lblSamplingTimeUnits.SetBounds(250, 199, 70, 17);


                    j += 1;
                }


            }




        }

        void chkBoxIsMixingHeightAvailable_CheckedChanged(object sender, EventArgs e)
        {

            string p = ActiveControl.Name;
            int j = Convert.ToInt32(p.Substring(29));

            if (((CheckBox)ActiveControl).Checked)
            {

                this.ActiveControl.Parent.SetBounds(514, 23, 390, 200);

                Label lblInsertMixingHeight = new Label();
                TextBox txtBoxMixingHeight = new TextBox();
                Label lblInsertMixingHeightUnits = new Label();

                lblInsertMixingHeight.Text = "Mixing Height :";
                lblInsertMixingHeight.Name = "lblInsertMixingHeight" + Convert.ToString(j);
                this.ActiveControl.Parent.Controls.Add(lblInsertMixingHeight);
                lblInsertMixingHeight.SetBounds(30, 97, 101, 17);

                txtBoxMixingHeight.Name = "txtBoxMixingHeight" + Convert.ToString(j);
                this.ActiveControl.Parent.Controls.Add(txtBoxMixingHeight);
                txtBoxMixingHeight.SetBounds(164, 94, 56, 25);

                //Fill mixing height data from file...

                if (frmMainHPCM.isFileMode == true)
                {
                    var mixingHeightFromFile = (lstDataInputFromFile.ElementAt(10)).Split(',');
                    txtBoxMixingHeight.Text = mixingHeightFromFile[j-1];
                }
                else {

                    txtBoxMixingHeight.Text = "0";
                
                }


                lblInsertMixingHeightUnits.Text = "(m)";
                lblInsertMixingHeightUnits.Name = "lblInsertMixingHeightUnits" + Convert.ToString(j);
                this.ActiveControl.Parent.Controls.Add(lblInsertMixingHeightUnits);
                lblInsertMixingHeightUnits.SetBounds(250, 97, 32, 17);

                foreach (var cntrl in ActiveControl.Parent.Controls.OfType<Label>())
                {
                    if (cntrl.Name.Contains("lblPrecipitationIntensity"))
                    {
                        cntrl.SetBounds(30, 151, 68, 17);

                    }
                    if (cntrl.Name.Contains("lblPrecipitationIntensityUnits"))
                    {

                        cntrl.SetBounds(250, 151, 44, 17);
                    }

                    //if (cntrl.Name.Contains("lblGroundElevation"))
                    //{
                    //    cntrl.SetBounds(30, 199, 125, 17);
                    //}
                    //if (cntrl.Name.Contains("lblGroundElevatioUnits"))
                    //{
                    //    cntrl.SetBounds(250, 199, 32, 17);
                    //}
                    //if (cntrl.Name.Contains("lblSamplingTime"))
                    //{
                    //    cntrl.SetBounds(30, 244, 107, 17);
                    //}
                    //if (cntrl.Name.Contains("lblSamplingTimeUnits"))
                    //{
                    //    cntrl.SetBounds(250, 244, 70, 17);
                    //}
                }

                foreach (var cntrl in ActiveControl.Parent.Controls.OfType<TextBox>())
                {
                    if (cntrl.Name.Contains("txtBoxPrecipitationIntensity"))
                    {

                        cntrl.SetBounds(164, 148, 56, 25);
                    }
                    //if (cntrl.Name.Contains("txtBoxElevationOfTheStation"))
                    //{
                    //    cntrl.SetBounds(164, 196, 56, 25);
                    //}
                    //if (cntrl.Name.Contains("txtBoxMetSamplingTime"))
                    //{

                    //    cntrl.SetBounds(164, 241, 56, 25);
                    //}



                }


            }
            else
            {

                this.ActiveControl.Parent.SetBounds(514, 23, 390, 150);

                foreach (var cntrl in ActiveControl.Parent.Controls.OfType<Label>())
                {

                    if (cntrl.Name.Contains("lblInsertMixingHeight"))
                    {
                        cntrl.Dispose();
                    }

                    if (cntrl.Name.Contains("lblInsertMixingHeightUnits"))
                    {
                        cntrl.Dispose();
                    }

                    if (cntrl.Name.Contains("lblPrecipitationIntensity"))
                    {
                        cntrl.SetBounds(30, 97, 68, 17);

                    }
                    if (cntrl.Name.Contains("lblPrecipitationIntensityUnits"))
                    {

                        cntrl.SetBounds(250, 97, 44, 17);
                    }

                    //if (cntrl.Name.Contains("lblGroundElevation"))
                    //{
                    //    cntrl.SetBounds(30, 151, 125, 17);
                    //}
                    //if (cntrl.Name.Contains("lblGroundElevatioUnits"))
                    //{
                    //    cntrl.SetBounds(250, 151, 32, 17);
                    //}
                    //if (cntrl.Name.Contains("lblSamplingTime"))
                    //{
                    //    cntrl.SetBounds(30, 199, 107, 17);
                    //}
                    //if (cntrl.Name.Contains("lblSamplingTimeUnits"))
                    //{
                    //    cntrl.SetBounds(250, 199, 70, 17);
                    //}

                }


                foreach (var cntrl in ActiveControl.Parent.Controls.OfType<TextBox>())
                {

                    if (cntrl.Name.Contains("txtBoxMixingHeight"))
                    {
                        cntrl.Dispose();
                    }

                    if (cntrl.Name.Contains("txtBoxPrecipitationIntensity"))
                    {
                        cntrl.SetBounds(164, 94, 56, 25);
                    }

                    //if (cntrl.Name.Contains("txtBoxElevationOfTheStation"))
                    //{
                    //    cntrl.SetBounds(164, 148, 56, 25);
                    //}
                    //if (cntrl.Name.Contains("txtBoxMetSamplingTime"))
                    //{
                    //    cntrl.SetBounds(164, 196, 56, 25);
                    //}
                }
            }
        }


        //Fills data into HOD combo boxes...
        private void fillComboBoxHOD(ComboBox cmbBox)
        {
            int maxRows = 0;
            maxRows = ds.Tables[0].Rows.Count;

            for (int p = 0; p < maxRows; p++)
            {

                cmbBox.Items.Add(ds.Tables[0].Rows[p].ItemArray[1]);
            }
        }

        //Fills data into Chemical agent combo box...
        private void FillComboBoxChemicalAgent() {

            int maxRows = 0;
            maxRows = ds.Tables["chemicalAgents"].Rows.Count;
            for (int i = 0; i < maxRows; i++) {

                cmbBoxChemicalAgent.Items.Add(ds.Tables["chemicalAgents"].Rows[i].ItemArray[1]);
            }

        }

        //Returns the albedo value for a selected surface type...
        private double GetAlbedo()
        {
            double albedo = 0;

            switch (Convert.ToInt16(cmbBoxSurfaceType.SelectedIndex))
            {
                case 0:
                    albedo = 0.05;
                    break;
                case 1:
                    albedo = 0.4;
                    break;
                case 2:
                    albedo = 0.16;
                    break;
                case 3:
                    albedo = 0.2;
                    break;
                case 4:
                    albedo = 0.19;
                    break;
                case 5:
                    albedo = 0.175;
                    break;
                case 6:
                    albedo = 0.175;
                    break;
                case 7:
                    albedo = 0.1;
                    break;
                case 8:
                    albedo = 0.325;
                    break;
            }
            return albedo;


        }

        //Returns the roughness length value in meters, for a selected terrain type by user...
        public double CalculateSurfaceRoughnessLength()
        {

            double surfaceRoughnessLength = 0.005;
            switch (Convert.ToInt16(cmbBoxTerrainType.SelectedIndex))
            {
                case 0:
                    surfaceRoughnessLength = 0.005;
                    break;
                case 1:
                    surfaceRoughnessLength = 0.03;
                    break;
                case 2:
                    surfaceRoughnessLength = 0.1;
                    break;
                case 3:
                    surfaceRoughnessLength = 0.25;
                    break;
                case 4:
                    surfaceRoughnessLength = 0.5;
                    break;
                case 5:
                    surfaceRoughnessLength = 1;
                    break;
                case 6:
                    surfaceRoughnessLength = 2;
                    break;
            }
            return surfaceRoughnessLength;
        }

        //Validates controls and assigns values into static variables...
        private void ValidateControls()
        {

              year = Convert.ToInt16(dateTimePickerMetData.Value.Year);
              month = Convert.ToInt16(dateTimePickerMetData.Value.Month);
              date = Convert.ToInt16(dateTimePickerMetData.Value.Day);
              hour = Convert.ToInt16(dateTimePickerMetData.Value.Hour);
              minute = Convert.ToInt16(dateTimePickerMetData.Value.Minute);
              second = Convert.ToInt16(dateTimePickerMetData.Value.Second);



            //Validating controls in source data input tab...


            if (cmbBoxChemicalAgent.Text.Length == 0)
            {
                errorProviderMetInput.SetError(cmbBoxChemicalAgent, "Please select for this field");
                throw new System.Exception("Please select a chemical agent");
                
            }
            else {
                errorProviderMetInput.Clear();
                chemicalAgent = (string)(cmbBoxChemicalAgent.SelectedItem);
                
            }


            try
            {
                errorProviderMetInput.Clear();
                agentWeight = Convert.ToDouble(txtBoxAgentWeight.Text);

            }
            catch {

                errorProviderMetInput.SetError(txtBoxAgentWeight, "Please insert a value in to this field");
                throw new System.Exception("Please specify the quantity of chemical agent");
            }

            if (radioButton1.Checked || radioButton2.Checked) {


                effectiveAgentPercentage = tBarEffectiveAgentPercentage.Value * 5;

                if (radioButton1.Checked)
                {
                    typeOfTheSource = "Instantaneous Source";



                    if (cmbBoxHOD.Text.Length == 0)
                    {
                        errorProviderMetInput.SetError(cmbBoxHOD, "Please select a value for this field");
                        throw new System.Exception("Please select a value for heat of detonation");
                    }

                    if (txtBoxWeightOfExplosive.Text.Length == 0)
                    {

                        errorProviderMetInput.SetError(txtBoxWeightOfExplosive, "Please insert a value for this field");
                        throw new System.Exception("Please insert a value for the weight of the explosive");
                    }
                    else
                    {
                        try
                        {

                            weightOfExplosive = Convert.ToDouble(txtBoxWeightOfExplosive.Text);
                        }
                        catch
                        {
                            errorProviderMetInput.SetError(txtBoxWeightOfExplosive, "Please check the value in this field");
                            throw new System.Exception("Please check the value of the weight of the explosive");
                        }

                    }

                    effectiveHeatPerentage = tbarEffectiveHeatPercentage.Value * 5;

                }
                if (radioButton2.Checked)
                {
                    typeOfTheSource = "Semi-Continuous Source";
                    
                    try
                    {
                        errorProviderMetInput.Clear();
                        sourceTemperature = Convert.ToDouble(txtBoxSourceTemp.Text);

                    }
                    catch
                    {

                        errorProviderMetInput.SetError(txtBoxSourceTemp, "Please insert a value in to this field");
                        throw new System.Exception("Please insert a numerical value for the release temperature");
                    }


                    try
                    {
                        errorProviderMetInput.Clear();
                        releaseRate = Convert.ToDouble(txtBoxReleaseRate.Text);

                    }
                    catch
                    {

                        errorProviderMetInput.SetError(txtBoxReleaseRate, "Please insert a value in to this field");
                        throw new System.Exception("Please insert a numerical value for the release rate");
                    }

                    try
                    {
                        errorProviderMetInput.Clear();
                        sourceRadius = Convert.ToDouble(txtBoxSourceRadius.Text);

                    }
                    catch
                    {

                        errorProviderMetInput.SetError(txtBoxSourceRadius, "Please insert a value in to this field");
                        throw new System.Exception("Please insert a numerical value for the source radius");
                    }

                }
            
            }
            else {

                typeOfTheSource = "Continuous Source";

            }



            //Validating controls in geographical data tab...

            if (cmbBoxTerrainType.Text.Length == 0 && chkBoxSurfaceRoughnessLength.CheckState == CheckState.Unchecked)
            {
                errorProviderMetInput.SetError(cmbBoxTerrainType, "Please select a value for this field");
                throw new System.Exception("Please select a terrain type or insert a value for the surface roughness length");
            }
            else if (cmbBoxTerrainType.Text.Length == 0 && chkBoxSurfaceRoughnessLength.CheckState == CheckState.Checked)
            {
                try
                {
                    errorProviderMetInput.Clear();
                    surfaceRoughnessLength = Convert.ToDouble(txtBoxSurfaceRoughnessLength.Text);
                    terrainType = "User Specified";
                }
                catch
                {
                    errorProviderMetInput.SetError(txtBoxSurfaceRoughnessLength, "Please check the value you have inserted in this field");
                    throw new System.Exception("Please check the surface roughness length value you have inserted");
                }

            }
            else {
                try
                {
                    errorProviderMetInput.Clear();
                    surfaceRoughnessLength = CalculateSurfaceRoughnessLength();
                    terrainType = cmbBoxTerrainType.SelectedText;
                }
                catch {
                    errorProviderMetInput.SetError(cmbBoxTerrainType, "Please check the value you selected in to this field");
                    throw new System.Exception("Please check the type of terrain you have selected");
                }
            }

            if (cmbBoxSurfaceType.Text.Length == 0 && chkBoxAlbedoValue.CheckState == CheckState.Unchecked) {

                errorProviderMetInput.SetError(cmbBoxSurfaceType,"Please select a value for this field");
                throw new System.Exception("Please select a surface type or insert an albedo value manually");

            }
            else if (cmbBoxSurfaceType.Text.Length == 0 && chkBoxAlbedoValue.CheckState == CheckState.Checked)
            {
                try
                {
                    errorProviderMetInput.Clear();
                    albedoValue = Convert.ToDouble(txtBoxAlbedo.Text);
                    surfaceType = "User specified";

                }
                catch
                {
                    errorProviderMetInput.SetError(txtBoxAlbedo, "Please check the value you have inserted in to this field");
                    throw new System.Exception("Please check the albedo value you have inserted");
                }
            }
            else {
                try
                {
                    errorProviderMetInput.Clear();
                    albedoValue = GetAlbedo();
                    surfaceType = cmbBoxSurfaceType.SelectedText;
                }
                catch {
                    errorProviderMetInput.SetError(cmbBoxSurfaceType, "Please check the value you have selected for this field");
                    throw new System.Exception("Please check the type of the surface you have selected");
                }
            }


            //Validating surface moisture availability textbox...
            if (txtBoxMoistureAvailability.Text.Length == 0) {
                errorProviderMetInput.SetError(txtBoxMoistureAvailability, "Please insert a value in to this field");
                throw new Exception("Please insert a value for the surface moisture availability");
            
            }
            else if(Convert.ToDouble(txtBoxMoistureAvailability.Text)< 0 || Convert.ToDouble(txtBoxMoistureAvailability.Text)>100){

                errorProviderMetInput.SetError(txtBoxMoistureAvailability, "Insert a positve value less than 100 in to this field");
                throw new Exception("Value of the surface moisture availability should be a positve percentage value less than 100"); 
            }else{
                try{
                    errorProviderMetInput.Clear();
                    surfaceMoistureAvailability=Convert.ToDouble(txtBoxMoistureAvailability.Text);
                }catch{

                    errorProviderMetInput.SetError(txtBoxMoistureAvailability, "Please check the value you have inserted in this field");
                    throw new Exception("Please check the value of the surface moisture availability you have inserted"); 
                }
            
            }


            //Validating controls in meteorological tabs...
            
         
            for (int i = 2; i < dataInputTab.TabPages.Count ; i++) {

                foreach (var grpBox in this.dataInputTab.TabPages[i].Controls.OfType<GroupBox>())
                {

                    
                    if (grpBox.Name.Contains("grpBoxWindData"))
                    {

                        foreach (var tb in grpBox.Controls.OfType<TextBox>())
                        {
                            if (tb.Name.Contains("txtBoxWindSpeed"))
                            {
                                try
                                {
                                    errorProviderMetInput.Clear();
                                    windSpeed.Add(Convert.ToDouble(tb.Text));
                                }
                                catch
                                {
                                    errorProviderMetInput.SetError(tb, " Please check the value of the wind speed you have inserted in this field");
                                    throw new System.Exception(" Please check the value of the wind speed you have inserted for the Meteorologocal Station No: " + Convert.ToString(i-1));

                                }

                            }
                            if (tb.Name.Contains("txtBoxWindDirection"))
                            {

                                try
                                {
                                    errorProviderMetInput.Clear();
                                    if (Convert.ToDouble(tb.Text) >= 0 && Convert.ToDouble(tb.Text) <= 360)
                                    {
                                       

                                        windDirection.Add(Convert.ToDouble(tb.Text));
                                    }
                                    else {
                                        errorProviderMetInput.SetError(tb, " Please check the value of the wind direction you have inserted in this field");
                                        throw new System.Exception("Please check the value of the wind direction you have inserted for the Meteorological Station No: " + Convert.ToString(i - 1));
                                 
                                    
                                    }
                                 
                                }
                                catch
                                {
                                    errorProviderMetInput.SetError(tb, " Please check the value of the wind direction you have inserted in this field");                          
                                    throw new System.Exception("Please check the value of the wind direction you have inserted for the Meteorological Station No: " + Convert.ToString(i-1));

                                }

                            }

                        }

                    }

                    if (grpBox.Name.Contains("grpBoxTempData")) {
                        foreach (var tb in grpBox.Controls.OfType<TextBox>()) {

                            if (tb.Name.Contains("txtBoxDryBulbTemp")) {

                                try
                                {
                                    errorProviderMetInput.Clear();
                                    DryBulbTemperature.Add(Convert.ToDouble(tb.Text));

                                }
                                catch {
                                    errorProviderMetInput.SetError(tb, " Please check the value of the temperature you have inserted in this field");                           
                                    throw new Exception("Please check the temperature value you have inserted for the Meteorological Station No: " + Convert.ToString(i - 1));
                                }
                            }
                        }
                    
                    }

                    if (grpBox.Name.Contains("grpBoxOtherMetParameters")) {

                        foreach (var tb in grpBox.Controls.OfType<TextBox>()) {

                            if (tb.Name.Contains("txtBoxAtmosphericPressure")) {

                                try
                                {
                                    errorProviderMetInput.Clear();
                                    pressure.Add(Convert.ToDouble(tb.Text));
                                }
                                catch {
                                    errorProviderMetInput.SetError(tb, " Please check the value of the atmospheric pressure you have inserted in this field");                           
                                    throw new Exception("Please check the value of the atmospheric pressure you have inserted for the Meteorological Station No: " + Convert.ToString(i - 1));
                                }
                            }

                            if (tb.Name.Contains("txtBoxRelativeHumidity")) {
                                try
                                {
                                    errorProviderMetInput.Clear();
                                    relativeHumidity.Add(Convert.ToDouble(tb.Text));
                                }
                                catch {
                                    errorProviderMetInput.SetError(tb, " Please check the value of the relative humidity you have inserted in this field");                           
                                    throw new Exception("Please check the value of the relative humidity you have inserted for the Meteorological Station No: " + Convert.ToString(i - 1)); 
                                }
                            }

                            if (tb.Name.Contains("txtBoxCloudCover")) {

                                try
                                {
                                    errorProviderMetInput.Clear();
                                    cloudCover.Add(Convert.ToDouble(tb.Text));
                                }
                                catch {
                                    errorProviderMetInput.SetError(tb, " Please check the value of the cloud cover you have inserted in this field");                           
                                    throw new Exception("Please check the value of the cloud cover you have inserted for the Meteorological Station No: " + Convert.ToString(i - 1));
                                }
                            }
                        }

                        
                    }

                    if (grpBox.Name.Contains("grpBoxMetParameters"))
                    {
                        foreach (var tb in grpBox.Controls.OfType<TextBox>())
                        {

                            if (tb.Name.Contains("txtBoxMixingHeight"))
                            {

                                try
                                {
                                    errorProviderMetInput.Clear();
                                    mixingHeight[i - 2] = Convert.ToDouble(tb.Text);

                                }
                                catch
                                {
                                    errorProviderMetInput.SetError(tb, " Please check the value of the mixing height you have inserted");
                                    throw new Exception("Please check the value of the mixing height you have inserted for the Meteorological Station No: " + Convert.ToString(i - 1));
                                }
                            }


                            //if (tb.Name.Contains("txtBoxElevationOfTheStation"))
                            //{

                            //    try
                            //    {
                            //        errorProviderMetInput.Clear();
                            //        elevationOfTheStation.Add(Convert.ToDouble(tb.Text));

                            //    }
                            //    catch
                            //    {
                            //        errorProviderMetInput.SetError(tb, " Please check the elevation of the meteorological station you have inserted in this field");
                            //        throw new Exception("Please check the elevation of the meteorological station you have inserted for the Meteorological Station No: " + Convert.ToString(i - 1));
                            //    }
                            //}

                            //if (tb.Name.Contains("txtBoxMetSamplingTime"))
                            //{

                            //    try
                            //    {
                            //        errorProviderMetInput.Clear();
                            //        samplingTime.Add(Convert.ToDouble(tb.Text));

                            //    }
                            //    catch
                            //    {
                            //        errorProviderMetInput.SetError(tb, " Please check the value of the sampling time you have inserted in this field");
                            //        throw new Exception("Please check the value of the sampling time you have inserted for the Meteorological Station No: " + Convert.ToString(i - 1));
                            //    }
                            //}

                            if (tb.Name.Contains("txtBoxPrecipitationIntensity"))
                            {

                                try
                                {
                                    errorProviderMetInput.Clear();
                                    precipitationIntensity.Add(Convert.ToDouble(tb.Text));

                                }
                                catch
                                {
                                    errorProviderMetInput.SetError(tb, " Please check the value of the rain fall you have inserted in this field");
                                    throw new Exception("Please check the value of the rain fall you have inserted for the Meteorological Station No: " + Convert.ToString(i - 1));
                                }
                            }



                        }


                    }


                    

                }   
            
            }



        
        }

        #endregion




        #region UI events...
        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                ValidateControls();
                frmMainHPCM.validated = true;
                mainForm.toolStripButtonInput.Enabled = false;
                mainForm.toolStripButtonRun.Enabled = true;
                this.Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
              
            }

           
            
        }

        private void cmbBoxTerrainType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int selectedTerrain = Convert.ToInt16(cmbBoxTerrainType.SelectedIndex);
            switch (Convert.ToInt16(cmbBoxTerrainType.SelectedIndex))
            {
                case 0:
                    rchTxtBoxTerrainType.Text = "Featureless land surface without any noticeable obstacles and negligible vegetation Eg. Beaches, pack ice without large ridges, marsh and snow covered or fallow open country";
                    break;
                case 1:
                    rchTxtBoxTerrainType.Text = "Level country with low vegetation (eg. Grass) and isolated obstacles with separations of at least 50 obstacle heights; eg. Grazing land without windbreaks, heather, moor and tundra, ruaway area of airports. Ice with ridges across-wind.";
                    break;
                case 2:
                    rchTxtBoxTerrainType.Text = "Cultivated or natural area with low crops or plant covers, or moderately open country with occasional obstacles (eg. Low hedges, isolated low buildings or trees) at relative horizontal distances of at least 20 obstacle heights.";
                    break;

                case 3:
                    rchTxtBoxTerrainType.Text = "Cultivated or natural area with high crops or crops of varying height, and scattered obstacles at relative distances of 12 to 15 obstacle heights for porous objects (eg. Shelterbelts) or 8 to 12 obstacle heights for low solid objects ";
                    break;
                case 4:
                    rchTxtBoxTerrainType.Text = "Intensively cultivated landscape with many rather large obstacle groups (large farms, clumps of forest) separated by open spaces of about 8 obstacle heights. Low densely planted major vegetation like bushland. Orchards, young forest. Also, area moderately covered by low buildings.";
                    break;
                case 5:
                    rchTxtBoxTerrainType.Text = "Landscape regularly covered with similar-size large obstacles, with open spaces of the same order of magnitude as obstacle heights; eg. Mature regular forests, densely built up area without much building height variation.";
                    break;
                case 6:
                    rchTxtBoxTerrainType.Text = "City centers with mixture of low-rise and high-rise buildings, or large forests of irregular height with many clearings.";
                    break;

            }
        } 

        //Positioning of source term group boxes with the changing of radio button status...

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lblHeatOfDetonation.Parent = grpBoxInstantaneous;
            cmbBoxHOD.Parent = grpBoxInstantaneous;
            lblHeatOfDetonationUnits.Parent = grpBoxInstantaneous;
            lblHODNewComposition.Parent = grpBoxInstantaneous;
            chkBoxHODNewComposition.Parent = grpBoxInstantaneous;
            lblEffectiveHeatPercentage.Parent = grpBoxInstantaneous;
            tbarEffectiveHeatPercentage.Parent = grpBoxInstantaneous;
            lblEffHeatPercentage.Parent = grpBoxInstantaneous;
            lblEffectiveAgentPercentage.Parent=grpBoxInstantaneous;
            tBarEffectiveAgentPercentage.Parent=grpBoxInstantaneous;
            lblEffectivePercentage.Parent=grpBoxInstantaneous;
            lblExplosiveWeight.Parent = grpBoxInstantaneous;
            txtBoxWeightOfExplosive.Parent = grpBoxInstantaneous;
            lblExplosiveWeightUnits.Parent = grpBoxInstantaneous;


            lblChemicalAgent.Parent = grpBoxInstantaneous;
            cmbBoxChemicalAgent.Parent = grpBoxInstantaneous;
            lblAgentWeight.Parent = grpBoxInstantaneous;
            txtBoxAgentWeight.Parent = grpBoxInstantaneous;
            lblAgentWeightUnits.Parent = grpBoxInstantaneous;



            lblEffectiveHeatPercentage.SetBounds(16, 148, 177, 17);
            tbarEffectiveHeatPercentage.SetBounds(219, 138, 191, 45);
            lblEffHeatPercentage.SetBounds(419, 147, 48, 17);
            lblHeatOfDetonation.SetBounds(16, 34, 135, 17);
            cmbBoxHOD.SetBounds(227, 31, 121, 25);
            lblHeatOfDetonationUnits.SetBounds(363, 34, 56, 17);
            lblHODNewComposition.SetBounds(501, 34, 369, 17);
            chkBoxHODNewComposition.SetBounds(876, 37, 15, 14);
            lblChemicalAgent.SetBounds(16, 208, 111, 17);
            cmbBoxChemicalAgent.SetBounds(227, 205, 101, 25);
            lblAgentWeight.SetBounds(16, 270, 101, 17);
            txtBoxAgentWeight.SetBounds(227, 267, 101, 25);
            lblAgentWeightUnits.SetBounds(363, 265, 36, 17);
            lblEffectiveAgentPercentage.SetBounds(16, 320, 184, 17);
            tBarEffectiveAgentPercentage.SetBounds(219, 321, 191, 45);
            lblEffectivePercentage.SetBounds(419, 327, 48, 17);
            lblExplosiveWeight.SetBounds( 16, 89,164, 17);
            txtBoxWeightOfExplosive.SetBounds( 227, 86,101, 25);
            lblExplosiveWeightUnits.SetBounds(363, 89, 36, 17);

            foreach (var t in this.grpBoxInstantaneous.Controls.OfType<TextBox>()) {
                t.Clear();
            
            }
            foreach (var cb in this.grpBoxInstantaneous.Controls.OfType<ComboBox>()) {
                cb.ResetText();
            }
            foreach (var tb in this.grpBoxInstantaneous.Controls.OfType<TrackBar>()) {
                tb.Value = 20;
            }

            this.grpBoxInstantaneous.Show();
            this.grpBoxInstantaneous.SetBounds(12, 149, 908, 369);
            this.grpBoxSemiContinuous.Hide();
            this.grpBoxContinuous.Hide();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            //lblHeatOfDetonation.Parent = grpBoxSemiContinuous;
            //cmbBoxHOD.Parent = grpBoxSemiContinuous;
            //lblHeatOfDetonationUnits.Parent = grpBoxSemiContinuous;
            //lblHODNewComposition.Parent = grpBoxSemiContinuous;
            //chkBoxHODNewComposition.Parent = grpBoxSemiContinuous;
            //lblEffectiveHeatPercentage.Parent = grpBoxSemiContinuous;
            //tbarEffectiveHeatPercentage.Parent = grpBoxSemiContinuous;
            //lblEffHeatPercentage.Parent = grpBoxSemiContinuous;

            lblEffectiveAgentPercentage.Parent = grpBoxSemiContinuous;
            tBarEffectiveAgentPercentage.Parent = grpBoxSemiContinuous;
            lblEffectivePercentage.Parent = grpBoxSemiContinuous;

            //lblExplosiveWeight.Parent = grpBoxSemiContinuous;
            //txtBoxWeightOfExplosive.Parent = grpBoxSemiContinuous;
            //lblExplosiveWeightUnits.Parent = grpBoxSemiContinuous;

            lblChemicalAgent.Parent = grpBoxSemiContinuous;
            cmbBoxChemicalAgent.Parent = grpBoxSemiContinuous;
            lblAgentWeight.Parent = grpBoxSemiContinuous;
            txtBoxAgentWeight.Parent = grpBoxSemiContinuous;
            lblAgentWeightUnits.Parent = grpBoxSemiContinuous;

            //lblEffectiveHeatPercentage.SetBounds(16, 148, 177, 17);
            //tbarEffectiveHeatPercentage.SetBounds(219, 138, 191, 45);
            //lblEffHeatPercentage.SetBounds(419, 147, 48, 17);
            //lblHeatOfDetonation.SetBounds(16, 34, 135, 17);
            //cmbBoxHOD.SetBounds(227, 31, 121, 25);
            //lblHeatOfDetonationUnits.SetBounds(363, 34, 56, 17);
            //lblHODNewComposition.SetBounds(501, 34, 369, 17);
            //chkBoxHODNewComposition.SetBounds(876, 37, 15, 14);

            lblChemicalAgent.SetBounds(16, 208, 111, 17);
            cmbBoxChemicalAgent.SetBounds(227, 205, 101, 25);
            lblAgentWeight.SetBounds(16, 270, 101, 17);
            txtBoxAgentWeight.SetBounds(227, 267, 101, 25);
            lblAgentWeightUnits.SetBounds(363, 265, 36, 17);

            lblEffectiveAgentPercentage.SetBounds(16, 320, 184, 17);
            tBarEffectiveAgentPercentage.SetBounds(219, 321, 191, 45);
            lblEffectivePercentage.SetBounds(419, 327, 48, 17);

            //lblExplosiveWeight.SetBounds(16, 89, 164, 17);
            //txtBoxWeightOfExplosive.SetBounds(227, 86, 101, 25);
            //lblExplosiveWeightUnits.SetBounds(363, 89, 36, 17);

            //lblReleaseDuration.SetBounds(501, 317, 121, 17);
            //txtBoxReleaseDuration.SetBounds(654, 317, 101, 25);
            //lblReleaseDurationUnits.SetBounds(783, 320, 70, 17);


            foreach (var t in this.grpBoxSemiContinuous.Controls.OfType<TextBox>())
            {
                t.Clear();

            }
            foreach (var cb in this.grpBoxSemiContinuous.Controls.OfType<ComboBox>())
            {
                cb.ResetText();
            }
            foreach (var tb in this.grpBoxSemiContinuous.Controls.OfType<TrackBar>())
            {
                tb.Value = 20;
            }

            this.grpBoxSemiContinuous.SetBounds(12, 149, 908, 369);
            this.grpBoxSemiContinuous.Show(); 
            this.grpBoxInstantaneous.Hide();
            this.grpBoxContinuous.Hide();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            lblChemicalAgent.Parent = grpBoxContinuous;
            cmbBoxChemicalAgent.Parent = grpBoxContinuous;
            lblAgentWeight.Parent = grpBoxContinuous;
            txtBoxAgentWeight.Parent = grpBoxContinuous;
            lblAgentWeightUnits.Parent = grpBoxContinuous;

            lblChemicalAgent.SetBounds(25, 45, 111, 17);
            cmbBoxChemicalAgent.SetBounds(163, 42, 101, 25);
            lblAgentWeight.SetBounds(25, 107, 101, 17);
            txtBoxAgentWeight.SetBounds(163, 104, 101, 25);
            lblAgentWeightUnits.SetBounds(290, 107, 36, 17);

            foreach (var t in this.grpBoxContinuous.Controls.OfType<TextBox>())
            {
                t.Clear();

            }

            this.grpBoxContinuous.SetBounds(12, 149, 433, 170);
            this.grpBoxContinuous.Show();
            this.grpBoxInstantaneous.Hide();
            this.grpBoxSemiContinuous.Hide();
        }

        private void chkBoxSurfaceRoughnessLength_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxSurfaceRoughnessLength.Checked)
            {
                cmbBoxTerrainType.Enabled = false;
                cmbBoxTerrainType.Text = "";
                rchTxtBoxTerrainType.Clear();
                txtBoxSurfaceRoughnessLength.Enabled = true;

            }
            else
            {

                cmbBoxTerrainType.Enabled = true;
                txtBoxSurfaceRoughnessLength.Clear();
                txtBoxSurfaceRoughnessLength.Enabled = false;
            }
        }

        private void chkBoxAlbedoValue_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxAlbedoValue.Checked)
            {
                cmbBoxSurfaceType.Text = "";
                cmbBoxSurfaceType.Enabled = false;
                txtBoxAlbedo.Enabled = true;
            }
            else
            {
                txtBoxAlbedo.Clear();
                txtBoxAlbedo.Enabled = false;
                cmbBoxSurfaceType.Enabled = true;
            }
        }
        
        private void cmbBoxHOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProviderMetInput.Clear();
            chkBoxHODNewComposition.CheckState = CheckState.Unchecked;
            frmDataInput.heatOfDetonation = Convert.ToDouble(ds.Tables["explosivesDS"].Rows[cmbBoxHOD.SelectedIndex].ItemArray[2]);

        }

        private void tBarEffectiveAgentPercentage_Scroll(object sender, EventArgs e)
        {
            lblEffectivePercentage.Text = Convert.ToString(tBarEffectiveAgentPercentage.Value * 5) + " %";
        }

        private void chkBoxHODNewComposition_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxHODNewComposition.Checked)
            {

                HPCM_REBUILD.frmCalculateHOD frmCalculateHD = new HPCM_REBUILD.frmCalculateHOD(this);
                cmbBoxHOD.Text = Convert.ToString(heatOfDetonation);
                frmCalculateHD.ShowDialog();
            }
        }

        private void tbarEffectiveHeatPercentage_Scroll(object sender, EventArgs e)
        {
            lblEffHeatPercentage.Text = Convert.ToString(tbarEffectiveHeatPercentage.Value * 5) + " %";
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 

        private void frmDataInput_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mainForm.toolStripButtonRun.Enabled == false) {

                mainForm.toolStripButtonInput.Enabled = true;
            }
        }

        #endregion

        private void btnReset_Click(object sender, EventArgs e)
        {

        }

    }
}
