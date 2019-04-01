using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Projections;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;





namespace HPCM_REBUILD
{
    class dispersion
    {

        GMapControl mapViewer;
        public dispersion(GMapControl mapViewer)
        {
            this.mapViewer = mapViewer;  
            
        }

        #region ---Class Level Variables---

        double originX = Geography.UTMX[0, 0] - Meteorology.HorizontalDimensions / 2;
        double originY = Geography.UTMY[0, 0] - Meteorology.HorizontalDimensions / 2;
        double[] releasePointUTM = new double[2];

        //Concentraion sampling time...
        private static double samplingTime = 1250;

        public static double SamplingTime
        {

            get { return samplingTime; }
            set { samplingTime = value; }

        }

        private static double correctionFactorOfCloudRise = 1;

        public static double CorrectionFactorOfCloudRise {

            get { return correctionFactorOfCloudRise; }
            set { correctionFactorOfCloudRise = value; }
        
        }

        private static int numOfHorizontalConcCells = 10;

        public static int NumOfHorizontalConcCells {

            get { return numOfHorizontalConcCells; }
            set { numOfHorizontalConcCells = value; }
                
        }

        private static int concentrationLayerHeight = 1;

        public static int ConcentrationLayerHeight {

            get { return concentrationLayerHeight; }
            set { concentrationLayerHeight = value; }
        
        }



        double[,] concGridPointsUTMX = new double[Meteorology.NumberOfMetCellsY * NumOfHorizontalConcCells, Meteorology.NumberOfMetCellsX * NumOfHorizontalConcCells];
        double[,] concGridPointsUTMY = new double[Meteorology.NumberOfMetCellsY * NumOfHorizontalConcCells, Meteorology.NumberOfMetCellsX * NumOfHorizontalConcCells];
        public static PointLatLng[,] concGridPoints = new PointLatLng[Meteorology.NumberOfMetCellsY * NumOfHorizontalConcCells, Meteorology.NumberOfMetCellsX * NumOfHorizontalConcCells];

        public static double[,] concentrations = new double[Meteorology.NumberOfMetCellsY * NumOfHorizontalConcCells, Meteorology.NumberOfMetCellsX * NumOfHorizontalConcCells];
        double[,] projectedConcGridPointsUTMX = new double[Meteorology.NumberOfMetCellsY * NumOfHorizontalConcCells, Meteorology.NumberOfMetCellsX * NumOfHorizontalConcCells];
        double[,] projectedConcGridPointsUTMY = new double[Meteorology.NumberOfMetCellsY * NumOfHorizontalConcCells, Meteorology.NumberOfMetCellsX * NumOfHorizontalConcCells];
        PointLatLng[,] projectedConcGridPoints = new PointLatLng[Meteorology.NumberOfMetCellsY * NumOfHorizontalConcCells, Meteorology.NumberOfMetCellsX * NumOfHorizontalConcCells];



        private List<double> puffMass = new List<double>();
        private List<double[]> puffSize = new List<double[]>();
        private List<double[]> puffCenterLocation = new List<double[]>();
        private List<double[]> turbulentComponents = new List<double[]>();
        private double releaseDuration = 0;

        private List<double> initialPuffMass = new List<double>(); // keeps track of initial puff mass for chemical effect calculations... 

        private int timeStep = 10;


        static bool considerChemicalEffects =true;

        public static bool ConsiderChemicalEffects
        {

            get { return considerChemicalEffects; }
            set { considerChemicalEffects = value; }

        }


       static double reciprocalDecayTimeScale = 0;

       static bool isStochasticApproach = false;

        public static bool IsStochasticApproach{

            get { return isStochasticApproach; }
            set { isStochasticApproach = value; }
        
        }

        static bool isBayersDispersion = false;

        public static bool IsBayersDispersion
        {

            get { return isBayersDispersion; }
            set { isBayersDispersion = value; }

        }


        static bool isOutOftheGrid = false;



        #endregion

        #region ---Grid Operations---

        private void GetReleasePointUTM() {

            if (frmMainHPCM.shapeOfTheSource == "Point") {

                var xy = new List<double>();
                var z = new List<double>();

                ProjectionInfo source = KnownCoordinateSystems.Geographic.World.WGS1984;
                ProjectionInfo dest = KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone44N;

                xy.Add(frmMainHPCM.sourcePointList.ElementAt(0).Lng);
                xy.Add(frmMainHPCM.sourcePointList.ElementAt(0).Lat);
                z.Add(0);

                var xyA = xy.ToArray();
                var zA = z.ToArray();
                Reproject.ReprojectPoints(xyA, zA, source, dest, 0, z.Count);

                releasePointUTM[0] = xyA[0];
                releasePointUTM[1] = xyA[1];
            
            }
        
        }

        private void CalculateConcentrationGridPoints() {

            double delta = Meteorology.HorizontalDimensions / NumOfHorizontalConcCells;


            double distY = originY + delta / 2;

            for (int j = 0; j < Meteorology.NumberOfMetCellsY * NumOfHorizontalConcCells; j++)
            {

                double distX = originX + delta / 2;

                for (int i = 0; i < Meteorology.NumberOfMetCellsX * NumOfHorizontalConcCells; i++)
                {

                    concGridPointsUTMX[j, i] = distX;
                    concGridPointsUTMY[j, i] = distY;

                    distX += delta;
                }
                distY += delta;
            }





            ProjectionInfo source = KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone44N;
            ProjectionInfo dest = KnownCoordinateSystems.Geographic.World.WGS1984;

            for (int j = 0; j < Meteorology.NumberOfMetCellsY * NumOfHorizontalConcCells; j++) {

                for (int i = 0; i < Meteorology.NumberOfMetCellsX * NumOfHorizontalConcCells; i++) {

                    var xy = new List<double>();
                    var z = new List<double>();

                    xy.Add( concGridPointsUTMX[j, i]);
                    xy.Add(concGridPointsUTMY[j, i]);
                    z.Add(0);

                    var xyA = xy.ToArray();
                    var zA = z.ToArray();
                    Reproject.ReprojectPoints(xyA, zA, source, dest, 0, z.Count);

                    concGridPoints[j, i] = new PointLatLng(xyA[1], xyA[0]);

                }
            
            
            }

        
        }



        #endregion

        public void RunDispersion()
        {

            GetReleasePointUTM();

            CalculateConcentrationGridPoints();

            int releasePointCellNumX = (int)Math.Truncate((releasePointUTM[0] - originX) / Meteorology.HorizontalDimensions);
            int releasePointCellNumY = (int)Math.Truncate((releasePointUTM[1] - originY) / Meteorology.HorizontalDimensions);

            if (frmMainHPCM.shapeOfTheSource == "Point" && frmDataInput.typeOfTheSource == "Instantaneous Source")
            {


                puffMass.Insert(0, frmDataInput.agentWeight);
                initialPuffMass.Insert(0, frmDataInput.agentWeight);

                double meanWindSpeed = 0;
                double meanTemperature = 0;

                for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
                {

                    meanWindSpeed += Math.Sqrt(Math.Pow(Meteorology.U[releasePointCellNumY, releasePointCellNumX, k], 2) + Math.Pow(Meteorology.V[releasePointCellNumY, releasePointCellNumX, k], 2));
                    meanTemperature += Meteorology.temperatureField[releasePointCellNumY, releasePointCellNumX, k];
                }
                meanWindSpeed = meanWindSpeed / Meteorology.NumberOfMetCellsZ;
                meanTemperature = meanTemperature / Meteorology.NumberOfMetCellsZ;

                CalculateZhangsInstantaneousCloudRise(frmDataInput.agentWeight, frmDataInput.weightOfExplosive, frmDataInput.heatOfDetonation, Meteorology.arrLapseRate[releasePointCellNumY, releasePointCellNumX], meanWindSpeed, meanTemperature, CorrectionFactorOfCloudRise);

                //puffSize.Insert(0, new double[] { 7.5, 7.5, 4 });

                //puffCenterLocation.Insert(0, new double[] { releasePointUTM[0] , releasePointUTM[1] , 6 });

                turbulentComponents.Add(new double[3] { 0, 0, 0 });

            }
            else if (frmMainHPCM.shapeOfTheSource == "Point" && frmDataInput.typeOfTheSource == "Semi-Continuous Source")
            {

                releaseDuration = Convert.ToDouble(frmDataInput.agentWeight/frmDataInput.releaseRate);


            }


            char stabilityClass = Meteorology.arrStabilityClass[releasePointCellNumY, releasePointCellNumX];
            if (stabilityClass == 'A' | stabilityClass == 'B' | stabilityClass == 'C' | stabilityClass == 'D')
            {

                GetbeetaR(stabilityClass);

            }


            //Calculates reciprocal time scales...

            if (ConsiderChemicalEffects == true && (frmDataInput.hour > Meteorology.SunRiseTime && frmDataInput.hour < Meteorology.SunSetTime))
            {

                var timeFac = Math.PI*(Convert.ToDouble(frmDataInput.hour) - Convert.ToDouble(Meteorology.SunRiseTime)) / (Convert.ToDouble(Meteorology.SunSetTime) - Convert.ToDouble(Meteorology.SunRiseTime));

                if (frmDataInput.chemicalAgent == "GB") {

                    reciprocalDecayTimeScale = 2 * Math.PI *Math.Pow(10,-5)* Math.Sin(timeFac);
                }
                else if (frmDataInput.chemicalAgent == "GA") {

                    reciprocalDecayTimeScale = 2.45 * Math.PI * Math.Pow(10, -5) * Math.Sin(timeFac);

                }
                else if (frmDataInput.chemicalAgent == "HD") {

                    reciprocalDecayTimeScale = 3.91 * Math.PI * Math.Pow(10, -6) * Math.Sin(timeFac);
                
                }
                        
            }



            int timeIntervals = Convert.ToInt32(SamplingTime / timeStep);
            int currentTime=1;

            for (int ti = 0; ti < timeIntervals; ti++) {

                if (currentTime < releaseDuration) {

                    double releaseRate=frmDataInput.agentWeight/releaseDuration;

                    CalculateInitialParameters(releasePointCellNumX, releasePointCellNumY, releaseRate);

                    puffMass.Add(releaseRate * timeStep);
                    initialPuffMass.Add(releaseRate * timeStep);

                    turbulentComponents.Add(new double[3] { 0, 0, 0 });

                    if (stabilityClass == 'A' | stabilityClass == 'B' | stabilityClass == 'C' | stabilityClass == 'D')
                    {

                        for (int p = 0; p < puffMass.Count; p++)
                        {
                            var location = puffCenterLocation.ElementAt(p);
                            var cell = GetCell(location[0], location[1], location[2],Meteorology.HorizontalDimensions);

                            var windU = Meteorology.U[cell[1], cell[0], cell[2]];
                            var windV = Meteorology.V[cell[1], cell[0], cell[2]];

                            var horizontalWind = Math.Sqrt(Math.Pow(windU, 2) + Math.Pow(windV, 2));

                            CalculateZpl(currentTime, p,horizontalWind);
                        }

                    }





                
                }


                for (int p = 0; p < puffMass.Count; p++)
                
                {

                    var location = puffCenterLocation.ElementAt(p);

                    if (double.IsNaN(location[0]) || double.IsNaN(location[2]))
                    {


                        var tur = turbulentComponents;
                        
                    
                    }


                    var cell = GetCell(location[0], location[1], location[2],Meteorology.HorizontalDimensions);


                    char currentStabilityClass = Meteorology.arrStabilityClass[cell[1], cell[0]];
                    double mixingHeight = Meteorology.arrMixingHeight[cell[1], cell[0]];
                    double surfaceFrictionVelocity = Meteorology.arrFrictionVelocity[cell[1], cell[0]];
                    double reciprocalObukhovLength = 1 / Meteorology.arrMoninObukhovLength[cell[1], cell[0]];
                    double convectiveVelocityScale = Meteorology.arrConvectiveVelocityScale[cell[1], cell[0]];



                    //Calculate the wind angle with respect to x coordinate...
                    var windU = Meteorology.U[cell[1], cell[0], cell[2]];
                    var windV = Meteorology.V[cell[1], cell[0], cell[2]];
                    double windW = Meteorology.W[cell[1], cell[0], cell[2]];
                    var meanHorizontalWind = Math.Sqrt(Math.Pow(windU, 2) + Math.Pow(windV, 2));
                    double theeta = Math.Acos(windV / meanHorizontalWind);


                    double[] velocityVarinces = new double[3];

                    velocityVarinces[0] = CalculateVelocityVariance(currentStabilityClass, 'u', location[2], mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale, reciprocalObukhovLength);
                    velocityVarinces[1] = CalculateVelocityVariance(currentStabilityClass, 'v', location[2], mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale, reciprocalObukhovLength);
                    velocityVarinces[2] = CalculateVelocityVariance(currentStabilityClass, 'w', location[2], mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale, reciprocalObukhovLength);

                    double[] lagrangianTimes = new double[3];
                    lagrangianTimes[0] = CalculateLagrangianTimeScale(currentStabilityClass, 'u', location[2], mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale, reciprocalObukhovLength);
                    lagrangianTimes[1] = CalculateLagrangianTimeScale(currentStabilityClass, 'v', location[2], mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale, reciprocalObukhovLength);
                    lagrangianTimes[2] = CalculateLagrangianTimeScale(currentStabilityClass, 'w', location[2], mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale, reciprocalObukhovLength);


                    var currentPuffSize = puffSize.ElementAt(p);

                    //var puffVariance = CalculatePuffSize(timeStep, currentTime, velocityVarinces, lagrangianTimes, currentPuffSize);

                    var puffVariance = CalculateModifiedPuffSize(currentTime, timeStep, velocityVarinces, lagrangianTimes, currentPuffSize);

                    currentPuffSize[0] = Math.Sqrt(puffVariance[0]);
                    currentPuffSize[1] = Math.Sqrt(puffVariance[1]);
                    currentPuffSize[2] = Math.Sqrt(puffVariance[2]);


                    //if (currentTime < releaseDuration)
                    //{

                        var turbulance = turbulentComponents.ElementAt(p);

                        double gradVarianceW = 0;

                        gradVarianceW = CalculateGraidentOfVariance(currentStabilityClass, location[2], mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale, reciprocalObukhovLength);

                        double[] newTurbulentComponents = new double[3];
                        newTurbulentComponents [0]= CalculateHorizontalTurbulance(turbulance[0], timeStep, lagrangianTimes[0], velocityVarinces[0]);
                       newTurbulentComponents [1] = CalculateHorizontalTurbulance(turbulance[1], timeStep, lagrangianTimes[1], velocityVarinces[1]);
                       newTurbulentComponents[2] = CalculateVerticalTurbulance(turbulance[2], timeStep, lagrangianTimes[2], velocityVarinces[2], gradVarianceW);

                       newTurbulentComponents[2] = CalculateHorizontalTurbulance(turbulance[2], timeStep, lagrangianTimes[2], velocityVarinces[2]);

                       var ceilingHeight = Math.Min(Meteorology.DomainHeight - Meteorology.gridHeights[cell[1], cell[0], 0], mixingHeight);


                       if ((location[2] < 6) && (newTurbulentComponents[2] < 0))
                       {

                           newTurbulentComponents[2] *= -1;
                       }
                       else if ((location[2] > (ceilingHeight-6)) && (newTurbulentComponents[2] > 0))
                       {

                           newTurbulentComponents[2] *= -1;
                       }

                        double[] newLocation = new double[3];
                        newLocation[0] = CalculateParticlePosition(location[0], windU, newTurbulentComponents[0], timeStep);
                        newLocation[1] = CalculateParticlePosition(location[1], windV, newTurbulentComponents[1], timeStep);
                        newLocation[2] = location[2];
                        newLocation[2] = CalculateParticlePosition(location[2], windW, newTurbulentComponents[2], timeStep);

                        if (newLocation[2] < 5)
                        {

                            newLocation[2] = 5;

                        }
                        else if (location[2] > (ceilingHeight-5)) {

                            newLocation[2] = ceilingHeight-5;
                        
                        }



                        if (newLocation[0] < originX) 
                        {

                            throw new System.Exception(" Puff center missed the grid from the left margin...");


                        }
                        else if (newLocation[1] < originY) 
                        {

                            throw new System.Exception(" Puff center missed the grid from the bottom margin...");
                        }
                        else if (newLocation[0] > Geography.UTMX[Meteorology.NumberOfMetCellsY - 1, Meteorology.NumberOfMetCellsX - 1])
                        {

                            throw new System.Exception(" Puff center missed the grid from the right margin...");
                        }
                        else if (newLocation[1] > Geography.UTMY[Meteorology.NumberOfMetCellsY - 1, Meteorology.NumberOfMetCellsX - 1])
                        {

                            throw new System.Exception(" Puff center missed the grid from the top margin...");
                        }

                        puffCenterLocation.Insert(p, newLocation);
                        puffCenterLocation.RemoveAt(p + 1);
                        turbulentComponents.Insert(p, newTurbulentComponents);
                        turbulentComponents.RemoveAt(p + 1);
                    //}


                }


                CalculateResidualPuffMass(timeStep);

                CalculateConcentration();

                //if (ti == 45 || ti == 135 || ti == 225 || ti == 315)
                //{

                //    WriteConcValuesIntoExcel();

                //}


                currentTime += timeStep;
            }


            
            //Calculates averaged dosage in pptv... 

            for (int j = 0; j < Meteorology.NumberOfMetCellsY * NumOfHorizontalConcCells; j++)
            {

                for (int i = 0; i < Meteorology.NumberOfMetCellsX * NumOfHorizontalConcCells; i++)
                {

                    concentrations[j, i] = concentrations[j, i] * Math.Pow(10, 12) / timeIntervals;

                }
            }


            //WriteConcValuesIntoExcel();

            //CreateConGridInExcel();

        }


        private void CalculatePuffMass()
        {

            if (frmMainHPCM.shapeOfTheSource == "Point" && frmDataInput.typeOfTheSource == "Instantaneous Source")
            {

                puffMass.Insert(0,frmDataInput.agentWeight);

            }

        
        }


        //Calculates residual puff mass after chemical processes

        private void CalculateResidualPuffMass(int timeStep) {


            for (int p = 0; p < initialPuffMass.Count; p++) {

                puffMass[p] = puffMass[p] - reciprocalDecayTimeScale * initialPuffMass[0]*timeStep;
            
            }


        
        }




        #region ---Cloud Rise Models---

        //Calculates cloud rise from explosive sources, returns equilibrium height,radius and the distance in an array form...
        private void CalculateZhangsInstantaneousCloudRise(double agentMass, double massOfTheExplosive, double heatOfDetonation, double lapseRate, double windSpeed, double ambientTemperature, double correctionFactor)
        {

            double heatReleased = massOfTheExplosive * heatOfDetonation;
            double densityOfAir = Meteorology.CalculateAtmosphericAirDensity(ambientTemperature);
            double initialFireBallRadius = correctionFactor * Math.Pow((0.239 * heatReleased / (densityOfAir * 1.006 * ambientTemperature)), 0.33333);
            double fractionAirborne = (2.783 * Math.Pow(agentMass / (heatReleased / 4652), 0.3617) * (heatReleased / 4652)) / agentMass;
            double initialFireBallDensity = (massOfTheExplosive + agentMass ) / ((4 * Math.PI / 3) * Math.Pow(initialFireBallRadius, 3));


            var bVF = (lapseRate + 9.76 * Math.Pow(10, -3));

            if (bVF < 0) {

                bVF = 9.76 * Math.Pow(10, -3);
            }

            double bruntVaisailaFrequency = Math.Pow((9.81 / (ambientTemperature)) *bVF , 0.5);
            double reducedGravity = 9.81 * (densityOfAir - initialFireBallDensity) / densityOfAir;
            double finalPuffRise = 0;
            double finalPuffRadius = 0;
            double finalPuffDistance = 0;

            if ((windSpeed * bruntVaisailaFrequency / reducedGravity) < 1 || (windSpeed * bruntVaisailaFrequency / reducedGravity) == 1)
            {

                finalPuffRise = Math.Pow((3 * (4 * Math.PI / 3) * Math.Pow(initialFireBallRadius, 3) * reducedGravity) / (Math.PI * 0.022 * Math.Pow(bruntVaisailaFrequency, 2)), 0.25);

                finalPuffRise = finalPuffRise * Math.Exp(-1.2 * Math.Pow((windSpeed * bruntVaisailaFrequency / reducedGravity), 0.5));
                finalPuffRadius = finalPuffRise * 0.28;
            }
            else
            {
                finalPuffRise = (reducedGravity / 5) * Math.Pow((4.271 * 3 * (4 * Math.PI / 3) * Math.Pow(initialFireBallRadius, 3) * Math.Pow(Math.PI, 4)) / (Math.Pow(windSpeed, 3) * Math.Pow(bruntVaisailaFrequency, 5)), 0.25);
                finalPuffDistance = Math.Pow((5 * finalPuffRise * Math.Pow(182.216 * Math.Pow(initialFireBallRadius, 3), -0.25) * Math.Pow(windSpeed, 2) / reducedGravity), 0.8);
                finalPuffRadius = Math.Pow(1.12 * Math.Pow(initialFireBallRadius, 3), 0.25) * Math.Pow(finalPuffDistance, 0.25);

            }
            double[] instantaneousCloudRise = new double[] { finalPuffRise, finalPuffRadius, finalPuffDistance };
            puffCenterLocation.Insert(0, new double[] { releasePointUTM[0] + finalPuffDistance, releasePointUTM[1] + finalPuffDistance, finalPuffRise });
            puffSize.Insert(0, new double[] { finalPuffRadius, finalPuffRadius, finalPuffRadius });


            if(double.IsNaN(finalPuffRise))
            {
            
            
            }

        }

        #endregion


        #region --- Plume Rise Models---

        static List<double> M = new List<double>();
        static List<double> F = new List<double>();
        static List<double> r = new List<double>();
        static List<double> Z = new List<double>();

        double sourceTemperature = 340;
        double sourceRadius = 0.5;

        
        float beetaR = 0.5f;

        //Sets the value of beetaR for a given stability class....
        private void GetbeetaR(char stabilityClass)
        {

            switch (stabilityClass)
            {
                case 'A':
                    beetaR = 0.75f;
                    break;

                case 'B':
                    beetaR = 0.7f;
                    break;

                case 'C':
                    beetaR = 0.65f;
                    break;

                case 'D':
                    beetaR = 0.6f;
                    break;

                case 'E':
                    beetaR = 0.5f;
                    break;

                case 'F':
                    beetaR = 0.5f;
                    break;

                case 'G':
                    beetaR = 0.5f;
                    break;

                default:
                    beetaR = 0.5f;
                    break;
            }

        }

        //calculates initial plume parameters and stores in lists...
        private void CalculateInitialParameters(int releasePointCellNumX,int releasePointCellNumY,double releaseRate)
        {

            double meanWindSpeed = 0;
            double meanTemperature = 300;

            for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
            {

                meanWindSpeed += Math.Sqrt(Math.Pow(Meteorology.U[releasePointCellNumY, releasePointCellNumX, k], 2) + Math.Pow(Meteorology.V[releasePointCellNumY, releasePointCellNumX, k], 2));
                //meanTemperature += Meteorology.temperatureField[releasePointCellNumY, releasePointCellNumX, k];
            }
            meanWindSpeed = meanWindSpeed / Meteorology.NumberOfMetCellsZ;
            //meanTemperature = meanTemperature / Meteorology.NumberOfMetCellsZ;


            double M0 = Math.Pow(releaseRate, 2) * Math.Pow(sourceRadius, 2) * meanTemperature / sourceTemperature;
            double F0 = 9.81 * releaseRate * Math.Pow(sourceRadius, 2) * (1 - (meanTemperature / sourceTemperature));
            double r0 = sourceRadius * Math.Sqrt(meanTemperature * releaseRate / (sourceTemperature * meanWindSpeed));
            double Z0 = r0 / beetaR;

            M.Add(M0);
            F.Add(F0);
            r.Add(r0);

            Z.Add(Z0);

            puffCenterLocation.Add(new double[3]{releasePointUTM[0], releasePointUTM[1], Z0});
            puffSize.Add(new double[3] { r0, r0, r0 });

        }


        //Calculates the rise height of a single plume segment and updates Z array...
        private void CalculateZpl(double currentTime, int puffNumber, double horizontalWindVelocity)
        {

            double Zpl = Math.Pow(Math.Pow(Z.ElementAt(puffNumber), 3) + 3 * (M.ElementAt(puffNumber) * timeStep + F.ElementAt(puffNumber) * 0.5 * (Math.Pow((currentTime + timeStep), 2) - Math.Pow(currentTime, 2))) / (horizontalWindVelocity * Math.Pow(beetaR, 2)), 0.33333);

            Z.Insert(puffNumber, Zpl);

            double[] currentPosition = puffCenterLocation.ElementAt(puffNumber);
            puffCenterLocation.Insert(puffNumber, new double[3] { currentPosition[0], currentPosition[1], Zpl });

        }


        #endregion


        //returns the grid values i,j,k for a given location... 
         int[] GetCell(double UTMX, double UTMY, double height,double cellWidth) {


            if (double.IsNaN(UTMX)) 
            {
            
            
            
            }

            int[] arrCellNum = new int[3];

            arrCellNum[0] = (int)Math.Truncate((UTMX - originX) / cellWidth);
            arrCellNum[1] = (int)Math.Truncate((UTMY - originY) / cellWidth);

            for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++) {

                if (height <= Meteorology.gridHeights[arrCellNum[1], arrCellNum[0], k + 1] && height >= Meteorology.gridHeights[arrCellNum[1], arrCellNum[0], k]) {

                    arrCellNum[2] = k;

                    break;
                }
            
            }

            return arrCellNum;
        }




        #region ---Concentration Calculations---



        private void CalculateConcentration() {


            for (int j = 0; j < Meteorology.NumberOfMetCellsY * NumOfHorizontalConcCells; j++)
            {

                for (int i = 0; i < Meteorology.NumberOfMetCellsX * NumOfHorizontalConcCells; i++)
                {

                    double concContribution = 0;
                    double instantaneousConc = 0;

                    double[] location = new double[] { concGridPointsUTMX[j, i], concGridPointsUTMY[j, i], ConcentrationLayerHeight };

                    int metCellX = Convert.ToInt32(Math.Truncate((double)(i / NumOfHorizontalConcCells)));
                    int metCellY = Convert.ToInt32(Math.Truncate((double)(j / NumOfHorizontalConcCells)));

                    int[] locationCell = GetCell(location[0], location[1], location[2], Meteorology.HorizontalDimensions);

                    for (int p = 0; p < puffMass.Count; p++)
                    {

                        try
                        {
                            var cell = GetCell((puffCenterLocation[p])[0], (puffCenterLocation[p])[1], (puffCenterLocation[p])[2], Meteorology.HorizontalDimensions);

                            //Calculate the wind angle with respect to x coordinate...
                            var windU = Meteorology.U[cell[1], cell[0], cell[2]];
                            var windV = Meteorology.V[cell[1], cell[0], cell[2]];
                            double windW = Meteorology.W[cell[1], cell[0], cell[2]];
                            var meanHorizontalWind = Math.Sqrt(Math.Pow(windU, 2) + Math.Pow(windV, 2));
                            double theeta = Math.Acos(windV / meanHorizontalWind);

                            var sigmaX = puffSize[p][0];
                            var sigmaY = puffSize[p][1];

                            var sigmaXPrime = sigmaX * sigmaY / Math.Sqrt(Math.Pow(sigmaY * Math.Cos(2 * Math.PI - theeta), 2) + Math.Pow(sigmaX * Math.Sin(2 * Math.PI - theeta), 2));
                            var sigmaYPrime = sigmaX * sigmaY / Math.Sqrt(Math.Pow(sigmaY * Math.Sin(theeta), 2) + Math.Pow(sigmaX * Math.Cos(theeta), 2));

                            var sigmaXYZ = new double[3] { sigmaXPrime, sigmaYPrime, puffSize[p][2] };
                            concContribution = CalculatePuffContribution(puffMass[p], sigmaXYZ, puffCenterLocation[p], location, Geography.terrainHeights[locationCell[1], locationCell[0]]);
                            instantaneousConc += 0.063494 * concContribution;


                        }
                        catch
                        {

                        }

                    }

                    concentrations[j,i] += instantaneousConc;


                }

            }

        }



        private double CalculatePuffContribution(double q, double[] sigmaXYZ, double[] puffCenterLocation, double[] location, double groundHeight)
        {

            int puffCenterUTMCellX = (int)Math.Truncate((puffCenterLocation[0] - originX) / Meteorology.HorizontalDimensions);
            int puffCenterUTMCellY = (int)Math.Truncate((puffCenterLocation[1] - originY) / Meteorology.HorizontalDimensions);


            double concContribution = (q / (sigmaXYZ[0] * sigmaXYZ[1] * sigmaXYZ[2])) * Math.Exp(-0.5 * Math.Pow((puffCenterLocation[0] - location[0]) / sigmaXYZ[0], 2)) * Math.Exp(-0.5 * Math.Pow((puffCenterLocation[1] - location[1]) / sigmaXYZ[1], 2)) * (Math.Exp(-0.5 * Math.Pow((puffCenterLocation[2] - location[2]) / sigmaXYZ[2], 2)) + Math.Exp(-0.5 * Math.Pow((puffCenterLocation[2]+ location[2]) / sigmaXYZ[2], 2)));

            return concContribution;
        }

        #endregion

        #region ---Turbulance Statistics---


        //Calculates velocity variances using hanna's turbulance parameterization scheme...


        private double CalculateHannasVelocityVariance(char stabilityClass, char component, double height, double mixingHeight, double surfaceFrictionVelocity, double convectiveVelocityScale, double reciprocalObukhovLength)
        {

            double velocityVariance = 0;

            if (stabilityClass == 'A' || stabilityClass == 'B' || stabilityClass == 'C')
            {


                switch (component)
                {
                    case 'u':
                        velocityVariance = surfaceFrictionVelocity * Math.Pow((12 + (0.5 * mixingHeight * Math.Abs(reciprocalObukhovLength))), 0.333);
                        break;

                    case 'v':
                        velocityVariance = surfaceFrictionVelocity * Math.Pow((12 + (0.5 * mixingHeight * Math.Abs(reciprocalObukhovLength))), 0.333);
                        break;

                    case 'w':
            

                        if ((height / mixingHeight) < 0.03) {

                            velocityVariance = convectiveVelocityScale * 0.96 * Math.Pow(((3 * height / mixingHeight) - (1 / (reciprocalObukhovLength * mixingHeight))), 0.333);

                        }
                        else if ((height / mixingHeight) < 0.4 && (height / mixingHeight) > 0.03) {

                            var velocityVariance1 = convectiveVelocityScale * 0.96 * Math.Pow(((3 * height / mixingHeight) - (1 / (reciprocalObukhovLength * mixingHeight))), 0.333);

                            var velocityVariance2 = convectiveVelocityScale * 0.763 * Math.Pow((height / mixingHeight), 0.175);

                            velocityVariance = Math.Min(velocityVariance1, velocityVariance2);


                        }
                        else if ((height / mixingHeight) < 0.96 && (height / mixingHeight) > 0.4)
                        {

                            velocityVariance = convectiveVelocityScale * 0.722 * Math.Pow((1 - (height / mixingHeight)), 0.207);

                        }
                        else if (0.96 < (height / mixingHeight) && (height / mixingHeight)<1)
                        {

                            velocityVariance = convectiveVelocityScale * 0.37;
                        
                        }



                        break;

                }

            
            }
            else if (stabilityClass == 'D') {

                var coriolisParameter = Math.Pow(10, -4);

                switch (component)
                {
                    case 'u':
                        velocityVariance = 2 * surfaceFrictionVelocity *Math.Exp((-3*coriolisParameter*height)/surfaceFrictionVelocity);
                        break;

                    case 'v':
                        velocityVariance = 2 * surfaceFrictionVelocity * Math.Exp((-3 * coriolisParameter * height) / surfaceFrictionVelocity);
                        break;

                    case 'w':


                        velocityVariance = 1.3 * surfaceFrictionVelocity * Math.Exp((-2 * coriolisParameter * height) / surfaceFrictionVelocity);

                        break;

                }
            
            }



            else if (stabilityClass == 'E' || stabilityClass == 'F'|| stabilityClass=='G')
            {


                switch (component)
                {
                    case 'u':
                        velocityVariance = 2 * surfaceFrictionVelocity * (1 - (height / mixingHeight));
                        break;

                    case 'v':
                        velocityVariance = 2 * surfaceFrictionVelocity * (1 - (height / mixingHeight));
                        break;

                    case 'w':


                        velocityVariance = 1.3 * surfaceFrictionVelocity * (1 - (height / mixingHeight));

                        break;

                }


            }


            velocityVariance = Math.Pow(velocityVariance,2);

            return velocityVariance;
        
        
        }


        private double CalculateHannasLagrangianTime(char stabilityClass, char component, double height, double mixingHeight, double surfaceFrictionVelocity, double convectiveVelocityScale, double reciprocalObukhovLength, double velocityVariance, double roughnessLength) {

            double lagrangianTime = 0;

            if (stabilityClass == 'A' || stabilityClass == 'B' || stabilityClass == 'C' )
            {


                switch (component)
                {
                    case 'u':
                        lagrangianTime = 0.15 * height / Math.Sqrt(velocityVariance);
                        break;

                    case 'v':
                        lagrangianTime = 0.15 * height / Math.Sqrt(velocityVariance);
                        break;

                    case 'w':


                        if ((height  < 0.1*mixingHeight) &&( (height-roughnessLength)>(-1/reciprocalObukhovLength)))
                        {

                            lagrangianTime = 0.1 * height / (velocityVariance * (0.55 + 0.38 * (height - roughnessLength) * reciprocalObukhovLength));

                        }
                        else if ((height < 0.1 * mixingHeight) &&( (height - roughnessLength) < (-1 / reciprocalObukhovLength)))
                        {

                            lagrangianTime = 0.59 * height / velocityVariance;


                        }

                        else
                        {

                            lagrangianTime = (0.15 * height / velocityVariance)* (1-Math.Exp(-5*height/mixingHeight));

                        }


                        if (lagrangianTime < 0)
                        
                        {
                        
                        
                        }

                        break;

                }


            }
            else if (stabilityClass == 'D')
            {
                double coriolisParameter=Math.Pow(10,-4);

                var lagrangianTime3 = (0.375 * height / velocityVariance) / (1 + (15 * coriolisParameter * height / surfaceFrictionVelocity));

                switch (component)
                {
                    case 'u':
                        lagrangianTime = 3 * lagrangianTime3;
                        break;

                    case 'v':
                        lagrangianTime = 3 * lagrangianTime3;
                        break;

                    case 'w':


                        lagrangianTime =  lagrangianTime3;

                        break;

                }

            }



            else if (stabilityClass == 'E' || stabilityClass == 'F' || stabilityClass == 'G')
            {

                var lagrangianTime3 = (0.1 * height / velocityVariance) * Math.Pow((height / mixingHeight), 0.8);
                switch (component)
                {
                    case 'u':
                        lagrangianTime = 7.5* lagrangianTime3;
                        break;

                    case 'v':
                        lagrangianTime = 7.5 * lagrangianTime3;
                        break;

                    case 'w':


                        lagrangianTime = lagrangianTime3;

                        break;

                }


            }

            if (double.IsNaN(lagrangianTime))
            
            {
            
            
            
            
            }


            return lagrangianTime;
        }





        //Calculates velocity variances for each wind component for any stability class...
        private double CalculateVelocityVariance(char stabilityClass, char component, double height, double mixingHeight, double surfaceFrictionVelocity,double convectiveVelocityScale, double reciprocalObukhovLength)
        {
            double velocityVariance = 0;

            if (stabilityClass == 'A' || stabilityClass == 'B')
            {

                double lambda = 0;
                double c = 0;

                switch (component)
                {
                    case 'u':
                        c = 0.2578;
                        lambda = 1.5 * mixingHeight;
                        break;

                    case 'v':
                        c = 0.3438;
                        lambda = 1.5 * mixingHeight;
                        break;

                    case 'w':
                        c = 0.3438;
                        lambda = 1.8 * mixingHeight * (1 - Math.Exp(-4 * height / mixingHeight) - 0.0003 * Math.Exp(8 * height / mixingHeight));
                        break;

                }

                velocityVariance = 0.795 * c * Math.Pow(height / mixingHeight, 0.66666) * Math.Pow(convectiveVelocityScale, 2) / Math.Pow((height /Math.Abs( lambda)),0.667);


            }


            if (stabilityClass == 'C')
            {

                double lambda = 0;
                double a = 0;
                double c = 0;
                double fm = 0;
                double frictionVelocity = surfaceFrictionVelocity * Math.Pow(1 - (height / mixingHeight), 0.85);

                switch (component)
                {
                    case 'u':
                        c = 0.2578;
                        a = 3889;
                        fm = 0.045;
                        lambda = 1.5 * mixingHeight;
                        break;

                    case 'v':
                        c = 0.3438;
                        a = 1094;
                        fm = 0.16;
                        lambda = 1.5 * mixingHeight;
                        break;

                    case 'w':
                        c = 0.3438;
                        a = 500;
                        fm = 0.33;
                        lambda = Math.Abs(1.8 * mixingHeight * (1 - Math.Exp(-4 * height / mixingHeight) - 0.0003 * Math.Exp(8 * height / mixingHeight)));
                        break;
                }

                if (height < 0.1 * mixingHeight)
                {
                    fm = fm * (1 + (0.03 * a * height / mixingHeight));

                }
                else
                {
                    fm = fm * (1 + (0.03 * Math.Pow(10, -4) * a * height / surfaceFrictionVelocity));
                }

                velocityVariance =( 0.795 * c * Math.Pow(height / mixingHeight, 0.66666) * Math.Pow(convectiveVelocityScale, 2) /Math.Pow( (height / lambda),0.667) )+( 2.69 * c * Math.Pow(frictionVelocity, 2) / Math.Pow(fm, 0.66666));


            }

            if (stabilityClass == 'D')
            {

                double c = 0;
                double a = 0;
                double fm = 0;
                double frictionVelocity = surfaceFrictionVelocity * Math.Pow(1 - (height / mixingHeight), 0.85);


                switch (component)
                {
                    case 'u':
                        c = 0.2578;
                        a = 3889;
                        fm = 0.045;
                        break;

                    case 'v':
                        c = 0.3438;
                        a = 1094;
                        fm = 0.16;
                        break;

                    case 'w':
                        c = 0.3438;
                        a = 500;
                        fm = 0.33;
                        break;

                }

                if (height < 0.1 * mixingHeight)
                {

                    fm = fm * (1 + (0.03 * a * height / mixingHeight));
                }
                else
                {
                    fm = fm * (1 + (0.03 * Math.Pow(10, -4) * a * height / surfaceFrictionVelocity));
                }

                velocityVariance = 2.69 * c * Math.Pow(frictionVelocity, 2) / Math.Pow(fm, 0.66666);

            }

            if (stabilityClass == 'E' || stabilityClass == 'F' || stabilityClass == 'G')
            {

                double c = 0;
                double a = 0;
                double fm = 0;
                double frictionVelocity = surfaceFrictionVelocity * Math.Pow(1 - (height / mixingHeight), 0.75);
                double lambda = (1 / reciprocalObukhovLength) * Math.Pow((1 - (height / mixingHeight)), 1.25);
                double phi = 1.25 * (1 + 3.7 * (height / lambda));

                switch (component)
                {
                    case 'u':
                        c = 0.2578;
                        a = 3889;
                        fm = 0.045;
                        break;

                    case 'v':
                        c = 0.3438;
                        a = 1094;
                        fm = 0.16;
                        break;

                    case 'w':
                        c = 0.3438;
                        a = 500;
                        fm = 0.33;
                        break;
                }


                if (height < 0.1 * mixingHeight)
                {
                    fm = fm * (1 + (0.03 * a * height / mixingHeight) + 3.7 * height / lambda);
                }
                else
                {
                    fm = fm * (1 + (0.03 * a * Math.Pow(10, -4) * height / surfaceFrictionVelocity) + 3.7 * height / lambda);
                }
                velocityVariance = 2.32 * c * Math.Pow(frictionVelocity, 2) * Math.Pow(phi, 0.66666) / fm;

            }


            return velocityVariance;
        }


        //Calculates gradients of vertical velocity variance...

        #region --- Gradients of vertical velocity variances---

        private double CalculateGraidentOfVariance(char stabilityClass, double height, double mixingHeight, double surfaceFrictionVelocity, double convectiveVelocityScale, double reciprocalObukhovLength)
        {
            double gradVariance = 0;

            if (stabilityClass == 'A' || stabilityClass == 'B') {

                gradVariance = CalculateGradOfM_Turbulance(stabilityClass, height, mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale);

            }
            else if(stabilityClass=='C'){


                gradVariance = CalculateGradOfM_Turbulance(stabilityClass, height, mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale) + CalculateGradOfShear_Turbulance_Neutral(stabilityClass, height, mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale);
            
            }
            else if (stabilityClass == 'D')
            {

                gradVariance = CalculateGradOfShear_Turbulance_Neutral(stabilityClass, height, mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale);

            }
            else {

                gradVariance = CalculateGradOfShear_Turbulance_Stable(stabilityClass, height, mixingHeight, surfaceFrictionVelocity, convectiveVelocityScale, reciprocalObukhovLength);
            
            }



            return gradVariance;
        }

        private double CalculateGradOfM_Turbulance(char stabilityClass, double height, double mixingHeight, double surfaceFrictionVelocity, double convectiveVelocityScale)
        {
            double gradMechVariance = 0;
            double Bw = 1.8 * (1 - Math.Exp(-4 * height / mixingHeight) - 0.0003 * Math.Exp(8 * height / mixingHeight));
            double B = height / (mixingHeight * Bw);

            double A = Math.Pow((height / mixingHeight), 0.667);
            double gradA = (2 / (3 * mixingHeight)) * Math.Pow((height / mixingHeight), -0.333);
            double gradBw = (1.8 / mixingHeight) * (4 * Math.Exp(-4 * height / mixingHeight) - 0.0024 * Math.Exp(8 * height / mixingHeight));
            double gradB = (Bw - height * gradBw) / (mixingHeight * Math.Pow(Bw, 2));

            return gradMechVariance = 0.2733 * Math.Pow(convectiveVelocityScale, 2) * (B * gradA - A * gradB) / Math.Pow(B, 2);
        }

        private double CalculateGradOfShear_Turbulance_Neutral(char stabilityClass, double height, double mixingHeight, double surfaceFrictionVelocity, double convectiveVelocityScale)
        {
            double gradOfShear_Turbulance = 0;

            double gamma = 0;

            if (height < 0.1 * mixingHeight)
            {

                gamma = mixingHeight;
            }
            else
            {
                gamma = surfaceFrictionVelocity / 0.0001;
            }

            double frictionVelocity = surfaceFrictionVelocity * Math.Pow((1 - (height / mixingHeight)), 0.85);
            double P = Math.Pow(frictionVelocity, 2);
            double Q = Math.Pow((1 + 15 * height / gamma), 0.667);

            double gradP = -1.7 * frictionVelocity * surfaceFrictionVelocity * Math.Pow((1 - (height / mixingHeight)), -0.15) / mixingHeight;
            double gradQ = (10 / gamma) * Math.Pow((1 + 15 * height / gamma), -0.333);

            return gradOfShear_Turbulance = 1.9364 * (Q * gradP - P * gradQ) / Math.Pow(Q, 2);
        }

        private double CalculateGradOfShear_Turbulance_Stable(char stabilityClass, double height, double mixingHeight, double surfaceFrictionVelocity, double convectiveVelocityScale, double reciprocalObukhovLength) {

            double gradShearTurbulance = 0;

            double gamma = 0;

            if (height < 0.1 * mixingHeight)
            {

                gamma = mixingHeight;
            }
            else
            {
                gamma = surfaceFrictionVelocity / 0.0001;
            }


            double Lambda = (1 / reciprocalObukhovLength) * Math.Pow((1 - height / mixingHeight), 1.25);
            double delta = height / Lambda;
            double frictionVelocity = surfaceFrictionVelocity * Math.Pow((1 - height / mixingHeight), 0.75);
            double P1 = Math.Pow(frictionVelocity, 2);
            double P2=Math.Pow((1 + 3.7 * delta), 0.667);
            double P =P1*P2 ;
            double gradLambda = (-1.25 * (1 / reciprocalObukhovLength) / mixingHeight) * Math.Pow((1 - height / mixingHeight), 0.25);
            double gradDelta = (Lambda - height * gradLambda) / Math.Pow(Lambda,2);
            double Q = Math.Pow((1 + (15 * height / gamma) + 3.7 * delta), 0.667);
            double gradQ = (2 / 3) * (15 / gamma + 3.7 * gradDelta) * Math.Pow((1 + (15 * height / gamma) + 3.7 * delta), -0.333);
            double gradP1 = (-0.75 * surfaceFrictionVelocity / mixingHeight) * Math.Pow((1 - (height / mixingHeight)), -0.25);
            double gradP2 = 2.467 * Math.Pow((1 + 3.7 * delta), -0.333) * gradDelta;

            double gradP = P1 * gradP2 + P2 * gradP1;

            return gradShearTurbulance = 1.9364 * (Q * gradP - P * gradQ) / Math.Pow(Q, 2);

           
        }

        #endregion



        //Calculates lagrangian decorrelation time scales...
        private double CalculateLagrangianTimeScale(char stabilityClass, char component, double height, double mixingHeight, double surfaceFrictionVelocity, double convectiveVelocityScale, double reciprocalObukhovLength)
        {
            double lagrangianTime = 0;


            double c = 0;
            double lambda = 0;
            double fm = 0;
            double a = 0;

             

            if (stabilityClass == 'A' || stabilityClass == 'B')
            {


                switch (component)
                {
                    case 'u':
                        c = 0.2578;
                        lambda = 1.5 * mixingHeight;
                        break;

                    case 'v':
                        c = 0.3438;
                        lambda = 1.5 * mixingHeight;
                        break;

                    case 'w':
                        c = 0.3438;
                        lambda = 1.8 * mixingHeight * (1 - Math.Exp(-4 * height / mixingHeight) - 0.0003 * Math.Exp(8 * height / mixingHeight));
                        break;

                }
                lagrangianTime = (height / Math.Pow(c, 0.5)) * (0.0162 * Math.Pow(mixingHeight * (-reciprocalObukhovLength), 0.5) / (Math.Pow(height / lambda, 0.66666) * convectiveVelocityScale * Math.Pow(height / mixingHeight, 0.33333)));

            }

            if (stabilityClass == 'C')
            {

                double frictionVelocity = surfaceFrictionVelocity * Math.Pow(1 - (height / mixingHeight), 0.85);

                switch (component)
                {
                    case 'u':
                        c = 0.2578;
                        a = 3889;
                        fm = 0.045;
                        lambda = 1.5 * mixingHeight;
                        break;

                    case 'v':
                        c = 0.3438;
                        a = 1094;
                        fm = 0.16;
                        lambda = 1.5 * mixingHeight;
                        break;

                    case 'w':
                        c = 0.3438;
                        a = 500;
                        fm = 0.33;
                        lambda = 1.8 * mixingHeight * (1 - Math.Exp(-4 * height / mixingHeight) - 0.0003 * Math.Exp(8 * height / mixingHeight));
                        break;
                }

                if (height < 0.1 * mixingHeight)
                {
                    fm = fm * (1 + (0.03 * a * height / mixingHeight));
                }
                else
                {
                    fm = fm * (1 + (0.03 * Math.Pow(10, -4) * a * height / surfaceFrictionVelocity));
                }
                lagrangianTime = (height / Math.Pow(c, 0.5)) * (0.0162 * Math.Pow(mixingHeight * (-reciprocalObukhovLength), 0.5) / (Math.Pow(height / lambda, 0.66666) * convectiveVelocityScale * Math.Pow(height / mixingHeight, 0.333333))) + (height / Math.Pow(c, 0.5)) * (0.055 / (Math.Pow(fm, 0.66666) * frictionVelocity));

            }



            if (stabilityClass == 'D')
            {

                double frictionVelocity = surfaceFrictionVelocity * Math.Pow(1 - (height / mixingHeight), 0.85);


                switch (component)
                {
                    case 'u':
                        c = 0.2578;
                        a = 3889;
                        fm = 0.045;
                        break;

                    case 'v':
                        c = 0.3438;
                        a = 1094;
                        fm = 0.16;
                        break;

                    case 'w':
                        c = 0.3438;
                        a = 500;
                        fm = 0.33;
                        break;

                }

                if (height < 0.1 * mixingHeight)
                {
                    fm = fm * (1 + (0.03 * a * height / mixingHeight));
                }
                else
                {
                    fm = fm * (1 + (0.03 * Math.Pow(10, -4) * a * height / surfaceFrictionVelocity));
                }
                lagrangianTime = (height / Math.Pow(c, 0.5)) * (0.055 / (Math.Pow(fm, 0.66666) * frictionVelocity));

            }

            if (stabilityClass == 'E' || stabilityClass == 'F' || stabilityClass == 'G')
            {

                double frictionVelocity = surfaceFrictionVelocity * Math.Pow(1 - (height / mixingHeight), 0.75);
                lambda = (1 / reciprocalObukhovLength) * Math.Pow((1 - (height / mixingHeight)), 1.25);
                double phi = 1.25 * (1 + 3.7 * (height / lambda));

                switch (component)
                {
                    case 'u':
                        c = 0.2578;
                        a = 3889;
                        fm = 0.045;
                        break;

                    case 'v':
                        c = 0.3438;
                        a = 1094;
                        fm = 0.16;
                        break;

                    case 'w':
                        c = 0.3438;
                        a = 500;
                        fm = 0.33;
                        break;
                }


                if (height < 0.1 * mixingHeight)
                {
                    fm = fm * (1 + (0.03 * a * height / mixingHeight) + 3.7 * height / lambda);
                }
                else
                {
                    fm = fm * (1 + (0.03 * a * Math.Pow(10, -4) * height / surfaceFrictionVelocity) + 3.7 * height / lambda);
                }
                lagrangianTime = (height / Math.Pow(c, 0.5)) * (0.059 / (Math.Pow(fm, 0.66666) * Math.Pow(phi, 0.333333) * frictionVelocity));

            }

            if (double.IsNaN(lagrangianTime))
            {
            
            
            
            }


            return lagrangianTime;

        }

        #endregion


        //Calculates the size of a puff  at a given time and returns puff variances in the array form... 
        private double[] CalculatePuffSize(double timeStep, double currentTime,double[] velocityVariances, double[] lagrangianTimes,double[] currentPuffSize)
        {

            double lateralPuffVariance = CalculateTaylorsPuffDiffusion(velocityVariances[0], timeStep, currentTime,lagrangianTimes[0],Math.Pow(currentPuffSize[0],2));
            double horizontalPuffVariance = CalculateTaylorsPuffDiffusion(velocityVariances[1], timeStep, currentTime, lagrangianTimes[1],Math.Pow(currentPuffSize[1],2));
            double verticalPuffVariance = CalculateTaylorsPuffDiffusion(velocityVariances[2], timeStep, currentTime, lagrangianTimes[2],Math.Pow(currentPuffSize[2],2));

            double[] puffVariance = new double[3] { lateralPuffVariance, horizontalPuffVariance, verticalPuffVariance };
            return puffVariance;


        }

        private double CalculateTaylorsPuffDiffusion(double velocityVariance, double timeStep,double currentTime, double lagrangianTime,double currentPuffVariance)
        {

            //double puffVariance = currentPuffVariance+2 * velocityVariance * lagrangianTime * (currentTime + (lagrangianTime * Math.Exp(-currentTime / lagrangianTime)) - lagrangianTime) * timeStep;
            //return puffVariance;

            double puffVariance = currentPuffVariance + (2 * velocityVariance * lagrangianTime * (1 - Math.Exp(-currentTime / lagrangianTime)) * timeStep);
            return puffVariance;

        }


        private double[] CalculateModifiedPuffSize(double currentTime, double timeStep, double[] velocityVariances, double[] lagrangianTimes, double[] currentPuffSize) {

            double lateralPuffVariance = CalculateModifiedPuffDiffusion(currentTime, timeStep, velocityVariances[0], lagrangianTimes[0], currentPuffSize[0]);
            double horizontalPuffVariance = CalculateModifiedPuffDiffusion(currentTime, timeStep, velocityVariances[1], lagrangianTimes[1], currentPuffSize[1]);
            double verticalPuffVariance = CalculateModifiedPuffDiffusion(currentTime, timeStep, velocityVariances[2], lagrangianTimes[2], currentPuffSize[2]);

            double[] puffVariance = new double[3] { lateralPuffVariance, horizontalPuffVariance, verticalPuffVariance };
            return puffVariance;
        
        
        }

        private double CalculateModifiedPuffDiffusion(double currentTime, double timeStep, double velocityVariance, double lagrangianTime,double currentPuffSize) {

            double puffVariance = 0;

            if (currentTime <= 2 * lagrangianTime)
            {

                puffVariance = currentPuffSize +Math.Sqrt( velocityVariance )* timeStep;

                puffVariance = Math.Pow(puffVariance, 2);
            }
            else {

                puffVariance = Math.Pow(currentPuffSize, 2) + 2 * lagrangianTime * velocityVariance* timeStep;
            
            }


            return puffVariance;
        }



        //Calculates the new position of a particle...
        private double CalculateParticlePosition(double currentPosition, double meanWind, double turbulentWindComponent,double timeStep) {

            double newPosition;

            if (isStochasticApproach == false && frmDataInput.typeOfTheSource == "Instantaneous Source")
            {
                newPosition = currentPosition + (meanWind) * timeStep;

            }
            else 
            {

                newPosition = currentPosition + (meanWind + turbulentWindComponent) * timeStep;          
            }

            return newPosition;
        }



        //Calculates homogeneous gaussian turbulance.... 
        private double CalculateHorizontalTurbulance(double currentTurbulance, double timeStep, double lagrangianTime, double velocityVariance) {

            double horizontalTurbulance = 0;
            
            double randomNumber=GetRandomNumber();

            horizontalTurbulance = currentTurbulance + (-currentTurbulance * timeStep / lagrangianTime) + (Math.Sqrt(2 * timeStep * velocityVariance / lagrangianTime) * randomNumber);



            if (double.IsNaN(horizontalTurbulance))
            {
            
            
            
            }
            return horizontalTurbulance;
        }

        //Calculates inhomogeneous gaussian turbulance...
        private double CalculateVerticalTurbulance(double currentTurbulance, double timeStep, double lagrangianTime, double velocityVariance, double gradVariance) {

            double verticalTurbulance = 0;

            double randomNumebr = GetRandomNumber();
            verticalTurbulance = currentTurbulance + (-currentTurbulance * timeStep / lagrangianTime) +( 0.5 * (1 + (Math.Pow(currentTurbulance, 2) / velocityVariance)) * gradVariance * timeStep) + (Math.Sqrt(2 * timeStep * velocityVariance / lagrangianTime) * randomNumebr);


            if (double.IsNaN(verticalTurbulance))
            {



            }

            return verticalTurbulance;
        }
       
        //Returns a random number with a zero mean and a variance of 1 using box muller transformation....

        private double GetRandomNumber() {

            double[] randomNumbers = new double[2];
  

            double u1 = 0;
            double u2 = 0;

            Random r = new Random();
            u1 = r.NextDouble();
           u2 = r.NextDouble();

           randomNumbers[0] = Math.Sqrt(-2 * Math.Log10(u1)) * Math.Cos(2 * Math.PI * u2);
           randomNumbers[1] = Math.Sqrt(-2 * Math.Log10(u1)) * Math.Sin(2 * Math.PI * u2);

           return randomNumbers[r.Next(2)];

        }

        public static void CreateConGridInExcel()
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
                    workSheet.Cells[rowCount, i + 1] = concentrations[j, i];


                    //workSheet = oExcel.Sheets[2];
                    //workSheet.Cells[rowCount, i + 1] = terrainSlopeX[j, i];

                    //workSheet = oExcel.Sheets[3];
                    //workSheet.Cells[rowCount, i + 1] = terrainSlopeY[j, i];
                }

                rowCount -= 1;


            }



        }

    }
}
