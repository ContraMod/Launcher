using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Contra
{
    public partial class moreOptionsForm : Form
    {
        public moreOptionsForm()
        {
            InitializeComponent();
            FogCheckBox.TabStop = false;
            LangFilterCheckBox.TabStop = false;
            button17.TabStop = false;
            button18.TabStop = false;
            comboBox1.TabStop = false;
            resOkButton.TabStop = false;

            if (Globals.GB_Checked == true)
            {
                toolTip3.SetToolTip(FogCheckBox, "Toggle fog (depth of field) effects on/off.\nThis effect adds a color layer at the top of the screen, depending on the map.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Disabling the language filter will show bad words written by players in chat.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Heat effects are standard graphical effects from Zero Hour. The area near heat sources distorts.\nTurn this off if your screen randomly turns black while playing.");
            }
            else if (Globals.RU_Checked == true)
            {
                toolTip3.SetToolTip(FogCheckBox, "Эффекты переключения тумана (глубина поля) вкл\\выкл.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Отключение языкового фильтра покажет плохие слова, написанные игроками в чате.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Тепловые эффекты - это стандартные графические эффекты от Zero Hour. Выключите это, если ваш экран случайно становится черным во время игры.");
                labelResolution.Text = "Разрешение экрана:";
                FogCheckBox.Text = "Эффект тумана";
                LangFilterCheckBox.Text = "Языковый фильтр";
                HeatEffectsCheckBox.Text = "Тепловые эффекты";
            }
            else if (Globals.UA_Checked == true)
            {
                toolTip3.SetToolTip(FogCheckBox, "Ефекти перемикання туману (глибина поля) вкл\\викл.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Вимкнення мовного фільтра покаже погані слова, написані гравцями в чаті.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Теплові ефекти є стандартними графічними ефектами від нульового часу. Вимкніть цю функцію, якщо екран у випадковому режимі стане чорним під час відтворення.");
                labelResolution.Text = "Роздільна здатність:";
                FogCheckBox.Text = "Ефект туману";
                LangFilterCheckBox.Text = "Мовний фільтр";
                HeatEffectsCheckBox.Text = "Теплові ефекти";
            }
            else if (Globals.BG_Checked == true)
            {
                toolTip3.SetToolTip(FogCheckBox, "Превключете ефекта \"дълбочина на рязкост\".\nТози ефект добавя цветен слой на върха на екрана, зависещ от атмосферата на картата. Например, мъгла.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Изключването на езиковия филтър ще спре да скрива лошите думи, написани от играчите.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Топлинните ефекти са стандартни графични ефекти в Zero Hour.\nИзключете ги, ако вашият екран става черен, докато играете.");
                labelResolution.Text = "Резолюция:";
                FogCheckBox.Text = "Мъглявинен ефект";
                LangFilterCheckBox.Text = "Езиков филтър";
                HeatEffectsCheckBox.Text = "Топлинни ефекти";
            }
            else if (Globals.DE_Checked == true)
            {
                toolTip3.SetToolTip(FogCheckBox, "Schalte Nebel (Tiefenschдrfe) Effekte An/Aus.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Das ausschalten vom Sprache Filter zeigt bцse Wцrter von anderen Spielern im Chat an.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Wärmeeffekte sind standardmäßige grafische Effekte von Zero Hour. Deaktivieren Sie diese Option, wenn der Bildschirm während des Spiels zufällig schwarz wird.");
                labelResolution.Text = "Auflцsung:";
                FogCheckBox.Text = "Nebel Effekte";
                LangFilterCheckBox.Text = "Sprache Filter";
                HeatEffectsCheckBox.Text = "Wärmeeffekte";
            }

            //Read from Options.ini and check/uncheck Heat Effects checkbox depending on value there:
            if (Directory.Exists(userDataLeafName()))
            {
                string text = File.ReadAllText(userDataLeafName() + "Options.ini");
                {
                    if (text.Contains("HeatEffects = No"))
                    {
                        HeatEffectsCheckBox.Checked = false;
                    }
                    else if (text.Contains("HeatEffects = no"))
                    {
                        HeatEffectsCheckBox.Checked = false;
                    }
                    else if (text.Contains("HeatEffects = Yes"))
                    {
                        HeatEffectsCheckBox.Checked = true;
                    }
                    else if (text.Contains("HeatEffects = yes"))
                    {
                        HeatEffectsCheckBox.Checked = true;
                    }
                    else
                    {
                        //
                    }
                }
            }
            else if (Directory.Exists(myDocPath))
            {
                string text = File.ReadAllText(myDocPath + "Options.ini");
                {
                    if (text.Contains("HeatEffects = No"))
                    {
                        HeatEffectsCheckBox.Checked = false;
                    }
                    else if (text.Contains("HeatEffects = no"))
                    {
                        HeatEffectsCheckBox.Checked = false;
                    }
                    else if (text.Contains("HeatEffects = Yes"))
                    {
                        HeatEffectsCheckBox.Checked = true;
                    }
                    else if (text.Contains("HeatEffects = yes"))
                    {
                        HeatEffectsCheckBox.Checked = true;
                    }
                    else
                    {
                        //
                    }
                }
            }


            //Get current resolution
            {
                if (Directory.Exists(userDataLeafName()))
                {
                    string s = File.ReadAllText(userDataLeafName() + "Options.ini");
                    List<string> found = new List<string>();
                    string line;
                    using (StringReader file = new StringReader(s))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.Contains("Resolution ="))
                            {
                                found.Add(line);
                                s = line;
                                s = s.Substring(s.IndexOf('=') + 2);
                                s = s.TrimEnd();
                                string s2 = s.Replace(" ", "x");
                                //                        MessageBox.Show(s2); //shows current res
                                Properties.Settings.Default.Res = s2;
                                Properties.Settings.Default.Save();
                            }
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string s = File.ReadAllText(myDocPath + "Options.ini");
                    List<string> found = new List<string>();
                    string line;
                    using (StringReader file = new StringReader(s))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.Contains("Resolution ="))
                            {
                                found.Add(line);
                                s = line;
                                s = s.Substring(s.IndexOf('=') + 2);
                                s = s.TrimEnd();
                                string s2 = s.Replace(" ", "x");
                                //                        MessageBox.Show(s2); //shows current res
                                Properties.Settings.Default.Res = s2;
                                Properties.Settings.Default.Save();
                            }
                        }
                    }
                }
                comboBox1.Text = Properties.Settings.Default.Res;

                FogCheckBox.Checked = Properties.Settings.Default.Fog;
                //HeatEffectsCheckBox.Checked = Properties.Settings.Default.HeatEffects;
                LangFilterCheckBox.Checked = Properties.Settings.Default.LangF;
            }
            if (!File.Exists(userDataLeafName() + "Options.ini") && (!File.Exists(myDocPath + "Options.ini")))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Options.ini not found! Could not load current resolution.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Файл \"Options.ini\" не найден! Не удалось загрузить текущее разрешение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Файл Options.ini не знайдений! Не вдалося завантажити поточну роздільну здатність.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Options.ini не беше намерен! Не можа да се зареди текущата резолюция.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Options.ini nicht gefunden! Aktuelle Auflцsung konnte nicht geladen werden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static string userDataLeafName()
        {
            //o gets "Command and Conquer Generals Zero Hour Data" from registry. It varies depending on language
            var o = string.Empty;
            if (Globals.userOS == "32")
            {
                var userDataRegistryPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Electronic Arts\EA Games\Command and Conquer Generals Zero Hour");
                if (userDataRegistryPath != null)
                {
                    o = userDataRegistryPath.GetValue("UserDataLeafName") as string;
                }
            }
            else if (Globals.userOS == "64")
            {
                var userDataRegistryPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Electronic Arts\EA Games\Command and Conquer Generals Zero Hour");
                if (userDataRegistryPath != null)
                {
                    o = userDataRegistryPath.GetValue("UserDataLeafName") as string;
                }
            }
            if (o != null)
            {
                return System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + o + @"\";
            }
            else
            {
                return null;
            }
        }

        public static string myDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Command and Conquer Generals Zero Hour Data\";


        //**********DRAG FORM CODE START**********
        const int WM_NCLBUTTONDBLCLK = 0xA3;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCLBUTTONDBLCLK)
                return;

            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }
            base.WndProc(ref m);
        }
        //**********DRAG FORM CODE END**********


        private void OnApplicationExit(object sender, EventArgs e) //MoreOptionsWindowExit
        {
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            OnApplicationExit(sender, e);
        }

        private void resOkButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(userDataLeafName()))
            {
                string text = File.ReadAllText(userDataLeafName() + "Options.ini");
                {
                    if (!Regex.IsMatch(comboBox1.Text, @"^[0-9]{3,4}x[0-9]{3,4}$")) //if selected res doesn't match valid input (input must match the regex)
                    {
                        if (Globals.GB_Checked == true)
                        {
                            MessageBox.Show("This resolution is not valid.", "Error");
                        }
                        else if (Globals.RU_Checked == true)
                        {
                            MessageBox.Show("Это разрешение экрана не является действительным.", "Ошибка");
                        }
                        else if (Globals.UA_Checked == true)
                        {
                            MessageBox.Show("Це розширення не є дійсним.", "Помилка");
                        }
                        else if (Globals.BG_Checked == true)
                        {
                            MessageBox.Show("Тази резолюция не е валидна.", "Грешка");
                        }
                        else if (Globals.DE_Checked == true)
                        {
                            MessageBox.Show("Diese Auflцsung ist nicht gьltig.", "Fehler");
                        }
                        //return;
                    }
                    else
                    {
                        string fixedText = comboBox1.Text.Replace("x", " ");
                        File.WriteAllText(userDataLeafName() + "Options.ini", Regex.Replace(File.ReadAllText(userDataLeafName() + "Options.ini"), "\r?\nResolution =.*", "\r\nResolution = " + fixedText + "\r"));
                        if (Globals.GB_Checked == true)
                        {
                            MessageBox.Show("Resolution changed successfully!");
                        }
                        else if (Globals.RU_Checked == true)
                        {
                            MessageBox.Show("Разрешение экрана успешно изменено!");
                        }
                        else if (Globals.UA_Checked == true)
                        {
                            MessageBox.Show("Розширення успішно змінено!");
                        }
                        else if (Globals.BG_Checked == true)
                        {
                            MessageBox.Show("Резолюцията беше променена успешно!");
                        }
                        else if (Globals.DE_Checked == true)
                        {
                            MessageBox.Show("Auflцsung erfolgreich geдndert!");
                        }
                    }
                }
            }
            else if (Directory.Exists(myDocPath))
            {
                string text = File.ReadAllText(myDocPath + "Options.ini");
                {
                    if (!Regex.IsMatch(comboBox1.Text, @"^[0-9]{3,4}x[0-9]{3,4}$")) //if selected res doesn't match valid input (input must match the regex)
                    {
                        if (Globals.GB_Checked == true)
                        {
                            MessageBox.Show("This resolution is not valid.", "Error");
                        }
                        else if (Globals.RU_Checked == true)
                        {
                            MessageBox.Show("Это разрешение экрана не является действительным.", "Ошибка");
                        }
                        else if (Globals.UA_Checked == true)
                        {
                            MessageBox.Show("Це розширення не є дійсним.", "Помилка");
                        }
                        else if (Globals.BG_Checked == true)
                        {
                            MessageBox.Show("Тази резолюция не е валидна.", "Грешка");
                        }
                        else if (Globals.DE_Checked == true)
                        {
                            MessageBox.Show("Diese Auflцsung ist nicht gьltig.", "Fehler");
                        }
                        //return;
                    }
                    else
                    {
                        string fixedText = comboBox1.Text.Replace("x", " ");
                        File.WriteAllText(myDocPath + "Options.ini", Regex.Replace(File.ReadAllText(myDocPath + "Options.ini"), "\r?\nResolution =.*", "\r\nResolution = " + fixedText + "\r\n"));
                        if (Globals.GB_Checked == true)
                        {
                            MessageBox.Show("Resolution changed successfully!");
                        }
                        else if (Globals.RU_Checked == true)
                        {
                            MessageBox.Show("Разрешение экрана успешно изменено!");
                        }
                        else if (Globals.UA_Checked == true)
                        {
                            MessageBox.Show("Розширення успішно змінено!");
                        }
                        else if (Globals.BG_Checked == true)
                        {
                            MessageBox.Show("Резолюцията беше променена успешно!");
                        }
                        else if (Globals.DE_Checked == true)
                        {
                            MessageBox.Show("Auflцsung erfolgreich geдndert!");
                        }
                    }
                }
            }
            if (!File.Exists(userDataLeafName() + "Options.ini") && (!File.Exists(myDocPath + "Options.ini")))
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Options.ini not found! Could not set new resolution.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Файл \"Options.ini\" не найден! Не удалось установить новое разрешение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Файл Options.ini не знайдений! Не вдалося встановити нову роздільну здатність.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Options.ini не беше намерен! Не можа да се приложи избраната резолюция.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Options.ini nicht gefunden! Neue Auflцsung konnte nicht eingestellt werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button18_MouseEnter(object sender, EventArgs e)
        {
            button18.BackgroundImage = (System.Drawing.Image)(Properties.Resources.exit11);
        }

        private void button18_MouseLeave(object sender, EventArgs e)
        {
            button18.BackgroundImage = (System.Drawing.Image)(Properties.Resources.exit1);
        }

        private void button17_MouseEnter(object sender, EventArgs e)
        {
            button17.BackgroundImage = (System.Drawing.Image)(Properties.Resources.min11);
        }

        private void button17_MouseLeave(object sender, EventArgs e)
        {
            button17.BackgroundImage = (System.Drawing.Image)(Properties.Resources.min);
        }

        private void FogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!FogCheckBox.Checked)
            {
                Properties.Settings.Default.Fog = false;
                Properties.Settings.Default.Save();
            }
            else Properties.Settings.Default.Fog = true;
            Properties.Settings.Default.Save();
        }

        private void LangFilterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!LangFilterCheckBox.Checked)
            {
                Properties.Settings.Default.LangF = false;
                Properties.Settings.Default.Save();
            }
            else Properties.Settings.Default.LangF = true;
            Properties.Settings.Default.Save();
        }

        private void resOkButton_MouseDown(object sender, MouseEventArgs e)
        {
            resOkButton.BackgroundImage = (System.Drawing.Image)(Properties.Resources.btnOk3a);
            resOkButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void resOkButton_MouseLeave(object sender, EventArgs e)
        {
            resOkButton.BackgroundImage = (System.Drawing.Image)(Properties.Resources.btnOk3);
            resOkButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void HeatEffectsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!HeatEffectsCheckBox.Checked)
            {
                //Properties.Settings.Default.HeatEffects = false;
                //Properties.Settings.Default.Save();
                //Disable Heat Effects. Fixes black screen issue.
                if (Directory.Exists(userDataLeafName()))
                {
                    string text = File.ReadAllText(userDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("HeatEffects = Yes"))
                        {
                            File.WriteAllText(userDataLeafName() + "Options.ini", File.ReadAllText(userDataLeafName() + "Options.ini").Replace("HeatEffects = Yes", "HeatEffects = No"));
                        }
                        else if (text.Contains("HeatEffects = yes"))
                        {
                            File.WriteAllText(userDataLeafName() + "Options.ini", File.ReadAllText(userDataLeafName() + "Options.ini").Replace("HeatEffects = yes", "HeatEffects = No"));
                        }
                        else
                        {
                            //
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("HeatEffects = Yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("HeatEffects = Yes", "HeatEffects = No"));
                        }
                        else if (text.Contains("HeatEffects = yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("HeatEffects = yes", "HeatEffects = No"));
                        }
                        else
                        {
                            //
                        }
                    }
                }
            }
            else
            {
                //Properties.Settings.Default.HeatEffects = true;
                //Properties.Settings.Default.Save();
                //Disable Heat Effects. Fixes black screen issue.
                if (Directory.Exists(userDataLeafName()))
                {
                    string text = File.ReadAllText(userDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("HeatEffects = No"))
                        {
                            File.WriteAllText(userDataLeafName() + "Options.ini", File.ReadAllText(userDataLeafName() + "Options.ini").Replace("HeatEffects = No", "HeatEffects = Yes"));
                        }
                        else if (text.Contains("HeatEffects = no"))
                        {
                            File.WriteAllText(userDataLeafName() + "Options.ini", File.ReadAllText(userDataLeafName() + "Options.ini").Replace("HeatEffects = no", "HeatEffects = Yes"));
                        }
                        else
                        {
                            //
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("HeatEffects = No"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("HeatEffects = No", "HeatEffects = Yes"));
                        }
                        else if (text.Contains("HeatEffects = no"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("HeatEffects = no", "HeatEffects = Yes"));
                        }
                        else
                        {
                            //
                        }
                    }
                }
            }
        }
    }
}
