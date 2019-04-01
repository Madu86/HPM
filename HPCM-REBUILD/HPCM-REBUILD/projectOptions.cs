using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HPCM_REBUILD
{
    public partial class projectOptions : Form
    {
        public projectOptions()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {

                //validateFields();
                this.Close();
            }
            catch(Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }


        private void validateFields() {

            try
            {

               Meteorology.NumberOfMetCellsX = Convert.ToInt32(nuNumberX.Value);

               Meteorology.NumberOfMetCellsY = Convert.ToInt32(nuNumberY.Value);

               Meteorology.NumberOfMetCellsZ = Convert.ToInt32(nuNumberZ.Value);

            }
            catch {

                throw new System.Exception("Please check the number of cells for the meteorological grid you have selected...");
            }



            try
            {
                Meteorology.HorizontalDimensions = Convert.ToInt32(txtBoxHorizontalCellWidth.Text);
            
            }
            catch
            {

                throw new System.Exception("Please check the value of the horizontal cell dimensions for the meteorological grid you have inserted...");
            }

            try
            {

                dispersion.NumOfHorizontalConcCells = Convert.ToInt32(nuNumberOfConcCells.Value);
            }
            catch {

                throw new System.Exception("Please check the value of number of cells for the concentration grid...");
            }



            try
            {

                Meteorology.TimeZone = Convert.ToDouble(txtBoxTimeZone.Text);
            }
            catch
            
            {
                throw new System.Exception("Please check the value of the time zone you have inserted...");
    
            }


            try
            {
                dispersion.CorrectionFactorOfCloudRise = Convert.ToDouble(txtBoxCorFacCloudRise.Text);

            }
            catch {
                throw new System.Exception("Please check the value of the correction factor in the cloud rise option tab..."); 
            }

            Meteorology.ConsiderHeightOfInfluence = chkBoxHeightOftheInfluence.Checked;
            


            if (chkBoxHeightOftheInfluence.Checked) {

                try {
                    Meteorology.HeightOfInfluence = Convert.ToDouble(txtBoxHeightOftheInfluence.Text);
                }
                catch {

                    throw new Exception("Please enter a value for the height of the influence...");
                }
            }

            try {
              

                double st = Convert.ToDouble(txtBoxConcSamplingTime.Text);

                if (st < 0 || st > 60) {

                    st = 60;
                
                }
                dispersion.SamplingTime = st * 60;

            
            }
            catch {

                throw new Exception("Please check the value you have inserted for the concentration sampling time...");
            }


            try
            {

                Base2.MinimumDivergence = Convert.ToDouble(txtBoxMinDiv.Text);
            }

            catch {

                throw new Exception("Please check the value of minimum divergence you have inserted..."); 
            }


            try
            {

                dispersion.ConcentrationLayerHeight = Convert.ToInt32(txtBoxConcLayerHeight.Text);
            }
            catch {

                throw new Exception("Please enter an integer value for the height at which the concentration calculations are performed..."); 
            }


            //Set boolean variables...

            try
            {


                Meteorology.ConsiderHeightOfInfluence = chkBoxHeightOftheInfluence.Checked;
                Meteorology.ConsiderRadiusOfInfluence = chkBoxRadInf.Checked;
                dispersion.IsStochasticApproach = chkBoxSto_Disp.Checked;
                dispersion.IsBayersDispersion = chkBoxBayersPuffCoeff.Checked;

            }
            catch
            {

            }


        }



        private void projectOptions_Load(object sender, EventArgs e)
        {

            if (frmMainHPCM.isFileMode == true) {

                var projectOptionsFromFile = Regex.Split(frmMainHPCM.projectDataString, "\r\n");
                var gridOptionsFromFile = projectOptionsFromFile[0].Split(',');
                nuNumberX.Value = Convert.ToDecimal(gridOptionsFromFile[0]);
                nuNumberY.Value = Convert.ToDecimal(gridOptionsFromFile[1]);
                nuNumberZ.Value = Convert.ToDecimal(gridOptionsFromFile[2]);
                txtBoxDomainHeight.Text = gridOptionsFromFile[3];
                txtBoxHorizontalCellWidth.Text = gridOptionsFromFile[4];
                nuNumberOfConcCells.Value = Convert.ToDecimal(gridOptionsFromFile[5]);

                var metOptionsFromFile = projectOptionsFromFile[1].Split(',');
                txtBoxTimeZone.Text = metOptionsFromFile[0];
                chkBoxHeightOftheInfluence.Checked = Convert.ToBoolean(metOptionsFromFile[1]);
                txtBoxHeightOftheInfluence.Text = metOptionsFromFile[2];
                chkBoxRadInf.Checked = Convert.ToBoolean(metOptionsFromFile[3]);
                txtBoxRadOfInf.Text = metOptionsFromFile[4];
                txtBoxMinDiv.Text = metOptionsFromFile[5];

                var dispersionOptionsFromFile = projectOptionsFromFile[2].Split(',');
                txtBoxConcLayerHeight.Text = dispersionOptionsFromFile[0];

                double st = Convert.ToDouble(dispersionOptionsFromFile[1]);
                st = st / 60;
                txtBoxConcSamplingTime.Text = Convert.ToString(st);
                chkBoxSto_Disp.Checked = Convert.ToBoolean(dispersionOptionsFromFile[2]);
                chkBoxBayersPuffCoeff.Checked = Convert.ToBoolean(dispersionOptionsFromFile[3]);
                txtBoxCorFacCloudRise.Text = dispersionOptionsFromFile[4];
                


            }


        }

        private void chkBoxHeightOftheInfluence_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkBoxHeightOftheInfluence.Checked)
            {

                txtBoxHeightOftheInfluence.Enabled = false;

            }
            else {

                txtBoxHeightOftheInfluence.Enabled = true;
            }
        }

        private void txtBoxMinDiv_TextChanged(object sender, EventArgs e)
        {
            toolTipProjectOptions.SetToolTip(txtBoxMinDiv, " Warning..!!! This parameter is a critical factor of the total runtime...");
            
        }

        private void txtBoxConcSamplingTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkBoxRadInf_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkBoxRadInf.Checked)
            {

                txtBoxRadOfInf.Enabled = false;

            }
            else
            {

                txtBoxRadOfInf.Enabled = true;
            }
        }





    }
}
