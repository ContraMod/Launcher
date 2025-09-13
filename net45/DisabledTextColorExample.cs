using System;
using System.Drawing;
using System.Windows.Forms;

namespace Contra
{
    /// <summary>
    /// Example class demonstrating how to use disabled text color functionality
    /// </summary>
    public partial class DisabledTextColorExample : Form
    {
        private CustomRadioButton customRadioButton;
        private RadioButton regularRadioButton;
        private CheckBox checkBox;
        private Label label;
        private Button toggleButton;

        public DisabledTextColorExample()
        {
            InitializeComponent();
            SetupControls();
            SetupEventHandlers();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form setup
            this.Text = "Disabled Text Color Example";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.DarkBlue; // Dark background to match your app theme

            this.ResumeLayout(false);
        }

        private void SetupControls()
        {
            // Create a custom radio button with disabled text color support
            customRadioButton = new CustomRadioButton
            {
                Text = "Custom Radio Button",
                Location = new Point(20, 20),
                Size = new Size(200, 20),
                ForeColor = Color.White,
                DisabledForeColor = Globals.disabledTextColor,
                Enabled = true
            };

            // Create a regular radio button
            regularRadioButton = new RadioButton
            {
                Text = "Regular Radio Button",
                Location = new Point(20, 50),
                Size = new Size(200, 20),
                ForeColor = Color.White,
                Enabled = true
            };

            // Create a checkbox
            checkBox = new CheckBox
            {
                Text = "Checkbox Example",
                Location = new Point(20, 80),
                Size = new Size(200, 20),
                ForeColor = Color.White,
                Enabled = true
            };

            // Create a label
            label = new Label
            {
                Text = "Label Example",
                Location = new Point(20, 110),
                Size = new Size(200, 20),
                ForeColor = Color.White,
                Enabled = true
            };

            // Create a toggle button to enable/disable controls
            toggleButton = new Button
            {
                Text = "Toggle Controls",
                Location = new Point(20, 150),
                Size = new Size(120, 30),
                ForeColor = Color.White,
                BackColor = Color.DarkGray
            };

            // Add controls to form
            this.Controls.Add(customRadioButton);
            this.Controls.Add(regularRadioButton);
            this.Controls.Add(checkBox);
            this.Controls.Add(label);
            this.Controls.Add(toggleButton);
        }

        private void SetupEventHandlers()
        {
            toggleButton.Click += ToggleButton_Click;
        }

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            // Toggle the enabled state of all controls
            bool newState = !customRadioButton.Enabled;
            
            customRadioButton.Enabled = newState;
            regularRadioButton.Enabled = newState;
            checkBox.Enabled = newState;
            label.Enabled = newState;

            // Update text colors based on new state
            ControlColorHelper.UpdateTextColorBasedOnState(regularRadioButton);
            ControlColorHelper.UpdateTextColorBasedOnState(checkBox);
            ControlColorHelper.UpdateTextColorBasedOnState(label);

            // Update button text
            toggleButton.Text = newState ? "Disable Controls" : "Enable Controls";
        }

        /// <summary>
        /// Example method showing how to apply disabled colors to existing controls in your MainForm
        /// </summary>
        public static void ApplyToMainFormControls(MainForm mainForm)
        {
            // Example 1: Apply to specific radio buttons
            RadioButton[] radioButtons = { mainForm.RadioEN, mainForm.RadioRU, mainForm.MNew, mainForm.MStandard };
            ControlColorHelper.SetDisabledTextColor(radioButtons);

            // Example 2: Apply to specific checkboxes
            ControlColorHelper.SetDisabledTextColor(mainForm.QSCheckBox);
            ControlColorHelper.SetDisabledTextColor(mainForm.WinCheckBox);
            ControlColorHelper.SetDisabledTextColor(mainForm.DefaultPics);

            // Example 3: Apply to all controls in the form
            ControlColorHelper.ApplyDisabledTextColorToContainer(mainForm);

            // Example 4: Custom disabled color
            Color customDisabledColor = Color.FromArgb(100, 100, 100); // Darker gray
            ControlColorHelper.SetDisabledTextColor(mainForm.RadioEN, customDisabledColor);
        }

        /// <summary>
        /// Example method showing how to apply disabled colors to OptionsForm controls
        /// </summary>
        public static void ApplyToOptionsFormControls(OptionsForm optionsForm)
        {
            // Apply to all radio buttons in the options form
            ControlColorHelper.ApplyDisabledTextColorToContainer(optionsForm);

            // Or apply to specific controls
            // ControlColorHelper.SetDisabledTextColor(optionsForm.ControlBarStandardRadioButton);
            // ControlColorHelper.SetDisabledTextColor(optionsForm.ControlBarContraRadioButton);
            // ControlColorHelper.SetDisabledTextColor(optionsForm.ControlBarProRadioButton);
        }

        /// <summary>
        /// Example of how to handle control state changes in your existing event handlers
        /// </summary>
        public static void HandleControlStateChange(Control control)
        {
            // This method can be called from your existing event handlers
            // when you need to enable/disable controls
            ControlColorHelper.UpdateTextColorBasedOnState(control);
        }

        /// <summary>
        /// Example of creating a custom radio button with specific disabled color
        /// </summary>
        public static CustomRadioButton CreateCustomRadioButton(string text, Point location, Color disabledColor)
        {
            return new CustomRadioButton
            {
                Text = text,
                Location = location,
                Size = new Size(200, 20),
                ForeColor = Color.White,
                DisabledForeColor = disabledColor,
                Enabled = true
            };
        }
    }
}

