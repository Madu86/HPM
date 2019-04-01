using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using dnAnalytics.LinearAlgebra;
using dnAnalytics.LinearAlgebra.Solvers.Iterative;
using dnAnalytics.LinearAlgebra.Solvers.Preconditioners;





namespace HPCM_REBUILD
{
    class Base2
    {

        public Base2()
        {

    
        
        }

        #region ---Varibles and properties---

        private static double alpha1 = 1;

        public static double Alpha1
        {
           
            get { return alpha1; }
            set { alpha1 = value; }
        }


        private static double alpha2 = 1;

        public static double Alpha2
        {

            get { return alpha2; }
            set { alpha2 = value; }
        }

        //Terrain conformal wind arrays...

        private static double[, ,] U = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX, Meteorology.NumberOfMetCellsZ + 1];
        private static double[, ,] V = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX, Meteorology.NumberOfMetCellsZ + 1];
        private static double[, ,] W = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX, Meteorology.NumberOfMetCellsZ + 1];

        private static double[, ,] UTF;
        private static double[, ,] VTF;
        private static double[, ,] WTF;

        private static double[, ,] UTFUpdated;
        private static double[, ,] VTFUpdated;
        private static double[, ,] WTFUpdated;

        //Terrain data...
        private static double[,] terrainHeights = Geography.terrainHeights;
        private static double[,] slopeX = Geography.terrainSlopeX;
        private static double[,] slopeY = Geography.terrainSlopeY;

        private static double[,] gradientX = new double[Meteorology.NumberOfMetCellsY,Meteorology.NumberOfMetCellsX];
        private static double[,] gradientY = new double[Meteorology.NumberOfMetCellsY,Meteorology.NumberOfMetCellsX];

        //Matrices...
        //static double[,] A = new double[(Meteorology.NumberOfMetCellsX * Meteorology.NumberOfMetCellsY * Meteorology.NumberOfMetCellsZ), (Meteorology.NumberOfMetCellsX * Meteorology.NumberOfMetCellsY * Meteorology.NumberOfMetCellsZ)];
        //static double[] B = new double[(Meteorology.NumberOfMetCellsX * Meteorology.NumberOfMetCellsY * Meteorology.NumberOfMetCellsZ)];
        //static double[] Lambda = new double[(Meteorology.NumberOfMetCellsX * Meteorology.NumberOfMetCellsY * Meteorology.NumberOfMetCellsZ)];

        static SparseMatrix A = new SparseMatrix((Meteorology.NumberOfMetCellsX * Meteorology.NumberOfMetCellsY * Meteorology.NumberOfMetCellsZ), (Meteorology.NumberOfMetCellsX * Meteorology.NumberOfMetCellsY * Meteorology.NumberOfMetCellsZ));
        static DenseVector B = new DenseVector((Meteorology.NumberOfMetCellsX * Meteorology.NumberOfMetCellsY * Meteorology.NumberOfMetCellsZ));
        static DenseVector Lambda = new DenseVector((Meteorology.NumberOfMetCellsX * Meteorology.NumberOfMetCellsY * Meteorology.NumberOfMetCellsZ));

        static double minimumDivergence = 0.001;


        public static double MinimumDivergence {

            get { return minimumDivergence;}
            set { minimumDivergence = value; }
        
        }



        #endregion

        #region ---Supporting methods---

        private static double Calculateh(double zt, double zg) {

            double h = (zt - zg) / zt;
            return h;
        
        }


        private static double CalculateZStar(double z,double zg,double h){

            double zStar = (z - zg) / h;

            return zStar;
    
    }

        #endregion



        private static void CalculateGradients()
        {

            for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
            {

                for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
                {

                    gradientX[j, i] = -Geography.terrainSlopeX[j, i] / Meteorology.DomainHeight;
                    gradientY[j, i] = -Geography.terrainSlopeY[j, i] / Meteorology.DomainHeight;

                }

            }

        }


        //Creates an array of vertically intepolated wind components.. 
        private static void CreateWindArrays()
        {

            for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
            {

                for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
                {

                    for (int k = 0; k < Meteorology.NumberOfMetCellsZ + 1; k++)
                    {

                        if (k == 0)
                        {

                            U[j, i, k] = 0;
                            V[j, i, k] = 0;
                            W[j, i, k] = 0;
                        }

                        else
                        {

                            U[j, i, k] = Meteorology.U[j, i, k - 1];
                            V[j, i, k] = Meteorology.V[j, i, k - 1];
                            W[j, i, k] = Meteorology.W[j, i, k - 1];

                        }


                    }

                }




            }



        }

        private static void CreateTCWindArrays()
        {
           


            UTF = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX, Meteorology.NumberOfMetCellsZ];
            VTF = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX, Meteorology.NumberOfMetCellsZ];
            WTF = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX, Meteorology.NumberOfMetCellsZ];

            UTFUpdated = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX, Meteorology.NumberOfMetCellsZ];
            VTFUpdated = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX, Meteorology.NumberOfMetCellsZ];
            WTFUpdated = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX, Meteorology.NumberOfMetCellsZ];


            for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
            {

            for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
            {

                    double h = Calculateh(Meteorology.DomainHeight, terrainHeights[j, i]);

                    for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
                    {

                        double zStar = Geography.sigmaLevel[k];

                        double sx = (zStar-Meteorology.DomainHeight) * gradientX[j, i];
                        double sy = (zStar -Meteorology.DomainHeight) * gradientY[j, i];

                       

                        UTF[j, i, k] = U[j, i, k];
                        VTF[j, i, k] =V[j, i, k];                        
                        WTF[j, i, k] = (1 / h) * (W[j, i, k] - sx * U[j, i, k] - sy * V[j, i, k]);

                    }



                }


            }

        }




        private static void CalculateDivergence()
        {

            int rowIndex = 0;

            
            for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
            {

                for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
                {

       
                    double h = Calculateh(Meteorology.DomainHeight, terrainHeights[j, i]);

                    for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
                    {



                        if (i == 0 | i == Meteorology.NumberOfMetCellsX - 1 | j == 0 | j == Meteorology.NumberOfMetCellsY - 1 |k==0| k == Meteorology.NumberOfMetCellsZ - 1)
                        {

                            B[rowIndex] = 0;

                        }
                        else
                        {
                            double hi0 = Calculateh(Meteorology.DomainHeight, terrainHeights[j, i - 1]);
                            double hi2 = Calculateh(Meteorology.DomainHeight, terrainHeights[j, i + 1]);

                            double hj0 = Calculateh(Meteorology.DomainHeight, terrainHeights[j - 1, i]);
                            double hj2 = Calculateh(Meteorology.DomainHeight, terrainHeights[j + 1, i]);

                            double hk1 = Geography.sigmaLevel[k] - Geography.sigmaLevel[k - 1];
                            double hk2 = Geography.sigmaLevel[k + 1] - Geography.sigmaLevel[k];

                            double valB=( -2* Math.Pow(Alpha1, 2)/h) * ((((hi2*UTF[j, i + 1, k]) -(hi0* UTF[j, i - 1, k])) / (2 * Meteorology.HorizontalDimensions)) + (((hj2*VTF[j + 1, i, k]) - (hj0*VTF[j - 1, i, k])) / (2 * Meteorology.HorizontalDimensions)) +(h* (Math.Pow(hk1,2)*WTF[j, i, k + 1] -(Math.Pow(hk1,2)-Math.Pow(hk2,2))*WTF[j,i,k]-Math.Pow(hk2,2)* WTF[j, i, k - 1]) / (hk1*hk2*(hk1+hk2))));
                            if (double.IsNaN(valB)) 
                            {
                            
                                                        
                            }
                            B[rowIndex] = valB;

                        }

                        rowIndex += 1;

                    }

                }

            }

        }



        //Populates matrix A...
        private static void FillMatrixA()
        {

            int n = 0;
  
            for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
            {

                for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
                {


                    for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
                    {
                        double h = Calculateh(Meteorology.DomainHeight, terrainHeights[j, i]);

                     
                 
                        if (i == 0)
                        {

                            A[n, n] = 1;
                        }
                        else if (i == Meteorology.NumberOfMetCellsX - 1)
                        {

                            A[n, n] = 1;
                        }
                        else if (j == 0)
                        {

                            A[n, n] = 1;
                        }
                        else if (j == Meteorology.NumberOfMetCellsY - 1)
                        {

                            A[n, n] = 1;
                        }
                        else if (k == Meteorology.NumberOfMetCellsZ - 1)
                        {
                            A[n, n] = 1;
                            A[n, n - 1] = -1;
                        }

                        else if (k == 0)
                        {

 
                            double K = h * Meteorology.DomainHeight / (Math.Pow((Alpha1 / Alpha2), 2) + (Math.Pow(Meteorology.DomainHeight, 2) * (Math.Pow(gradientX[j, i], 2) + Math.Pow(gradientY[j, i], 2))));
                            double Kx = K * gradientX[j, i] / (2 * Meteorology.HorizontalDimensions);
                            double Ky = K * gradientY[j, i] / (2 * Meteorology.HorizontalDimensions);



                            A[n, n] = -1/(Geography.sigmaLevel[k+1]-Geography.sigmaLevel[k]);
                            A[n, n + 1] = 1 / (Geography.sigmaLevel[k+1]- Geography.sigmaLevel[k]);

                            A[n, n - Meteorology.NumberOfMetCellsZ] = -Ky;
                            A[n, n + Meteorology.NumberOfMetCellsZ] = Ky;
                            A[n, n - (Meteorology.NumberOfMetCellsZ * Meteorology.NumberOfMetCellsY)] = -Kx;
                            A[n, n + (Meteorology.NumberOfMetCellsZ * Meteorology.NumberOfMetCellsY)] = Kx;



                        }
                        else
                        {

                            double zx = (1 / h) * gradientX[j, i];
                            double zy = (1 / h) * gradientY[j, i];

                            double p = (1 / Math.Pow(h, 2)) * Math.Pow((Alpha1 / Alpha2), 2) + Math.Pow((Geography.sigmaLevel[k]- Meteorology.DomainHeight), 2) * (Math.Pow(zx, 2) + Math.Pow(zy, 2));
                            double q = 2 * (Geography.sigmaLevel[k] - Meteorology.DomainHeight) * (Math.Pow(zx, 2) + Math.Pow(zy, 2));
                            double Rx = 2 * (Geography.sigmaLevel[k] - Meteorology.DomainHeight) * zx;
                            double Ry = 2 * (Geography.sigmaLevel[k] - Meteorology.DomainHeight) * zy;

                            double hk1=Geography.sigmaLevel[k+1]-Geography.sigmaLevel[k];
                            double hk=Geography.sigmaLevel[k]-Geography.sigmaLevel[k-1];

                            double Ck2 = ((2 * p + hk * q) / (hk1 * (hk1 + hk)));
                            double Cijk = (-2 / Math.Pow(Meteorology.HorizontalDimensions, 2)) + (-2 / Math.Pow(Meteorology.HorizontalDimensions, 2)) - (2 * p + q * (hk - hk1)) / (hk * hk1);
                            double Ck1 = ((2 * p - hk1 * q) / (hk * (hk1 + hk)));

                            double H2 = hk / (hk1 * (hk + hk1));        //First coefficient...
                            double H = (hk - hk1) / (hk * hk1);         //Middle coefficient...
                            double H1 = hk1 / (hk * (hk + hk1));    // Third coefficient...

                            double Ci2 = (1 / Math.Pow(Meteorology.HorizontalDimensions, 2)) + Rx * H / (2 * Meteorology.HorizontalDimensions);
                            double Cj2 = (1 / Math.Pow(Meteorology.HorizontalDimensions, 2)) + Ry * H / (2 * Meteorology.HorizontalDimensions);

                            double Ci1 = (1 / Math.Pow(Meteorology.HorizontalDimensions, 2)) - Rx * H / (2 * Meteorology.HorizontalDimensions);
                            double Cj1 = (1 / Math.Pow(Meteorology.HorizontalDimensions, 2)) - Ry * H / (2 * Meteorology.HorizontalDimensions);




                            A[n, n] = Cijk;
                            A[n, n - 1] = Ck1;
                            A[n, n + 1] = Ck2;

                            A[n, n - Meteorology.NumberOfMetCellsZ] = Cj1;
                            A[n, n + Meteorology.NumberOfMetCellsZ] = Cj2;

                            A[n, n - (Meteorology.NumberOfMetCellsZ * Meteorology.NumberOfMetCellsY)] = Ci1;
                            A[n, n + (Meteorology.NumberOfMetCellsZ * Meteorology.NumberOfMetCellsY)] = Ci2;


                            A[n, n + Meteorology.NumberOfMetCellsZ + 1] = -Ry*H2/(2*Meteorology.HorizontalDimensions);
                            A[n, n + Meteorology.NumberOfMetCellsZ - 1] = Ry*H1/(2*Meteorology.HorizontalDimensions);

                            A[n, n - Meteorology.NumberOfMetCellsZ + 1] = Ry*H2/(2*Meteorology.HorizontalDimensions);
                            A[n, n - Meteorology.NumberOfMetCellsZ - 1] = -Ry * H1 / (2 * Meteorology.HorizontalDimensions);

                            A[n, n + (Meteorology.NumberOfMetCellsZ * Meteorology.NumberOfMetCellsY) + 1] = -Rx * H2 / (2 * Meteorology.HorizontalDimensions);
                            A[n, n + (Meteorology.NumberOfMetCellsZ * Meteorology.NumberOfMetCellsY) - 1] = Rx * H1 / (2 * Meteorology.HorizontalDimensions);
                            A[n, n - (Meteorology.NumberOfMetCellsZ * Meteorology.NumberOfMetCellsY) + 1] = Rx * H2 / (2 * Meteorology.HorizontalDimensions);
                            A[n, n - (Meteorology.NumberOfMetCellsZ * Meteorology.NumberOfMetCellsY) - 1] = -Rx * H1 / (2 * Meteorology.HorizontalDimensions);



                        }

                        n += 1;
                     
                    }

                }

            }


        }


        //Populates array lambda...
        private static void CreateLambda()
        {

            int rowNum = 0;


            for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
            {


                for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
                {


                    for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
                    {


                        Lambda[rowNum] = 1;

                        rowNum += 1;
                    }
                }
            }

        }


        //Calculates d(lambda)/dn for interior grid points...
        private static double CalculateLambdaNormal(double delta, double lambda1, double lambda2)
        {

            double lambdaNormal = (lambda2 - lambda1) / (2 * delta);

            return lambdaNormal;
        }


        //Calculates d(lambda)/dn for boundry points...
        private static double CalculateLambdaNormalBP(double delta, double lambda1, double lambda2)
        {

            double lambdaNormal = (lambda2 - lambda1) / delta;
            return lambdaNormal;
        }


        //Calculates the wind field based on new lambdas... 
        private static void UpdateWindArrays()
        {

            double alphaSq1 = 1 / Math.Pow(Alpha1, 2);
            double alphaSq2 = 1 / Math.Pow(Alpha2, 2);
         

            //Load lambdas from the matrix into a 3d array for the ease of access...

            double[, ,] arrLambda = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX, Meteorology.NumberOfMetCellsZ];

            int rowIndex = 0;


            for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
            {

                for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
                {

                    for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
                    {

                        arrLambda[j, i, k] = Lambda[rowIndex];

                        rowIndex += 1;

                    }

                }


            }






            for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
            {

                for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
                {


                    double h = Calculateh(Meteorology.DomainHeight, terrainHeights[j, i]);


                    for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
                    {

                        double sigma = Geography.sigmaLevel[k];

                        double lambdaNormalX;
                        double lambdaNormalY;
                        double lambdaNormalZ;

                        //Calculate lambda normal in X direction...

                        if (i == 0)
                        {
                            lambdaNormalX = CalculateLambdaNormalBP(Meteorology.HorizontalDimensions, arrLambda[j, i, k], arrLambda[j, i + 1, k]);                                                 
                        }

                        else if (i == Meteorology.NumberOfMetCellsX - 1)
                        {
                           lambdaNormalX =CalculateLambdaNormalBP(Meteorology.HorizontalDimensions, arrLambda[j, i - 1, k], arrLambda[j, i, k]);                      
                        }
                        else
                        {
                            lambdaNormalX =CalculateLambdaNormal(Meteorology.HorizontalDimensions, arrLambda[j, i - 1, k], arrLambda[j, i + 1, k]);                                
                        }



                        //Calculate lambda normal in Y direction...

                        if (j == 0)
                        {
                            lambdaNormalY = CalculateLambdaNormalBP(Meteorology.HorizontalDimensions, arrLambda[j, i, k], arrLambda[j + 1, i, k]);
                        }
                        else if (j == Meteorology.NumberOfMetCellsY - 1)
                        {
                            lambdaNormalY = CalculateLambdaNormalBP(Meteorology.HorizontalDimensions, arrLambda[j - 1, i, k], arrLambda[j, i, k]);
                        }
                        else
                        {
                            lambdaNormalY = CalculateLambdaNormal(Meteorology.HorizontalDimensions, arrLambda[j - 1, i, k], arrLambda[j + 1, i, k]);
                        }



                        //Calculate lambda normal in z direction...
                        if (k == 0)
                        {

                            
                            double alphaRatio = Math.Pow((Alpha1 / Alpha2), 2);

                            lambdaNormalZ = (-h * Meteorology.DomainHeight * (gradientX[j, i] * lambdaNormalX + gradientY[j, i] * lambdaNormalY)) / (alphaRatio + (Math.Pow(Meteorology.DomainHeight, 2) * (Math.Pow(gradientX[j, i], 2) + Math.Pow(gradientY[j, i], 2))));

                        }
                        else if (k == Meteorology.NumberOfMetCellsZ - 1)
                        {
                            lambdaNormalZ = 0;
                        }
                        else
                        {
                            double hk1 = Geography.sigmaLevel[k] - Geography.sigmaLevel[k - 1];
                            double hk2 = Geography.sigmaLevel[k + 1] - Geography.sigmaLevel[k];
                            lambdaNormalZ = (Math.Pow(hk1,2)*arrLambda[j, i, k + 1] - (Math.Pow(hk1,2)-Math.Pow(hk2,2))*arrLambda[j,i,k]-Math.Pow(hk2,2)*arrLambda[j, i, k - 1]) / (hk1*hk2*(hk1+hk2));
                        }


                        //Calculate wind components...


                        UTFUpdated[j, i, k] = UTF[j, i, k] + 0.5 * alphaSq1 * (lambdaNormalX - (sigma - Meteorology.DomainHeight) * (1 / h) * gradientX[j, i] * lambdaNormalZ);
                        VTFUpdated[j, i, k] = VTF[j, i, k] + 0.5 * alphaSq1 * (lambdaNormalY - (sigma - Meteorology.DomainHeight) * (1 / h) * gradientY[j, i] * lambdaNormalZ);
 
                        if (k == 0)
                        {
                            WTFUpdated[j, i, k] = 0;
                        }
                        else
                        {
                              WTFUpdated[j, i, k] = WTF[j, i, k] + ((1 / (2 * Math.Pow(h, 2))) * (alphaSq2 + alphaSq1 * Math.Pow((sigma - Meteorology.DomainHeight), 2) * (Math.Pow(gradientX[j, i], 2) + Math.Pow(gradientY[j, i], 2))) * lambdaNormalZ) - (sigma - Meteorology.DomainHeight) * 0.5 * (1 / h) * alphaSq1 * ((gradientX[j, i] * lambdaNormalX) + (gradientY[j, i] * lambdaNormalY));

                        }

                    }

                }

            }


            //Copies all the element from Updated array to intial array...

            for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
            {

                for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
                {

                    for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
                    {

                        UTF[j, i, k] = UTFUpdated[j, i, k];
                        VTF[j, i, k] = VTFUpdated[j, i, k];
                        WTF[j, i, k] = WTFUpdated[j, i, k];

                    }
                }
            }




        }



        //Calculates cartesian wind components and stores in the meteorology arrays...
        private static void CalculateCartesianWindComponents()
        {


            for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
            {

                for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
                {

                    double h = Calculateh(Meteorology.DomainHeight, terrainHeights[j, i]);

                    for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
                    {                   

                        double sx = (Geography.sigmaLevel[k]- Meteorology.DomainHeight) * gradientX[j, i];
                        double sy = (Geography.sigmaLevel[k] - Meteorology.DomainHeight) * gradientY[j, i];


                        U[j, i, k] = UTF[j, i, k];
                        V[j, i, k] = VTF[j, i, k];
                        W[j, i, k] = WTF[j, i, k] * h + sx * UTF[j, i, k] + sy * VTF[j, i, k];

                  
                    }

                  
                }

            }

        }



        private static void UpdateMeteorology() {


            for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
            {

                for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
                {


                    for (int k = 0; k < Meteorology.NumberOfMetCellsZ; k++)
                    {

                        Meteorology.U[j, i, k] = U[j, i, k + 1];
                        Meteorology.V[j, i, k] = V[j, i, k + 1];
                        Meteorology.W[j, i, k] = W[j, i, k + 1];

                    }


                }

            }
        
        
        }


        static int excelColumnNumber = 0;

        public static void MinimizeDivergence()
        {

            

            CalculateGradients();

            CreateWindArrays();

            CreateTCWindArrays();

            CreateLambda();

            FillMatrixA();

            //#region ---Load MS Excel and write A---
            //Excel.Application oExcel = new Excel.Application { Visible = true };

            //oExcel.Workbooks.Add();
            //Excel._Worksheet workSheet = oExcel.ActiveSheet;

            //var arrA = A;


            //for (int i = 0; i < (Meteorology.NumberOfMetCellsX * Meteorology.NumberOfMetCellsY * Meteorology.NumberOfMetCellsZ); i++)
            //{

            //    for (excelColumnNumber = 0; excelColumnNumber < (Meteorology.NumberOfMetCellsX * Meteorology.NumberOfMetCellsY * Meteorology.NumberOfMetCellsZ); excelColumnNumber++)
            //    {

            //        workSheet.Cells[i + 1, excelColumnNumber + 1] = arrA[i, excelColumnNumber];

            //    }

            //}

            //#endregion

  

            BiCgStab b = new BiCgStab();
            b.SetIterator(dnAnalytics.LinearAlgebra.Solvers.Iterator.CreateDefault());
            b.SetPreconditioner(new Diagonal());

            bool divergenceMinimized = false;
            for (int p=0;p<50000 ;p++ )
            {

                CalculateDivergence();

                double count = 0;
                foreach (double d in B.ToArray())
                {
                    count += 1;
                    if (-d > minimumDivergence)
                    {
                        break;
                    }
                    else if (count == B.Count)
                    {
                        
                        divergenceMinimized = true;
                        break;
                    }


                }

                if (divergenceMinimized)
                {
                    break;
                }
                else
                {


                    var M = b.Solve(A, B);

                    for (int i = 0; i < M.Count; i++)
                    {
                        Lambda[i] = M[i];
                    }

                    UpdateWindArrays();
                }

            }

          
    
            CalculateCartesianWindComponents();

            UpdateMeteorology();
            

        }



    }
}
