using System;
using System.Drawing;
using System.Windows.Forms;

namespace Contra
{
    /// <summary>
    /// Utility class for managing control colors, especially disabled states
    /// </summary>
    public static class ControlColorHelper
    {
        /// <summary>
        /// Sets the disabled text color for a radio button
        /// </summary>
        /// <param name="radioButton">The radio button to modify</param>
        /// <param name="disabledColor">The color to use when disabled (optional, uses Globals.disabledTextColor if null)</param>
        public static void SetDisabledTextColor(RadioButton radioButton, Color? disabledColor = null)
        {
            if (radioButton is CustomRadioButton customRadio)
            {
                customRadio.DisabledForeColor = disabledColor ?? Globals.disabledTextColor;
            }
            else
            {
                // For regular RadioButtons, we need to handle this differently
                // This is a workaround since regular RadioButtons don't support custom disabled colors
                radioButton.ForeColor = radioButton.Enabled ? Color.White : (disabledColor ?? Globals.disabledTextColor);
            }
        }

        /// <summary>
        /// Sets the disabled text color for multiple radio buttons
        /// </summary>
        /// <param name="radioButtons">Array of radio buttons to modify</param>
        /// <param name="disabledColor">The color to use when disabled (optional, uses Globals.disabledTextColor if null)</param>
        public static void SetDisabledTextColor(RadioButton[] radioButtons, Color? disabledColor = null)
        {
            foreach (var radioButton in radioButtons)
            {
                SetDisabledTextColor(radioButton, disabledColor);
            }
        }

        /// <summary>
        /// Sets the disabled text color for a checkbox
        /// </summary>
        /// <param name="checkBox">The checkbox to modify</param>
        /// <param name="disabledColor">The color to use when disabled (optional, uses Globals.disabledTextColor if null)</param>
        public static void SetDisabledTextColor(CheckBox checkBox, Color? disabledColor = null)
        {
            checkBox.ForeColor = checkBox.Enabled ? Color.White : (disabledColor ?? Globals.disabledTextColor);
        }

        /// <summary>
        /// Sets the disabled text color for a label
        /// </summary>
        /// <param name="label">The label to modify</param>
        /// <param name="disabledColor">The color to use when disabled (optional, uses Globals.disabledTextColor if null)</param>
        public static void SetDisabledTextColor(Label label, Color? disabledColor = null)
        {
            label.ForeColor = label.Enabled ? Color.White : (disabledColor ?? Globals.disabledTextColor);
        }

        /// <summary>
        /// Sets the disabled text color for a button
        /// </summary>
        /// <param name="button">The button to modify</param>
        /// <param name="disabledColor">The color to use when disabled (optional, uses Globals.disabledTextColor if null)</param>
        public static void SetDisabledTextColor(Button button, Color? disabledColor = null)
        {
            button.ForeColor = button.Enabled ? Color.White : (disabledColor ?? Globals.disabledTextColor);
        }

        /// <summary>
        /// Applies disabled text color to all controls in a container
        /// </summary>
        /// <param name="container">The container (Form, Panel, etc.) containing the controls</param>
        /// <param name="disabledColor">The color to use when disabled (optional, uses Globals.disabledTextColor if null)</param>
        public static void ApplyDisabledTextColorToContainer(Control container, Color? disabledColor = null)
        {
            ApplyDisabledTextColorToControls(container.Controls, disabledColor);
        }

        /// <summary>
        /// Recursively applies disabled text color to all controls in a collection
        /// </summary>
        /// <param name="controls">The collection of controls</param>
        /// <param name="disabledColor">The color to use when disabled (optional, uses Globals.disabledTextColor if null)</param>
        private static void ApplyDisabledTextColorToControls(Control.ControlCollection controls, Color? disabledColor = null)
        {
            foreach (Control control in controls)
            {
                switch (control)
                {
                    case RadioButton radioButton:
                        SetDisabledTextColor(radioButton, disabledColor);
                        break;
                    case CheckBox checkBox:
                        SetDisabledTextColor(checkBox, disabledColor);
                        break;
                    case Label label:
                        SetDisabledTextColor(label, disabledColor);
                        break;
                    case Button button:
                        SetDisabledTextColor(button, disabledColor);
                        break;
                }

                // Recursively apply to child controls
                if (control.HasChildren)
                {
                    ApplyDisabledTextColorToControls(control.Controls, disabledColor);
                }
            }
        }

        /// <summary>
        /// Updates the text color of a control based on its enabled state
        /// </summary>
        /// <param name="control">The control to update</param>
        /// <param name="enabledColor">The color to use when enabled (optional, uses Color.White if null)</param>
        /// <param name="disabledColor">The color to use when disabled (optional, uses Globals.disabledTextColor if null)</param>
        public static void UpdateTextColorBasedOnState(Control control, Color? enabledColor = null, Color? disabledColor = null)
        {
            Color targetColor = control.Enabled ? (enabledColor ?? Color.White) : (disabledColor ?? Globals.disabledTextColor);
            
            switch (control)
            {
                case RadioButton radioButton:
                    radioButton.ForeColor = targetColor;
                    break;
                case CheckBox checkBox:
                    checkBox.ForeColor = targetColor;
                    break;
                case Label label:
                    label.ForeColor = targetColor;
                    break;
                case Button button:
                    button.ForeColor = targetColor;
                    break;
            }
        }
    }
}

