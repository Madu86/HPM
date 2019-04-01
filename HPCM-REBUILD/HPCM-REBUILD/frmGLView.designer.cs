namespace HPCM_REBUILD
{
    partial class Visualizer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visualizer));
            this.OGLControl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.timer3dVisualizer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // OGLControl
            // 
            this.OGLControl.AccumBits = ((byte)(0));
            this.OGLControl.AutoCheckErrors = false;
            this.OGLControl.AutoFinish = false;
            this.OGLControl.AutoMakeCurrent = true;
            this.OGLControl.AutoSwapBuffers = true;
            this.OGLControl.BackColor = System.Drawing.Color.Black;
            this.OGLControl.ColorBits = ((byte)(32));
            this.OGLControl.DepthBits = ((byte)(16));
            this.OGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OGLControl.Location = new System.Drawing.Point(0, 0);
            this.OGLControl.Name = "OGLControl";
            this.OGLControl.Size = new System.Drawing.Size(792, 566);
            this.OGLControl.StencilBits = ((byte)(0));
            this.OGLControl.TabIndex = 0;
            this.OGLControl.Paint += new System.Windows.Forms.PaintEventHandler(this.OGLControl_Paint);
            // 
            // timer3dVisualizer
            // 
            this.timer3dVisualizer.Enabled = true;
            this.timer3dVisualizer.Tick += new System.EventHandler(this.timer3dVisualizer_Tick);
            // 
            // Visualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.OGLControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Visualizer";
            this.ShowInTaskbar = false;
            this.Text = "3D Visualizer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl OGLControl;
        private System.Windows.Forms.Timer timer3dVisualizer;
    }
}

