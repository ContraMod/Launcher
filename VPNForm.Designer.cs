namespace Contra
{
    partial class VPNForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VPNForm));
            this.button18 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.UPnPCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoConnectCheckBox = new System.Windows.Forms.CheckBox();
            this.buttonVPNdebuglog = new System.Windows.Forms.Button();
            this.buttonVPNinvkey = new System.Windows.Forms.Button();
            this.buttonVPNconsole = new System.Windows.Forms.Button();
            this.InvitePanel = new System.Windows.Forms.Panel();
            this.labelEnterInvite = new System.Windows.Forms.Label();
            this.buttonVPNinvclose = new System.Windows.Forms.Button();
            this.buttonVPNinvOK = new System.Windows.Forms.Button();
            this.invkeytextBox = new System.Windows.Forms.TextBox();
            this.labelInvite = new System.Windows.Forms.Label();
            this.labelConsole = new System.Windows.Forms.Label();
            this.labelMonitor = new System.Windows.Forms.Label();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.portOkButton = new System.Windows.Forms.Button();
            this.showConsoleCheckBox = new System.Windows.Forms.CheckBox();
            this.IP_Label1 = new System.Windows.Forms.Label();
            this.KillTincTimer = new System.Windows.Forms.Timer(this.components);
            this.InvitePanel.SuspendLayout();
            this.SuspendLayout();
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
            // UPnPCheckBox
            // 
            resources.ApplyResources(this.UPnPCheckBox, "UPnPCheckBox");
            this.UPnPCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.UPnPCheckBox.ForeColor = System.Drawing.Color.White;
            this.UPnPCheckBox.Name = "UPnPCheckBox";
            this.UPnPCheckBox.UseVisualStyleBackColor = false;
            this.UPnPCheckBox.CheckedChanged += new System.EventHandler(this.UPnPCheckBox_CheckedChanged);
            // 
            // AutoConnectCheckBox
            // 
            resources.ApplyResources(this.AutoConnectCheckBox, "AutoConnectCheckBox");
            this.AutoConnectCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.AutoConnectCheckBox.ForeColor = System.Drawing.Color.White;
            this.AutoConnectCheckBox.Name = "AutoConnectCheckBox";
            this.AutoConnectCheckBox.UseVisualStyleBackColor = false;
            this.AutoConnectCheckBox.CheckedChanged += new System.EventHandler(this.AutoConnectCheckBox_CheckedChanged);
            // 
            // buttonVPNdebuglog
            // 
            this.buttonVPNdebuglog.BackColor = System.Drawing.Color.Transparent;
            this.buttonVPNdebuglog.BackgroundImage = global::Contra.Properties.Resources._button_log;
            this.buttonVPNdebuglog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonVPNdebuglog.FlatAppearance.BorderSize = 0;
            this.buttonVPNdebuglog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonVPNdebuglog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.buttonVPNdebuglog, "buttonVPNdebuglog");
            this.buttonVPNdebuglog.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonVPNdebuglog.Name = "buttonVPNdebuglog";
            this.buttonVPNdebuglog.UseVisualStyleBackColor = false;
            this.buttonVPNdebuglog.Click += new System.EventHandler(this.buttonVPNdebuglog_Click);
            this.buttonVPNdebuglog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonVPNdebuglog_MouseDown);
            this.buttonVPNdebuglog.MouseEnter += new System.EventHandler(this.buttonVPNdebuglog_MouseEnter);
            this.buttonVPNdebuglog.MouseLeave += new System.EventHandler(this.buttonVPNdebuglog_MouseLeave);
            // 
            // buttonVPNinvkey
            // 
            this.buttonVPNinvkey.BackColor = System.Drawing.Color.Transparent;
            this.buttonVPNinvkey.BackgroundImage = global::Contra.Properties.Resources._button_invite;
            this.buttonVPNinvkey.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonVPNinvkey.FlatAppearance.BorderSize = 0;
            this.buttonVPNinvkey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonVPNinvkey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.buttonVPNinvkey, "buttonVPNinvkey");
            this.buttonVPNinvkey.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonVPNinvkey.Name = "buttonVPNinvkey";
            this.buttonVPNinvkey.UseVisualStyleBackColor = false;
            this.buttonVPNinvkey.Click += new System.EventHandler(this.buttonVPNinvkey_Click);
            this.buttonVPNinvkey.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonVPNinvkey_MouseDown);
            this.buttonVPNinvkey.MouseEnter += new System.EventHandler(this.buttonVPNinvkey_MouseEnter);
            this.buttonVPNinvkey.MouseLeave += new System.EventHandler(this.buttonVPNinvkey_MouseLeave);
            // 
            // buttonVPNconsole
            // 
            this.buttonVPNconsole.BackColor = System.Drawing.Color.Transparent;
            this.buttonVPNconsole.BackgroundImage = global::Contra.Properties.Resources._button_console;
            this.buttonVPNconsole.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonVPNconsole.FlatAppearance.BorderSize = 0;
            this.buttonVPNconsole.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonVPNconsole.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.buttonVPNconsole, "buttonVPNconsole");
            this.buttonVPNconsole.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonVPNconsole.Name = "buttonVPNconsole";
            this.buttonVPNconsole.UseVisualStyleBackColor = false;
            this.buttonVPNconsole.Click += new System.EventHandler(this.buttonVPNconsole_Click);
            this.buttonVPNconsole.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonVPNconsole_MouseDown);
            this.buttonVPNconsole.MouseEnter += new System.EventHandler(this.buttonVPNconsole_MouseEnter);
            this.buttonVPNconsole.MouseLeave += new System.EventHandler(this.buttonVPNconsole_MouseLeave);
            // 
            // InvitePanel
            // 
            this.InvitePanel.BackgroundImage = global::Contra.Properties.Resources.invinp;
            this.InvitePanel.Controls.Add(this.labelEnterInvite);
            this.InvitePanel.Controls.Add(this.buttonVPNinvclose);
            this.InvitePanel.Controls.Add(this.buttonVPNinvOK);
            this.InvitePanel.Controls.Add(this.invkeytextBox);
            resources.ApplyResources(this.InvitePanel, "InvitePanel");
            this.InvitePanel.Name = "InvitePanel";
            this.InvitePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.InvitePanel_Paint);
            // 
            // labelEnterInvite
            // 
            resources.ApplyResources(this.labelEnterInvite, "labelEnterInvite");
            this.labelEnterInvite.BackColor = System.Drawing.Color.Transparent;
            this.labelEnterInvite.ForeColor = System.Drawing.Color.Transparent;
            this.labelEnterInvite.Name = "labelEnterInvite";
            // 
            // buttonVPNinvclose
            // 
            this.buttonVPNinvclose.BackColor = System.Drawing.Color.Transparent;
            this.buttonVPNinvclose.BackgroundImage = global::Contra.Properties.Resources.btnOk1;
            this.buttonVPNinvclose.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.buttonVPNinvclose, "buttonVPNinvclose");
            this.buttonVPNinvclose.ForeColor = System.Drawing.Color.White;
            this.buttonVPNinvclose.Name = "buttonVPNinvclose";
            this.buttonVPNinvclose.UseVisualStyleBackColor = false;
            this.buttonVPNinvclose.Click += new System.EventHandler(this.buttonVPNinvclose_Click);
            this.buttonVPNinvclose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonVPNinvclose_MouseDown);
            this.buttonVPNinvclose.MouseLeave += new System.EventHandler(this.buttonVPNinvclose_MouseLeave);
            // 
            // buttonVPNinvOK
            // 
            this.buttonVPNinvOK.BackColor = System.Drawing.Color.Transparent;
            this.buttonVPNinvOK.BackgroundImage = global::Contra.Properties.Resources.btnOk1;
            resources.ApplyResources(this.buttonVPNinvOK, "buttonVPNinvOK");
            this.buttonVPNinvOK.FlatAppearance.BorderSize = 0;
            this.buttonVPNinvOK.ForeColor = System.Drawing.Color.White;
            this.buttonVPNinvOK.Name = "buttonVPNinvOK";
            this.buttonVPNinvOK.UseVisualStyleBackColor = false;
            this.buttonVPNinvOK.Click += new System.EventHandler(this.buttonVPNinvOK_Click);
            this.buttonVPNinvOK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonVPNinvOK_MouseDown);
            this.buttonVPNinvOK.MouseLeave += new System.EventHandler(this.buttonVPNinvOK_MouseLeave);
            // 
            // invkeytextBox
            // 
            resources.ApplyResources(this.invkeytextBox, "invkeytextBox");
            this.invkeytextBox.Name = "invkeytextBox";
            this.invkeytextBox.TextChanged += new System.EventHandler(this.invkeytextBox_TextChanged);
            // 
            // labelInvite
            // 
            this.labelInvite.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelInvite, "labelInvite");
            this.labelInvite.ForeColor = System.Drawing.Color.Transparent;
            this.labelInvite.Name = "labelInvite";
            // 
            // labelConsole
            // 
            this.labelConsole.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelConsole, "labelConsole");
            this.labelConsole.ForeColor = System.Drawing.Color.Transparent;
            this.labelConsole.Name = "labelConsole";
            // 
            // labelMonitor
            // 
            this.labelMonitor.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelMonitor, "labelMonitor");
            this.labelMonitor.ForeColor = System.Drawing.Color.Transparent;
            this.labelMonitor.Name = "labelMonitor";
            // 
            // toolTip2
            // 
            this.toolTip2.AutoPopDelay = 5000;
            this.toolTip2.InitialDelay = 50;
            this.toolTip2.ReshowDelay = 100;
            // 
            // portTextBox
            // 
            resources.ApplyResources(this.portTextBox, "portTextBox");
            this.portTextBox.Name = "portTextBox";
            // 
            // labelPort
            // 
            resources.ApplyResources(this.labelPort, "labelPort");
            this.labelPort.BackColor = System.Drawing.Color.Transparent;
            this.labelPort.ForeColor = System.Drawing.Color.White;
            this.labelPort.Name = "labelPort";
            // 
            // portOkButton
            // 
            this.portOkButton.BackColor = System.Drawing.Color.Transparent;
            this.portOkButton.BackgroundImage = global::Contra.Properties.Resources.btnOk2;
            this.portOkButton.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.portOkButton, "portOkButton");
            this.portOkButton.ForeColor = System.Drawing.Color.White;
            this.portOkButton.Name = "portOkButton";
            this.portOkButton.UseVisualStyleBackColor = false;
            this.portOkButton.Click += new System.EventHandler(this.portOkButton_Click);
            this.portOkButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.portOkButton_MouseDown);
            this.portOkButton.MouseLeave += new System.EventHandler(this.portOkButton_MouseLeave);
            // 
            // showConsoleCheckBox
            // 
            resources.ApplyResources(this.showConsoleCheckBox, "showConsoleCheckBox");
            this.showConsoleCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.showConsoleCheckBox.ForeColor = System.Drawing.Color.White;
            this.showConsoleCheckBox.Name = "showConsoleCheckBox";
            this.showConsoleCheckBox.UseVisualStyleBackColor = false;
            this.showConsoleCheckBox.CheckedChanged += new System.EventHandler(this.showConsoleCheckBox_CheckedChanged);
            // 
            // IP_Label1
            // 
            resources.ApplyResources(this.IP_Label1, "IP_Label1");
            this.IP_Label1.BackColor = System.Drawing.Color.Transparent;
            this.IP_Label1.ForeColor = System.Drawing.Color.White;
            this.IP_Label1.Name = "IP_Label1";
            // 
            // KillTincTimer
            // 
            this.KillTincTimer.Interval = 10000;
            this.KillTincTimer.Tick += new System.EventHandler(this.KillTincTimer_Tick);
            // 
            // VPNForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::Contra.Properties.Resources.vpnbg;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.InvitePanel);
            this.Controls.Add(this.IP_Label1);
            this.Controls.Add(this.portOkButton);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.labelMonitor);
            this.Controls.Add(this.labelConsole);
            this.Controls.Add(this.labelInvite);
            this.Controls.Add(this.buttonVPNconsole);
            this.Controls.Add(this.buttonVPNinvkey);
            this.Controls.Add(this.buttonVPNdebuglog);
            this.Controls.Add(this.AutoConnectCheckBox);
            this.Controls.Add(this.UPnPCheckBox);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.showConsoleCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "VPNForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.VPNForm_Load);
            this.InvitePanel.ResumeLayout(false);
            this.InvitePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.CheckBox UPnPCheckBox;
        private System.Windows.Forms.CheckBox AutoConnectCheckBox;
        private System.Windows.Forms.Button buttonVPNdebuglog;
        private System.Windows.Forms.Button buttonVPNinvkey;
        private System.Windows.Forms.Button buttonVPNconsole;
        private System.Windows.Forms.Panel InvitePanel;
        private System.Windows.Forms.Button buttonVPNinvclose;
        private System.Windows.Forms.Button buttonVPNinvOK;
        private System.Windows.Forms.TextBox invkeytextBox;
        private System.Windows.Forms.Label labelEnterInvite;
        private System.Windows.Forms.Label labelInvite;
        private System.Windows.Forms.Label labelConsole;
        private System.Windows.Forms.Label labelMonitor;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Button portOkButton;
        private System.Windows.Forms.CheckBox showConsoleCheckBox;
        private System.Windows.Forms.Label IP_Label1;
        private System.Windows.Forms.Timer KillTincTimer;
    }
}