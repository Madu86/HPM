using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Projections;
using System.Data.OleDb;
using System.Data;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;



namespace HPCM_REBUILD
{
    class Geography
    {
        //Keeps track of the terrain height as the elevation from the sea level...
        public static double[,] terrainHeights = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX];
        public static double[,] terrainSlopeX = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX];
        public static double[,] terrainSlopeY = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX];


        public static double[,] angleOfThePlane = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX];


        public static double[,] UTMX = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX];
        public static double[,] UTMY = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX];

        public static PointLatLng[,] gridPoints = new PointLatLng[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX];
        public static double[] sigmaLevel = new double[Meteorology.NumberOfMetCellsZ+1];

        public static double[,] UTMX_SL = new double[920, 510];
        public static double[,] UTMY_SL = new double[920, 510];
        public static double[,] slGridHeights = new double[920, 510];
       


        OleDbConnection con = new OleDbConnection();
        OleDbDataAdapter da;
        DataSet ds = new DataSet();


        double[] releasePointUTM = new double[2];

        //Reads the data from data base file and stores in arrays...
        private void GetTerrainData() {

            //Loads data from SL terrain database...

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "University of Colombo/Hazard Prediction Model/Sl_Grid_Data.mdb");



            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+filePath+";Persist Security Info=True;Jet OLEDB:Database Password=keevamalamute";
            con.Open();
            da = new OleDbDataAdapter("SELECT * FROM Sl_Grid_Data", con);
            da.Fill(ds, "Sl_Grid_Data");
            con.Close();

            int a = 0;
            for (int j = 0; j < 920; j++)
            {
                for (int i = 0; i < 510; i++)
                {
                    UTMX_SL[j, i] = (double)ds.Tables[0].Rows[a].ItemArray[1];
                    UTMY_SL[j, i] = (double)ds.Tables[0].Rows[a].ItemArray[2];
                    slGridHeights[j, i] = (double)ds.Tables[0].Rows[a].ItemArray[3];

                    a += 1;
                }
            }


            //Gets the array location of source point in SLDB...

            List<double> sourcePointUTMCoordX_TEMP = new List<double>();
            List<double> sourcePointUTMCoordY_TEMP = new List<double>();

            var xy = new List<double>();
            var z = new List<double>();

            ProjectionInfo source = KnownCoordinateSystems.Geographic.World.WGS1984;
            ProjectionInfo dest = KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone44N;

            foreach (PointLatLng p in frmMainHPCM.sourcePointList )
            {

                xy.Add(p.Lng);
                xy.Add(p.Lat);
                z.Add(0);

                var xyA = xy.ToArray();
                var zA = z.ToArray();
                Reproject.ReprojectPoints(xyA, zA, source, dest, 0, z.Count);
                sourcePointUTMCoordX_TEMP.Add(xyA[0]);
                sourcePointUTMCoordY_TEMP.Add(xyA[1]);

                xy.Clear();
                z.Clear();

            }




            int xi = (Convert.ToInt32(sourcePointUTMCoordX_TEMP[0] - UTMX_SL[0, 0])) / 500;
            int yi = (Convert.ToInt32(sourcePointUTMCoordY_TEMP[0] - UTMY_SL[0, 0])) / 500;


            if (Meteorology.NumberOfMetCellsX % 2 == 0)
            {

                xi = xi - (Meteorology.NumberOfMetCellsX / 2);

            }
            else
            {

                xi = xi - ((Meteorology.NumberOfMetCellsX+1 )/ 2);
            }

            if (Meteorology.NumberOfMetCellsY % 2 == 0)
            {

                yi = yi - (Meteorology.NumberOfMetCellsY / 2);

            }
            else
            {

                yi = yi - ((Meteorology.NumberOfMetCellsY + 1) / 2);
            }


            //Pics data from SLDB and loads populates terrain heights...


     

       dest   = KnownCoordinateSystems.Geographic.World.WGS1984;
       source = KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone44N;

            for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
            {

                for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
                {

                    xy.Clear();
                    z.Clear();

                    double y = UTMY_SL[yi + j, xi + i];
                    double x = UTMX_SL[yi + j, xi + i];

                    xy.Add(x);
                    xy.Add(y);
                    z.Add(0);

                    var xyA = xy.ToArray();
                    var zA = z.ToArray();
                    Reproject.ReprojectPoints(xyA, zA, source, dest, 0, z.Count);


                    gridPoints[j, i] = new PointLatLng(xyA[1], xyA[0]);
                    Geography.terrainHeights[j, i] = Geography.slGridHeights[yi + j, xi + i];
                    UTMX[j, i] = UTMX_SL[yi + j, xi + i];
                    UTMY[j, i] = UTMY_SL[yi + j, xi + i];


                }


            }



        }





        public static List<double> metStationUTMCoordX = new List<double>();
        public static List<double> metStationUTMCoordY = new List<double>();

        //Calculates UTM coordinates of meteorological stations and stores in array lists...
        private void CalculateUTMOfMetStations()
        {

            var xy = new List<double>();
            var z = new List<double>();

            ProjectionInfo source = KnownCoordinateSystems.Geographic.World.WGS1984;
            ProjectionInfo dest = KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone44N;

            foreach (GMapMarkerCircle p in frmMainHPCM.metStationList)
            {

                xy.Add(p.Position.Lng);
                xy.Add(p.Position.Lat);
                z.Add(0);

                var xyA = xy.ToArray();
                var zA = z.ToArray();
                Reproject.ReprojectPoints(xyA, zA, source, dest, 0, z.Count);
                metStationUTMCoordX.Add(xyA[0]);
                metStationUTMCoordY.Add(xyA[1]);

                xy.Clear();
                z.Clear();

            }

        }


        //Calculates the elevation of the meteorological station using stored terrain heights and map marker positions...
        private void CalculateElevationOfMetStations()
        {


            frmDataInput.elevationOfTheStation.Clear();

            for (int st = 0; st < metStationUTMCoordY.Count; st++)
            {

                //var distanceX = metStationUTMCoordX[st] - Geography.UTMX[0, 0];
                //var distanceY = metStationUTMCoordY[st] - Geography.UTMY[0, 0];

                int xi = (Convert.ToInt32(metStationUTMCoordX[st] - UTMX_SL[0, 0])) / 500;
                int yi = (Convert.ToInt32(metStationUTMCoordY[st] - UTMY_SL[0, 0])) / 500;


                //var i = Convert.ToInt32(distanceX / Meteorology.HorizontalDimensions);
                //var j = Convert.ToInt32(distanceY / Meteorology.HorizontalDimensions);


                //if (i > Meteorology.NumberOfMetCellsX || j > Meteorology.NumberOfMetCellsY)
                //{

                //    throw new Exception(" Meteorological stations must be located inside the grid size you have specified...");
                //}

                //else
                //{
                    frmDataInput.elevationOfTheStation.Add(Geography.slGridHeights[yi, xi]);
                //}

            }


        }




        //Calculates slope values and stores in a 2D array...
        private void CalculateSlope()
        {

            for (int j = 0; j < Meteorology.NumberOfMetCellsY-1; j++) {

                for (int i = 0; i < Meteorology.NumberOfMetCellsX-1; i++) {

                    terrainSlopeX[j, i] = (terrainHeights[j, i + 1] - terrainHeights[j, i]) / Meteorology.HorizontalDimensions;
                    terrainSlopeY[j, i] = (terrainHeights[j + 1, i] - terrainHeights[j, i]) / Meteorology.HorizontalDimensions;
                }
            
            }
        
        }


        public static void CreateTerrainInExcel()
        {

            Excel.Application oExcel = new Excel.Application { Visible = true };
            oExcel.Workbooks.Add();

            Excel.Worksheet workSheet = oExcel.ActiveSheet;



            int rowCount = Meteorology.NumberOfMetCellsY;
            for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
            {


                for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
                {
                    workSheet = oExcel.Sheets[1];
                    workSheet.Cells[rowCount, i + 1] = terrainHeights[j, i];


                    workSheet = oExcel.Sheets[2];
                    workSheet.Cells[rowCount, i + 1] = terrainSlopeX[j, i];

                    workSheet = oExcel.Sheets[3];
                    workSheet.Cells[rowCount, i + 1] = terrainSlopeY[j, i];
                }

                rowCount -= 1;


            }



        }



        //Calculates the angle between a tagent plane to the surface and horizontal plane.... 
        private void CalculateAngleOfThePlane()
        {

            Vector v1;
            Vector v2;
            Vector v3;
            Vector v4;
            Vector v5;
            Vector v6;


            for (int j = 0; j < Meteorology.NumberOfMetCellsY - 1; j++)
            {

                for (int i = 0; i < Meteorology.NumberOfMetCellsX - 1; i++)
                {

                    v1 = new Vector(UTMX[j, i + 1] - UTMX[j, i], UTMY[j, i + 1] - UTMY[j, i], terrainHeights[j, i + 1] - terrainHeights[j, i]);
                    v2 = new Vector(UTMX[j + 1, i] - UTMX[j, i], UTMY[j + 1, i] - UTMY[j, i], terrainHeights[j + 1, i] - terrainHeights[j, i]);
                    v3 = Vector.CrossProduct(v1, v2);

                    v4 = new Vector(UTMX[j, i + 1] - UTMX[j, i], UTMY[j, i + 1] - UTMY[j, i], 0);
                    v5 = new Vector(UTMX[j + 1, i] - UTMX[j, i], UTMY[j + 1, i] - UTMY[j, i], 0);
                    v6 = Vector.CrossProduct(v4, v5);

                    angleOfThePlane[j, i] = Math.Acos(Vector.DotProduct(v3, v6) / (v3.Magnitude * v6.Magnitude));

                }

            }

        }



        private static void CalculateSigmaLevel()
        {


            double delta = (double)1 /(Meteorology.NumberOfMetCellsZ+1);

            double l = 1;
            for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
            {

                sigmaLevel[k] = -Math.Log10(l) * Meteorology.DomainHeight;
                l -= delta;

            }

            sigmaLevel[10] = 1000;


        }




        public void CalculateGeography() {

            GetTerrainData();
            CalculateSlope();
            CalculateSigmaLevel();

            CalculateUTMOfMetStations();
            CalculateElevationOfMetStations();

            //GetSamplingLocations();
            //CreateTerrainInExcel();
              
        }



    }
}
