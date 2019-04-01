using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Drawing.Drawing2D;
using GMap.NET;
using GMap.NET.WindowsForms;



namespace HPCM_REBUILD
{
    public partial class frmDrawConc : Form
    {

        // Class level variables

        GMapControl mapViewer;
        frmMainHPCM mainForm;
        ColorDialog cd1;
        ColorDialog cd2 ;
        ColorDialog cd3 ;
        ColorDialog cd4 ;
        public static bool drawCon = false;


        public frmDrawConc( GMapControl mapViewer, frmMainHPCM mainForm)
        {
            InitializeComponent();
            this.mapViewer =mapViewer;
            this.mainForm = mainForm;


            cd1 = new ColorDialog();
            cd2 = new ColorDialog();
           cd3 = new ColorDialog();
           cd4 = new ColorDialog();



        }

        #region --- Paint Methods---





        private void DrawDispersion()
        {
            frmMainHPCM.lstCurvePoints.Clear();
            frmMainHPCM.lstCurvePoints_2.Clear();
            frmMainHPCM.lstCurvePoints_3.Clear();
            frmMainHPCM.lstCurvePoints_4.Clear();

            double maxCon = 0;
            for (int j = 0; j < Meteorology.NumberOfMetCellsY * dispersion.NumOfHorizontalConcCells; j++)
            {

                for (int i = 0; i < Meteorology.NumberOfMetCellsX * dispersion.NumOfHorizontalConcCells; i++)
                {

                    if (maxCon < dispersion.concentrations[j, i])
                    {
                        maxCon = dispersion.concentrations[j, i];
                    }

                }


            }



            for (int j = 1; j < dispersion.NumOfHorizontalConcCells * Meteorology.NumberOfMetCellsY; j++)
            {

                for (int i = 1; i < dispersion.NumOfHorizontalConcCells * Meteorology.NumberOfMetCellsX - 1; i++)
                {

                    if (dispersion.concentrations[j, i] > mainForm.conc_Level_4 && dispersion.concentrations[j, i - 1] < mainForm.conc_Level_4)
                    {

                       frmMainHPCM. lstCurvePoints.Add(dispersion.concGridPoints[j, i]);
                    }
                    else if (dispersion.concentrations[j, i] > mainForm.conc_Level_4 && dispersion.concentrations[j, i + 1] < mainForm.conc_Level_4)
                    {

                        frmMainHPCM.lstCurvePoints.Add(dispersion.concGridPoints[j, i]);

                    }


                    if (dispersion.concentrations[j, i] > mainForm.conc_Level_3 && dispersion.concentrations[j, i - 1] < mainForm.conc_Level_3)
                    {

                        frmMainHPCM.lstCurvePoints_2.Add(dispersion.concGridPoints[j, i]);
                    }
                    else if (dispersion.concentrations[j, i] > mainForm.conc_Level_3 && dispersion.concentrations[j, i + 1] < mainForm.conc_Level_3)
                    {

                        frmMainHPCM.lstCurvePoints_2.Add(dispersion.concGridPoints[j, i]);

                    }


                    if (dispersion.concentrations[j, i] > mainForm.conc_Level_2 && dispersion.concentrations[j, i - 1] < mainForm.conc_Level_2)
                    {

                       frmMainHPCM. lstCurvePoints_3.Add(dispersion.concGridPoints[j, i]);
                    }
                    else if (dispersion.concentrations[j, i] > mainForm.conc_Level_2 && dispersion.concentrations[j, i + 1] < mainForm.conc_Level_2)
                    {

                       frmMainHPCM. lstCurvePoints_3.Add(dispersion.concGridPoints[j, i]);

                    }


                    if (dispersion.concentrations[j, i] >mainForm.conc_Level_1 && dispersion.concentrations[j, i - 1] <mainForm.conc_Level_1)
                    {

                       frmMainHPCM. lstCurvePoints_4.Add(dispersion.concGridPoints[j, i]);
                    }
                    else if (dispersion.concentrations[j, i] >mainForm.conc_Level_1 && dispersion.concentrations[j, i + 1] <mainForm.conc_Level_1)
                    {

                       frmMainHPCM. lstCurvePoints_4.Add(dispersion.concGridPoints[j, i]);

                    }


                }

            }



            List<PointLatLng> tempLst_1 = new List<PointLatLng>();
            List<PointLatLng> tempLst_2 = new List<PointLatLng>();



            for (int k = 0; k < frmMainHPCM.lstCurvePoints.Count - 1; k++)
            {


                if (k % 2 == 0)
                {

                    tempLst_1.Add(frmMainHPCM.lstCurvePoints[k]);
                }
                else
                {
                    tempLst_2.Add(frmMainHPCM.lstCurvePoints[k]);
                }

            }

            tempLst_2.Reverse();

            frmMainHPCM.lstCurvePoints.Clear();
            frmMainHPCM.lstCurvePoints.AddRange(tempLst_1);
            frmMainHPCM.lstCurvePoints.AddRange(tempLst_2);


            tempLst_1.Clear();
            tempLst_2.Clear();

            for (int k = 0; k < frmMainHPCM.lstCurvePoints_2.Count - 1; k++)
            {


                if (k % 2 == 0)
                {

                    tempLst_1.Add(frmMainHPCM.lstCurvePoints_2[k]);
                }
                else
                {
                    tempLst_2.Add(frmMainHPCM.lstCurvePoints_2[k]);
                }

            }

            tempLst_2.Reverse();

            frmMainHPCM.lstCurvePoints_2.Clear();
            frmMainHPCM.lstCurvePoints_2.AddRange(tempLst_1);
            frmMainHPCM.lstCurvePoints_2.AddRange(tempLst_2);


            tempLst_1.Clear();
            tempLst_2.Clear();

            for (int k = 0; k < frmMainHPCM.lstCurvePoints_3.Count - 1; k++)
            {


                if (k % 2 == 0)
                {

                    tempLst_1.Add(frmMainHPCM.lstCurvePoints_3[k]);
                }
                else
                {
                    tempLst_2.Add(frmMainHPCM.lstCurvePoints_3[k]);
                }

            }

            tempLst_2.Reverse();

            frmMainHPCM.lstCurvePoints_3.Clear();
            frmMainHPCM.lstCurvePoints_3.AddRange(tempLst_1);
            frmMainHPCM.lstCurvePoints_3.AddRange(tempLst_2);


            tempLst_1.Clear();
            tempLst_2.Clear();

            for (int k = 0; k < frmMainHPCM.lstCurvePoints_4.Count - 1; k++)
            {


                if (k % 2 == 0)
                {

                    tempLst_1.Add(frmMainHPCM.lstCurvePoints_4[k]);
                }
                else
                {
                    tempLst_2.Add(frmMainHPCM.lstCurvePoints_4[k]);
                }

            }

            tempLst_2.Reverse();

            frmMainHPCM.lstCurvePoints_4.Clear();
            frmMainHPCM.lstCurvePoints_4.AddRange(tempLst_1);
            frmMainHPCM.lstCurvePoints_4.AddRange(tempLst_2);


            drawCon = true;

            mapViewer.Paint += new PaintEventHandler(mainForm.ConPaintEventHandler);

        }


        #endregion

        private void btnDrawConc_Click(object sender, EventArgs e)
        {

                mainForm.color_Level_1 = cd1.Color;

                mainForm.color_Level_2 = cd2.Color;

                mainForm.color_Level_3 = cd3.Color;

                mainForm.color_Level_4 = cd4.Color;

                mainForm.conc_Level_1 = Convert.ToDouble(nuUpDownConcLevel_1.Value);
                mainForm.conc_Level_2 = Convert.ToDouble(nuUpDownConcLevel_2.Value);
                mainForm.conc_Level_3 = Convert.ToDouble(nuUpDownConcLevel_3.Value);
                mainForm.conc_Level_4 = Convert.ToDouble(nuUpDownConcLevel_4.Value);

            this.DrawDispersion();

            this.Close();
    
        }

  
        private void frmDrawConc_FormClosed(object sender, FormClosedEventArgs e)
        {
      

            mapViewer.Invalidate();
        }



        private void btn_Color_Lvl_1_Click(object sender, EventArgs e)
        {

            cd1.Color = mainForm.color_Level_1;
            cd1.ShowDialog(this);
            cd1.FullOpen = true;
        }

        private void btn_Color_Lvl_2_Click(object sender, EventArgs e)
        {

            cd2.Color = mainForm.color_Level_2;
            cd2.ShowDialog(this);
            cd2.FullOpen = true;
        }

        private void btn_Color_Lvl_3_Click(object sender, EventArgs e)
        {

            cd3.Color = mainForm.color_Level_3;
            cd3.ShowDialog(this);
            cd3.FullOpen = true;
        }

        private void btn_Color_Lvl_4_Click(object sender, EventArgs e)
        {

            cd4.Color = mainForm.color_Level_4;
            cd4.ShowDialog(this);
            cd4.FullOpen = true;
        }

        private void frmDrawConc_Load(object sender, EventArgs e)
        {
            cd1.Color = mainForm.color_Level_1;
            cd2.Color = mainForm.color_Level_2;
            cd3.Color = mainForm.color_Level_3;
            cd4.Color = mainForm.color_Level_4;

            btn_Color_Lvl_1.BackColor = cd1.Color;
            btn_Color_Lvl_2.BackColor = cd2.Color;
            btn_Color_Lvl_3.BackColor = cd3.Color;
            btn_Color_Lvl_4.BackColor = cd4.Color;
        }

        private void btnClearGraphics_Click(object sender, EventArgs e)
        {
            drawCon = false;
            mapViewer.Paint -= new PaintEventHandler(mainForm.ConPaintEventHandler);
            mapViewer.Invalidate();
            this.Close();

        }

    }
}
