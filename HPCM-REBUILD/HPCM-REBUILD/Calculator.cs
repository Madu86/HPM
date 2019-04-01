using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;



namespace HPCM_REBUILD
{



    class Calculator
    {

        //Class level variables...
        private static PointLatLng[,] concentrationGridPoints = new PointLatLng[1000, 1000];



        //Map viewer variable for point type conversions...
        GMap.NET.WindowsForms.GMapControl mapViewer;

        public Calculator(GMap.NET.WindowsForms.GMapControl mapViewer) 
        {

            
            this.mapViewer = mapViewer;
            
        }


        public void RunProject() {

            Meteorology m = new Meteorology(this.mapViewer);
            m.RunMeteorology();

            dispersion d = new dispersion(mapViewer);
            d.RunDispersion();

        }






    

    }
}
