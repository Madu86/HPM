namespace HPCM_REBUILD
{
    partial class projectOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(projectOptions));
            this.tbCntrlProjectOptions = new System.Windows.Forms.TabControl();
            this.tabPageGridOptions = new System.Windows.Forms.TabPage();
            this.grpGridOptions = new System.Windows.Forms.GroupBox();
            this.nuNumberOfConcCells = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.lblHorizontalCellWidthUnits = new System.Windows.Forms.Label();
            this.nuNumberZ = new System.Windows.Forms.NumericUpDown();
            this.txtBoxHorizontalCellWidth = new System.Windows.Forms.TextBox();
            this.lblNumberZ = new System.Windows.Forms.Label();
            this.lblHorizontalCellWidth = new System.Windows.Forms.Label();
            this.nuNumberY = new System.Windows.Forms.NumericUpDown();
            this.lblDomainHeightUNits = new System.Windows.Forms.Label();
            this.lblNumberY = new System.Windows.Forms.Label();
            this.txtBoxDomainHeight = new System.Windows.Forms.TextBox();
            this.nuNumberX = new System.Windows.Forms.NumericUpDown();
            this.lblDomainHeight = new System.Windows.Forms.Label();
            this.lblNumberX = new System.Windows.Forms.Label();
            this.tbPageMeteorology = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtBoxMinDiv = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.grpBoxInterpolationCriteria = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxRadOfInf = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkBoxRadInf = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInfluencingHeightUnits = new System.Windows.Forms.Label();
            this.txtBoxHeightOftheInfluence = new System.Windows.Forms.TextBox();
            this.lblInfluencingHeight = new System.Windows.Forms.Label();
            this.chkBoxHeightOftheInfluence = new System.Windows.Forms.CheckBox();
            this.lblHeightOftheInfluence = new System.Windows.Forms.Label();
            this.lblTimeZoneUnits = new System.Windows.Forms.Label();
            this.txtBoxTimeZone = new System.Windows.Forms.TextBox();
            this.lblTimeZone = new System.Windows.Forms.Label();
            this.tabPageDispersion = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblConcentrationSamplingTimeUnits = new System.Windows.Forms.Label();
            this.txtBoxConcSamplingTime = new System.Windows.Forms.TextBox();
            this.lblConcentrationSamplingTime = new System.Windows.Forms.Label();
            this.lblConcLayerHeightUnits = new System.Windows.Forms.Label();
            this.txtBoxConcLayerHeight = new System.Windows.Forms.TextBox();
            this.lblConcLayerHeight = new System.Windows.Forms.Label();
            this.grpBoxAdvanced = new System.Windows.Forms.GroupBox();
            this.txtBoxCorFacCloudRise = new System.Windows.Forms.TextBox();
            this.lblcorFacCloudRise = new System.Windows.Forms.Label();
            this.chkBoxBayersPuffCoeff = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkBoxSto_Disp = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTipProjectOptions = new System.Windows.Forms.ToolTip(this.components);
            this.tbCntrlProjectOptions.SuspendLayout();
            this.tabPageGridOptions.SuspendLayout();
            this.grpGridOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuNumberOfConcCells)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuNumberZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuNumberY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuNumberX)).BeginInit();
            this.tbPageMeteorology.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpBoxInterpolationCriteria.SuspendLayout();
            this.tabPageDispersion.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpBoxAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbCntrlProjectOptions
            // 
            this.tbCntrlProjectOptions.Controls.Add(this.tabPageGridOptions);
            this.tbCntrlProjectOptions.Controls.Add(this.tbPageMeteorology);
            this.tbCntrlProjectOptions.Controls.Add(this.tabPageDispersion);
            this.tbCntrlProjectOptions.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCntrlProjectOptions.Location = new System.Drawing.Point(12, 11);
            this.tbCntrlProjectOptions.Name = "tbCntrlProjectOptions";
            this.tbCntrlProjectOptions.SelectedIndex = 0;
            this.tbCntrlProjectOptions.Size = new System.Drawing.Size(647, 431);
            this.tbCntrlProjectOptions.TabIndex = 0;
            // 
            // tabPageGridOptions
            // 
            this.tabPageGridOptions.Controls.Add(this.grpGridOptions);
            this.tabPageGridOptions.Location = new System.Drawing.Point(4, 26);
            this.tabPageGridOptions.Name = "tabPageGridOptions";
            this.tabPageGridOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGridOptions.Size = new System.Drawing.Size(639, 401);
            this.tabPageGridOptions.TabIndex = 4;
            this.tabPageGridOptions.Text = "Computational Grid";
            this.tabPageGridOptions.ToolTipText = "Change properties of computational grid";
            this.tabPageGridOptions.UseVisualStyleBackColor = true;
            // 
            // grpGridOptions
            // 
            this.grpGridOptions.Controls.Add(this.nuNumberOfConcCells);
            this.grpGridOptions.Controls.Add(this.label9);
            this.grpGridOptions.Controls.Add(this.lblHorizontalCellWidthUnits);
            this.grpGridOptions.Controls.Add(this.nuNumberZ);
            this.grpGridOptions.Controls.Add(this.txtBoxHorizontalCellWidth);
            this.grpGridOptions.Controls.Add(this.lblNumberZ);
            this.grpGridOptions.Controls.Add(this.lblHorizontalCellWidth);
            this.grpGridOptions.Controls.Add(this.nuNumberY);
            this.grpGridOptions.Controls.Add(this.lblDomainHeightUNits);
            this.grpGridOptions.Controls.Add(this.lblNumberY);
            this.grpGridOptions.Controls.Add(this.txtBoxDomainHeight);
            this.grpGridOptions.Controls.Add(this.nuNumberX);
            this.grpGridOptions.Controls.Add(this.lblDomainHeight);
            this.grpGridOptions.Controls.Add(this.lblNumberX);
            this.grpGridOptions.Location = new System.Drawing.Point(17, 16);
            this.grpGridOptions.Name = "grpGridOptions";
            this.grpGridOptions.Size = new System.Drawing.Size(489, 331);
            this.grpGridOptions.TabIndex = 1;
            this.grpGridOptions.TabStop = false;
            this.grpGridOptions.Text = "Grid Options";
            // 
            // nuNumberOfConcCells
            // 
            this.nuNumberOfConcCells.Location = new System.Drawing.Point(322, 253);
            this.nuNumberOfConcCells.Name = "nuNumberOfConcCells";
            this.nuNumberOfConcCells.ReadOnly = true;
            this.nuNumberOfConcCells.Size = new System.Drawing.Size(59, 25);
            this.nuNumberOfConcCells.TabIndex = 9;
            this.nuNumberOfConcCells.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 253);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(283, 17);
            this.label9.TabIndex = 7;
            this.label9.Text = "Concentration Cells in Horizontal Direction :";
            // 
            // lblHorizontalCellWidthUnits
            // 
            this.lblHorizontalCellWidthUnits.AutoSize = true;
            this.lblHorizontalCellWidthUnits.Location = new System.Drawing.Point(390, 209);
            this.lblHorizontalCellWidthUnits.Name = "lblHorizontalCellWidthUnits";
            this.lblHorizontalCellWidthUnits.Size = new System.Drawing.Size(32, 17);
            this.lblHorizontalCellWidthUnits.TabIndex = 6;
            this.lblHorizontalCellWidthUnits.Text = "(m)";
            // 
            // nuNumberZ
            // 
            this.nuNumberZ.Location = new System.Drawing.Point(322, 121);
            this.nuNumberZ.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nuNumberZ.Name = "nuNumberZ";
            this.nuNumberZ.ReadOnly = true;
            this.nuNumberZ.Size = new System.Drawing.Size(59, 25);
            this.nuNumberZ.TabIndex = 5;
            this.nuNumberZ.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // txtBoxHorizontalCellWidth
            // 
            this.txtBoxHorizontalCellWidth.Location = new System.Drawing.Point(322, 206);
            this.txtBoxHorizontalCellWidth.Name = "txtBoxHorizontalCellWidth";
            this.txtBoxHorizontalCellWidth.ReadOnly = true;
            this.txtBoxHorizontalCellWidth.Size = new System.Drawing.Size(51, 25);
            this.txtBoxHorizontalCellWidth.TabIndex = 5;
            this.txtBoxHorizontalCellWidth.Text = "500";
            // 
            // lblNumberZ
            // 
            this.lblNumberZ.AutoSize = true;
            this.lblNumberZ.Location = new System.Drawing.Point(29, 123);
            this.lblNumberZ.Name = "lblNumberZ";
            this.lblNumberZ.Size = new System.Drawing.Size(202, 17);
            this.lblNumberZ.TabIndex = 4;
            this.lblNumberZ.Text = "Number of Cells in Z Direction :";
            // 
            // lblHorizontalCellWidth
            // 
            this.lblHorizontalCellWidth.AutoSize = true;
            this.lblHorizontalCellWidth.Location = new System.Drawing.Point(31, 209);
            this.lblHorizontalCellWidth.Name = "lblHorizontalCellWidth";
            this.lblHorizontalCellWidth.Size = new System.Drawing.Size(150, 17);
            this.lblHorizontalCellWidth.TabIndex = 4;
            this.lblHorizontalCellWidth.Text = "Horizontal Cell Width :";
            // 
            // nuNumberY
            // 
            this.nuNumberY.Location = new System.Drawing.Point(322, 81);
            this.nuNumberY.Name = "nuNumberY";
            this.nuNumberY.Size = new System.Drawing.Size(59, 25);
            this.nuNumberY.TabIndex = 3;
            this.nuNumberY.Value = new decimal(new int[] {
            54,
            0,
            0,
            0});
            // 
            // lblDomainHeightUNits
            // 
            this.lblDomainHeightUNits.AutoSize = true;
            this.lblDomainHeightUNits.Location = new System.Drawing.Point(390, 167);
            this.lblDomainHeightUNits.Name = "lblDomainHeightUNits";
            this.lblDomainHeightUNits.Size = new System.Drawing.Size(32, 17);
            this.lblDomainHeightUNits.TabIndex = 3;
            this.lblDomainHeightUNits.Text = "(m)";
            // 
            // lblNumberY
            // 
            this.lblNumberY.AutoSize = true;
            this.lblNumberY.Location = new System.Drawing.Point(29, 83);
            this.lblNumberY.Name = "lblNumberY";
            this.lblNumberY.Size = new System.Drawing.Size(203, 17);
            this.lblNumberY.TabIndex = 2;
            this.lblNumberY.Text = "Number of Cells in Y Direction :";
            // 
            // txtBoxDomainHeight
            // 
            this.txtBoxDomainHeight.Location = new System.Drawing.Point(322, 164);
            this.txtBoxDomainHeight.Name = "txtBoxDomainHeight";
            this.txtBoxDomainHeight.ReadOnly = true;
            this.txtBoxDomainHeight.Size = new System.Drawing.Size(51, 25);
            this.txtBoxDomainHeight.TabIndex = 2;
            this.txtBoxDomainHeight.Text = "1000";
            // 
            // nuNumberX
            // 
            this.nuNumberX.Location = new System.Drawing.Point(322, 40);
            this.nuNumberX.Name = "nuNumberX";
            this.nuNumberX.Size = new System.Drawing.Size(59, 25);
            this.nuNumberX.TabIndex = 1;
            this.nuNumberX.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lblDomainHeight
            // 
            this.lblDomainHeight.AutoSize = true;
            this.lblDomainHeight.Location = new System.Drawing.Point(31, 167);
            this.lblDomainHeight.Name = "lblDomainHeight";
            this.lblDomainHeight.Size = new System.Drawing.Size(201, 17);
            this.lblDomainHeight.TabIndex = 1;
            this.lblDomainHeight.Text = "Computational domain Height :";
            // 
            // lblNumberX
            // 
            this.lblNumberX.AutoSize = true;
            this.lblNumberX.Location = new System.Drawing.Point(29, 42);
            this.lblNumberX.Name = "lblNumberX";
            this.lblNumberX.Size = new System.Drawing.Size(203, 17);
            this.lblNumberX.TabIndex = 0;
            this.lblNumberX.Text = "Number of Cells in X Direction :";
            // 
            // tbPageMeteorology
            // 
            this.tbPageMeteorology.Controls.Add(this.groupBox3);
            this.tbPageMeteorology.Controls.Add(this.grpBoxInterpolationCriteria);
            this.tbPageMeteorology.Controls.Add(this.lblTimeZoneUnits);
            this.tbPageMeteorology.Controls.Add(this.txtBoxTimeZone);
            this.tbPageMeteorology.Controls.Add(this.lblTimeZone);
            this.tbPageMeteorology.Location = new System.Drawing.Point(4, 26);
            this.tbPageMeteorology.Name = "tbPageMeteorology";
            this.tbPageMeteorology.Padding = new System.Windows.Forms.Padding(3);
            this.tbPageMeteorology.Size = new System.Drawing.Size(639, 401);
            this.tbPageMeteorology.TabIndex = 0;
            this.tbPageMeteorology.Text = "Meteorology";
            this.tbPageMeteorology.ToolTipText = "Change a meteorological option";
            this.tbPageMeteorology.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBoxMinDiv);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(27, 293);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(329, 71);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Wind Field Divergence Minimization";
            // 
            // txtBoxMinDiv
            // 
            this.txtBoxMinDiv.Location = new System.Drawing.Point(217, 30);
            this.txtBoxMinDiv.Name = "txtBoxMinDiv";
            this.txtBoxMinDiv.Size = new System.Drawing.Size(72, 25);
            this.txtBoxMinDiv.TabIndex = 6;
            this.txtBoxMinDiv.Text = "0.0001";
            this.txtBoxMinDiv.TextChanged += new System.EventHandler(this.txtBoxMinDiv_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(202, 17);
            this.label10.TabIndex = 5;
            this.label10.Text = "Maximum allowed divergence :";
            // 
            // grpBoxInterpolationCriteria
            // 
            this.grpBoxInterpolationCriteria.Controls.Add(this.label2);
            this.grpBoxInterpolationCriteria.Controls.Add(this.txtBoxRadOfInf);
            this.grpBoxInterpolationCriteria.Controls.Add(this.label3);
            this.grpBoxInterpolationCriteria.Controls.Add(this.chkBoxRadInf);
            this.grpBoxInterpolationCriteria.Controls.Add(this.label4);
            this.grpBoxInterpolationCriteria.Controls.Add(this.lblInfluencingHeightUnits);
            this.grpBoxInterpolationCriteria.Controls.Add(this.txtBoxHeightOftheInfluence);
            this.grpBoxInterpolationCriteria.Controls.Add(this.lblInfluencingHeight);
            this.grpBoxInterpolationCriteria.Controls.Add(this.chkBoxHeightOftheInfluence);
            this.grpBoxInterpolationCriteria.Controls.Add(this.lblHeightOftheInfluence);
            this.grpBoxInterpolationCriteria.Location = new System.Drawing.Point(27, 69);
            this.grpBoxInterpolationCriteria.Name = "grpBoxInterpolationCriteria";
            this.grpBoxInterpolationCriteria.Size = new System.Drawing.Size(330, 190);
            this.grpBoxInterpolationCriteria.TabIndex = 5;
            this.grpBoxInterpolationCriteria.TabStop = false;
            this.grpBoxInterpolationCriteria.Text = "Interpolation Criteria";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "(m)";
            // 
            // txtBoxRadOfInf
            // 
            this.txtBoxRadOfInf.Location = new System.Drawing.Point(180, 155);
            this.txtBoxRadOfInf.Name = "txtBoxRadOfInf";
            this.txtBoxRadOfInf.Size = new System.Drawing.Size(52, 25);
            this.txtBoxRadOfInf.TabIndex = 10;
            this.txtBoxRadOfInf.Text = "5000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Radius of the Influence :";
            // 
            // chkBoxRadInf
            // 
            this.chkBoxRadInf.AutoSize = true;
            this.chkBoxRadInf.Location = new System.Drawing.Point(238, 120);
            this.chkBoxRadInf.Name = "chkBoxRadInf";
            this.chkBoxRadInf.Size = new System.Drawing.Size(15, 14);
            this.chkBoxRadInf.TabIndex = 8;
            this.toolTipProjectOptions.SetToolTip(this.chkBoxRadInf, "Indicates whether the height of the meteoroogical station should be considered in" +
                    " meteorological data interpolation.");
            this.chkBoxRadInf.UseVisualStyleBackColor = true;
            this.chkBoxRadInf.CheckedChanged += new System.EventHandler(this.chkBoxRadInf_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Consider radius of the Influence :";
            // 
            // lblInfluencingHeightUnits
            // 
            this.lblInfluencingHeightUnits.AutoSize = true;
            this.lblInfluencingHeightUnits.Location = new System.Drawing.Point(249, 76);
            this.lblInfluencingHeightUnits.Name = "lblInfluencingHeightUnits";
            this.lblInfluencingHeightUnits.Size = new System.Drawing.Size(32, 17);
            this.lblInfluencingHeightUnits.TabIndex = 6;
            this.lblInfluencingHeightUnits.Text = "(m)";
            // 
            // txtBoxHeightOftheInfluence
            // 
            this.txtBoxHeightOftheInfluence.Location = new System.Drawing.Point(180, 73);
            this.txtBoxHeightOftheInfluence.Name = "txtBoxHeightOftheInfluence";
            this.txtBoxHeightOftheInfluence.Size = new System.Drawing.Size(52, 25);
            this.txtBoxHeightOftheInfluence.TabIndex = 5;
            this.txtBoxHeightOftheInfluence.Text = "100";
            // 
            // lblInfluencingHeight
            // 
            this.lblInfluencingHeight.AutoSize = true;
            this.lblInfluencingHeight.Location = new System.Drawing.Point(17, 76);
            this.lblInfluencingHeight.Name = "lblInfluencingHeight";
            this.lblInfluencingHeight.Size = new System.Drawing.Size(157, 17);
            this.lblInfluencingHeight.TabIndex = 4;
            this.lblInfluencingHeight.Text = "Height of the Influence :";
            // 
            // chkBoxHeightOftheInfluence
            // 
            this.chkBoxHeightOftheInfluence.AutoSize = true;
            this.chkBoxHeightOftheInfluence.Location = new System.Drawing.Point(238, 35);
            this.chkBoxHeightOftheInfluence.Name = "chkBoxHeightOftheInfluence";
            this.chkBoxHeightOftheInfluence.Size = new System.Drawing.Size(15, 14);
            this.chkBoxHeightOftheInfluence.TabIndex = 1;
            this.toolTipProjectOptions.SetToolTip(this.chkBoxHeightOftheInfluence, "Indicates whether the height of the meteoroogical station should be considered in" +
                    " meteorological data interpolation.");
            this.chkBoxHeightOftheInfluence.UseVisualStyleBackColor = true;
            this.chkBoxHeightOftheInfluence.CheckedChanged += new System.EventHandler(this.chkBoxHeightOftheInfluence_CheckedChanged);
            // 
            // lblHeightOftheInfluence
            // 
            this.lblHeightOftheInfluence.AutoSize = true;
            this.lblHeightOftheInfluence.Location = new System.Drawing.Point(17, 32);
            this.lblHeightOftheInfluence.Name = "lblHeightOftheInfluence";
            this.lblHeightOftheInfluence.Size = new System.Drawing.Size(215, 17);
            this.lblHeightOftheInfluence.TabIndex = 0;
            this.lblHeightOftheInfluence.Text = "Consider Height of the Influence :";
            // 
            // lblTimeZoneUnits
            // 
            this.lblTimeZoneUnits.AutoSize = true;
            this.lblTimeZoneUnits.Location = new System.Drawing.Point(184, 28);
            this.lblTimeZoneUnits.Name = "lblTimeZoneUnits";
            this.lblTimeZoneUnits.Size = new System.Drawing.Size(40, 17);
            this.lblTimeZoneUnits.TabIndex = 4;
            this.lblTimeZoneUnits.Text = "(hrs)";
            // 
            // txtBoxTimeZone
            // 
            this.txtBoxTimeZone.Location = new System.Drawing.Point(111, 25);
            this.txtBoxTimeZone.Name = "txtBoxTimeZone";
            this.txtBoxTimeZone.Size = new System.Drawing.Size(58, 25);
            this.txtBoxTimeZone.TabIndex = 3;
            this.txtBoxTimeZone.Text = "+5.5";
            // 
            // lblTimeZone
            // 
            this.lblTimeZone.AutoSize = true;
            this.lblTimeZone.Location = new System.Drawing.Point(24, 28);
            this.lblTimeZone.Name = "lblTimeZone";
            this.lblTimeZone.Size = new System.Drawing.Size(81, 17);
            this.lblTimeZone.TabIndex = 2;
            this.lblTimeZone.Text = "Time Zone :";
            // 
            // tabPageDispersion
            // 
            this.tabPageDispersion.Controls.Add(this.groupBox1);
            this.tabPageDispersion.Controls.Add(this.grpBoxAdvanced);
            this.tabPageDispersion.Location = new System.Drawing.Point(4, 26);
            this.tabPageDispersion.Name = "tabPageDispersion";
            this.tabPageDispersion.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDispersion.Size = new System.Drawing.Size(639, 401);
            this.tabPageDispersion.TabIndex = 2;
            this.tabPageDispersion.Text = "Dispersion";
            this.tabPageDispersion.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblConcentrationSamplingTimeUnits);
            this.groupBox1.Controls.Add(this.txtBoxConcSamplingTime);
            this.groupBox1.Controls.Add(this.lblConcentrationSamplingTime);
            this.groupBox1.Controls.Add(this.lblConcLayerHeightUnits);
            this.groupBox1.Controls.Add(this.txtBoxConcLayerHeight);
            this.groupBox1.Controls.Add(this.lblConcLayerHeight);
            this.groupBox1.Location = new System.Drawing.Point(24, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 147);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sampling ";
            // 
            // lblConcentrationSamplingTimeUnits
            // 
            this.lblConcentrationSamplingTimeUnits.AutoSize = true;
            this.lblConcentrationSamplingTimeUnits.Location = new System.Drawing.Point(439, 76);
            this.lblConcentrationSamplingTimeUnits.Name = "lblConcentrationSamplingTimeUnits";
            this.lblConcentrationSamplingTimeUnits.Size = new System.Drawing.Size(70, 17);
            this.lblConcentrationSamplingTimeUnits.TabIndex = 5;
            this.lblConcentrationSamplingTimeUnits.Text = "(minutes)";
            // 
            // txtBoxConcSamplingTime
            // 
            this.txtBoxConcSamplingTime.Location = new System.Drawing.Point(369, 73);
            this.txtBoxConcSamplingTime.Name = "txtBoxConcSamplingTime";
            this.txtBoxConcSamplingTime.Size = new System.Drawing.Size(46, 25);
            this.txtBoxConcSamplingTime.TabIndex = 4;
            this.txtBoxConcSamplingTime.Text = "30";
            this.txtBoxConcSamplingTime.TextChanged += new System.EventHandler(this.txtBoxConcSamplingTime_TextChanged);
            // 
            // lblConcentrationSamplingTime
            // 
            this.lblConcentrationSamplingTime.AutoSize = true;
            this.lblConcentrationSamplingTime.Location = new System.Drawing.Point(15, 76);
            this.lblConcentrationSamplingTime.Name = "lblConcentrationSamplingTime";
            this.lblConcentrationSamplingTime.Size = new System.Drawing.Size(199, 17);
            this.lblConcentrationSamplingTime.TabIndex = 3;
            this.lblConcentrationSamplingTime.Text = "Concentration Sampling Time :";
            // 
            // lblConcLayerHeightUnits
            // 
            this.lblConcLayerHeightUnits.AutoSize = true;
            this.lblConcLayerHeightUnits.Location = new System.Drawing.Point(445, 39);
            this.lblConcLayerHeightUnits.Name = "lblConcLayerHeightUnits";
            this.lblConcLayerHeightUnits.Size = new System.Drawing.Size(32, 17);
            this.lblConcLayerHeightUnits.TabIndex = 2;
            this.lblConcLayerHeightUnits.Text = "(m)";
            // 
            // txtBoxConcLayerHeight
            // 
            this.txtBoxConcLayerHeight.Location = new System.Drawing.Point(369, 36);
            this.txtBoxConcLayerHeight.Name = "txtBoxConcLayerHeight";
            this.txtBoxConcLayerHeight.Size = new System.Drawing.Size(59, 25);
            this.txtBoxConcLayerHeight.TabIndex = 1;
            this.txtBoxConcLayerHeight.Text = "1";
            // 
            // lblConcLayerHeight
            // 
            this.lblConcLayerHeight.AutoSize = true;
            this.lblConcLayerHeight.Location = new System.Drawing.Point(15, 39);
            this.lblConcLayerHeight.Name = "lblConcLayerHeight";
            this.lblConcLayerHeight.Size = new System.Drawing.Size(346, 17);
            this.lblConcLayerHeight.TabIndex = 0;
            this.lblConcLayerHeight.Text = "Reference height for the Calculation of Concentration :";
            // 
            // grpBoxAdvanced
            // 
            this.grpBoxAdvanced.Controls.Add(this.txtBoxCorFacCloudRise);
            this.grpBoxAdvanced.Controls.Add(this.lblcorFacCloudRise);
            this.grpBoxAdvanced.Controls.Add(this.chkBoxBayersPuffCoeff);
            this.grpBoxAdvanced.Controls.Add(this.label6);
            this.grpBoxAdvanced.Controls.Add(this.chkBoxSto_Disp);
            this.grpBoxAdvanced.Controls.Add(this.label5);
            this.grpBoxAdvanced.Location = new System.Drawing.Point(24, 202);
            this.grpBoxAdvanced.Name = "grpBoxAdvanced";
            this.grpBoxAdvanced.Size = new System.Drawing.Size(521, 146);
            this.grpBoxAdvanced.TabIndex = 2;
            this.grpBoxAdvanced.TabStop = false;
            this.grpBoxAdvanced.Text = "Advanced Dispersion Options";
            // 
            // txtBoxCorFacCloudRise
            // 
            this.txtBoxCorFacCloudRise.Location = new System.Drawing.Point(259, 100);
            this.txtBoxCorFacCloudRise.Name = "txtBoxCorFacCloudRise";
            this.txtBoxCorFacCloudRise.Size = new System.Drawing.Size(58, 25);
            this.txtBoxCorFacCloudRise.TabIndex = 10;
            this.txtBoxCorFacCloudRise.Text = "1";
            // 
            // lblcorFacCloudRise
            // 
            this.lblcorFacCloudRise.AutoSize = true;
            this.lblcorFacCloudRise.Location = new System.Drawing.Point(15, 103);
            this.lblcorFacCloudRise.Name = "lblcorFacCloudRise";
            this.lblcorFacCloudRise.Size = new System.Drawing.Size(238, 17);
            this.lblcorFacCloudRise.TabIndex = 9;
            this.lblcorFacCloudRise.Text = "Correction Factor for the Cloud Rise :";
            // 
            // chkBoxBayersPuffCoeff
            // 
            this.chkBoxBayersPuffCoeff.AutoSize = true;
            this.chkBoxBayersPuffCoeff.Location = new System.Drawing.Point(387, 75);
            this.chkBoxBayersPuffCoeff.Name = "chkBoxBayersPuffCoeff";
            this.chkBoxBayersPuffCoeff.Size = new System.Drawing.Size(15, 14);
            this.chkBoxBayersPuffCoeff.TabIndex = 3;
            this.chkBoxBayersPuffCoeff.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(339, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Use Bayer\'s instantaneous puff diffusion coefficients:";
            // 
            // chkBoxSto_Disp
            // 
            this.chkBoxSto_Disp.AutoSize = true;
            this.chkBoxSto_Disp.Location = new System.Drawing.Point(387, 42);
            this.chkBoxSto_Disp.Name = "chkBoxSto_Disp";
            this.chkBoxSto_Disp.Size = new System.Drawing.Size(15, 14);
            this.chkBoxSto_Disp.TabIndex = 1;
            this.chkBoxSto_Disp.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(366, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Follow stochastic approach for instantaneous dispersion :";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Cambria", 9F);
            this.btnOk.Location = new System.Drawing.Point(240, 448);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 27);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Cambria", 9F);
            this.btnCancel.Location = new System.Drawing.Point(345, 448);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // projectOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 487);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbCntrlProjectOptions);
            this.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "projectOptions";
            this.Opacity = 0.98D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Options";
            this.Load += new System.EventHandler(this.projectOptions_Load);
            this.tbCntrlProjectOptions.ResumeLayout(false);
            this.tabPageGridOptions.ResumeLayout(false);
            this.grpGridOptions.ResumeLayout(false);
            this.grpGridOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuNumberOfConcCells)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuNumberZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuNumberY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuNumberX)).EndInit();
            this.tbPageMeteorology.ResumeLayout(false);
            this.tbPageMeteorology.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpBoxInterpolationCriteria.ResumeLayout(false);
            this.grpBoxInterpolationCriteria.PerformLayout();
            this.tabPageDispersion.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpBoxAdvanced.ResumeLayout(false);
            this.grpBoxAdvanced.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbCntrlProjectOptions;
        private System.Windows.Forms.TabPage tbPageMeteorology;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabPageDispersion;
        private System.Windows.Forms.Label lblTimeZone;
        private System.Windows.Forms.Label lblTimeZoneUnits;
        private System.Windows.Forms.TextBox txtBoxTimeZone;
        private System.Windows.Forms.TabPage tabPageGridOptions;
        private System.Windows.Forms.GroupBox grpGridOptions;
        private System.Windows.Forms.Label lblHorizontalCellWidthUnits;
        private System.Windows.Forms.NumericUpDown nuNumberZ;
        private System.Windows.Forms.TextBox txtBoxHorizontalCellWidth;
        private System.Windows.Forms.Label lblNumberZ;
        private System.Windows.Forms.Label lblHorizontalCellWidth;
        private System.Windows.Forms.NumericUpDown nuNumberY;
        private System.Windows.Forms.Label lblDomainHeightUNits;
        private System.Windows.Forms.Label lblNumberY;
        private System.Windows.Forms.TextBox txtBoxDomainHeight;
        private System.Windows.Forms.NumericUpDown nuNumberX;
        private System.Windows.Forms.Label lblDomainHeight;
        private System.Windows.Forms.Label lblNumberX;
        private System.Windows.Forms.GroupBox grpBoxInterpolationCriteria;
        private System.Windows.Forms.CheckBox chkBoxHeightOftheInfluence;
        private System.Windows.Forms.Label lblHeightOftheInfluence;
        private System.Windows.Forms.ToolTip toolTipProjectOptions;
        private System.Windows.Forms.Label lblInfluencingHeightUnits;
        private System.Windows.Forms.TextBox txtBoxHeightOftheInfluence;
        private System.Windows.Forms.Label lblInfluencingHeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nuNumberOfConcCells;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBoxMinDiv;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxRadOfInf;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkBoxRadInf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpBoxAdvanced;
        private System.Windows.Forms.CheckBox chkBoxBayersPuffCoeff;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkBoxSto_Disp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblConcentrationSamplingTimeUnits;
        private System.Windows.Forms.TextBox txtBoxConcSamplingTime;
        private System.Windows.Forms.Label lblConcentrationSamplingTime;
        private System.Windows.Forms.Label lblConcLayerHeightUnits;
        private System.Windows.Forms.TextBox txtBoxConcLayerHeight;
        private System.Windows.Forms.Label lblConcLayerHeight;
        private System.Windows.Forms.TextBox txtBoxCorFacCloudRise;
        private System.Windows.Forms.Label lblcorFacCloudRise;
    }
}