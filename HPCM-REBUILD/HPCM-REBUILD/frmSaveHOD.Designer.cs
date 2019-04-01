namespace HPCM_REBUILD
{
    partial class frmSaveHOD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaveHOD));
            this.grpBoxExplosive = new System.Windows.Forms.GroupBox();
            this.txtBoxNewExplosive = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpBoxExplosive.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxExplosive
            // 
            this.grpBoxExplosive.Controls.Add(this.txtBoxNewExplosive);
            this.grpBoxExplosive.Controls.Add(this.label1);
            this.grpBoxExplosive.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxExplosive.Location = new System.Drawing.Point(9, 12);
            this.grpBoxExplosive.Name = "grpBoxExplosive";
            this.grpBoxExplosive.Size = new System.Drawing.Size(276, 72);
            this.grpBoxExplosive.TabIndex = 2;
            this.grpBoxExplosive.TabStop = false;
            this.grpBoxExplosive.Text = "Save New Explosive Type";
            // 
            // txtBoxNewExplosive
            // 
            this.txtBoxNewExplosive.Location = new System.Drawing.Point(119, 27);
            this.txtBoxNewExplosive.Name = "txtBoxNewExplosive";
            this.txtBoxNewExplosive.Size = new System.Drawing.Size(121, 25);
            this.txtBoxNewExplosive.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Insert Name :";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(56, 99);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 32);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(163, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmSaveHOD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 152);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpBoxExplosive);
            this.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSaveHOD";
            this.Opacity = 0.98;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save New Explosive Type";
            this.grpBoxExplosive.ResumeLayout(false);
            this.grpBoxExplosive.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxExplosive;
        private System.Windows.Forms.TextBox txtBoxNewExplosive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}