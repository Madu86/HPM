namespace HPCM_REBUILD
{
    partial class frmDrawWind
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBoxChangeColor = new System.Windows.Forms.CheckBox();
            this.lblChangeColor = new System.Windows.Forms.Label();
            this.btnDraw = new System.Windows.Forms.Button();
            this.colorDialogWind = new System.Windows.Forms.ColorDialog();
            this.btnClearWind = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBoxChangeColor);
            this.groupBox1.Controls.Add(this.lblChangeColor);
            this.groupBox1.Font = new System.Drawing.Font("Cambria", 11.25F);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Simulate Wind";
            // 
            // chkBoxChangeColor
            // 
            this.chkBoxChangeColor.AutoSize = true;
            this.chkBoxChangeColor.Location = new System.Drawing.Point(199, 43);
            this.chkBoxChangeColor.Name = "chkBoxChangeColor";
            this.chkBoxChangeColor.Size = new System.Drawing.Size(15, 14);
            this.chkBoxChangeColor.TabIndex = 3;
            this.chkBoxChangeColor.UseVisualStyleBackColor = true;
            this.chkBoxChangeColor.CheckStateChanged += new System.EventHandler(this.chkBoxChangeColor_CheckStateChanged);
            // 
            // lblChangeColor
            // 
            this.lblChangeColor.AutoSize = true;
            this.lblChangeColor.Location = new System.Drawing.Point(16, 40);
            this.lblChangeColor.Name = "lblChangeColor";
            this.lblChangeColor.Size = new System.Drawing.Size(177, 17);
            this.lblChangeColor.TabIndex = 2;
            this.lblChangeColor.Text = "Change the Drawing Color :";
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(12, 112);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(127, 23);
            this.btnDraw.TabIndex = 1;
            this.btnDraw.Text = "Draw Wind On Map";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // colorDialogWind
            // 
            this.colorDialogWind.Color = System.Drawing.Color.Red;
            // 
            // btnClearWind
            // 
            this.btnClearWind.Location = new System.Drawing.Point(170, 112);
            this.btnClearWind.Name = "btnClearWind";
            this.btnClearWind.Size = new System.Drawing.Size(127, 23);
            this.btnClearWind.TabIndex = 2;
            this.btnClearWind.Text = "Clear Wind Field";
            this.btnClearWind.UseVisualStyleBackColor = true;
            this.btnClearWind.Click += new System.EventHandler(this.btnClearWind_Click);
            // 
            // frmDrawWind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 148);
            this.Controls.Add(this.btnClearWind);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(481, 79);
            this.Name = "frmDrawWind";
            this.Opacity = 0.98D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDrawWind_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.ColorDialog colorDialogWind;
        private System.Windows.Forms.CheckBox chkBoxChangeColor;
        private System.Windows.Forms.Label lblChangeColor;
        private System.Windows.Forms.Button btnClearWind;
    }
}