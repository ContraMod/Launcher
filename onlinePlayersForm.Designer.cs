namespace Contra
{
    partial class onlinePlayersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(onlinePlayersForm));
            this.onlinePlayersTextBox = new System.Windows.Forms.RichTextBox();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.refreshOnlinePlayersBtn = new System.Windows.Forms.Button();
            this.onlinePlayersLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.playersOnlineLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.IP_Label = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // onlinePlayersTextBox
            // 
            this.onlinePlayersTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(18)))), ((int)(((byte)(7)))));
            this.onlinePlayersTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.onlinePlayersTextBox.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.onlinePlayersTextBox.ForeColor = System.Drawing.Color.White;
            this.onlinePlayersTextBox.Location = new System.Drawing.Point(183, 561);
            this.onlinePlayersTextBox.Name = "onlinePlayersTextBox";
            this.onlinePlayersTextBox.ReadOnly = true;
            this.onlinePlayersTextBox.Size = new System.Drawing.Size(114, 27);
            this.onlinePlayersTextBox.TabIndex = 1;
            this.onlinePlayersTextBox.Text = "";
            this.onlinePlayersTextBox.Visible = false;
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button17.BackgroundImage = global::Contra.Properties.Resources.min;
            this.button17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button17.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button17.FlatAppearance.BorderSize = 0;
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button17.Location = new System.Drawing.Point(233, 23);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(20, 20);
            this.button17.TabIndex = 24;
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            this.button17.MouseEnter += new System.EventHandler(this.button17_MouseEnter);
            this.button17.MouseLeave += new System.EventHandler(this.button17_MouseLeave);
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button18.BackgroundImage = global::Contra.Properties.Resources.exit1;
            this.button18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button18.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button18.FlatAppearance.BorderSize = 0;
            this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button18.Location = new System.Drawing.Point(259, 23);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(20, 20);
            this.button18.TabIndex = 26;
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            this.button18.MouseEnter += new System.EventHandler(this.button18_MouseEnter);
            this.button18.MouseLeave += new System.EventHandler(this.button18_MouseLeave);
            // 
            // refreshOnlinePlayersBtn
            // 
            this.refreshOnlinePlayersBtn.BackColor = System.Drawing.Color.Transparent;
            this.refreshOnlinePlayersBtn.BackgroundImage = global::Contra.Properties.Resources.refresh;
            this.refreshOnlinePlayersBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.refreshOnlinePlayersBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshOnlinePlayersBtn.FlatAppearance.BorderSize = 0;
            this.refreshOnlinePlayersBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshOnlinePlayersBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.refreshOnlinePlayersBtn.Location = new System.Drawing.Point(163, 39);
            this.refreshOnlinePlayersBtn.Name = "refreshOnlinePlayersBtn";
            this.refreshOnlinePlayersBtn.Size = new System.Drawing.Size(32, 32);
            this.refreshOnlinePlayersBtn.TabIndex = 56;
            this.refreshOnlinePlayersBtn.UseVisualStyleBackColor = false;
            this.refreshOnlinePlayersBtn.Click += new System.EventHandler(this.refreshOnlinePlayersBtn_Click);
            this.refreshOnlinePlayersBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onlinePlayersBtn_MouseDown);
            this.refreshOnlinePlayersBtn.MouseEnter += new System.EventHandler(this.onlinePlayersBtn_MouseEnter);
            this.refreshOnlinePlayersBtn.MouseLeave += new System.EventHandler(this.onlinePlayersBtn_MouseLeave);
            this.refreshOnlinePlayersBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.refreshOnlinePlayersBtn_MouseUp);
            // 
            // onlinePlayersLabel
            // 
            this.onlinePlayersLabel.AutoSize = true;
            this.onlinePlayersLabel.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.onlinePlayersLabel.ForeColor = System.Drawing.Color.White;
            this.onlinePlayersLabel.Location = new System.Drawing.Point(8, 10);
            this.onlinePlayersLabel.Name = "onlinePlayersLabel";
            this.onlinePlayersLabel.Size = new System.Drawing.Size(20, 18);
            this.onlinePlayersLabel.TabIndex = 57;
            this.onlinePlayersLabel.Text = "...";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Contra.Properties.Resources.blackDot;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.onlinePlayersLabel);
            this.panel1.Location = new System.Drawing.Point(21, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 502);
            this.panel1.TabIndex = 58;
            // 
            // playersOnlineLabel
            // 
            this.playersOnlineLabel.AutoSize = true;
            this.playersOnlineLabel.BackColor = System.Drawing.Color.Transparent;
            this.playersOnlineLabel.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.playersOnlineLabel.ForeColor = System.Drawing.Color.White;
            this.playersOnlineLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.playersOnlineLabel.Location = new System.Drawing.Point(18, 48);
            this.playersOnlineLabel.Name = "playersOnlineLabel";
            this.playersOnlineLabel.Size = new System.Drawing.Size(100, 18);
            this.playersOnlineLabel.TabIndex = 59;
            this.playersOnlineLabel.Text = "Players online:";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 50;
            this.toolTip1.ReshowDelay = 100;
            // 
            // IP_Label
            // 
            this.IP_Label.AutoSize = true;
            this.IP_Label.BackColor = System.Drawing.Color.Transparent;
            this.IP_Label.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.IP_Label.ForeColor = System.Drawing.Color.White;
            this.IP_Label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.IP_Label.Location = new System.Drawing.Point(18, 23);
            this.IP_Label.Name = "IP_Label";
            this.IP_Label.Size = new System.Drawing.Size(156, 18);
            this.IP_Label.TabIndex = 60;
            this.IP_Label.Text = "ContraVPN IP: unknown";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 2500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // onlinePlayersForm
            // 
            this.BackgroundImage = global::Contra.Properties.Resources.playersOnline;
            this.ClientSize = new System.Drawing.Size(300, 600);
            this.ControlBox = false;
            this.Controls.Add(this.IP_Label);
            this.Controls.Add(this.playersOnlineLabel);
            this.Controls.Add(this.refreshOnlinePlayersBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.onlinePlayersTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "onlinePlayersForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Online Players";
            this.Load += new System.EventHandler(this.onlinePlayersForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox onlinePlayersTextBox;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button refreshOnlinePlayersBtn;
        private System.Windows.Forms.Label onlinePlayersLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label playersOnlineLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label IP_Label;
        private System.Windows.Forms.Timer refreshTimer;
    }
}