using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using DotSpatial.Projections;
using Excel = Microsoft.Office.Interop.Excel;

namespace HPCM_REBUILD
{
    class Meteorology
    {
        private static int numberOfMetCellsX = 30;

        public static double coriolisParameter;

        private static float minimumNightTimeMOL = 50;

        public static int NumberOfMetCellsX
        {

            get
            {

                return numberOfMetCellsX;
            }
            set
            {

                numberOfMetCellsX = value;
            }

        }

        private static int numberOfMetCellsY = 54;

        public static int NumberOfMetCellsY
        {

            get
            {

                return numberOfMetCellsY;
            }
            set
            {

                numberOfMetCellsY = value;
            }

        }

        private static int numberOfMetCellsZ = 10;

        public static int NumberOfMetCellsZ
        {

            get
            {

                return numberOfMetCellsZ;
            }
            set
            {

                numberOfMetCellsZ = value;
            }

        }

        //Keeps heights of the meteorological grid levels, starting from the zeroth level..
        
        public static double[, ,] gridHeights = new double[NumberOfMetCellsY, NumberOfMetCellsX, NumberOfMetCellsZ+1];


        private static int domainHeight = 1000;

        //Property declaration for private domain variables...
        public static int DomainHeight
        {

            get
            {

                return domainHeight;
            }
            set
            {

                domainHeight = value;
            }

        }

        private static int horizontalDimensions = 500;

        //Property declaration for private horizontal cell width variable...

        public static int HorizontalDimensions
        {

            get
            {

                return horizontalDimensions;
            }
            set
            {

                horizontalDimensions = value;
            }

        }


        private static bool considerHeightOfInfluence = true;

        public static bool ConsiderHeightOfInfluence {

            get { return considerHeightOfInfluence; }
            set { considerHeightOfInfluence = value; }
        
        }


        private static bool considerRadiusOfInfluence = true;

        public static bool ConsiderRadiusOfInfluence
        {

            get { return considerRadiusOfInfluence; }
            set { considerRadiusOfInfluence = value; }

        }

        private static double heightOfInfluence = 1500;

        public static double HeightOfInfluence {

            get { return heightOfInfluence; }
            set { heightOfInfluence = value; }
        
        }


        private static double radiusOfInfluence = 1500;

        public static double RadiusOfInfluence 
        {

            get { return radiusOfInfluence; }
            set { radiusOfInfluence = value; }

        }


        //A variable declaration for the height at which pressure measurements are made...
        private static double referenceHeightOfPressure=1;

        public static double ReferenceHeightOfPressure {

            get { return referenceHeightOfPressure; }
            set { referenceHeightOfPressure = value; }
        }

        //A variable declaration for the height at which humidity measurements are made...

        private static double referenceHeightOfHumidity=1;

        public static double ReferenceHeightOfHumidity{
        
            get{return referenceHeightOfHumidity;}
            set{referenceHeightOfHumidity=value;}
        }



        //Variable and property declaration for crtical Froude number...
        private static double criticalFroudeNumber = 1;

        public static double CriticalFroudeNumber {

            get { return criticalFroudeNumber; }
            set { criticalFroudeNumber = value; }
        
        }


        ////Variable and property declaration for maximum radial distance for calculating blocking effects...
        //private static double radiusOfInfluenceForBlockingEffects = 1000;

        //public static double RadiusOfInfluenceForBlockingEffects {

        //    get { return radiusOfInfluenceForBlockingEffects; }
        //    set { radiusOfInfluenceForBlockingEffects = value; }
        
        //}


        //Time zone variable for heat flux calculations...
        private static double timeZone = 5.5;

        public static double TimeZone
        {

            get { return timeZone; }
            set { timeZone = value; }

        }



     private static int sunRiseTime = 7;


        public static int SunRiseTime
        {

            get { return sunRiseTime; }
            set { sunRiseTime = value; }

        }


        public static int sunSetTime = 17;

        public static int SunSetTime
        {

            get { return sunSetTime; }
            set { sunSetTime = value; }

        }

        ////Variables for grid based meteorological parameters...
        //public static PointLatLng[,] gridPoints = new PointLatLng[numberOfMetCellsY, numberOfMetCellsX];

        double[, ,] windComponents = new double[numberOfMetCellsY, numberOfMetCellsX, numberOfMetCellsZ];
        double[,] windDirections = new double[numberOfMetCellsY, numberOfMetCellsX];

        public static double[, ,] U = new double[numberOfMetCellsY, numberOfMetCellsX , numberOfMetCellsZ ];
        public static double[, ,] V = new double[numberOfMetCellsY , numberOfMetCellsX , numberOfMetCellsZ ];
        public static double[, ,] W = new double[numberOfMetCellsY , numberOfMetCellsX , numberOfMetCellsZ ];
        public static double[, ,] temperatureField = new double[numberOfMetCellsY , numberOfMetCellsX , numberOfMetCellsZ];
        public static double[, ,] arrPressure = new double[numberOfMetCellsY, NumberOfMetCellsX, numberOfMetCellsZ];
        public static double[, ,] arrRelativeHumidity = new double[numberOfMetCellsY, numberOfMetCellsX, numberOfMetCellsZ];

        public static double[,] arrMoninObukhovLength = new double[NumberOfMetCellsY, numberOfMetCellsX];
        public static double[,] arrFrictionVelocity = new double[NumberOfMetCellsY, numberOfMetCellsX];
        public static double[,] arrMixingHeight = new double[NumberOfMetCellsY, numberOfMetCellsX];
        public static double[,] arrSensibleHeatFlux = new double[NumberOfMetCellsY, numberOfMetCellsX];
        public static double[,] arrPrecipitationIntensity = new double[NumberOfMetCellsY, numberOfMetCellsX];
        public static double[,] arrLapseRate = new double[NumberOfMetCellsY, NumberOfMetCellsX];
        public static char[,] arrStabilityClass = new char[NumberOfMetCellsY, NumberOfMetCellsX];
        public static double[,] arrConvectiveVelocityScale = new double[NumberOfMetCellsY, NumberOfMetCellsX];




        double[] reciprocalObukhovLengthLst = new double[frmMainHPCM.metStationList.Count()]; // Keeps track of the reciprocal of the monin obukhov length for each and every meteorlogical station...
        double[] frictionVelocityLst = new double[frmMainHPCM.metStationList.Count()];//Keeps track of the friction velocity as calculated for each and every meteorological station...
        double[] mixingHeightLst = new double[frmMainHPCM.metStationList.Count()];//Keeps track of the mixing height as calculated by each meteorological station...
        double[] sensibleHeatFluxLst = new double[frmMainHPCM.metStationList.Count()];//Keeps track of the sensible heat flux as calculated by each meteorological station...
        double[] lapseRateLst = new double[frmMainHPCM.metStationList.Count()];// Keeps track of the laspse rate as calculated for each metorological station...
        double[] wetBulbTempLst = new double[frmMainHPCM.metStationList.Count()];


        GMap.NET.WindowsForms.GMapControl mapViewer;


        public Meteorology(GMap.NET.WindowsForms.GMapControl mapViewer)
        {
         
            this.mapViewer = mapViewer;
            
        }




        //Calculation of the julion day...
        private int CalculateJulianDay(int year, int month, int date)
        {

            int julianDay;
            int[] julianTable = new int[] { 365, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };

            if (Convert.ToBoolean(year % 4 == 0))
            {
                if (Convert.ToBoolean(year % 100 == 0))
                {

                    if (Convert.ToBoolean(year % 400 == 0))
                    {
                        if (month < 3)
                        {
                            julianDay = date + julianTable[month - 1];

                        }
                        else
                        {
                            julianDay = date + julianTable[month - 1] + 1;
                        }

                    }
                    else
                    {

                        julianDay = date + julianTable[month - 1];

                    }

                }
                else
                {
                    if (month < 3)
                    {
                        julianDay = date + julianTable[month - 1];

                    }
                    else
                    {

                        julianDay = date + julianTable[month - 1] + 1;

                    }

                }

            }
            else
            {

                julianDay = date + julianTable[month - 1];

            }
            return julianDay;

        }


        //Calculation of the solar elevation angle...
        private double CalculateSolarElevation(double Lat, double Lng)
        {

            double julianDay = Convert.ToDouble(CalculateJulianDay(frmDataInput.year, frmDataInput.month, frmDataInput.date));

            double solarLongitude = 4.871 + 0.0175 * julianDay + 0.033 * Math.Sin(Convert.ToDouble(0.0175 * julianDay));
            double localTime = Convert.ToDouble(frmDataInput.hour) + Convert.ToDouble(frmDataInput.minute) / 60 + Convert.ToDouble(frmDataInput.second) / 3600;
            double universalTime = localTime - TimeZone;
            double alphaTheSmallAngle = (Convert.ToDouble(Lng) * 3.14 / 180) + 0.043 * Math.Sin(2 * solarLongitude) - 0.033 * Math.Sin(0.0175 * julianDay) + 3.14 * ((universalTime / 12) - 1);
            double solarDeclination = Math.Asin(0.398 * Math.Sin(solarLongitude));
            double solarElevation = Math.Asin(Math.Sin(solarDeclination) * Math.Sin(Convert.ToDouble(Lat) * 3.14 / 180) + Math.Cos(solarDeclination) * Math.Cos(Convert.ToDouble(Lat) * 3.14 / 180) * Math.Cos(alphaTheSmallAngle));

            return solarElevation;
        }


        //Calculation of the rate of change of saturation specific humidity with temperature(gamma)...
        private double CalculateGamma(double temperatureInCelcius)
        {
            var temperature = temperatureInCelcius;

            double gamma = 0.21;


            if (-1 < temperature && temperature < 6)
            {

                gamma = 1.44 - 0.076 * temperature;

            }
            if (5 < temperature && temperature < 11)
            {

                gamma = 1.06 - 0.054 * (temperature - 5);
            }

            if (10 < temperature && temperature < 16)
            {

                gamma = 0.79 - 0.038 * (temperature - 10);
            }

            if (15 < temperature && temperature < 21)
            {

                gamma = 0.6 - 0.03 * (temperature - 15);
            }

            if (20 < temperature && temperature < 26)
            {

                gamma = 0.45 - 0.02 * (temperature - 20);
            }

            if (25 < temperature && temperature < 31)
            {

                gamma = 0.35 - 0.016 * (temperature - 25);
            }

            if (30 < temperature && temperature < 36)
            {

                gamma = 0.27 - 0.012 * (temperature - 30);
            }
            return gamma;

        }

        //Calculation of the density of atmospheric air...
        public static double CalculateAtmosphericAirDensity(double temperatueInCelcius)
        {
            double densityOfAir = 1.293;

            var temperature = temperatueInCelcius;

            if (-1 < temperature && temperature < 21)
            {

                densityOfAir = 1.293 - 0.0044 * (temperature - 0);
            }

            if (20 < temperature && temperature < 41)
            {
                densityOfAir = 1.205 - 0.0039 * (temperature - 20);

            }

            if (40 < temperature && temperature < 61)
            {

                densityOfAir = 1.127 - 0.003 * (temperature - 40);

            }

            return densityOfAir;

        }


        private double CalculateIncommingSolarRadiation(double Lat, double Lng,int station)
        {
            double albedo = frmDataInput.albedoValue;
            double cloudCover = frmDataInput.cloudCover[station];

            double incomingSolarRadiation = (990 * Math.Sin(CalculateSolarElevation(Lat, Lng)) - 30) * (1 - 0.75 * Math.Pow(cloudCover / 100, 3.4))*(1 - albedo) ;

            return incomingSolarRadiation;
        }

        //Calculation of day time heat flux...
        private double CalculateDayTimeHeatFlux(double Lat, double Lng, double temperature, int station)
        {


            double cloudCover = frmDataInput.cloudCover[station];

            var incomingSolarRadiation = CalculateIncommingSolarRadiation(Lat, Lng, station);
            var gamma = CalculateGamma(temperature);

            double netRadiativeHeatFlux = ( incomingSolarRadiation + 5.31 * Math.Pow(10, -13) * Math.Pow((temperature + 273.15), 6) - Math.Pow((temperature + 273.15), 4) * 5.67 * Math.Pow(10, -8) + 60 * cloudCover / 100) / 1.12;



            double sensibleHeatFlux = (((1 - frmDataInput.surfaceMoistureAvailability / 100) + gamma) / (1 + gamma) * 0.9 * netRadiativeHeatFlux) - 20 * frmDataInput.surfaceMoistureAvailability / 100;



            return sensibleHeatFlux;

        }

        //Calculates the value of f(z/z0,L) function...
        private double CalculateEmpericalFunction(double height, double reciprocalObukhovLength)
        {

            double functionValue = 0;


            if (Convert.ToBoolean(reciprocalObukhovLength > 0))
            {

                functionValue = Math.Log((height / frmDataInput.surfaceRoughnessLength), Math.E) + 5 * (height - frmDataInput.surfaceRoughnessLength) * reciprocalObukhovLength;

            }
            else
            {

                double psiPrimeOfHeight = Math.Pow((1 - 16 * height * reciprocalObukhovLength), 0.25);
                double psiPrimeOfRoughnessLength = Math.Pow((1 - 16 * frmDataInput.surfaceRoughnessLength * reciprocalObukhovLength), 0.25);

                double psiOfHeight = 2 * Math.Log(((1 + psiPrimeOfHeight) / 2), Math.E) + Math.Log(((1 + Math.Pow(psiPrimeOfHeight, 2)) / 2), Math.E) - 2 * Math.Atan(psiPrimeOfHeight) + 1.57;
                double psiOfRoughnessLength = 2 * Math.Log(((1 + psiPrimeOfRoughnessLength) / 2), Math.E) + Math.Log(((1 + Math.Pow(psiPrimeOfRoughnessLength, 2)) / 2), Math.E) - 2 * Math.Atan(psiPrimeOfRoughnessLength) + 1.57;


                functionValue = Math.Log((height / frmDataInput.surfaceRoughnessLength), Math.E) - psiOfHeight + psiOfRoughnessLength;

            }

            return functionValue;
        }

        //Calculates the value of f(z/z0,L) function...
        private double CalculateEmpericalFunctionForTempCalculation(double height2, double height1, double reciprocalObukhovLength)
        {

            double functionValue = 0;


            if (Convert.ToBoolean(reciprocalObukhovLength > 0))
            {

                functionValue = Math.Log((height2 / height1), Math.E) + 5 * (height2 - height1) * reciprocalObukhovLength;

            }
            else
            {

                double psiPrimeOfHeight2 = Math.Pow((1 - 16 * height2 * reciprocalObukhovLength), 0.5);
                double psiPrimeOfHeight1 = Math.Pow((1 - 16 * height1 * reciprocalObukhovLength), 0.5);

                double psiOfHeight2 = 2 * Math.Log(((1 + psiPrimeOfHeight2) / 2), Math.E) + Math.Log(((1 + Math.Pow(psiPrimeOfHeight2, 2)) / 2), Math.E) - 2 * Math.Atan(psiPrimeOfHeight2) + 1.57;
                double psiOfHeight1 = 2 * Math.Log(((1 + psiPrimeOfHeight1) / 2), Math.E) + Math.Log(((1 + Math.Pow(psiPrimeOfHeight1, 2)) / 2), Math.E) - 2 * Math.Atan(psiPrimeOfHeight1) + 1.57;


                functionValue = Math.Log((height2 / height1), Math.E) - psiOfHeight2 + psiOfHeight1;

            }

            return functionValue;
        }



        //Calculation of the friction velocity...
        private double CalculateSurfaceFrictionVelocity(double baseWindSpeed, double height, double reciprocalObukhovLength)
        {
            double frictionVelocity = baseWindSpeed * 0.41 / CalculateEmpericalFunction(height, reciprocalObukhovLength);

            return frictionVelocity;

        }

        //Calculates the temperature scale...
        private double CalculateTemperatureScale(double reciprocalObukhovLength, double temperatureInKelvin, double surfaceFrictionVelocity)
        {

            double temperatureScale = (Math.Pow(surfaceFrictionVelocity, 2) * temperatureInKelvin ) * reciprocalObukhovLength / (0.41 * 9.81);

            return temperatureScale;
        }


        private double CalcTemp(double height, double temperatureInKelvin, double reciprocalObukhovLength, double frictionVelocity) {

            var temperatureScale = CalculateTemperatureScale(reciprocalObukhovLength, temperatureInKelvin, frictionVelocity);

            var temp = temperatureInKelvin + (temperatureScale / 0.41) * (Math.Log(height / 2) + 5 * (height - 2) * reciprocalObukhovLength) - 0.011 * (height - 2);

            return temp;
        }



        //Calculates the temperature at different heights...
        private double CalculateTemperature(double height, double temperature, double reciprocalObukhovLength, double frictionVelocity)
        {

            var temperatureScale=CalculateTemperatureScale(reciprocalObukhovLength,(temperature+273.15), frictionVelocity) ;
            var dalr = CalculateAdiabaticLapseRate(CalculateStabilityCategory(reciprocalObukhovLength));



            if (height > 2)
            {
                if (reciprocalObukhovLength >= 0)
                {
                    temperature = (temperature + 273.15) + (temperatureScale / 0.41) * (Math.Log((height / 2)) + 5 * (height - 2) * reciprocalObukhovLength) -0.011 * (height - 2);
                }
                else {

                    temperature = (temperature + 273.15) + (temperatureScale / 0.41) * CalculateEmpericalFunctionForTempCalculation(height, 2, reciprocalObukhovLength) + dalr * (height - 2);
                
                }
            }
            else
            {
                if (reciprocalObukhovLength >= 0)
                {
                //    temperature = (temperature + 273.15) - (temperatureScale / 0.41) * (Math.Log((2 / height), Math.E) + 5 * (2 - height) * reciprocalObukhovLength) + dalr * (2 - height);
                //
                    temperature = (temperature + 273.15) + (temperatureScale / 0.41) * (Math.Log((height / 2), Math.E) + 5 * (height - 2) * reciprocalObukhovLength) - 0.011 * (height - 2);
  
                
                }
                else {

                    temperature = (temperature + 273.15) - (temperatureScale / 0.41) * CalculateEmpericalFunctionForTempCalculation(height, 2, reciprocalObukhovLength);
                
                }
            }

            return temperature;

        }


        //Calculation of monin-obukhov length...
        private double CalculateObukhovLength(double sensibleHeatFlux, double baseWindSpeed, double temperature)
        {


            double constantOfTheObukhovIteration = (-CalculateAtmosphericAirDensity(temperature) * 1006 * (temperature + 273.15)) / (0.43 * 9.81 * sensibleHeatFlux);
            double obukhovLength = constantOfTheObukhovIteration * Math.Pow(CalculateSurfaceFrictionVelocity(baseWindSpeed, 10, 100), 3);


            for (int i = 0; i < 100; i++)
            {
                obukhovLength = constantOfTheObukhovIteration * Math.Pow(CalculateSurfaceFrictionVelocity(baseWindSpeed, 10, (1 / obukhovLength)), 3);

            }

            return obukhovLength;
        }


        //Calculates the wet bulb temperature for given values of dry bulb temperature, pressure and relative humidity...
        private double CalculateWetBulbTemperature(double temperature, int station)
        {

            double wetBulbTemperature = 0;
            double dryBulbTemperature = temperature;
            double relativeHumidity = frmDataInput.relativeHumidity[station];
            double atmosphericPressure =frmDataInput.pressure[station] * 1.3332; //atmospheric pressure in hecto pascals...

            double initialWetBulbTemperature = dryBulbTemperature - 5;
            double initialSaturationVaporPressureAtWBT = Math.Exp((21.4 * initialWetBulbTemperature + 494.41) / (initialWetBulbTemperature + 273.15));
            double initialSaturationVaporPressureAtDBT = Math.Exp((21.4 * dryBulbTemperature + 494.41) / (dryBulbTemperature + 273.15));

            int numInt = 0;

            for (int i = 0; i < 50000; i++)
            {
                numInt = i;
                wetBulbTemperature = dryBulbTemperature - (initialSaturationVaporPressureAtWBT - (relativeHumidity / 100) * initialSaturationVaporPressureAtDBT) / (6.53 * Math.Pow(10, -4) * atmosphericPressure);

                if (Math.Abs(wetBulbTemperature-initialWetBulbTemperature)<= 0.1)
                {
                    break;
                }

                else
                {

                    if (Math.Abs(wetBulbTemperature - initialWetBulbTemperature) > 0.1)
                    {

                        initialSaturationVaporPressureAtWBT = dryBulbTemperature - 5 + i* Math.Pow(10, -3);
                    }
                    if (Math.Abs(wetBulbTemperature - initialWetBulbTemperature) < -0.1)
                    {

                        initialSaturationVaporPressureAtWBT = dryBulbTemperature - 5 - i* Math.Pow(10, -3);
                    }

                }


            }


            return wetBulbTemperature;

        }

        //Calculates the sensible heat flux for the night time...
        private double CalculateNightTimeSensibleHeatFlux(double densityOfAir, double frictionVelocity, double reciprocalObukhovLength,double temperatureScale) 
        {

            double sensibleHeatFlux = -densityOfAir * 1006 * frictionVelocity * temperatureScale;

            return sensibleHeatFlux;
        }

        //Calculates the value of the heat balance function for night time conditions...
        private double CalculateNightTimeHeatBalanceFunction(double reciprocalObukhovLength, double frictionVelocity, double roughnessLength, double densityOfAir, double gamma, double temperature, int station)
        {

            double temperatureScale = CalculateTemperatureScale(reciprocalObukhovLength, (temperature+273.15), frictionVelocity);

            double temperature50 = CalcTemp(50, (temperature+273.15), reciprocalObukhovLength, frictionVelocity);
            double temperatureZ0 = CalcTemp(roughnessLength, (temperature+273.15), reciprocalObukhovLength, frictionVelocity);

            double surfaceVegetationTemperature = (temperatureZ0) - temperatureScale * (10 + (4.2 / frictionVelocity));
            double isothermalNetLongWaveRadiation = -5.67 * Math.Pow(10, -8) * Math.Pow(temperature50, 4) * (1 - (9.35 * Math.Pow(10, -6) * Math.Pow(temperature50, 2))) + 60 * frmDataInput.cloudCover[station] / 100;

            double netLongWaveRadiation = isothermalNetLongWaveRadiation + (4 * 5.67 * Math.Pow(10, -8) * Math.Pow(temperature50, 3) * (temperature50 - surfaceVegetationTemperature));
            double sensibleHeatFlux = CalculateNightTimeSensibleHeatFlux(densityOfAir, frictionVelocity, reciprocalObukhovLength, temperatureScale);
            double groundHeatFlux = 1.2 * ((isothermalNetLongWaveRadiation / 3) - (sensibleHeatFlux / 4));

            double tempDiff=((temperature + 273.15) - surfaceVegetationTemperature);
            double transferCoefficient = temperatureScale / tempDiff;

            double latentHeatFlux = ((1 / (1 + gamma)) * (netLongWaveRadiation - groundHeatFlux) + densityOfAir * 1006 * (temperature - wetBulbTempLst[station]) * transferCoefficient * frictionVelocity) / (1 + (500 * gamma * transferCoefficient * frictionVelocity / (1 + gamma)));

            double heatBalanceFunction = sensibleHeatFlux + latentHeatFlux + groundHeatFlux - netLongWaveRadiation;

            return heatBalanceFunction;



        }


        //Calculates the sensible heat flux for night time but returns the relevant reciprocal of the monin obukhov length...
        private double CalculateNightTimeObukhovLength(double baseWindSpeed, double temperature, int station)
        {
            double roughnessLength = frmDataInput.surfaceRoughnessLength;
            double densityOfAir = CalculateAtmosphericAirDensity(temperature);
            double gamma = CalculateGamma(temperature);

            double reciprocalObukhovLength1 = 0;
            double reciprocalObukhovLength2 = 1;
            double reciprocalObukhovLength3 = 0;
            double functionValue1;
            double functionValue2;
            double functionValue3;


            for (int i = 0; i < 1000; i++)
            {

                functionValue1 = CalculateNightTimeHeatBalanceFunction(reciprocalObukhovLength1, CalculateSurfaceFrictionVelocity(baseWindSpeed, 10, reciprocalObukhovLength1), roughnessLength, densityOfAir, gamma, temperature,station);
                functionValue2 = CalculateNightTimeHeatBalanceFunction(reciprocalObukhovLength2, CalculateSurfaceFrictionVelocity(baseWindSpeed, 10, reciprocalObukhovLength2), roughnessLength, densityOfAir, gamma, temperature,station);

                if ((functionValue1 > 0 && functionValue2 > 0) || (functionValue1 < 0 && functionValue2 < 0))
                {


                }

                    reciprocalObukhovLength3 = reciprocalObukhovLength1 - ((reciprocalObukhovLength2 - reciprocalObukhovLength1) * functionValue1 / (functionValue2 - functionValue1));

                    functionValue3 = CalculateNightTimeHeatBalanceFunction(reciprocalObukhovLength3, CalculateSurfaceFrictionVelocity(baseWindSpeed, 10, reciprocalObukhovLength3), roughnessLength, densityOfAir, gamma, temperature, station);


                    if (Math.Abs(functionValue1) < 1) {

                        reciprocalObukhovLength3 = reciprocalObukhovLength1;
                        break;
                    }
                    else if (Math.Abs(functionValue2) < 1) {

                        reciprocalObukhovLength3 = reciprocalObukhovLength2;
                    
                    }
                
                    else  if (Math.Abs(Math.Abs(functionValue1) - Math.Abs(functionValue2))<0.01)
                    {

                        break;
                    
                    }

                    else if (functionValue1 * functionValue3 < 0)
                    {

                        reciprocalObukhovLength2 = reciprocalObukhovLength3;
                    }

                    else
                    {
                        reciprocalObukhovLength1 = reciprocalObukhovLength3;

                    }

                

            }

            return reciprocalObukhovLength3;

        }


        //Calculates the wind speed at different heights...
        private double CalculateWindSpeed(double height, double frictionVelocity, double reciprocalObukhovLength)
        {

            double windSpeed = frictionVelocity * CalculateEmpericalFunction(height, reciprocalObukhovLength) / 0.41;

            return windSpeed;

        }

        //Returns the atmospheric stability for a particular monin-obukhov length...
        private char CalculateStabilityCategory(double reciprocalObukhovLength)
        {

            double obukhovLength = 1 / reciprocalObukhovLength;
            char stabilityCategory = 'A';

            if (Convert.ToBoolean(-100 < obukhovLength && obukhovLength < 0))
            {
                stabilityCategory = 'A';

            }

            else if (Convert.ToBoolean(-200 <= obukhovLength && obukhovLength < -100))
            {

                stabilityCategory = 'B';
            }

            else if (Convert.ToBoolean(-500 <= obukhovLength && obukhovLength < -200))
            {

                stabilityCategory = 'C';
            }

            else  if (Convert.ToBoolean(Math.Abs(obukhovLength) > 500))
            {

                stabilityCategory = 'D';
            }

            else  if (Convert.ToBoolean(200 <= obukhovLength && obukhovLength <= 500))
            {

                stabilityCategory = 'E';
            }

            else  if (Convert.ToBoolean(50 <= obukhovLength && obukhovLength < 200))
            {

                stabilityCategory = 'F';
            }

            else if (Convert.ToBoolean(0 < obukhovLength && obukhovLength < 50))
            {

                stabilityCategory = 'G';
            }

            return stabilityCategory;

        }


        //Calculates the mixing height ...
        private double CalculateMixingHeight(char stabilityCategory, double reciprocalObukhovLength, double frictionVelocity, double Lat)
        {

            double mixingHeight = 1000;
            switch (stabilityCategory)
            {
                case 'A':
                    mixingHeight = 1500;
                    break;

                case 'B':
                    mixingHeight = 1500;
                    break;

                case 'C':
                    mixingHeight = 1000;
                    break;

                case 'D':
                    mixingHeight = 0.2 * frictionVelocity / (2 * 7.2921 * Math.Pow(10, -5) * Math.Sin(Lat * 3.14 / 180));
                    if (mixingHeight > 500)
                    {

                        mixingHeight = 500;
                    }

                    break;

                case 'E':

                    mixingHeight = 0.4 * Math.Pow((frictionVelocity / (2 * 7.2921 * Math.Pow(10, -5) * Math.Sin(Lat * 3.14 / 180) * reciprocalObukhovLength)), 0.5);
                    break;

                case 'F':

                    mixingHeight = 0.4 * Math.Pow((frictionVelocity / (2 * 7.2921 * Math.Pow(10, -5) * Math.Sin(Lat * 3.14 / 180) * reciprocalObukhovLength)), 0.5);
                    break;
                case 'G':

                    mixingHeight = 0.4 * Math.Pow((frictionVelocity / (2 * 7.2921 * Math.Pow(10, -5) * Math.Sin(Lat * 3.14 / 180) * reciprocalObukhovLength)), 0.5);
                    break;
            }
            return mixingHeight;

        }


        //Calculates the convective friction velocity...
        private double CalculateConvectiveVelocityScale(double surfaceFrictionVelocity, double mixingHeight, double reciprocalObukhovLength)
        {
            double convectiveVelocityScale = 0;
            if (reciprocalObukhovLength < 0)
            {
                convectiveVelocityScale = surfaceFrictionVelocity * Math.Pow((-mixingHeight / 0.41) * reciprocalObukhovLength, 0.333333);
              
            }

            return convectiveVelocityScale;
        }


        //Returns the adiabatic laspe rate for a required atmospheric stability class...
        private double CalculateAdiabaticLapseRate(char stabilityClass)
        {

            double dryAdiabaticLapseRate = 0;
            switch (stabilityClass)
            {
                case 'A':
                    dryAdiabaticLapseRate = -0.017;
                    break;

                case 'B':
                    dryAdiabaticLapseRate = -0.015;
                    break;

                case 'C':
                    dryAdiabaticLapseRate = -0.013;
                    break;

                case 'D':
                    dryAdiabaticLapseRate = -0.01;
                    break;

                case 'E':
                    dryAdiabaticLapseRate = 0.005;
                    break;

                case 'F':
                    dryAdiabaticLapseRate = 0.0275;
                    break;

                case 'G':
                    dryAdiabaticLapseRate = 0.0625;
                    break;
            }

            return dryAdiabaticLapseRate;
        }



        private double CalculateLapseRate(int i, int j)
        {
            double deltaZ = (domainHeight - Geography.terrainHeights[j, i]) / numberOfMetCellsZ;

            double lapseRate = 0;

            lapseRate = (temperatureField[j, i, 0] - temperatureField[j, i, numberOfMetCellsZ]) / (numberOfMetCellsZ * deltaZ);

            return lapseRate;
        }


        //Calculates pressure for a given height...
        private double CalculatePressure(double referenceHeight, double height, double pressure, double temperature)
        {
            
            double pressureAtHeight = pressure * Math.Exp((referenceHeight - height) * 9.81 / (1000*8.314 * temperature));
            return pressureAtHeight;
        }


        //Calculates relative humidity for a given height...

        private double CalculateRelativeHumidity(double referenceHeight, double height, double relativeHumidity, double temperature)
        {

            
            double wl = 0.09205 * Math.Pow(10, -4) * Math.Pow((height - referenceHeight)/1000, 0.453117);
            double referenceHumidityAtHeight = relativeHumidity * Math.Exp(-(wl - (9.81 / (8.314 * temperature))) * ((height - referenceHeight)/1000));
            return referenceHumidityAtHeight;

        }




        //Calculates the reference turning angle using Van ulden and holtslags method...
        private double CalculateReferenceTurningAngle(double reciprocalObukhovLength)
        {

            double obukhovLength = 1 / reciprocalObukhovLength;
            double turningAngle = 0;
            if (obukhovLength <= -370)
            {

                turningAngle = 9;

            }
            else if (obukhovLength > -370 && obukhovLength <= -100)
            {

                turningAngle = 10;
            }
            else if (obukhovLength > -100 && obukhovLength <= 0)
            {

                turningAngle = 12;
            }
            else if (obukhovLength >= 10000)
            {

                turningAngle = 12;
            }
            else if (obukhovLength < 10000 && obukhovLength >= 350)
            {

                turningAngle = 18;
            }
            else if (obukhovLength < 350 && obukhovLength >= 130)
            {

                turningAngle = 28;
            }
            else if (obukhovLength < 130 && obukhovLength >= 60)
            {

                turningAngle = 35;
            }
            else if (obukhovLength < 60 && obukhovLength >= 20)
            {

                turningAngle = 38;
            }
            else if (obukhovLength < 20 && obukhovLength > 0)
            {

                turningAngle = 39;
            }


            return turningAngle;

        }

        //Calculates the wind direction for a given height...
        private double CalculateWindDirection(double height, double windDirection, double reciprocalObukhovLength, double latitude)
        {

            double referenceTurningAngle = CalculateReferenceTurningAngle(reciprocalObukhovLength);
            double turningAngle10 = referenceTurningAngle * 0.07706;

            double turningAngleZ = referenceTurningAngle * 1.58 * (1 - Math.Exp(-height / 200));

            turningAngleZ = turningAngleZ - turningAngle10;

            if (latitude > 0)
            {
                windDirection = windDirection + turningAngleZ;
            }
            else
            {
                windDirection = windDirection - turningAngleZ;
            }

            return windDirection;
        }

        //Calculates wind componets and assigns +,- sign based on the quadrant...

        private double[] GetWindComponents(double directionQuadrant, double windAngle, double windSpeed)
        {
            double[] windComponents = new double[2];
            if (directionQuadrant >= 0 && directionQuadrant <= 1)
            {
                windComponents[0] = -Math.Sin(windAngle * Math.PI / 180) * windSpeed;
                windComponents[1] = -Math.Cos(windAngle * Math.PI / 180) * windSpeed;
            }
            else if (directionQuadrant > 1 && directionQuadrant < 2)
            {
                windComponents[0] = -Math.Cos((windAngle - 90) * Math.PI / 180) * windSpeed;
                windComponents[1] = Math.Sin((windAngle - 90) * Math.PI / 180) * windSpeed;

            }
            else if (directionQuadrant >= 2 && directionQuadrant <= 3)
            {

                windComponents[0] = Math.Sin((windAngle - 180) * Math.PI / 180) * windSpeed;
                windComponents[1] = Math.Cos((windAngle - 180) * Math.PI / 180) * windSpeed;

            }
            else
            {
                windComponents[0] = Math.Cos((windAngle - 270) * Math.PI / 180) * windSpeed;
                windComponents[1] = -Math.Sin((windAngle - 270) * Math.PI / 180) * windSpeed;


            }
            return windComponents;


        }






        private double GetDistance(double x1, double y1, double x2, double y2) {

            double distance = Math.Pow((Math.Pow(Math.Abs(x2 - x1), 2) + Math.Pow(Math.Abs(y2 - y1), 2)), 0.5);
            return distance;
        }



     


        //Calculates meteorological parameters and stores in arrays...

       private void CalculateMeteorologicalParameters() {



           CalculateGridHeights();

            int numberOfMetSt = frmMainHPCM.metStationList.Count();
            double solarElevationAngle;


            double numeratorWSU;
            double numeratorWSV;
            double squaredDistance;
            double numeratorT;
            double numeratorP;
            double denominator;
            double numeratorRH;
            double numeratorMH;
            double numeratorMOL;
            double numeratorFV;
            double numeratorSHF;
            double numeratorPI;
            double numeratorLR;

            double frictionVelocity;
            double reciprocalObukhovLength;
            double Lat;
            double Lng;
  

            double height;
            double quadrant;
            List<int> effectiveStations = new List<int>(); // Keeps track of how many number of stations are effecting a particular grid cell...
            double[] windComponents = new double[2];
            double windSpeed;
            double windAngle;
            double temperature;
            double pressure;
            double relativeHumidity;
            double mixingHeight;
            double sensibleHeatFlux;

           double netRadiation;


            for (int a = 0; a < numberOfMetSt; a++)
            {

                Lat = frmMainHPCM.metStationList[a].Position.Lat;
                Lng = frmMainHPCM.metStationList[a].Position.Lng;

                // Calculating solar elevation angle...
                solarElevationAngle = CalculateSolarElevation(Lat, Lng);

                var incomingSolarRadiation = CalculateIncommingSolarRadiation(Lat, Lng, a);

                netRadiation = incomingSolarRadiation - 91 + 60 * frmDataInput.cloudCover[a] / 100;

                //Calculating friction velocity and the reciprocal monin obukhov lenght based on day or night time...
                if (netRadiation > 0)
                {

                    frictionVelocity = CalculateSurfaceFrictionVelocity(frmDataInput.windSpeed[a], frmDataInput.referenceHeightForWindMeasurents, (1 / CalculateObukhovLength(CalculateDayTimeHeatFlux(Lat, Lng, frmDataInput.DryBulbTemperature[a],a), frmDataInput.windSpeed[a], frmDataInput.DryBulbTemperature[a])));
                    sensibleHeatFlux = CalculateDayTimeHeatFlux(Lat, Lng, frmDataInput.DryBulbTemperature[a], a);
                    reciprocalObukhovLength = (1 / CalculateObukhovLength(sensibleHeatFlux, frmDataInput.windSpeed[a], frmDataInput.DryBulbTemperature[a]));

                }
                else
                {

                    wetBulbTempLst[a] = CalculateWetBulbTemperature(frmDataInput.DryBulbTemperature[a], a);
                    reciprocalObukhovLength = CalculateNightTimeObukhovLength(frmDataInput.windSpeed[a], frmDataInput.DryBulbTemperature[a], a);

                    if (reciprocalObukhovLength > (1 / minimumNightTimeMOL)) 
                    {
                        reciprocalObukhovLength = 1/minimumNightTimeMOL;
                    }

                    frictionVelocity = CalculateSurfaceFrictionVelocity(frmDataInput.windSpeed[a], frmDataInput.referenceHeightForWindMeasurents, reciprocalObukhovLength);                   
                    sensibleHeatFlux = CalculateNightTimeSensibleHeatFlux(CalculateAtmosphericAirDensity(frmDataInput.DryBulbTemperature[a]), frictionVelocity, reciprocalObukhovLength, CalculateTemperatureScale(reciprocalObukhovLength, (frmDataInput.DryBulbTemperature[a]+273.15), frictionVelocity));
                }

                
                //Store reciprocal obukhov length and friction velocity in stationwise temporary lists...
                reciprocalObukhovLengthLst[a] = reciprocalObukhovLength;
                frictionVelocityLst[a] = frictionVelocity;

                //Stores mixing height in stationwise temporary list...
                
                if (frmDataInput.mixingHeight[a] != 0)
                {
                    mixingHeightLst[a] = frmDataInput.mixingHeight[a];
                }
                else
                {
                    mixingHeightLst[a] = CalculateMixingHeight(CalculateStabilityCategory(reciprocalObukhovLength), reciprocalObukhovLength, frictionVelocity, Lat);
                }

                //Stores sensible heat flux in stationwise temporary list...

                sensibleHeatFluxLst[a] = sensibleHeatFlux;

                //Stores lapse rate in stationwise temporary array...

                lapseRateLst[a] = CalculateAdiabaticLapseRate(CalculateStabilityCategory(reciprocalObukhovLength));


            }


            //Interpolation of 2D meteorological fields...
            for (int j = 0; j < numberOfMetCellsY ; j++)
            {

                for (int i = 0; i < numberOfMetCellsX ; i++)
                {




                        effectiveStations.Clear();
                        squaredDistance = 0;
                        denominator = 0;
                        mixingHeight = 0;
                        numeratorMH = 0;
                        reciprocalObukhovLength = 0;
                        numeratorMOL = 0;
                        frictionVelocity = 0;
                        numeratorFV = 0;
                        sensibleHeatFlux = 0;
                        numeratorSHF = 0;
                        numeratorPI = 0;
                        numeratorLR = 0;

                        double effctiveDistance = frmDataInput.maximumRadialDistance;
                    double effectiveHeight=HeightOfInfluence;





                        for (; ; )
                        {

                            for (int a = 0; a < frmDataInput.numberOfMetStations; a++)
                            {

                                Lat = frmMainHPCM.metStationList[a].Position.Lat;
                                Lng = frmMainHPCM.metStationList[a].Position.Lng;


                                squaredDistance = Math.Pow(GetDistance(Geography.UTMX[j, i], Geography.UTMY[j, i], Geography.metStationUTMCoordX[a], Geography.metStationUTMCoordY[a]), 2);


                                if ((ConsiderHeightOfInfluence && Math.Abs(frmDataInput.elevationOfTheStation[a] - Geography.terrainHeights[j, i]) < heightOfInfluence)||(ConsiderHeightOfInfluence == false && ConsiderRadiusOfInfluence == false))
                                {


                                    if (squaredDistance < Math.Pow(effctiveDistance, 2))
                                    {
                                        effectiveStations.Add(a);



                                        mixingHeight = mixingHeightLst[a];
                                        numeratorMH += (mixingHeight / squaredDistance);
                                        numeratorMOL += ((1 / reciprocalObukhovLengthLst[a]) / squaredDistance);
                                        numeratorFV += (frictionVelocityLst[a] / squaredDistance);
                                        numeratorSHF += (sensibleHeatFluxLst[a] / squaredDistance);
                                        numeratorPI += (frmDataInput.precipitationIntensity[a] / squaredDistance);
                                        numeratorLR += (lapseRateLst[a] / squaredDistance);

                                        denominator += (1 / squaredDistance);
                                    }
                                }


                            }

                            if (ConsiderHeightOfInfluence && effectiveStations.Count == 0)
                            {
           
                                effectiveHeight += 100;
                                effctiveDistance += 1000;

                            }
                            else
                            {
                                arrMixingHeight[j, i] = (numeratorMH / denominator);
                                arrMoninObukhovLength[j, i] = (numeratorMOL / denominator);
                                arrFrictionVelocity[j, i] = (numeratorFV / denominator);
                                arrSensibleHeatFlux[j, i] = (numeratorSHF / denominator);
                                arrPrecipitationIntensity[j, i] = (numeratorPI / denominator);
                                arrLapseRate[j, i] = (numeratorLR / denominator);

                                effectiveStations.Clear();
                                break;

                            }
                        }

                    //Assigns the stability category for each cell and stores in an array...
                    arrStabilityClass[j, i] = CalculateStabilityCategory(1/arrMoninObukhovLength[j,i]);
                    arrConvectiveVelocityScale[j, i] = CalculateConvectiveVelocityScale(arrFrictionVelocity[j, i], arrMixingHeight[j, i], (1 / arrMoninObukhovLength[j, i]));

                }
            }



           //Interpolation of 3D meteorological parameters...

                for (int j = 0; j < numberOfMetCellsY ; j++)
                {

                    for (int i = 0; i < numberOfMetCellsX ; i++)
                    {

                        height = 0;

                        for (int k = 0; k < numberOfMetCellsZ; k++)
                        {
                            effectiveStations.Clear();
                            numeratorWSU = 0;
                            numeratorWSV = 0;
                            numeratorT = 0;
                            squaredDistance = 0;

                            windSpeed = 0;
                            temperature = 0;
                            windComponents = null;
                            pressure = 0;
                            numeratorP = 0;
                            denominator = 0;
                            relativeHumidity = 0;
                            numeratorRH = 0;



                            var deltaheight = (gridHeights[j, i, k + 1] - gridHeights[j, i, k]) / 2;


                            height = deltaheight + gridHeights[j, i, k] - gridHeights[j, i, 0];


                            double effctiveDistance = frmDataInput.maximumRadialDistance;
                            double effectiveHeight = HeightOfInfluence;


                            for (; ; )
                            {

                                for (int a = 0; a < frmDataInput.numberOfMetStations; a++)
                                {

                                    Lat = frmMainHPCM.metStationList[a].Position.Lat;
                                    Lng = frmMainHPCM.metStationList[a].Position.Lng;

                                    squaredDistance = Math.Pow(GetDistance(Geography.UTMX[j, i], Geography.UTMY[j, i], Geography.metStationUTMCoordX[a], Geography.metStationUTMCoordY[a]), 2);


                                    if ((ConsiderHeightOfInfluence && Math.Abs(frmDataInput.elevationOfTheStation[a] - Geography.terrainHeights[j, i]) < heightOfInfluence)||(ConsiderHeightOfInfluence==false && ConsiderRadiusOfInfluence==false))
                                    {

                                        if (squaredDistance < Math.Pow(effctiveDistance, 2))
                                        {

                                            effectiveStations.Add(a);


                                            if (height < 100)
                                            {
                                                windSpeed = CalculateWindSpeed(height, frictionVelocityLst[a], reciprocalObukhovLengthLst[a]);
                                                windAngle = CalculateWindDirection(height, frmDataInput.windDirection[a], reciprocalObukhovLengthLst[a], Lat);
                                                windAngle = frmDataInput.windDirection[a];
                                                quadrant = windAngle / 90;
                                                windComponents = GetWindComponents(quadrant, windAngle, windSpeed);

                                            }
                                            else
                                            {

                                                windSpeed = CalculateWindSpeed(100, frictionVelocityLst[a], reciprocalObukhovLengthLst[a]);
                                                windAngle = frmDataInput.windDirection[a];
                                                quadrant = windAngle / 90;
                                                windComponents = GetWindComponents(quadrant, windAngle, windSpeed);
                                            }

                                            numeratorWSU += (windComponents[0] / squaredDistance);
                                            numeratorWSV += (windComponents[1] / squaredDistance);
                                            denominator += (1 / squaredDistance);



                                            temperature = CalculateTemperature(height, frmDataInput.DryBulbTemperature[a], reciprocalObukhovLengthLst[a], frictionVelocityLst[a]);
                                            
                                            numeratorT += (temperature / squaredDistance);


                                            pressure = CalculatePressure(ReferenceHeightOfPressure, height, frmDataInput.pressure[a], temperature);
                                            numeratorP += (pressure / squaredDistance);

                                            if (height + Geography.terrainHeights[j, i] < frmDataInput.elevationOfTheStation[a])
                                            {

                                                relativeHumidity = frmDataInput.relativeHumidity[a];

                                            }
                                            else
                                            {

                                                relativeHumidity = CalculateRelativeHumidity(ReferenceHeightOfHumidity, height, frmDataInput.relativeHumidity[a], temperature);

                                            }

                                            numeratorRH += (relativeHumidity / squaredDistance);

                                        }

                                    }
                                }

                                if (ConsiderHeightOfInfluence && effectiveStations.Count == 0)
                                {
                               
                                    effctiveDistance += 1000;
                                    HeightOfInfluence += 100;

                                }
                                else
                                {

                                    U[j, i, k] = numeratorWSU / denominator;

                                    if (double.IsNaN(U[j, i, k])) {
                                    
                                    
                                    }

                                    V[j, i, k] = numeratorWSV / denominator;
                                    temperatureField[j, i, k] = numeratorT / denominator;
                                    arrPressure[j, i, k] = numeratorP / denominator;
                                    arrRelativeHumidity[j, i, k] = numeratorRH / denominator;

                                    effectiveStations.Clear();
                                    break;
                                }



                            }
                        }
                }
            }


                SmoothWindField();


        }


       private void SmoothWindField() {

            for (int k = 0; k < NumberOfMetCellsZ ; k++) {
           for (int j = 1; j < NumberOfMetCellsY-1 ; j++) {

               for (int i = 1; i < NumberOfMetCellsX-1 ; i++)
               {

                   U[j, i, k] = 0.5 * U[j, i, k] + 0.125 * (U[j - 1, i, k] + U[j + 1, i, k] + U[j, i - 1, k] + U[j, i + 1, k]);

                   V[j, i, k] = 0.5 * V[j, i, k] + 0.125 * (V[j - 1, i, k] + V[j + 1, i, k] + V[j, i - 1, k] + V[j, i + 1, k]);

                    }

               }
                            
           }
              
       }


       private void CalculateGridHeights() {

          
           double minimumTerrainHeight = Geography.terrainHeights[0, 0];

           for(int j=0;j<NumberOfMetCellsY;j++){

               for (int i = 0; i < NumberOfMetCellsX; i++) {
                   if (Geography.terrainHeights[j, i] < minimumTerrainHeight) {

                       minimumTerrainHeight = Geography.terrainHeights[j, i];
                   }
                
               }
           
           }


           for (int j = 0; j < NumberOfMetCellsY; j++) {

               for (int i = 0; i < NumberOfMetCellsX; i++) {

                   
                   for (int k = 0; k < NumberOfMetCellsZ + 1; k++) {

                       gridHeights[j, i, k] = Geography.sigmaLevel[k ] * (DomainHeight + minimumTerrainHeight - Geography.terrainHeights[j, i]) / DomainHeight+(Geography.terrainHeights[j,i]-minimumTerrainHeight);

                   }
               
               
               }
            
           
           
           
           }
       
       
       }




       public void RunMeteorology()
        {


           
           

            CalculateMeteorologicalParameters();

            //CreateWindSpeedInExcel(U, V, W, 0);

        

            //CreateWindSpeedInExcel(U, V, W, 0);
            //CalculateTerrainEffects();


            //for (int k = 0; k < Meteorology.numberOfMetCellsZ; k++) {

            //    for (int j = 0; j < Meteorology.numberOfMetCellsY; j++) {

            //        for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++) {

            //            U[j, i, k] = 0;
            //            V[j, i, k] = 1;
            //            W[j, i, k] = 0;
                    
            //        }
                
            //    }
            
            //}

            Base2.MinimizeDivergence();
            //CreateWindSpeedInExcel(U, V, W, 0);
            
   

        
       }



       public static void CreateWindSpeedInExcel(double[, ,] U, double[, ,] V, double[, ,] W, int layer)
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
                   workSheet.Cells[rowCount, i + 1] = U[j, i, layer];


                   workSheet = oExcel.Sheets[2];
                   workSheet.Cells[rowCount, i + 1] = V[j, i, layer];

                   workSheet = oExcel.Sheets[3];
                   workSheet.Cells[rowCount, i + 1] = W[j, i, layer];



               }

               rowCount -= 1;
           }


       }



        

    }
}
