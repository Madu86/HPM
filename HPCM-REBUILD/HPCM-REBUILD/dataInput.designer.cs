namespace HPCM_REBUILD
{
    partial class frmDataInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataInput));
            this.dataInputTab = new System.Windows.Forms.TabControl();
            this.sourceDataTab = new System.Windows.Forms.TabPage();
            this.grpBoxSemiContinuous = new System.Windows.Forms.GroupBox();
            this.lblSourceRadiusUnits = new System.Windows.Forms.Label();
            this.txtBoxSourceRadius = new System.Windows.Forms.TextBox();
            this.lblSourceRadius = new System.Windows.Forms.Label();
            this.lblReleaseRateUnits = new System.Windows.Forms.Label();
            this.txtBoxReleaseRate = new System.Windows.Forms.TextBox();
            this.lblReleaseRate = new System.Windows.Forms.Label();
            this.lblSourceTempUnits = new System.Windows.Forms.Label();
            this.txtBoxSourceTemp = new System.Windows.Forms.TextBox();
            this.lblSourceTemp = new System.Windows.Forms.Label();
            this.grpBoxInstantaneous = new System.Windows.Forms.GroupBox();
            this.lblExplosiveWeightUnits = new System.Windows.Forms.Label();
            this.txtBoxWeightOfExplosive = new System.Windows.Forms.TextBox();
            this.lblExplosiveWeight = new System.Windows.Forms.Label();
            this.lblEffHeatPercentage = new System.Windows.Forms.Label();
            this.tbarEffectiveHeatPercentage = new System.Windows.Forms.TrackBar();
            this.lblEffectiveHeatPercentage = new System.Windows.Forms.Label();
            this.chkBoxHODNewComposition = new System.Windows.Forms.CheckBox();
            this.lblHODNewComposition = new System.Windows.Forms.Label();
            this.lblEffectivePercentage = new System.Windows.Forms.Label();
            this.tBarEffectiveAgentPercentage = new System.Windows.Forms.TrackBar();
            this.lblEffectiveAgentPercentage = new System.Windows.Forms.Label();
            this.cmbBoxChemicalAgent = new System.Windows.Forms.ComboBox();
            this.lblAgentWeightUnits = new System.Windows.Forms.Label();
            this.txtBoxAgentWeight = new System.Windows.Forms.TextBox();
            this.lblAgentWeight = new System.Windows.Forms.Label();
            this.lblChemicalAgent = new System.Windows.Forms.Label();
            this.cmbBoxHOD = new System.Windows.Forms.ComboBox();
            this.lblHeatOfDetonationUnits = new System.Windows.Forms.Label();
            this.lblHeatOfDetonation = new System.Windows.Forms.Label();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerMetData = new System.Windows.Forms.DateTimePicker();
            this.grpBoxContinuous = new System.Windows.Forms.GroupBox();
            this.geoDataTab = new System.Windows.Forms.TabPage();
            this.grpBoxTerrainData = new System.Windows.Forms.GroupBox();
            this.chkBoxSurfaceRoughnessLength = new System.Windows.Forms.CheckBox();
            this.lblRoughnessLengthChkBox = new System.Windows.Forms.Label();
            this.rchTxtBoxTerrainType = new System.Windows.Forms.RichTextBox();
            this.lblRoughnessLengthUnits = new System.Windows.Forms.Label();
            this.lblRoughnessLength = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBoxTerrainType = new System.Windows.Forms.ComboBox();
            this.txtBoxSurfaceRoughnessLength = new System.Windows.Forms.TextBox();
            this.groupBox33 = new System.Windows.Forms.GroupBox();
            this.lblMoistureAvailabilityPercentage = new System.Windows.Forms.Label();
            this.lblSurfaceMoistureAvailability = new System.Windows.Forms.Label();
            this.txtBoxMoistureAvailability = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxAlbedo = new System.Windows.Forms.TextBox();
            this.chkBoxAlbedoValue = new System.Windows.Forms.CheckBox();
            this.lblAlbedoChkBox = new System.Windows.Forms.Label();
            this.cmbBoxSurfaceType = new System.Windows.Forms.ComboBox();
            this.lblSurfaceType = new System.Windows.Forms.Label();
            this.btnInputOK = new System.Windows.Forms.Button();
            this.toolTipMetInput = new System.Windows.Forms.ToolTip(this.components);
            this.errorProviderMetInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataInputTab.SuspendLayout();
            this.sourceDataTab.SuspendLayout();
            this.grpBoxSemiContinuous.SuspendLayout();
            this.grpBoxInstantaneous.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarEffectiveHeatPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarEffectiveAgentPercentage)).BeginInit();
            this.groupBox19.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.geoDataTab.SuspendLayout();
            this.grpBoxTerrainData.SuspendLayout();
            this.groupBox33.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderMetInput)).BeginInit();
            this.SuspendLayout();
            // 
            // dataInputTab
            // 
            this.dataInputTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataInputTab.Controls.Add(this.sourceDataTab);
            this.dataInputTab.Controls.Add(this.geoDataTab);
            this.dataInputTab.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataInputTab.Location = new System.Drawing.Point(12, 11);
            this.dataInputTab.Name = "dataInputTab";
            this.dataInputTab.SelectedIndex = 0;
            this.dataInputTab.Size = new System.Drawing.Size(949, 555);
            this.dataInputTab.TabIndex = 0;
            // 
            // sourceDataTab
            // 
            this.sourceDataTab.Controls.Add(this.grpBoxSemiContinuous);
            this.sourceDataTab.Controls.Add(this.grpBoxInstantaneous);
            this.sourceDataTab.Controls.Add(this.groupBox19);
            this.sourceDataTab.Controls.Add(this.groupBox10);
            this.sourceDataTab.Controls.Add(this.grpBoxContinuous);
            this.sourceDataTab.Location = new System.Drawing.Point(4, 26);
            this.sourceDataTab.Name = "sourceDataTab";
            this.sourceDataTab.Padding = new System.Windows.Forms.Padding(3);
            this.sourceDataTab.Size = new System.Drawing.Size(941, 525);
            this.sourceDataTab.TabIndex = 2;
            this.sourceDataTab.Text = "Source Term";
            this.sourceDataTab.UseVisualStyleBackColor = true;
            // 
            // grpBoxSemiContinuous
            // 
            this.grpBoxSemiContinuous.Controls.Add(this.lblSourceRadiusUnits);
            this.grpBoxSemiContinuous.Controls.Add(this.txtBoxSourceRadius);
            this.grpBoxSemiContinuous.Controls.Add(this.lblSourceRadius);
            this.grpBoxSemiContinuous.Controls.Add(this.lblReleaseRateUnits);
            this.grpBoxSemiContinuous.Controls.Add(this.txtBoxReleaseRate);
            this.grpBoxSemiContinuous.Controls.Add(this.lblReleaseRate);
            this.grpBoxSemiContinuous.Controls.Add(this.lblSourceTempUnits);
            this.grpBoxSemiContinuous.Controls.Add(this.txtBoxSourceTemp);
            this.grpBoxSemiContinuous.Controls.Add(this.lblSourceTemp);
            this.grpBoxSemiContinuous.Location = new System.Drawing.Point(12, 142);
            this.grpBoxSemiContinuous.Name = "grpBoxSemiContinuous";
            this.grpBoxSemiContinuous.Size = new System.Drawing.Size(873, 336);
            this.grpBoxSemiContinuous.TabIndex = 2;
            this.grpBoxSemiContinuous.TabStop = false;
            this.grpBoxSemiContinuous.Text = "Semi-Continuous Source Details";
            // 
            // lblSourceRadiusUnits
            // 
            this.lblSourceRadiusUnits.AutoSize = true;
            this.lblSourceRadiusUnits.Location = new System.Drawing.Point(306, 147);
            this.lblSourceRadiusUnits.Name = "lblSourceRadiusUnits";
            this.lblSourceRadiusUnits.Size = new System.Drawing.Size(32, 17);
            this.lblSourceRadiusUnits.TabIndex = 20;
            this.lblSourceRadiusUnits.Text = "(m)";
            // 
            // txtBoxSourceRadius
            // 
            this.txtBoxSourceRadius.Location = new System.Drawing.Point(184, 144);
            this.txtBoxSourceRadius.Name = "txtBoxSourceRadius";
            this.txtBoxSourceRadius.Size = new System.Drawing.Size(101, 25);
            this.txtBoxSourceRadius.TabIndex = 18;
            // 
            // lblSourceRadius
            // 
            this.lblSourceRadius.AutoSize = true;
            this.lblSourceRadius.Location = new System.Drawing.Point(16, 147);
            this.lblSourceRadius.Name = "lblSourceRadius";
            this.lblSourceRadius.Size = new System.Drawing.Size(103, 17);
            this.lblSourceRadius.TabIndex = 19;
            this.lblSourceRadius.Text = "Source Radius :";
            // 
            // lblReleaseRateUnits
            // 
            this.lblReleaseRateUnits.AutoSize = true;
            this.lblReleaseRateUnits.Location = new System.Drawing.Point(306, 94);
            this.lblReleaseRateUnits.Name = "lblReleaseRateUnits";
            this.lblReleaseRateUnits.Size = new System.Drawing.Size(48, 17);
            this.lblReleaseRateUnits.TabIndex = 17;
            this.lblReleaseRateUnits.Text = "(kg/s)";
            // 
            // txtBoxReleaseRate
            // 
            this.txtBoxReleaseRate.Location = new System.Drawing.Point(184, 91);
            this.txtBoxReleaseRate.Name = "txtBoxReleaseRate";
            this.txtBoxReleaseRate.Size = new System.Drawing.Size(101, 25);
            this.txtBoxReleaseRate.TabIndex = 15;
            // 
            // lblReleaseRate
            // 
            this.lblReleaseRate.AutoSize = true;
            this.lblReleaseRate.Location = new System.Drawing.Point(16, 94);
            this.lblReleaseRate.Name = "lblReleaseRate";
            this.lblReleaseRate.Size = new System.Drawing.Size(93, 17);
            this.lblReleaseRate.TabIndex = 16;
            this.lblReleaseRate.Text = "Release Rate :";
            // 
            // lblSourceTempUnits
            // 
            this.lblSourceTempUnits.AutoSize = true;
            this.lblSourceTempUnits.Location = new System.Drawing.Point(306, 40);
            this.lblSourceTempUnits.Name = "lblSourceTempUnits";
            this.lblSourceTempUnits.Size = new System.Drawing.Size(34, 17);
            this.lblSourceTempUnits.TabIndex = 14;
            this.lblSourceTempUnits.Text = "(°C)";
            // 
            // txtBoxSourceTemp
            // 
            this.txtBoxSourceTemp.Location = new System.Drawing.Point(184, 37);
            this.txtBoxSourceTemp.Name = "txtBoxSourceTemp";
            this.txtBoxSourceTemp.Size = new System.Drawing.Size(101, 25);
            this.txtBoxSourceTemp.TabIndex = 12;
            // 
            // lblSourceTemp
            // 
            this.lblSourceTemp.AutoSize = true;
            this.lblSourceTemp.Location = new System.Drawing.Point(16, 40);
            this.lblSourceTemp.Name = "lblSourceTemp";
            this.lblSourceTemp.Size = new System.Drawing.Size(142, 17);
            this.lblSourceTemp.TabIndex = 13;
            this.lblSourceTemp.Text = "Source Temperature :";
            // 
            // grpBoxInstantaneous
            // 
            this.grpBoxInstantaneous.Controls.Add(this.lblExplosiveWeightUnits);
            this.grpBoxInstantaneous.Controls.Add(this.txtBoxWeightOfExplosive);
            this.grpBoxInstantaneous.Controls.Add(this.lblExplosiveWeight);
            this.grpBoxInstantaneous.Controls.Add(this.lblEffHeatPercentage);
            this.grpBoxInstantaneous.Controls.Add(this.tbarEffectiveHeatPercentage);
            this.grpBoxInstantaneous.Controls.Add(this.lblEffectiveHeatPercentage);
            this.grpBoxInstantaneous.Controls.Add(this.chkBoxHODNewComposition);
            this.grpBoxInstantaneous.Controls.Add(this.lblHODNewComposition);
            this.grpBoxInstantaneous.Controls.Add(this.lblEffectivePercentage);
            this.grpBoxInstantaneous.Controls.Add(this.tBarEffectiveAgentPercentage);
            this.grpBoxInstantaneous.Controls.Add(this.lblEffectiveAgentPercentage);
            this.grpBoxInstantaneous.Controls.Add(this.cmbBoxChemicalAgent);
            this.grpBoxInstantaneous.Controls.Add(this.lblAgentWeightUnits);
            this.grpBoxInstantaneous.Controls.Add(this.txtBoxAgentWeight);
            this.grpBoxInstantaneous.Controls.Add(this.lblAgentWeight);
            this.grpBoxInstantaneous.Controls.Add(this.lblChemicalAgent);
            this.grpBoxInstantaneous.Controls.Add(this.cmbBoxHOD);
            this.grpBoxInstantaneous.Controls.Add(this.lblHeatOfDetonationUnits);
            this.grpBoxInstantaneous.Controls.Add(this.lblHeatOfDetonation);
            this.grpBoxInstantaneous.Location = new System.Drawing.Point(12, 141);
            this.grpBoxInstantaneous.Name = "grpBoxInstantaneous";
            this.grpBoxInstantaneous.Size = new System.Drawing.Size(908, 369);
            this.grpBoxInstantaneous.TabIndex = 1;
            this.grpBoxInstantaneous.TabStop = false;
            this.grpBoxInstantaneous.Text = "Instantaneous Source Details";
            // 
            // lblExplosiveWeightUnits
            // 
            this.lblExplosiveWeightUnits.AutoSize = true;
            this.lblExplosiveWeightUnits.Location = new System.Drawing.Point(363, 89);
            this.lblExplosiveWeightUnits.Name = "lblExplosiveWeightUnits";
            this.lblExplosiveWeightUnits.Size = new System.Drawing.Size(35, 17);
            this.lblExplosiveWeightUnits.TabIndex = 20;
            this.lblExplosiveWeightUnits.Text = "(kg)";
            // 
            // txtBoxWeightOfExplosive
            // 
            this.txtBoxWeightOfExplosive.Location = new System.Drawing.Point(227, 86);
            this.txtBoxWeightOfExplosive.Name = "txtBoxWeightOfExplosive";
            this.txtBoxWeightOfExplosive.Size = new System.Drawing.Size(101, 25);
            this.txtBoxWeightOfExplosive.TabIndex = 19;
            // 
            // lblExplosiveWeight
            // 
            this.lblExplosiveWeight.AutoSize = true;
            this.lblExplosiveWeight.Location = new System.Drawing.Point(16, 89);
            this.lblExplosiveWeight.Name = "lblExplosiveWeight";
            this.lblExplosiveWeight.Size = new System.Drawing.Size(162, 17);
            this.lblExplosiveWeight.TabIndex = 18;
            this.lblExplosiveWeight.Text = "Weight of the Explosive :";
            // 
            // lblEffHeatPercentage
            // 
            this.lblEffHeatPercentage.AutoSize = true;
            this.lblEffHeatPercentage.Location = new System.Drawing.Point(419, 147);
            this.lblEffHeatPercentage.Name = "lblEffHeatPercentage";
            this.lblEffHeatPercentage.Size = new System.Drawing.Size(48, 17);
            this.lblEffHeatPercentage.TabIndex = 17;
            this.lblEffHeatPercentage.Text = "100 %";
            // 
            // tbarEffectiveHeatPercentage
            // 
            this.tbarEffectiveHeatPercentage.BackColor = System.Drawing.Color.White;
            this.tbarEffectiveHeatPercentage.Location = new System.Drawing.Point(219, 138);
            this.tbarEffectiveHeatPercentage.Maximum = 20;
            this.tbarEffectiveHeatPercentage.Name = "tbarEffectiveHeatPercentage";
            this.tbarEffectiveHeatPercentage.Size = new System.Drawing.Size(191, 45);
            this.tbarEffectiveHeatPercentage.TabIndex = 16;
            this.tbarEffectiveHeatPercentage.Value = 20;
            this.tbarEffectiveHeatPercentage.Scroll += new System.EventHandler(this.tbarEffectiveHeatPercentage_Scroll);
            // 
            // lblEffectiveHeatPercentage
            // 
            this.lblEffectiveHeatPercentage.AutoSize = true;
            this.lblEffectiveHeatPercentage.Location = new System.Drawing.Point(16, 148);
            this.lblEffectiveHeatPercentage.Name = "lblEffectiveHeatPercentage";
            this.lblEffectiveHeatPercentage.Size = new System.Drawing.Size(177, 17);
            this.lblEffectiveHeatPercentage.TabIndex = 15;
            this.lblEffectiveHeatPercentage.Text = "Effective Heat Percentage :";
            // 
            // chkBoxHODNewComposition
            // 
            this.chkBoxHODNewComposition.AutoSize = true;
            this.chkBoxHODNewComposition.Location = new System.Drawing.Point(834, 37);
            this.chkBoxHODNewComposition.Name = "chkBoxHODNewComposition";
            this.chkBoxHODNewComposition.Size = new System.Drawing.Size(15, 14);
            this.chkBoxHODNewComposition.TabIndex = 14;
            this.chkBoxHODNewComposition.UseVisualStyleBackColor = true;
            this.chkBoxHODNewComposition.CheckedChanged += new System.EventHandler(this.chkBoxHODNewComposition_CheckedChanged);
            // 
            // lblHODNewComposition
            // 
            this.lblHODNewComposition.AutoSize = true;
            this.lblHODNewComposition.Location = new System.Drawing.Point(459, 34);
            this.lblHODNewComposition.Name = "lblHODNewComposition";
            this.lblHODNewComposition.Size = new System.Drawing.Size(365, 17);
            this.lblHODNewComposition.TabIndex = 13;
            this.lblHODNewComposition.Text = "Calculate the Heat of Detonation for  a New Composition :";
            // 
            // lblEffectivePercentage
            // 
            this.lblEffectivePercentage.AutoSize = true;
            this.lblEffectivePercentage.Location = new System.Drawing.Point(419, 327);
            this.lblEffectivePercentage.Name = "lblEffectivePercentage";
            this.lblEffectivePercentage.Size = new System.Drawing.Size(48, 17);
            this.lblEffectivePercentage.TabIndex = 12;
            this.lblEffectivePercentage.Text = "100 %";
            // 
            // tBarEffectiveAgentPercentage
            // 
            this.tBarEffectiveAgentPercentage.BackColor = System.Drawing.Color.White;
            this.tBarEffectiveAgentPercentage.Location = new System.Drawing.Point(219, 321);
            this.tBarEffectiveAgentPercentage.Maximum = 20;
            this.tBarEffectiveAgentPercentage.Name = "tBarEffectiveAgentPercentage";
            this.tBarEffectiveAgentPercentage.Size = new System.Drawing.Size(191, 45);
            this.tBarEffectiveAgentPercentage.TabIndex = 11;
            this.tBarEffectiveAgentPercentage.Value = 20;
            this.tBarEffectiveAgentPercentage.Scroll += new System.EventHandler(this.tBarEffectiveAgentPercentage_Scroll);
            // 
            // lblEffectiveAgentPercentage
            // 
            this.lblEffectiveAgentPercentage.AutoSize = true;
            this.lblEffectiveAgentPercentage.Location = new System.Drawing.Point(16, 320);
            this.lblEffectiveAgentPercentage.Name = "lblEffectiveAgentPercentage";
            this.lblEffectiveAgentPercentage.Size = new System.Drawing.Size(184, 17);
            this.lblEffectiveAgentPercentage.TabIndex = 10;
            this.lblEffectiveAgentPercentage.Text = "Effective Agent Percentage :";
            // 
            // cmbBoxChemicalAgent
            // 
            this.cmbBoxChemicalAgent.FormattingEnabled = true;
            this.cmbBoxChemicalAgent.Location = new System.Drawing.Point(227, 205);
            this.cmbBoxChemicalAgent.Name = "cmbBoxChemicalAgent";
            this.cmbBoxChemicalAgent.Size = new System.Drawing.Size(101, 25);
            this.cmbBoxChemicalAgent.TabIndex = 9;
            // 
            // lblAgentWeightUnits
            // 
            this.lblAgentWeightUnits.AutoSize = true;
            this.lblAgentWeightUnits.Location = new System.Drawing.Point(363, 265);
            this.lblAgentWeightUnits.Name = "lblAgentWeightUnits";
            this.lblAgentWeightUnits.Size = new System.Drawing.Size(35, 17);
            this.lblAgentWeightUnits.TabIndex = 8;
            this.lblAgentWeightUnits.Text = "(kg)";
            // 
            // txtBoxAgentWeight
            // 
            this.txtBoxAgentWeight.Location = new System.Drawing.Point(227, 267);
            this.txtBoxAgentWeight.Name = "txtBoxAgentWeight";
            this.txtBoxAgentWeight.Size = new System.Drawing.Size(101, 25);
            this.txtBoxAgentWeight.TabIndex = 0;
            // 
            // lblAgentWeight
            // 
            this.lblAgentWeight.AutoSize = true;
            this.lblAgentWeight.Location = new System.Drawing.Point(16, 270);
            this.lblAgentWeight.Name = "lblAgentWeight";
            this.lblAgentWeight.Size = new System.Drawing.Size(101, 17);
            this.lblAgentWeight.TabIndex = 7;
            this.lblAgentWeight.Text = " Agent Weight :";
            // 
            // lblChemicalAgent
            // 
            this.lblChemicalAgent.AutoSize = true;
            this.lblChemicalAgent.Location = new System.Drawing.Point(16, 208);
            this.lblChemicalAgent.Name = "lblChemicalAgent";
            this.lblChemicalAgent.Size = new System.Drawing.Size(111, 17);
            this.lblChemicalAgent.TabIndex = 6;
            this.lblChemicalAgent.Text = "Chemical Agent :";
            // 
            // cmbBoxHOD
            // 
            this.cmbBoxHOD.FormattingEnabled = true;
            this.cmbBoxHOD.Location = new System.Drawing.Point(227, 31);
            this.cmbBoxHOD.Name = "cmbBoxHOD";
            this.cmbBoxHOD.Size = new System.Drawing.Size(121, 25);
            this.cmbBoxHOD.TabIndex = 4;
            this.cmbBoxHOD.SelectedIndexChanged += new System.EventHandler(this.cmbBoxHOD_SelectedIndexChanged);
            // 
            // lblHeatOfDetonationUnits
            // 
            this.lblHeatOfDetonationUnits.AutoSize = true;
            this.lblHeatOfDetonationUnits.Location = new System.Drawing.Point(363, 34);
            this.lblHeatOfDetonationUnits.Name = "lblHeatOfDetonationUnits";
            this.lblHeatOfDetonationUnits.Size = new System.Drawing.Size(55, 17);
            this.lblHeatOfDetonationUnits.TabIndex = 5;
            this.lblHeatOfDetonationUnits.Text = "(kJ/kg)";
            // 
            // lblHeatOfDetonation
            // 
            this.lblHeatOfDetonation.AutoSize = true;
            this.lblHeatOfDetonation.Location = new System.Drawing.Point(16, 34);
            this.lblHeatOfDetonation.Name = "lblHeatOfDetonation";
            this.lblHeatOfDetonation.Size = new System.Drawing.Size(133, 17);
            this.lblHeatOfDetonation.TabIndex = 4;
            this.lblHeatOfDetonation.Text = "Heat of Detonation :";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.radioButton2);
            this.groupBox19.Controls.Add(this.radioButton3);
            this.groupBox19.Controls.Add(this.radioButton1);
            this.groupBox19.Location = new System.Drawing.Point(12, 11);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(521, 104);
            this.groupBox19.TabIndex = 0;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Source Type";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(24, 54);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(458, 21);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Semi-Continuous /Continuous (Release time is more than 30 seconds)";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(24, 78);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(345, 21);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Continuous (Release time is more than 30 minutes.)";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Visible = false;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(24, 31);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(352, 21);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Instantaneous (Release time is less than 30 seconds.)";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.dateTimePickerMetData);
            this.groupBox10.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(596, 11);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(324, 64);
            this.groupBox10.TabIndex = 4;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Date and Time";
            // 
            // dateTimePickerMetData
            // 
            this.dateTimePickerMetData.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerMetData.Location = new System.Drawing.Point(57, 24);
            this.dateTimePickerMetData.Name = "dateTimePickerMetData";
            this.dateTimePickerMetData.Size = new System.Drawing.Size(200, 25);
            this.dateTimePickerMetData.TabIndex = 2;
            this.dateTimePickerMetData.Tag = "";
            this.toolTipMetInput.SetToolTip(this.dateTimePickerMetData, "Change the time or pick a date, month and year by clicking the arrow button . ");
            // 
            // grpBoxContinuous
            // 
            this.grpBoxContinuous.Location = new System.Drawing.Point(12, 345);
            this.grpBoxContinuous.Name = "grpBoxContinuous";
            this.grpBoxContinuous.Size = new System.Drawing.Size(433, 170);
            this.grpBoxContinuous.TabIndex = 3;
            this.grpBoxContinuous.TabStop = false;
            this.grpBoxContinuous.Text = "Continuous Source Details";
            // 
            // geoDataTab
            // 
            this.geoDataTab.Controls.Add(this.grpBoxTerrainData);
            this.geoDataTab.Controls.Add(this.groupBox33);
            this.geoDataTab.Location = new System.Drawing.Point(4, 26);
            this.geoDataTab.Name = "geoDataTab";
            this.geoDataTab.Padding = new System.Windows.Forms.Padding(3);
            this.geoDataTab.Size = new System.Drawing.Size(941, 525);
            this.geoDataTab.TabIndex = 1;
            this.geoDataTab.Text = "Geographical Input";
            this.geoDataTab.UseVisualStyleBackColor = true;
            // 
            // grpBoxTerrainData
            // 
            this.grpBoxTerrainData.Controls.Add(this.chkBoxSurfaceRoughnessLength);
            this.grpBoxTerrainData.Controls.Add(this.lblRoughnessLengthChkBox);
            this.grpBoxTerrainData.Controls.Add(this.rchTxtBoxTerrainType);
            this.grpBoxTerrainData.Controls.Add(this.lblRoughnessLengthUnits);
            this.grpBoxTerrainData.Controls.Add(this.lblRoughnessLength);
            this.grpBoxTerrainData.Controls.Add(this.label1);
            this.grpBoxTerrainData.Controls.Add(this.cmbBoxTerrainType);
            this.grpBoxTerrainData.Controls.Add(this.txtBoxSurfaceRoughnessLength);
            this.grpBoxTerrainData.Location = new System.Drawing.Point(25, 23);
            this.grpBoxTerrainData.Name = "grpBoxTerrainData";
            this.grpBoxTerrainData.Size = new System.Drawing.Size(888, 208);
            this.grpBoxTerrainData.TabIndex = 11;
            this.grpBoxTerrainData.TabStop = false;
            this.grpBoxTerrainData.Text = "Terrain Data";
            // 
            // chkBoxSurfaceRoughnessLength
            // 
            this.chkBoxSurfaceRoughnessLength.AutoSize = true;
            this.chkBoxSurfaceRoughnessLength.Location = new System.Drawing.Point(309, 95);
            this.chkBoxSurfaceRoughnessLength.Name = "chkBoxSurfaceRoughnessLength";
            this.chkBoxSurfaceRoughnessLength.Size = new System.Drawing.Size(15, 14);
            this.chkBoxSurfaceRoughnessLength.TabIndex = 20;
            this.chkBoxSurfaceRoughnessLength.UseVisualStyleBackColor = true;
            this.chkBoxSurfaceRoughnessLength.CheckedChanged += new System.EventHandler(this.chkBoxSurfaceRoughnessLength_CheckedChanged);
            // 
            // lblRoughnessLengthChkBox
            // 
            this.lblRoughnessLengthChkBox.AutoSize = true;
            this.lblRoughnessLengthChkBox.Location = new System.Drawing.Point(16, 93);
            this.lblRoughnessLengthChkBox.Name = "lblRoughnessLengthChkBox";
            this.lblRoughnessLengthChkBox.Size = new System.Drawing.Size(282, 17);
            this.lblRoughnessLengthChkBox.TabIndex = 19;
            this.lblRoughnessLengthChkBox.Text = "Insert Surface Roughness Length  Manually :";
            // 
            // rchTxtBoxTerrainType
            // 
            this.rchTxtBoxTerrainType.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.rchTxtBoxTerrainType.BackColor = System.Drawing.Color.White;
            this.rchTxtBoxTerrainType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchTxtBoxTerrainType.Cursor = System.Windows.Forms.Cursors.Default;
            this.rchTxtBoxTerrainType.Location = new System.Drawing.Point(347, 24);
            this.rchTxtBoxTerrainType.Name = "rchTxtBoxTerrainType";
            this.rchTxtBoxTerrainType.ReadOnly = true;
            this.rchTxtBoxTerrainType.Size = new System.Drawing.Size(524, 73);
            this.rchTxtBoxTerrainType.TabIndex = 18;
            this.rchTxtBoxTerrainType.Text = "";
            // 
            // lblRoughnessLengthUnits
            // 
            this.lblRoughnessLengthUnits.AutoSize = true;
            this.lblRoughnessLengthUnits.Location = new System.Drawing.Point(302, 150);
            this.lblRoughnessLengthUnits.Name = "lblRoughnessLengthUnits";
            this.lblRoughnessLengthUnits.Size = new System.Drawing.Size(32, 17);
            this.lblRoughnessLengthUnits.TabIndex = 17;
            this.lblRoughnessLengthUnits.Text = "(m)";
            // 
            // lblRoughnessLength
            // 
            this.lblRoughnessLength.AutoSize = true;
            this.lblRoughnessLength.Location = new System.Drawing.Point(16, 150);
            this.lblRoughnessLength.Name = "lblRoughnessLength";
            this.lblRoughnessLength.Size = new System.Drawing.Size(178, 17);
            this.lblRoughnessLength.TabIndex = 16;
            this.lblRoughnessLength.Text = "Surface Roughness Length :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Type Of the Terrain :";
            // 
            // cmbBoxTerrainType
            // 
            this.cmbBoxTerrainType.FormattingEnabled = true;
            this.cmbBoxTerrainType.Items.AddRange(new object[] {
            "Smooth",
            "Open",
            "roughly Open",
            "Rough",
            "Very Rough",
            "Skimming",
            "Chaotic"});
            this.cmbBoxTerrainType.Location = new System.Drawing.Point(203, 33);
            this.cmbBoxTerrainType.Name = "cmbBoxTerrainType";
            this.cmbBoxTerrainType.Size = new System.Drawing.Size(121, 25);
            this.cmbBoxTerrainType.TabIndex = 12;
            this.toolTipMetInput.SetToolTip(this.cmbBoxTerrainType, "Select the most appropriate terrain type for your scenario.");
            this.cmbBoxTerrainType.SelectedIndexChanged += new System.EventHandler(this.cmbBoxTerrainType_SelectedIndexChanged_1);
            // 
            // txtBoxSurfaceRoughnessLength
            // 
            this.txtBoxSurfaceRoughnessLength.Enabled = false;
            this.txtBoxSurfaceRoughnessLength.Location = new System.Drawing.Point(203, 147);
            this.txtBoxSurfaceRoughnessLength.Name = "txtBoxSurfaceRoughnessLength";
            this.txtBoxSurfaceRoughnessLength.Size = new System.Drawing.Size(70, 25);
            this.txtBoxSurfaceRoughnessLength.TabIndex = 14;
            // 
            // groupBox33
            // 
            this.groupBox33.Controls.Add(this.lblMoistureAvailabilityPercentage);
            this.groupBox33.Controls.Add(this.lblSurfaceMoistureAvailability);
            this.groupBox33.Controls.Add(this.txtBoxMoistureAvailability);
            this.groupBox33.Controls.Add(this.label2);
            this.groupBox33.Controls.Add(this.txtBoxAlbedo);
            this.groupBox33.Controls.Add(this.chkBoxAlbedoValue);
            this.groupBox33.Controls.Add(this.lblAlbedoChkBox);
            this.groupBox33.Controls.Add(this.cmbBoxSurfaceType);
            this.groupBox33.Controls.Add(this.lblSurfaceType);
            this.groupBox33.Location = new System.Drawing.Point(25, 258);
            this.groupBox33.Name = "groupBox33";
            this.groupBox33.Size = new System.Drawing.Size(888, 206);
            this.groupBox33.TabIndex = 14;
            this.groupBox33.TabStop = false;
            this.groupBox33.Text = "Surface Data";
            // 
            // lblMoistureAvailabilityPercentage
            // 
            this.lblMoistureAvailabilityPercentage.AutoSize = true;
            this.lblMoistureAvailabilityPercentage.Location = new System.Drawing.Point(306, 146);
            this.lblMoistureAvailabilityPercentage.Name = "lblMoistureAvailabilityPercentage";
            this.lblMoistureAvailabilityPercentage.Size = new System.Drawing.Size(33, 17);
            this.lblMoistureAvailabilityPercentage.TabIndex = 24;
            this.lblMoistureAvailabilityPercentage.Text = "(%)";
            // 
            // lblSurfaceMoistureAvailability
            // 
            this.lblSurfaceMoistureAvailability.AutoSize = true;
            this.lblSurfaceMoistureAvailability.Location = new System.Drawing.Point(16, 146);
            this.lblSurfaceMoistureAvailability.Name = "lblSurfaceMoistureAvailability";
            this.lblSurfaceMoistureAvailability.Size = new System.Drawing.Size(195, 17);
            this.lblSurfaceMoistureAvailability.TabIndex = 23;
            this.lblSurfaceMoistureAvailability.Text = "Surface Moisture Availability :";
            // 
            // txtBoxMoistureAvailability
            // 
            this.txtBoxMoistureAvailability.Location = new System.Drawing.Point(217, 143);
            this.txtBoxMoistureAvailability.Name = "txtBoxMoistureAvailability";
            this.txtBoxMoistureAvailability.Size = new System.Drawing.Size(70, 25);
            this.txtBoxMoistureAvailability.TabIndex = 17;
            this.toolTipMetInput.SetToolTip(this.txtBoxMoistureAvailability, "Enter the surface moisture availability as a percentage.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = " Albedo Value :";
            // 
            // txtBoxAlbedo
            // 
            this.txtBoxAlbedo.Enabled = false;
            this.txtBoxAlbedo.Location = new System.Drawing.Point(217, 89);
            this.txtBoxAlbedo.Name = "txtBoxAlbedo";
            this.txtBoxAlbedo.Size = new System.Drawing.Size(70, 25);
            this.txtBoxAlbedo.TabIndex = 16;
            // 
            // chkBoxAlbedoValue
            // 
            this.chkBoxAlbedoValue.AutoSize = true;
            this.chkBoxAlbedoValue.Location = new System.Drawing.Point(629, 41);
            this.chkBoxAlbedoValue.Name = "chkBoxAlbedoValue";
            this.chkBoxAlbedoValue.Size = new System.Drawing.Size(15, 14);
            this.chkBoxAlbedoValue.TabIndex = 21;
            this.chkBoxAlbedoValue.UseVisualStyleBackColor = true;
            this.chkBoxAlbedoValue.CheckedChanged += new System.EventHandler(this.chkBoxAlbedoValue_CheckedChanged);
            // 
            // lblAlbedoChkBox
            // 
            this.lblAlbedoChkBox.AutoSize = true;
            this.lblAlbedoChkBox.Location = new System.Drawing.Point(426, 38);
            this.lblAlbedoChkBox.Name = "lblAlbedoChkBox";
            this.lblAlbedoChkBox.Size = new System.Drawing.Size(197, 17);
            this.lblAlbedoChkBox.TabIndex = 19;
            this.lblAlbedoChkBox.Text = "Insert Albedo Value Manually :";
            // 
            // cmbBoxSurfaceType
            // 
            this.cmbBoxSurfaceType.FormattingEnabled = true;
            this.cmbBoxSurfaceType.Items.AddRange(new object[] {
            "Soil (Wet)",
            "Soil (Dry)",
            "Long Grass (1 m)",
            "Short Grass (0.02 m)",
            "Agricultural crops",
            "Orchards ",
            "Forests (Bare Leaved)",
            "Forests (Coniferous)",
            "Buildings"});
            this.cmbBoxSurfaceType.Location = new System.Drawing.Point(217, 35);
            this.cmbBoxSurfaceType.Name = "cmbBoxSurfaceType";
            this.cmbBoxSurfaceType.Size = new System.Drawing.Size(150, 25);
            this.cmbBoxSurfaceType.TabIndex = 15;
            this.toolTipMetInput.SetToolTip(this.cmbBoxSurfaceType, "Select the most appropriate type of the surface for your scenario.");
            // 
            // lblSurfaceType
            // 
            this.lblSurfaceType.AutoSize = true;
            this.lblSurfaceType.Location = new System.Drawing.Point(16, 38);
            this.lblSurfaceType.Name = "lblSurfaceType";
            this.lblSurfaceType.Size = new System.Drawing.Size(136, 17);
            this.lblSurfaceType.TabIndex = 18;
            this.lblSurfaceType.Text = "Type of the Surface :";
            // 
            // btnInputOK
            // 
            this.btnInputOK.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInputOK.Location = new System.Drawing.Point(715, 568);
            this.btnInputOK.Name = "btnInputOK";
            this.btnInputOK.Size = new System.Drawing.Size(119, 33);
            this.btnInputOK.TabIndex = 9;
            this.btnInputOK.Text = "OK";
            this.btnInputOK.UseVisualStyleBackColor = true;
            this.btnInputOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // errorProviderMetInput
            // 
            this.errorProviderMetInput.ContainerControl = this;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Cambria", 9F);
            this.btnCancel.Location = new System.Drawing.Point(840, 568);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 33);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmDataInput
            // 
            this.AcceptButton = this.btnInputOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 603);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dataInputTab);
            this.Controls.Add(this.btnInputOK);
            this.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDataInput";
            this.Opacity = 0.98D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Input";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDataInput_FormClosed);
            this.Load += new System.EventHandler(this.meteorolgicalData_Load);
            this.dataInputTab.ResumeLayout(false);
            this.sourceDataTab.ResumeLayout(false);
            this.grpBoxSemiContinuous.ResumeLayout(false);
            this.grpBoxSemiContinuous.PerformLayout();
            this.grpBoxInstantaneous.ResumeLayout(false);
            this.grpBoxInstantaneous.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarEffectiveHeatPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarEffectiveAgentPercentage)).EndInit();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.geoDataTab.ResumeLayout(false);
            this.grpBoxTerrainData.ResumeLayout(false);
            this.grpBoxTerrainData.PerformLayout();
            this.groupBox33.ResumeLayout(false);
            this.groupBox33.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderMetInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl dataInputTab;
        private System.Windows.Forms.TabPage geoDataTab;
        private System.Windows.Forms.ToolTip toolTipMetInput;
        private System.Windows.Forms.ErrorProvider errorProviderMetInput;
        private System.Windows.Forms.Button btnInputOK;
        private System.Windows.Forms.TabPage sourceDataTab;
        private System.Windows.Forms.TextBox txtBoxMoistureAvailability;
        private System.Windows.Forms.ComboBox cmbBoxSurfaceType;
        private System.Windows.Forms.GroupBox grpBoxTerrainData;
        private System.Windows.Forms.ComboBox cmbBoxTerrainType;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox grpBoxInstantaneous;
        private System.Windows.Forms.GroupBox grpBoxSemiContinuous;
        private System.Windows.Forms.TextBox txtBoxAgentWeight;
        private System.Windows.Forms.GroupBox grpBoxContinuous;
        private System.Windows.Forms.GroupBox groupBox33;
        private System.Windows.Forms.TextBox txtBoxAlbedo;
        public System.Windows.Forms.ComboBox cmbBoxHOD;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.DateTimePicker dateTimePickerMetData;
        private System.Windows.Forms.TextBox txtBoxSurfaceRoughnessLength;
        private System.Windows.Forms.Label lblRoughnessLengthUnits;
        private System.Windows.Forms.Label lblRoughnessLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rchTxtBoxTerrainType;
        private System.Windows.Forms.CheckBox chkBoxSurfaceRoughnessLength;
        private System.Windows.Forms.Label lblRoughnessLengthChkBox;
        private System.Windows.Forms.Label lblAlbedoChkBox;
        private System.Windows.Forms.Label lblSurfaceType;
        private System.Windows.Forms.Label lblMoistureAvailabilityPercentage;
        private System.Windows.Forms.Label lblSurfaceMoistureAvailability;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkBoxAlbedoValue;
        private System.Windows.Forms.Label lblHeatOfDetonationUnits;
        private System.Windows.Forms.Label lblHeatOfDetonation;
        private System.Windows.Forms.Label lblChemicalAgent;
        private System.Windows.Forms.Label lblAgentWeight;
        private System.Windows.Forms.Label lblAgentWeightUnits;
        private System.Windows.Forms.ComboBox cmbBoxChemicalAgent;
        private System.Windows.Forms.Label lblEffectiveAgentPercentage;
        private System.Windows.Forms.TrackBar tBarEffectiveAgentPercentage;
        private System.Windows.Forms.Label lblEffectivePercentage;
        private System.Windows.Forms.Label lblHODNewComposition;
        private System.Windows.Forms.CheckBox chkBoxHODNewComposition;
        private System.Windows.Forms.Label lblEffHeatPercentage;
        private System.Windows.Forms.TrackBar tbarEffectiveHeatPercentage;
        private System.Windows.Forms.Label lblEffectiveHeatPercentage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblExplosiveWeightUnits;
        private System.Windows.Forms.TextBox txtBoxWeightOfExplosive;
        private System.Windows.Forms.Label lblExplosiveWeight;
        private System.Windows.Forms.Label lblSourceTempUnits;
        private System.Windows.Forms.TextBox txtBoxSourceTemp;
        private System.Windows.Forms.Label lblSourceTemp;
        private System.Windows.Forms.Label lblSourceRadiusUnits;
        private System.Windows.Forms.TextBox txtBoxSourceRadius;
        private System.Windows.Forms.Label lblSourceRadius;
        private System.Windows.Forms.Label lblReleaseRateUnits;
        private System.Windows.Forms.TextBox txtBoxReleaseRate;
        private System.Windows.Forms.Label lblReleaseRate;
    }
}

