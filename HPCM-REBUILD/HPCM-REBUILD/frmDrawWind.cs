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


namespace HPCM_REBUILD
{
    public partial class frmDrawWind : Form
    {

        frmMainHPCM mainForm;
        public static  int drawWindOfLayer ;
        public static Color windColor = Color.Red;




        public frmDrawWind(frmMainHPCM mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            
            //nUDLayer.Minimum = 1;
            //nUDLayer.Maximum = Meteorology.NumberOfMetCellsZ;

        }


        //Draws wind on the map viewer...




        public static bool drawWind = false;
        private void btnDraw_Click(object sender, EventArgs e)
        {

            frmMainHPCM.graphicItem = "Wind";
            drawWindOfLayer = 1;
            drawWind = true;

            if (chkBoxChangeColor.Checked == true) {

                windColor = cd.Color;
            
            }

            mainForm.mapViewer.Paint += new PaintEventHandler(mainForm.WindPaintEventHandler);
            if (mainForm.toolStripButtonDrawConc.Enabled == false)
            {
                mainForm.toolStripButtonDrawConc.Enabled = true;
            }

            if (cd != null) {
                cd.Disposed += new EventHandler(cd_Disposed);           
            }

            this.Hide();
            mainForm.Focus();
            mainForm.mapViewer.Refresh();
        }






        

        private void frmDrawWind_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMainHPCM.dw = null;
          

        }




        ColorDialog cd;


        private void chkBoxChangeColor_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkBoxChangeColor.Checked)
            {
                cd = new ColorDialog();
                cd.ShowDialog(this);
                
                cd.FullOpen = true;
                
            }


        }

        void cd_Disposed(object sender, EventArgs e)
        {

            chkBoxChangeColor.Checked = false;

        }

        private void btnClearWind_Click(object sender, EventArgs e)
        {
            drawWind = false;

            mainForm.mapViewer.ReloadMap();

            mainForm.mapViewer.Paint -= new PaintEventHandler(mainForm.WindPaintEventHandler);
            this.Close();

        }

        



    }
}
