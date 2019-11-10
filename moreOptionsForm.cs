using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Text;

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
                toolTip3.SetToolTip(camHeightLabel, "The camera height setting changes the default and maximum player view distance in-game.\nThe higher this value is, the further away the view will be.");
                toolTip3.SetToolTip(WaterEffectsCheckBox, "Toggle water effects on/off.\nThis effect simulates water reflection.");
            }
            else if (Globals.RU_Checked == true)
            {
                toolTip3.SetToolTip(FogCheckBox, "Эффекты переключения тумана (глубина поля) вкл\\выкл.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Отключение языкового фильтра покажет плохие слова, написанные игроками в чате.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Тепловые эффекты - это стандартные графические эффекты от Zero Hour.\nВыключите это, если ваш экран случайно становится черным во время игры.");
                toolTip3.SetToolTip(camHeightLabel, "Настройка высоты камеры изменяет стандартное и максимальное расстояние поле зрения игрока.\nЧем выше это значение, тем дальше будет поле зрения.");
                toolTip3.SetToolTip(WaterEffectsCheckBox, "Эффекты переключения отражение воды вкл\\выкл.");
                labelResolution.Text = "Разрешение экрана:";
                FogCheckBox.Text = "Эффект тумана";
                LangFilterCheckBox.Text = "Языковый фильтр";
                HeatEffectsCheckBox.Text = "Тепловые эффекты";
                camHeightLabel.Text = "Высота камеры: ?";
                WaterEffectsCheckBox.Text = "Эффект воды";
            }
            else if (Globals.UA_Checked == true)
            {
                toolTip3.SetToolTip(FogCheckBox, "Ефекти перемикання туману (глибина поля) вкл\\викл.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Вимкнення мовного фільтра покаже погані слова, написані гравцями в чаті.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Теплові ефекти є стандартними графічними ефектами від нульового часу.\nВимкніть цю функцію, якщо екран у випадковому режимі стане чорним під час відтворення.");
                toolTip3.SetToolTip(camHeightLabel, "Налаштування висоти камери змінює стандартне і максимальна відстань поле зору гравця.\nЧем вище це значення, тим далі буде поле зору.");
                toolTip3.SetToolTip(WaterEffectsCheckBox, "Ефекти перемикання відображення води вкл\\викл.");
                labelResolution.Text = "Роздільна здатність:";
                FogCheckBox.Text = "Ефект туману";
                LangFilterCheckBox.Text = "Мовний фільтр";
                HeatEffectsCheckBox.Text = "Теплові ефекти";
                camHeightLabel.Text = "Висота камери: ?";
                WaterEffectsCheckBox.Text = "Водний ефект";
            }
            else if (Globals.BG_Checked == true)
            {
                toolTip3.SetToolTip(FogCheckBox, "Превключете ефекта \"дълбочина на рязкост\".\nТози ефект добавя цветен слой на върха на екрана, зависещ от атмосферата на картата. Например, мъгла.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Изключването на езиковия филтър ще спре да скрива лошите думи, написани от играчите.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Топлинните ефекти са стандартни графични ефекти в Zero Hour.\nИзключете ги, ако вашият екран става черен, докато играете.");
                toolTip3.SetToolTip(camHeightLabel, "Настройката за височина на камерата променя стандартното и максималното разстояние на изглед на играча.\nКолкото по-висока е тази стойност, толкова по-далеч ще бъде изгледът.");
                toolTip3.SetToolTip(WaterEffectsCheckBox, "Превключете водните ефекти (симулация на слънчево и облачно отражение).");
                labelResolution.Text = "Резолюция:";
                FogCheckBox.Text = "Мъглявинен ефект";
                LangFilterCheckBox.Text = "Езиков филтър";
                HeatEffectsCheckBox.Text = "Топлинни ефекти";
                camHeightLabel.Text = "Височина на камерата: ?";
                WaterEffectsCheckBox.Text = "Водни ефекти";
            }
            else if (Globals.DE_Checked == true)
            {
                toolTip3.SetToolTip(FogCheckBox, "Schalte Nebel (Tiefenschдrfe) Effekte An/Aus.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Das ausschalten vom Sprache Filter zeigt bцse Wцrter von anderen Spielern im Chat an.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Wärmeeffekte sind standardmäßige grafische Effekte von Zero Hour.\nDeaktivieren Sie diese Option, wenn der Bildschirm während des Spiels zufällig schwarz wird.");
                toolTip3.SetToolTip(camHeightLabel, "Mit der Einstellung für die Kamerahöhe werden die Standard- und die maximale Anzeigedistanz des Players geändert.\nJe höher dieser Wert ist, desto weiter entfernt ist die Ansicht.");
                toolTip3.SetToolTip(WaterEffectsCheckBox, "Schalte Wasser Reflexion Effekte An/Aus.");
                labelResolution.Text = "Auflцsung:";
                FogCheckBox.Text = "Nebel Effekte";
                LangFilterCheckBox.Text = "Sprache Filter";
                HeatEffectsCheckBox.Text = "Wärmeeffekte";
                camHeightLabel.Text = "Kamerahöhe: ?";
                WaterEffectsCheckBox.Text = "Wassereffekt";
            }

            //Read from Options.ini and check/uncheck Heat Effects checkbox depending on value there:
            if (Directory.Exists(UserDataLeafName()))
            {
                string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
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


            //Get current camera height
            if (File.Exists("!!!Contra009Final_Patch2_GameData.big"))
            {
                string s = File.ReadAllText("!!!Contra009Final_Patch2_GameData.big");
                List<string> found = new List<string>();
                string line;
                using (StringReader file = new StringReader(s))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains(" MaxCameraHeight ="))
                        {
                            found.Add(line);
                            line = line.Substring(0, line.IndexOf(".") + 1);
                            line = Regex.Replace(line, @"[^\d]", "");
                            if (Globals.GB_Checked == true)
                            {
                                camHeightLabel.Text = "Camera Height: " + camTrackBar.Value.ToString() + ".0";
                                camTrackBar.Value = Convert.ToInt32(line);
                            }
                            else if (Globals.RU_Checked == true)
                            {
                                camHeightLabel.Text = "Высота камеры: " + camTrackBar.Value.ToString() + ".0";
                                camTrackBar.Value = Convert.ToInt32(line);
                            }
                            else if (Globals.UA_Checked == true)
                            {
                                camHeightLabel.Text = "Висота камери: " + camTrackBar.Value.ToString() + ".0";
                                camTrackBar.Value = Convert.ToInt32(line);
                            }
                            else if (Globals.BG_Checked == true)
                            {
                                camHeightLabel.Text = "Височина на камерата: " + camTrackBar.Value.ToString() + ".0";
                                camTrackBar.Value = Convert.ToInt32(line);
                            }
                            else if (Globals.DE_Checked == true)
                            {
                                camHeightLabel.Text = "Kamerahöhe: " + camTrackBar.Value.ToString() + ".0";
                                camTrackBar.Value = Convert.ToInt32(line);
                            }
                        }
                    }
                }
            }


            //Get current resolution
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string s = File.ReadAllText(UserDataLeafName() + "Options.ini");
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
                WaterEffectsCheckBox.Checked = Properties.Settings.Default.WaterEffects;
            }
            if (!File.Exists(UserDataLeafName() + "Options.ini") && (!File.Exists(myDocPath + "Options.ini")))
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

        public static string UserDataLeafName()
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
            if (Directory.Exists(UserDataLeafName()))
            {
                string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
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
                        File.WriteAllText(UserDataLeafName() + "Options.ini", Regex.Replace(File.ReadAllText(UserDataLeafName() + "Options.ini"), "\r?\nResolution =.*", "\r\nResolution = " + fixedText + "\r"));
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
            if (!File.Exists(UserDataLeafName() + "Options.ini") && (!File.Exists(myDocPath + "Options.ini")))
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
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("HeatEffects = Yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("HeatEffects = Yes", "HeatEffects = No"));
                        }
                        else if (text.Contains("HeatEffects = yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("HeatEffects = yes", "HeatEffects = No"));
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
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("HeatEffects = No"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("HeatEffects = No", "HeatEffects = Yes"));
                        }
                        else if (text.Contains("HeatEffects = no"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("HeatEffects = no", "HeatEffects = Yes"));
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

        private void ChangeCamHeight()
        {
            if (File.Exists("!!!Contra009Final_Patch2_GameData.big"))
            {
                Encoding encoding = Encoding.GetEncoding("windows-1252");
                var regex = Regex.Replace(File.ReadAllText("!!!Contra009Final_Patch2_GameData.big"), "  MaxCameraHeight = .*\r?\n", "  MaxCameraHeight = " + camTrackBar.Value + ".0" + " ;350.0\r\n");
                string read = File.ReadAllText("!!!Contra009Final_Patch2_GameData.big", encoding);
                File.WriteAllText("!!!Contra009Final_Patch2_GameData.big", regex, encoding);

                if (camTrackBar.Value > 392)
                {
                    var regex2 = Regex.Replace(File.ReadAllText("!!!Contra009Final_Patch2_GameData.big"), "  DrawEntireTerrain = No\r?\n", "  DrawEntireTerrain = Yes\r\n");
                    string read2 = File.ReadAllText("!!!Contra009Final_Patch2_GameData.big", encoding);
                    File.WriteAllText("!!!Contra009Final_Patch2_GameData.big", regex2, encoding);
                }
                else
                {
                    var regex2 = Regex.Replace(File.ReadAllText("!!!Contra009Final_Patch2_GameData.big"), "  DrawEntireTerrain = Yes\r?\n", "  DrawEntireTerrain = No\r\n");
                    string read2 = File.ReadAllText("!!!Contra009Final_Patch2_GameData.big", encoding);
                    File.WriteAllText("!!!Contra009Final_Patch2_GameData.big", regex2, encoding);
                }

                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("Camera height changed!\n\nNOTE: High camera height values may decrease performance.");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Высота камеры изменена!\n\nЗаметка. Высокие значения высоты камеры могут снизить производительность.");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Висота камери змінилася!\n\nПримітка. Високі значення висоти камери можуть знизити продуктивність.");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Височината на камерата е променена!\n\nЗабележка: Високите стойности на камерата могат да намалят производителността.");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("Kamerahöhe geändert!\n\nHinweis: Hohe Kamerahöhen können die Leistung beeinträchtigen.");
                }
            }
            else
            {
                if (Globals.GB_Checked == true)
                {
                    MessageBox.Show("\"!!!Contra009Final_Patch2_GameData.big\" file not found!", "Error");
                }
                else if (Globals.RU_Checked == true)
                {
                    MessageBox.Show("Файл \"!!!Contra009Final_Patch2_GameData.big\" не найден!", "Ошибка");
                }
                else if (Globals.UA_Checked == true)
                {
                    MessageBox.Show("Файл \"!!!Contra009Final_Patch2_GameData.big\" не знайдений!", "Помилка");
                }
                else if (Globals.BG_Checked == true)
                {
                    MessageBox.Show("Файлът \"!!!Contra009Final_Patch2_GameData.big\" не беше намерен!", "Грешка");
                }
                else if (Globals.DE_Checked == true)
                {
                    MessageBox.Show("\"!!!Contra009Final_Patch2_GameData.big\" nicht gefunden!", "Fehler");
                }
            }
        }

        private void camOkButton_Click(object sender, EventArgs e)
        {
            ChangeCamHeight();
            //if (!cfgText.Contains("5.8")) //if user isn't on ver 6.2
            //{
            //    if (Globals.GB_Checked == true)
            //    {
            //        var result = MessageBox.Show("Gentool versions above 6.2 do not allow custom camera height for mods in LAN lobby.\n\nDo you still want to set camera height?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //        if (result == DialogResult.Yes)
            //        {
            //            changeCamHeight();
            //        }
            //    }
            //    else if (Globals.RU_Checked == true)
            //    {
            //        var result = MessageBox.Show("Версии Gentool выше 6.2 не позволяют настраивать высоту камеры для модов в лобби локальной сети.\n\nВы все еще хотите установить высоту камеры?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //        if (result == DialogResult.Yes)
            //        {
            //            changeCamHeight();
            //        }
            //    }
            //    else if (Globals.UA_Checked == true)
            //    {
            //        var result = MessageBox.Show("Версії Gentool вище 6.2 не дозволяють налаштувати висоту камер для модів у лобі LAN.\n\nВи все ще бажаєте встановити висоту камери?", "Застереження", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //        if (result == DialogResult.Yes)
            //        {
            //            changeCamHeight();
            //        }
            //    }
            //    else if (Globals.BG_Checked == true)
            //    {
            //        var result = MessageBox.Show("Gentool версиите над 6.2 не позволяват персонализирана височина на камерата за модове в LAN лобито.\n\nВсе още ли искате да я промените?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //        if (result == DialogResult.Yes)
            //        {
            //            changeCamHeight();
            //        }
            //    }
            //    else if (Globals.DE_Checked == true)
            //    {
            //        var result = MessageBox.Show("Gentool-Versionen über 6.2 erlauben keine benutzerdefinierte Kamerahöhe für Mods in der LAN-Lobby.\n\nMöchten Sie die Kamerahöhe noch einstellen?", "Warnung", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //        if (result == DialogResult.Yes)
            //        {
            //            changeCamHeight();
            //        }
            //    }
            //}
        }

        private void camTrackBar_Scroll(object sender, EventArgs e)
        {
            if (Globals.GB_Checked == true)
            {
                camHeightLabel.Text = "Camera Height: " + camTrackBar.Value.ToString() + ".0";
            }
            else if (Globals.RU_Checked == true)
            {
                camHeightLabel.Text = "Высота камеры: " + camTrackBar.Value.ToString() + ".0";
            }
            else if (Globals.UA_Checked == true)
            {
                camHeightLabel.Text = "Висота камери: " + camTrackBar.Value.ToString() + ".0";
            }
            else if (Globals.BG_Checked == true)
            {
                camHeightLabel.Text = "Височина на камерата: " + camTrackBar.Value.ToString() + ".0";
            }
            else if (Globals.DE_Checked == true)
            {
                camHeightLabel.Text = "Kamerahöhe: " + camTrackBar.Value.ToString() + ".0";
            }
        }

        private void WaterEffectsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!WaterEffectsCheckBox.Checked)
            {
                Properties.Settings.Default.WaterEffects = false;
                Properties.Settings.Default.Save();
            }
            else Properties.Settings.Default.WaterEffects = true;
            Properties.Settings.Default.Save();
        }
    }
}
