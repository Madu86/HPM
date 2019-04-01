using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;


namespace HPCM_REBUILD
{
    public partial class frmCalculateHOD : Form
    {

        Form dataInputForm;
        public frmCalculateHOD()
        {
            InitializeComponent();
        }
        public frmCalculateHOD(Form form)
        {

            this.dataInputForm = form;
            InitializeComponent();
        }



        List<TextBox> textBoxList = new List<TextBox>();


        OleDbConnection con = new OleDbConnection();
        OleDbDataAdapter da;
        DataSet ds = new DataSet();
        int maxRows = 0;


        private void frmCalculateHOD_Load(object sender, EventArgs e)
        {

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "University of Colombo/Hazard Prediction Model/dbHpm.mdb");


            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+filePath+";Persist Security Info=True;Jet OLEDB:Database Password=keevamalamute";
            con.Open();
            da=new OleDbDataAdapter("SELECT * FROM explosives",con);
            con.Close();
            da.Fill(ds, "explosivesDS");
            fillComboBox(comboBox1);
        }

        private void fillComboBox(ComboBox cmbBox) {
        
            maxRows = ds.Tables[0].Rows.Count;

            for (int p = 0; p < maxRows; p++) {

                cmbBox.Items.Add(ds.Tables[0].Rows[p].ItemArray[1]);
            }
        }



        int i = 2;
        int yBtn = 168;
        private void button1_Click(object sender, EventArgs e)
        {
            if (i < 7) {

                yBtn += 100;
                GroupBox grpBox = new GroupBox();
                grpBox.Text = "Explosive " + Convert.ToString(i);
                grpBox.Font = grpBoxExplosive.Font;

                ComboBox cmbBox = new ComboBox();
                cmbBox.Parent = grpBox;
                cmbBox.SetBounds(150, 27, 121, 25);
                fillComboBox(cmbBox);

                Label lbl1 = new Label();
                lbl1.Parent = grpBox;
                lbl1.Text = "Select Explosive :";
                lbl1.SetBounds(17, 30, 116, 17);
               

                Label lbl2 = new Label();
                lbl2.Parent = grpBox;
                lbl2.Text = "Amount (kg) :";
                lbl2.SetBounds(17, 59, 95, 17);

                TextBox txtBox = new TextBox();
                txtBox.Parent = grpBox;
                txtBox.SetBounds(150, 56, 121, 25);
                
                grpBox.Parent = this;
                grpBox.SetBounds(12, this.grpBoxExplosive.Height + 61 + 100 * (i - 2), 300, 90);
                this.btnSave.SetBounds(32, yBtn, 68, 30);
                this.btnOK.SetBounds(123, yBtn, 68, 30);
                this.btnCancel.SetBounds(214, yBtn, 68, 30);
                this.SetBounds(this.Location.X, this.Location.Y - 50, 335, this.Height + 100);


               
            } else {
                MessageBox.Show("You cannot add more than six types of explosives");
            }

            i += 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        List<double> lstExplosive = new List<double>();
        List<double> lstAmount = new List<double>();

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CalculateHeatOfDetonation();
                RefreshCmbBoxHOD(dataInputForm);
 
                this.Close();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

        }

        //Refreshes cmbBoxHOD and sets the current value of heat of detonation...
        private void RefreshCmbBoxHOD(Form form) {

            ComboBox cmbBox=(ComboBox)dataInputForm.Controls.Find("cmbBoxHOD", true).GetValue(0);
            cmbBox.Text = Convert.ToString(frmDataInput.heatOfDetonation);
        
        }

        //Calculates heat of detonation...

        private void CalculateHeatOfDetonation()
        {
           
          frmDataInput.heatOfDetonation = 0;
            foreach (var grpBox in this.Controls.OfType<GroupBox>())
            {

                int i = 0;
                foreach (var cmbBox in grpBox.Controls.OfType<ComboBox>())
                {
                    try
                    {
                        lstExplosive.Add(Convert.ToDouble(ds.Tables[0].Rows[cmbBox.SelectedIndex].ItemArray[2]));
                        i++;
                    }
                    catch
                    {

                        lstAmount.Clear();
                        lstExplosive.Clear();
                        throw new System.Exception("Please select an explosive type for the Explosive " + Convert.ToString(i + 1));
                        
                    }
                }

                int j = 0;
                foreach (var txtBox in grpBox.Controls.OfType<TextBox>())
                {
                    try
                    {
                        lstAmount.Add(Convert.ToDouble(((TextBox)txtBox).Text));
                        j++;

                    }
                    catch
                    {
                        lstAmount.Clear();
                        lstExplosive.Clear();
                        throw new System.Exception("Please check the amount value you have inserted for the Explosive " + Convert.ToString(j + 1));
                    }
                }

            }


            if (lstExplosive.Count == lstAmount.Count)
            {

                for (int a = 0; a < lstExplosive.Count; a++)
                {

                    frmDataInput.heatOfDetonation += lstExplosive[a] * lstAmount[a];
                }
            }
            else
            {

                throw new System.Exception("Please check whether you have filled all the fields properly");
            }




            double m = 0;
            foreach (double mass in lstAmount)
            {

                m += mass;
            }
            TextBox tb = (TextBox)dataInputForm.Controls.Find("txtBoxWeightOfExplosive", true).GetValue(0);
            tb.Text = Convert.ToString(m);

            lstAmount.Clear();
            lstExplosive.Clear();

        }




        private void btnSave_Click(object sender, EventArgs e)
        {

            CalculateHeatOfDetonation();
            RefreshCmbBoxHOD(dataInputForm);

            frmSaveHOD frmSaveHod = new frmSaveHOD();
            frmSaveHod.ShowDialog();

        }
        



    }
}
