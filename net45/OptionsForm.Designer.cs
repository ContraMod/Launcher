namespace Contra
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.LangFilterCheckBox = new System.Windows.Forms.CheckBox();
            this.FogCheckBox = new System.Windows.Forms.CheckBox();
            this.resolutionComboBox = new System.Windows.Forms.ComboBox();
            this.labelResolution = new System.Windows.Forms.Label();
            this.HeatEffectsCheckBox = new System.Windows.Forms.CheckBox();
            this.CameraHeightLabel = new System.Windows.Forms.Label();
            this.WaterEffectsCheckBox = new System.Windows.Forms.CheckBox();
            this.DisableDynamicLODCheckBox = new System.Windows.Forms.CheckBox();
            this.ExtraAnimationsCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowPropsCheckBox = new System.Windows.Forms.CheckBox();
            this.BehindBuildingsCheckBox = new System.Windows.Forms.CheckBox();
            this.Shadows3DCheckBox = new System.Windows.Forms.CheckBox();
            this.Shadows2DCheckBox = new System.Windows.Forms.CheckBox();
            this.CloudShadowsCheckBox = new System.Windows.Forms.CheckBox();
            this.ExtraGroundLightingCheckBox = new System.Windows.Forms.CheckBox();
            this.SmoothWaterBordersCheckBox = new System.Windows.Forms.CheckBox();
            this.LegacyHotkeysRadioButton = new System.Windows.Forms.RadioButton();
            this.LeikezeHotkeysRadioButton = new System.Windows.Forms.RadioButton();
            this.MinBtnSm = new System.Windows.Forms.Button();
            this.ExitBtnSm = new System.Windows.Forms.Button();
            this.TextureResLabel = new System.Windows.Forms.Label();
            this.ParticleCapLabel = new System.Windows.Forms.Label();
            this.AcceptBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.camOkButton = new System.Windows.Forms.Button();
            this.GraphicsInfoPictureBox = new System.Windows.Forms.PictureBox();
            this.GraphicsInfoDescriptionLabel = new System.Windows.Forms.Label();
            this.GraphicsInfoTextPanel = new System.Windows.Forms.Panel();
            this.GraphicsInfoHeaderLabel = new System.Windows.Forms.Label();
            this.GraphicsInfoPerformanceLabel = new System.Windows.Forms.Label();
            this.HotkeyStylePanel = new System.Windows.Forms.Panel();
            this.HotkeyStyleLabel = new System.Windows.Forms.Label();
            this.AnisoCheckBox = new System.Windows.Forms.CheckBox();
            this.ControlBarPanel = new System.Windows.Forms.Panel();
            this.ControlBarStandardRadioButton = new System.Windows.Forms.RadioButton();
            this.ControlBarContraRadioButton = new System.Windows.Forms.RadioButton();
            this.ControlBarProRadioButton = new System.Windows.Forms.RadioButton();
            this.ControlBarLabel = new System.Windows.Forms.Label();
            this.CameoResPanel = new System.Windows.Forms.Panel();
            this.IconQualityLabel = new System.Windows.Forms.Label();
            this.CameosStandardRadioButton = new System.Windows.Forms.RadioButton();
            this.CameosDoubleRadioButton = new System.Windows.Forms.RadioButton();
            this.ExtraBuildingPropsCheckBox = new System.Windows.Forms.CheckBox();
            this.NoPreviewText = new System.Windows.Forms.Label();
            this.ParticleCapTrackBar = new Contra.TrackBar();
            this.TextureResTrackBar = new Contra.TrackBar();
            this.CameraHeightTrackBar = new Contra.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsInfoPictureBox)).BeginInit();
            this.GraphicsInfoTextPanel.SuspendLayout();
            this.HotkeyStylePanel.SuspendLayout();
            this.ControlBarPanel.SuspendLayout();
            this.CameoResPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LangFilterCheckBox
            // 
            resources.ApplyResources(this.LangFilterCheckBox, "LangFilterCheckBox");
            this.LangFilterCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.LangFilterCheckBox.Checked = true;
            this.LangFilterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LangFilterCheckBox.ForeColor = System.Drawing.Color.White;
            this.LangFilterCheckBox.Name = "LangFilterCheckBox";
            this.LangFilterCheckBox.UseVisualStyleBackColor = false;
            this.LangFilterCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.LangFilterCheckBox.MouseHover += new System.EventHandler(this.LangFilterCheckBox_MouseHover);
            // 
            // FogCheckBox
            // 
            resources.ApplyResources(this.FogCheckBox, "FogCheckBox");
            this.FogCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.FogCheckBox.ForeColor = System.Drawing.Color.White;
            this.FogCheckBox.Name = "FogCheckBox";
            this.FogCheckBox.UseVisualStyleBackColor = false;
            this.FogCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.FogCheckBox.MouseHover += new System.EventHandler(this.FogCheckBox_MouseHover);
            // 
            // resolutionComboBox
            // 
            resources.ApplyResources(this.resolutionComboBox, "resolutionComboBox");
            this.resolutionComboBox.FormattingEnabled = true;
            this.resolutionComboBox.Name = "resolutionComboBox";
            // 
            // labelResolution
            // 
            resources.ApplyResources(this.labelResolution, "labelResolution");
            this.labelResolution.BackColor = System.Drawing.Color.Transparent;
            this.labelResolution.ForeColor = System.Drawing.Color.White;
            this.labelResolution.Name = "labelResolution";
            // 
            // HeatEffectsCheckBox
            // 
            resources.ApplyResources(this.HeatEffectsCheckBox, "HeatEffectsCheckBox");
            this.HeatEffectsCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.HeatEffectsCheckBox.ForeColor = System.Drawing.Color.White;
            this.HeatEffectsCheckBox.Name = "HeatEffectsCheckBox";
            this.HeatEffectsCheckBox.UseVisualStyleBackColor = false;
            this.HeatEffectsCheckBox.CheckedChanged += new System.EventHandler(this.HeatEffectsCheckBox_CheckedChanged);
            this.HeatEffectsCheckBox.Click += new System.EventHandler(this.HeatEffectsCheckBox_Click);
            this.HeatEffectsCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.HeatEffectsCheckBox.MouseHover += new System.EventHandler(this.HeatEffectsCheckBox_MouseHover);
            // 
            // CameraHeightLabel
            // 
            resources.ApplyResources(this.CameraHeightLabel, "CameraHeightLabel");
            this.CameraHeightLabel.BackColor = System.Drawing.Color.Transparent;
            this.CameraHeightLabel.ForeColor = System.Drawing.Color.White;
            this.CameraHeightLabel.Name = "CameraHeightLabel";
            // 
            // WaterEffectsCheckBox
            // 
            resources.ApplyResources(this.WaterEffectsCheckBox, "WaterEffectsCheckBox");
            this.WaterEffectsCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.WaterEffectsCheckBox.Checked = true;
            this.WaterEffectsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WaterEffectsCheckBox.ForeColor = System.Drawing.Color.White;
            this.WaterEffectsCheckBox.Name = "WaterEffectsCheckBox";
            this.WaterEffectsCheckBox.UseVisualStyleBackColor = false;
            this.WaterEffectsCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.WaterEffectsCheckBox.MouseHover += new System.EventHandler(this.WaterEffectsCheckBox_MouseHover);
            // 
            // DisableDynamicLODCheckBox
            // 
            resources.ApplyResources(this.DisableDynamicLODCheckBox, "DisableDynamicLODCheckBox");
            this.DisableDynamicLODCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.DisableDynamicLODCheckBox.ForeColor = System.Drawing.Color.White;
            this.DisableDynamicLODCheckBox.Name = "DisableDynamicLODCheckBox";
            this.DisableDynamicLODCheckBox.UseVisualStyleBackColor = false;
            this.DisableDynamicLODCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.DisableDynamicLODCheckBox.MouseHover += new System.EventHandler(this.DisableDynamicLODCheckBox_MouseHover);
            // 
            // ExtraAnimationsCheckBox
            // 
            resources.ApplyResources(this.ExtraAnimationsCheckBox, "ExtraAnimationsCheckBox");
            this.ExtraAnimationsCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.ExtraAnimationsCheckBox.ForeColor = System.Drawing.Color.White;
            this.ExtraAnimationsCheckBox.Name = "ExtraAnimationsCheckBox";
            this.ExtraAnimationsCheckBox.UseVisualStyleBackColor = false;
            this.ExtraAnimationsCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.ExtraAnimationsCheckBox.MouseHover += new System.EventHandler(this.ExtraAnimationsCheckBox_MouseHover);
            // 
            // ShowPropsCheckBox
            // 
            resources.ApplyResources(this.ShowPropsCheckBox, "ShowPropsCheckBox");
            this.ShowPropsCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.ShowPropsCheckBox.ForeColor = System.Drawing.Color.White;
            this.ShowPropsCheckBox.Name = "ShowPropsCheckBox";
            this.ShowPropsCheckBox.UseVisualStyleBackColor = false;
            this.ShowPropsCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.ShowPropsCheckBox.MouseHover += new System.EventHandler(this.ShowPropsCheckBox_MouseHover);
            // 
            // BehindBuildingsCheckBox
            // 
            resources.ApplyResources(this.BehindBuildingsCheckBox, "BehindBuildingsCheckBox");
            this.BehindBuildingsCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.BehindBuildingsCheckBox.ForeColor = System.Drawing.Color.White;
            this.BehindBuildingsCheckBox.Name = "BehindBuildingsCheckBox";
            this.BehindBuildingsCheckBox.UseVisualStyleBackColor = false;
            this.BehindBuildingsCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.BehindBuildingsCheckBox.MouseHover += new System.EventHandler(this.BehindBuildingsCheckBox_MouseHover);
            // 
            // Shadows3DCheckBox
            // 
            resources.ApplyResources(this.Shadows3DCheckBox, "Shadows3DCheckBox");
            this.Shadows3DCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.Shadows3DCheckBox.ForeColor = System.Drawing.Color.White;
            this.Shadows3DCheckBox.Name = "Shadows3DCheckBox";
            this.Shadows3DCheckBox.UseVisualStyleBackColor = false;
            this.Shadows3DCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.Shadows3DCheckBox.MouseHover += new System.EventHandler(this.Shadows3DCheckBox_MouseHover);
            // 
            // Shadows2DCheckBox
            // 
            resources.ApplyResources(this.Shadows2DCheckBox, "Shadows2DCheckBox");
            this.Shadows2DCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.Shadows2DCheckBox.ForeColor = System.Drawing.Color.White;
            this.Shadows2DCheckBox.Name = "Shadows2DCheckBox";
            this.Shadows2DCheckBox.UseVisualStyleBackColor = false;
            this.Shadows2DCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.Shadows2DCheckBox.MouseHover += new System.EventHandler(this.Shadows2DCheckBox_MouseHover);
            // 
            // CloudShadowsCheckBox
            // 
            resources.ApplyResources(this.CloudShadowsCheckBox, "CloudShadowsCheckBox");
            this.CloudShadowsCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.CloudShadowsCheckBox.ForeColor = System.Drawing.Color.White;
            this.CloudShadowsCheckBox.Name = "CloudShadowsCheckBox";
            this.CloudShadowsCheckBox.UseVisualStyleBackColor = false;
            this.CloudShadowsCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.CloudShadowsCheckBox.MouseHover += new System.EventHandler(this.CloudShadowsCheckBox_MouseHover);
            // 
            // ExtraGroundLightingCheckBox
            // 
            resources.ApplyResources(this.ExtraGroundLightingCheckBox, "ExtraGroundLightingCheckBox");
            this.ExtraGroundLightingCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.ExtraGroundLightingCheckBox.ForeColor = System.Drawing.Color.White;
            this.ExtraGroundLightingCheckBox.Name = "ExtraGroundLightingCheckBox";
            this.ExtraGroundLightingCheckBox.UseVisualStyleBackColor = false;
            this.ExtraGroundLightingCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.ExtraGroundLightingCheckBox.MouseHover += new System.EventHandler(this.ExtraGroundLightingCheckBox_MouseHover);
            // 
            // SmoothWaterBordersCheckBox
            // 
            resources.ApplyResources(this.SmoothWaterBordersCheckBox, "SmoothWaterBordersCheckBox");
            this.SmoothWaterBordersCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.SmoothWaterBordersCheckBox.ForeColor = System.Drawing.Color.White;
            this.SmoothWaterBordersCheckBox.Name = "SmoothWaterBordersCheckBox";
            this.SmoothWaterBordersCheckBox.UseVisualStyleBackColor = false;
            this.SmoothWaterBordersCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.SmoothWaterBordersCheckBox.MouseHover += new System.EventHandler(this.SmoothWaterBordersCheckBox_MouseHover);
            // 
            // LegacyHotkeysRadioButton
            // 
            resources.ApplyResources(this.LegacyHotkeysRadioButton, "LegacyHotkeysRadioButton");
            this.LegacyHotkeysRadioButton.ForeColor = System.Drawing.Color.White;
            this.LegacyHotkeysRadioButton.Name = "LegacyHotkeysRadioButton";
            this.LegacyHotkeysRadioButton.UseVisualStyleBackColor = true;
            this.LegacyHotkeysRadioButton.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.LegacyHotkeysRadioButton.MouseHover += new System.EventHandler(this.LegacyHotkeysRadioButton_MouseHover);
            // 
            // LeikezeHotkeysRadioButton
            // 
            resources.ApplyResources(this.LeikezeHotkeysRadioButton, "LeikezeHotkeysRadioButton");
            this.LeikezeHotkeysRadioButton.Checked = true;
            this.LeikezeHotkeysRadioButton.ForeColor = System.Drawing.Color.White;
            this.LeikezeHotkeysRadioButton.Name = "LeikezeHotkeysRadioButton";
            this.LeikezeHotkeysRadioButton.TabStop = true;
            this.LeikezeHotkeysRadioButton.UseVisualStyleBackColor = true;
            this.LeikezeHotkeysRadioButton.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.LeikezeHotkeysRadioButton.MouseHover += new System.EventHandler(this.LeikezeHotkeysRadioButton_MouseHover);
            // 
            // MinBtnSm
            // 
            this.MinBtnSm.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.MinBtnSm, "MinBtnSm");
            this.MinBtnSm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MinBtnSm.FlatAppearance.BorderSize = 0;
            this.MinBtnSm.Name = "MinBtnSm";
            this.MinBtnSm.UseVisualStyleBackColor = false;
            this.MinBtnSm.Click += new System.EventHandler(this.MinBtnSm_Click);
            this.MinBtnSm.MouseEnter += new System.EventHandler(this.MinBtnSm_MouseEnter);
            this.MinBtnSm.MouseLeave += new System.EventHandler(this.MinBtnSm_MouseLeave);
            // 
            // ExitBtnSm
            // 
            this.ExitBtnSm.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.ExitBtnSm, "ExitBtnSm");
            this.ExitBtnSm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitBtnSm.FlatAppearance.BorderSize = 0;
            this.ExitBtnSm.Name = "ExitBtnSm";
            this.ExitBtnSm.UseVisualStyleBackColor = false;
            this.ExitBtnSm.Click += new System.EventHandler(this.ExitBtnSm_Click);
            this.ExitBtnSm.MouseEnter += new System.EventHandler(this.ExitBtnSm_MouseEnter);
            this.ExitBtnSm.MouseLeave += new System.EventHandler(this.ExitBtnSm_MouseLeave);
            // 
            // TextureResLabel
            // 
            resources.ApplyResources(this.TextureResLabel, "TextureResLabel");
            this.TextureResLabel.BackColor = System.Drawing.Color.Transparent;
            this.TextureResLabel.ForeColor = System.Drawing.Color.White;
            this.TextureResLabel.Name = "TextureResLabel";
            // 
            // ParticleCapLabel
            // 
            resources.ApplyResources(this.ParticleCapLabel, "ParticleCapLabel");
            this.ParticleCapLabel.BackColor = System.Drawing.Color.Transparent;
            this.ParticleCapLabel.ForeColor = System.Drawing.Color.White;
            this.ParticleCapLabel.Name = "ParticleCapLabel";
            // 
            // AcceptBtn
            // 
            this.AcceptBtn.BackColor = System.Drawing.Color.Transparent;
            this.AcceptBtn.BackgroundImage = global::Contra.Properties.Resources._button_big;
            this.AcceptBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AcceptBtn.FlatAppearance.BorderSize = 0;
            this.AcceptBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.AcceptBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.AcceptBtn, "AcceptBtn");
            this.AcceptBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AcceptBtn.Name = "AcceptBtn";
            this.AcceptBtn.UseVisualStyleBackColor = false;
            this.AcceptBtn.Click += new System.EventHandler(this.AcceptBtn_Click);
            this.AcceptBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AcceptBtn_MouseDown);
            this.AcceptBtn.MouseEnter += new System.EventHandler(this.AcceptBtn_MouseEnter);
            this.AcceptBtn.MouseLeave += new System.EventHandler(this.AcceptBtn_MouseLeave);
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.Transparent;
            this.CloseBtn.BackgroundImage = global::Contra.Properties.Resources._button_big;
            this.CloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.CloseBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.CloseBtn, "CloseBtn");
            this.CloseBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            this.CloseBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CloseBtn_MouseDown);
            this.CloseBtn.MouseEnter += new System.EventHandler(this.CloseBtn_MouseEnter);
            this.CloseBtn.MouseLeave += new System.EventHandler(this.CloseBtn_MouseLeave);
            // 
            // camOkButton
            // 
            this.camOkButton.BackColor = System.Drawing.Color.Transparent;
            this.camOkButton.BackgroundImage = global::Contra.Properties.Resources._button_small;
            this.camOkButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.camOkButton.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.camOkButton, "camOkButton");
            this.camOkButton.ForeColor = System.Drawing.Color.White;
            this.camOkButton.Name = "camOkButton";
            this.camOkButton.UseVisualStyleBackColor = false;
            // 
            // GraphicsInfoPictureBox
            // 
            this.GraphicsInfoPictureBox.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.GraphicsInfoPictureBox, "GraphicsInfoPictureBox");
            this.GraphicsInfoPictureBox.Name = "GraphicsInfoPictureBox";
            this.GraphicsInfoPictureBox.TabStop = false;
            // 
            // GraphicsInfoDescriptionLabel
            // 
            resources.ApplyResources(this.GraphicsInfoDescriptionLabel, "GraphicsInfoDescriptionLabel");
            this.GraphicsInfoDescriptionLabel.ForeColor = System.Drawing.Color.White;
            this.GraphicsInfoDescriptionLabel.Name = "GraphicsInfoDescriptionLabel";
            // 
            // GraphicsInfoTextPanel
            // 
            this.GraphicsInfoTextPanel.BackColor = System.Drawing.Color.Transparent;
            this.GraphicsInfoTextPanel.Controls.Add(this.GraphicsInfoHeaderLabel);
            this.GraphicsInfoTextPanel.Controls.Add(this.GraphicsInfoPerformanceLabel);
            this.GraphicsInfoTextPanel.Controls.Add(this.GraphicsInfoDescriptionLabel);
            resources.ApplyResources(this.GraphicsInfoTextPanel, "GraphicsInfoTextPanel");
            this.GraphicsInfoTextPanel.Name = "GraphicsInfoTextPanel";
            // 
            // GraphicsInfoHeaderLabel
            // 
            resources.ApplyResources(this.GraphicsInfoHeaderLabel, "GraphicsInfoHeaderLabel");
            this.GraphicsInfoHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.GraphicsInfoHeaderLabel.Name = "GraphicsInfoHeaderLabel";
            // 
            // GraphicsInfoPerformanceLabel
            // 
            resources.ApplyResources(this.GraphicsInfoPerformanceLabel, "GraphicsInfoPerformanceLabel");
            this.GraphicsInfoPerformanceLabel.ForeColor = System.Drawing.Color.White;
            this.GraphicsInfoPerformanceLabel.Name = "GraphicsInfoPerformanceLabel";
            // 
            // HotkeyStylePanel
            // 
            this.HotkeyStylePanel.BackColor = System.Drawing.Color.Transparent;
            this.HotkeyStylePanel.Controls.Add(this.LegacyHotkeysRadioButton);
            this.HotkeyStylePanel.Controls.Add(this.LeikezeHotkeysRadioButton);
            this.HotkeyStylePanel.Controls.Add(this.HotkeyStyleLabel);
            resources.ApplyResources(this.HotkeyStylePanel, "HotkeyStylePanel");
            this.HotkeyStylePanel.Name = "HotkeyStylePanel";
            // 
            // HotkeyStyleLabel
            // 
            resources.ApplyResources(this.HotkeyStyleLabel, "HotkeyStyleLabel");
            this.HotkeyStyleLabel.BackColor = System.Drawing.Color.Transparent;
            this.HotkeyStyleLabel.ForeColor = System.Drawing.Color.White;
            this.HotkeyStyleLabel.Name = "HotkeyStyleLabel";
            // 
            // AnisoCheckBox
            // 
            resources.ApplyResources(this.AnisoCheckBox, "AnisoCheckBox");
            this.AnisoCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.AnisoCheckBox.Checked = true;
            this.AnisoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AnisoCheckBox.ForeColor = System.Drawing.Color.White;
            this.AnisoCheckBox.Name = "AnisoCheckBox";
            this.AnisoCheckBox.UseVisualStyleBackColor = false;
            this.AnisoCheckBox.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.AnisoCheckBox.MouseHover += new System.EventHandler(this.AnisoCheckBox_MouseHover);
            // 
            // ControlBarPanel
            // 
            this.ControlBarPanel.BackColor = System.Drawing.Color.Transparent;
            this.ControlBarPanel.Controls.Add(this.ControlBarStandardRadioButton);
            this.ControlBarPanel.Controls.Add(this.ControlBarContraRadioButton);
            this.ControlBarPanel.Controls.Add(this.ControlBarProRadioButton);
            this.ControlBarPanel.Controls.Add(this.ControlBarLabel);
            resources.ApplyResources(this.ControlBarPanel, "ControlBarPanel");
            this.ControlBarPanel.Name = "ControlBarPanel";
            // 
            // ControlBarStandardRadioButton
            // 
            resources.ApplyResources(this.ControlBarStandardRadioButton, "ControlBarStandardRadioButton");
            this.ControlBarStandardRadioButton.ForeColor = System.Drawing.Color.White;
            this.ControlBarStandardRadioButton.Name = "ControlBarStandardRadioButton";
            this.ControlBarStandardRadioButton.UseVisualStyleBackColor = true;
            this.ControlBarStandardRadioButton.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.ControlBarStandardRadioButton.MouseHover += new System.EventHandler(this.ControlBarStandardRadioButton_MouseHover);
            // 
            // ControlBarContraRadioButton
            // 
            resources.ApplyResources(this.ControlBarContraRadioButton, "ControlBarContraRadioButton");
            this.ControlBarContraRadioButton.ForeColor = System.Drawing.Color.White;
            this.ControlBarContraRadioButton.Name = "ControlBarContraRadioButton";
            this.ControlBarContraRadioButton.UseVisualStyleBackColor = true;
            this.ControlBarContraRadioButton.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.ControlBarContraRadioButton.MouseHover += new System.EventHandler(this.ControlBarContraRadioButton_MouseHover);
            // 
            // ControlBarProRadioButton
            // 
            resources.ApplyResources(this.ControlBarProRadioButton, "ControlBarProRadioButton");
            this.ControlBarProRadioButton.Checked = true;
            this.ControlBarProRadioButton.ForeColor = System.Drawing.Color.White;
            this.ControlBarProRadioButton.Name = "ControlBarProRadioButton";
            this.ControlBarProRadioButton.TabStop = true;
            this.ControlBarProRadioButton.UseVisualStyleBackColor = true;
            this.ControlBarProRadioButton.CheckedChanged += new System.EventHandler(this.ControlBarProRadioButton_CheckedChanged);
            this.ControlBarProRadioButton.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.ControlBarProRadioButton.MouseHover += new System.EventHandler(this.ControlBarProRadioButton_MouseHover);
            // 
            // ControlBarLabel
            // 
            resources.ApplyResources(this.ControlBarLabel, "ControlBarLabel");
            this.ControlBarLabel.BackColor = System.Drawing.Color.Transparent;
            this.ControlBarLabel.ForeColor = System.Drawing.Color.White;
            this.ControlBarLabel.Name = "ControlBarLabel";
            // 
            // CameoResPanel
            // 
            this.CameoResPanel.BackColor = System.Drawing.Color.Transparent;
            this.CameoResPanel.Controls.Add(this.IconQualityLabel);
            this.CameoResPanel.Controls.Add(this.CameosStandardRadioButton);
            this.CameoResPanel.Controls.Add(this.CameosDoubleRadioButton);
            resources.ApplyResources(this.CameoResPanel, "CameoResPanel");
            this.CameoResPanel.Name = "CameoResPanel";
            // 
            // IconQualityLabel
            // 
            resources.ApplyResources(this.IconQualityLabel, "IconQualityLabel");
            this.IconQualityLabel.BackColor = System.Drawing.Color.Transparent;
            this.IconQualityLabel.ForeColor = System.Drawing.Color.White;
            this.IconQualityLabel.Name = "IconQualityLabel";
            // 
            // CameosStandardRadioButton
            // 
            resources.ApplyResources(this.CameosStandardRadioButton, "CameosStandardRadioButton");
            this.CameosStandardRadioButton.ForeColor = System.Drawing.Color.White;
            this.CameosStandardRadioButton.Name = "CameosStandardRadioButton";
            this.CameosStandardRadioButton.UseVisualStyleBackColor = true;
            this.CameosStandardRadioButton.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.CameosStandardRadioButton.MouseHover += new System.EventHandler(this.CameosStandardRadioButton_MouseHover);
            // 
            // CameosDoubleRadioButton
            // 
            resources.ApplyResources(this.CameosDoubleRadioButton, "CameosDoubleRadioButton");
            this.CameosDoubleRadioButton.Checked = true;
            this.CameosDoubleRadioButton.ForeColor = System.Drawing.Color.White;
            this.CameosDoubleRadioButton.Name = "CameosDoubleRadioButton";
            this.CameosDoubleRadioButton.TabStop = true;
            this.CameosDoubleRadioButton.UseVisualStyleBackColor = true;
            this.CameosDoubleRadioButton.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.CameosDoubleRadioButton.MouseHover += new System.EventHandler(this.CameosDoubleRadioButton_MouseHover);
            // 
            // ExtraBuildingPropsCheckBox
            // 
            resources.ApplyResources(this.ExtraBuildingPropsCheckBox, "ExtraBuildingPropsCheckBox");
            this.ExtraBuildingPropsCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.ExtraBuildingPropsCheckBox.Checked = true;
            this.ExtraBuildingPropsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ExtraBuildingPropsCheckBox.ForeColor = System.Drawing.Color.White;
            this.ExtraBuildingPropsCheckBox.Name = "ExtraBuildingPropsCheckBox";
            this.ExtraBuildingPropsCheckBox.UseVisualStyleBackColor = false;
            this.ExtraBuildingPropsCheckBox.MouseHover += new System.EventHandler(this.ExtraBuildingPropsCheckBox_MouseHover);
            // 
            // NoPreviewText
            // 
            this.NoPreviewText.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.NoPreviewText, "NoPreviewText");
            this.NoPreviewText.ForeColor = System.Drawing.Color.White;
            this.NoPreviewText.Name = "NoPreviewText";
            // 
            // ParticleCapTrackBar
            // 
            this.ParticleCapTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.ParticleCapTrackBar.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.ParticleCapTrackBar, "ParticleCapTrackBar");
            this.ParticleCapTrackBar.Maximum = 5000;
            this.ParticleCapTrackBar.Minimum = 100;
            this.ParticleCapTrackBar.Name = "ParticleCapTrackBar";
            this.ParticleCapTrackBar.TabStop = true;
            this.ParticleCapTrackBar.Value = 100;
            this.ParticleCapTrackBar.Scroll += new System.EventHandler(this.ParticleCapTrackBar_Scroll);
            this.ParticleCapTrackBar.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.ParticleCapTrackBar.MouseHover += new System.EventHandler(this.ParticleCapTrackBar_MouseHover);
            // 
            // TextureResTrackBar
            // 
            this.TextureResTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.TextureResTrackBar.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.TextureResTrackBar, "TextureResTrackBar");
            this.TextureResTrackBar.Maximum = 3;
            this.TextureResTrackBar.Minimum = 1;
            this.TextureResTrackBar.Name = "TextureResTrackBar";
            this.TextureResTrackBar.TabStop = true;
            this.TextureResTrackBar.Value = 1;
            this.TextureResTrackBar.Scroll += new System.EventHandler(this.TextureResTrackBar_Scroll);
            this.TextureResTrackBar.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.TextureResTrackBar.MouseHover += new System.EventHandler(this.TextureResTrackBar_MouseHover);
            // 
            // CameraHeightTrackBar
            // 
            this.CameraHeightTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.CameraHeightTrackBar.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.CameraHeightTrackBar, "CameraHeightTrackBar");
            this.CameraHeightTrackBar.Maximum = 750;
            this.CameraHeightTrackBar.Minimum = 392;
            this.CameraHeightTrackBar.Name = "CameraHeightTrackBar";
            this.CameraHeightTrackBar.TabStop = true;
            this.CameraHeightTrackBar.Scroll += new System.EventHandler(this.CameraHeightTrackBar_Scroll);
            this.CameraHeightTrackBar.MouseLeave += new System.EventHandler(this.Option_MouseLeave);
            this.CameraHeightTrackBar.MouseHover += new System.EventHandler(this.CameraHeightTrackBar_MouseHover);
            // 
            // OptionsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Contra.Properties.Resources._bg_options;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.NoPreviewText);
            this.Controls.Add(this.ExtraBuildingPropsCheckBox);
            this.Controls.Add(this.CameoResPanel);
            this.Controls.Add(this.ControlBarPanel);
            this.Controls.Add(this.AnisoCheckBox);
            this.Controls.Add(this.GraphicsInfoTextPanel);
            this.Controls.Add(this.GraphicsInfoPictureBox);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.AcceptBtn);
            this.Controls.Add(this.ParticleCapLabel);
            this.Controls.Add(this.TextureResLabel);
            this.Controls.Add(this.ParticleCapTrackBar);
            this.Controls.Add(this.TextureResTrackBar);
            this.Controls.Add(this.Shadows3DCheckBox);
            this.Controls.Add(this.Shadows2DCheckBox);
            this.Controls.Add(this.CloudShadowsCheckBox);
            this.Controls.Add(this.ExtraGroundLightingCheckBox);
            this.Controls.Add(this.SmoothWaterBordersCheckBox);
            this.Controls.Add(this.BehindBuildingsCheckBox);
            this.Controls.Add(this.ShowPropsCheckBox);
            this.Controls.Add(this.ExtraAnimationsCheckBox);
            this.Controls.Add(this.DisableDynamicLODCheckBox);
            this.Controls.Add(this.WaterEffectsCheckBox);
            this.Controls.Add(this.CameraHeightLabel);
            this.Controls.Add(this.CameraHeightTrackBar);
            this.Controls.Add(this.camOkButton);
            this.Controls.Add(this.HeatEffectsCheckBox);
            this.Controls.Add(this.labelResolution);
            this.Controls.Add(this.resolutionComboBox);
            this.Controls.Add(this.LangFilterCheckBox);
            this.Controls.Add(this.FogCheckBox);
            this.Controls.Add(this.MinBtnSm);
            this.Controls.Add(this.ExitBtnSm);
            this.Controls.Add(this.HotkeyStylePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "OptionsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsInfoPictureBox)).EndInit();
            this.GraphicsInfoTextPanel.ResumeLayout(false);
            this.GraphicsInfoTextPanel.PerformLayout();
            this.HotkeyStylePanel.ResumeLayout(false);
            this.HotkeyStylePanel.PerformLayout();
            this.ControlBarPanel.ResumeLayout(false);
            this.ControlBarPanel.PerformLayout();
            this.CameoResPanel.ResumeLayout(false);
            this.CameoResPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button MinBtnSm;
        private System.Windows.Forms.Button ExitBtnSm;
        private System.Windows.Forms.CheckBox LangFilterCheckBox;
        private System.Windows.Forms.CheckBox FogCheckBox;
        private System.Windows.Forms.ComboBox resolutionComboBox;
        private System.Windows.Forms.Label labelResolution;
        private System.Windows.Forms.CheckBox HeatEffectsCheckBox;
        private TrackBar CameraHeightTrackBar;
        private System.Windows.Forms.Label CameraHeightLabel;
        private System.Windows.Forms.CheckBox WaterEffectsCheckBox;
        private System.Windows.Forms.CheckBox DisableDynamicLODCheckBox;
        private System.Windows.Forms.CheckBox ExtraAnimationsCheckBox;
        private System.Windows.Forms.CheckBox ShowPropsCheckBox;
        private System.Windows.Forms.CheckBox BehindBuildingsCheckBox;
        private System.Windows.Forms.CheckBox Shadows3DCheckBox;
        private System.Windows.Forms.CheckBox Shadows2DCheckBox;
        private System.Windows.Forms.CheckBox CloudShadowsCheckBox;
        private System.Windows.Forms.CheckBox ExtraGroundLightingCheckBox;
        private System.Windows.Forms.CheckBox SmoothWaterBordersCheckBox;
        private System.Windows.Forms.RadioButton LegacyHotkeysRadioButton;
        private System.Windows.Forms.RadioButton LeikezeHotkeysRadioButton;
        private TrackBar TextureResTrackBar;
        private TrackBar ParticleCapTrackBar;
        private System.Windows.Forms.Label TextureResLabel;
        private System.Windows.Forms.Label ParticleCapLabel;
        private System.Windows.Forms.Button AcceptBtn;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Button camOkButton;
        private System.Windows.Forms.PictureBox GraphicsInfoPictureBox;
        private System.Windows.Forms.Label GraphicsInfoDescriptionLabel;
        private System.Windows.Forms.Panel GraphicsInfoTextPanel;
        private System.Windows.Forms.Label GraphicsInfoPerformanceLabel;
        private System.Windows.Forms.Panel HotkeyStylePanel;
        private System.Windows.Forms.Label HotkeyStyleLabel;
        private System.Windows.Forms.CheckBox AnisoCheckBox;
        private System.Windows.Forms.Panel ControlBarPanel;
        private System.Windows.Forms.RadioButton ControlBarContraRadioButton;
        private System.Windows.Forms.RadioButton ControlBarProRadioButton;
        private System.Windows.Forms.Label ControlBarLabel;
        private System.Windows.Forms.Panel CameoResPanel;
        private System.Windows.Forms.RadioButton CameosStandardRadioButton;
        private System.Windows.Forms.RadioButton CameosDoubleRadioButton;
        private System.Windows.Forms.Label GraphicsInfoHeaderLabel;
        private System.Windows.Forms.Label IconQualityLabel;
        private System.Windows.Forms.RadioButton ControlBarStandardRadioButton;
        private System.Windows.Forms.CheckBox ExtraBuildingPropsCheckBox;
        private System.Windows.Forms.Label NoPreviewText;
    }
}