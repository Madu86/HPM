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
    public partial class frmSaveHOD : Form
    {


        public frmSaveHOD()
        {
            InitializeComponent();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        OleDbConnection con = new OleDbConnection();
 
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

        }


        private void Save() {

            if (txtBoxNewExplosive.Text != string.Empty)
            {

                try
                {
                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "University of Colombo/Hazard Prediction Model/dbHpm.mdb");

                    con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+filePath+";Persist Security Info=True;Jet OLEDB:Database Password=keevamalamute";
                    con.Open();
                    OleDbCommand cmdAdd = new OleDbCommand();
                    cmdAdd.Connection = con;
                    cmdAdd.CommandText = "INSERT INTO explosives ([Explosive],[heatOfDetonation]) VALUES (@a,@b) ";
                    cmdAdd.Parameters.Add("@a", OleDbType.VarChar).Value = Convert.ToString(txtBoxNewExplosive.Text);
                    cmdAdd.Parameters.Add("@b", OleDbType.Double).Value = Convert.ToDouble(frmDataInput.heatOfDetonation);
                    cmdAdd.ExecuteNonQuery();
                    con.Close();
                    this.Close();
                }
                catch
                {
                    throw new System.Exception("Save failed..!");
                }

            }
            else
            {
                throw new System.Exception("Please insert a name for the new explosive type");
            }
        }



        

    }
}
