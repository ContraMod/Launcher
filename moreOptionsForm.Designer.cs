namespace Contra
{
    partial class moreOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(moreOptionsForm));
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.LangFilterCheckBox = new System.Windows.Forms.CheckBox();
            this.FogCheckBox = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelResolution = new System.Windows.Forms.Label();
            this.resOkButton = new System.Windows.Forms.Button();
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.HeatEffectsCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button17.BackgroundImage = global::Contra.Properties.Resources.min;
            resources.ApplyResources(this.button17, "button17");
            this.button17.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button17.FlatAppearance.BorderSize = 0;
            this.button17.Name = "button17";
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            this.button17.MouseEnter += new System.EventHandler(this.button17_MouseEnter);
            this.button17.MouseLeave += new System.EventHandler(this.button17_MouseLeave);
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button18.BackgroundImage = global::Contra.Properties.Resources.exit1;
            resources.ApplyResources(this.button18, "button18");
            this.button18.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button18.FlatAppearance.BorderSize = 0;
            this.button18.Name = "button18";
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            this.button18.MouseEnter += new System.EventHandler(this.button18_MouseEnter);
            this.button18.MouseLeave += new System.EventHandler(this.button18_MouseLeave);
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
            this.LangFilterCheckBox.CheckedChanged += new System.EventHandler(this.LangFilterCheckBox_CheckedChanged);
            // 
            // FogCheckBox
            // 
            resources.ApplyResources(this.FogCheckBox, "FogCheckBox");
            this.FogCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.FogCheckBox.Checked = true;
            this.FogCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FogCheckBox.ForeColor = System.Drawing.Color.White;
            this.FogCheckBox.Name = "FogCheckBox";
            this.FogCheckBox.UseVisualStyleBackColor = false;
            this.FogCheckBox.CheckedChanged += new System.EventHandler(this.FogCheckBox_CheckedChanged);
            // 
            // comboBox1
            // 
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items"),
            resources.GetString("comboBox1.Items1"),
            resources.GetString("comboBox1.Items2"),
            resources.GetString("comboBox1.Items3"),
            resources.GetString("comboBox1.Items4"),
            resources.GetString("comboBox1.Items5"),
            resources.GetString("comboBox1.Items6"),
            resources.GetString("comboBox1.Items7"),
            resources.GetString("comboBox1.Items8"),
            resources.GetString("comboBox1.Items9"),
            resources.GetString("comboBox1.Items10"),
            resources.GetString("comboBox1.Items11"),
            resources.GetString("comboBox1.Items12"),
            resources.GetString("comboBox1.Items13")});
            this.comboBox1.Name = "comboBox1";
            // 
            // labelResolution
            // 
            resources.ApplyResources(this.labelResolution, "labelResolution");
            this.labelResolution.BackColor = System.Drawing.Color.Transparent;
            this.labelResolution.ForeColor = System.Drawing.Color.White;
            this.labelResolution.Name = "labelResolution";
            // 
            // resOkButton
            // 
            this.resOkButton.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.resOkButton, "resOkButton");
            this.resOkButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.resOkButton.FlatAppearance.BorderSize = 0;
            this.resOkButton.ForeColor = System.Drawing.Color.White;
            this.resOkButton.Name = "resOkButton";
            this.resOkButton.UseVisualStyleBackColor = false;
            this.resOkButton.Click += new System.EventHandler(this.resOkButton_Click);
            this.resOkButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.resOkButton_MouseDown);
            this.resOkButton.MouseLeave += new System.EventHandler(this.resOkButton_MouseLeave);
            // 
            // toolTip3
            // 
            this.toolTip3.AutoPopDelay = 5000;
            this.toolTip3.InitialDelay = 50;
            this.toolTip3.ReshowDelay = 100;
            // 
            // HeatEffectsCheckBox
            // 
            resources.ApplyResources(this.HeatEffectsCheckBox, "HeatEffectsCheckBox");
            this.HeatEffectsCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.HeatEffectsCheckBox.ForeColor = System.Drawing.Color.White;
            this.HeatEffectsCheckBox.Name = "HeatEffectsCheckBox";
            this.HeatEffectsCheckBox.UseVisualStyleBackColor = false;
            this.HeatEffectsCheckBox.CheckedChanged += new System.EventHandler(this.HeatEffectsCheckBox_CheckedChanged);
            // 
            // moreOptionsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Contra.Properties.Resources.vpnbg;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.HeatEffectsCheckBox);
            this.Controls.Add(this.resOkButton);
            this.Controls.Add(this.labelResolution);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.LangFilterCheckBox);
            this.Controls.Add(this.FogCheckBox);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button18);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "moreOptionsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.CheckBox LangFilterCheckBox;
        private System.Windows.Forms.CheckBox FogCheckBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label labelResolution;
        private System.Windows.Forms.Button resOkButton;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.CheckBox HeatEffectsCheckBox;
    }
}