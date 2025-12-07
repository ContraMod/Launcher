using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;

namespace Contra
{
    public partial class OptionsForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);
        const int ENUM_CURRENT_SETTINGS = -1;
        const int ENUM_REGISTRY_SETTINGS = -2;

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        // Bool that helps check if heat effects checkbox has been checked by the user and not automatically
        bool heatEffectsCheckBoxIsClicked = false;

        public OptionsForm()
        {
            InitializeComponent();
            FogCheckBox.TabStop = false;
            LangFilterCheckBox.TabStop = false;
            MinBtnSm.TabStop = false;
            ExitBtnSm.TabStop = false;
            resolutionComboBox.TabStop = false;

            // Get supported resolutions
            DEVMODE vDevMode = new DEVMODE();
            int i = 0;
            var dataSource = new List<string>();
            while (EnumDisplaySettings(null, i, ref vDevMode))
            {
                dataSource.Add(vDevMode.dmPelsWidth.ToString() + "x" + vDevMode.dmPelsHeight.ToString());
                i++;
            }
            var noDupes = dataSource.Distinct().ToList();

            // Populate Resolution comboBox with supported resolutions
            resolutionComboBox.DataSource = noDupes;

            if (Globals.RU_Checked == true)
            {
                labelResolution.Text = "Разрешение:";
                Shadows3DCheckBox.Text = "3D Тени";
                Shadows2DCheckBox.Text = "2D Тени";
                CloudShadowsCheckBox.Text = "Тени облаков";
                ExtraGroundLightingCheckBox.Text = "Освещение земли";
                SmoothWaterBordersCheckBox.Text = "Ровные края воды";
                BehindBuildingsCheckBox.Text = "Юниты через здания";
                ShowPropsCheckBox.Text = "Маленькие объекты";
                ExtraAnimationsCheckBox.Text = "Дополнит. анимации";
                DisableDynamicLODCheckBox.Text = "Откл. динам. ур. детал.";
                HeatEffectsCheckBox.Text = "Тепловые эффекты";
                FogCheckBox.Text = "Эффект тумана";
                LangFilterCheckBox.Text = "Языковый фильтр";
                WaterEffectsCheckBox.Text = "Эффект воды";
                CameraHeightLabel.Text = "Высота камеры: ?";
                HotkeyStyleLabel.Text = "Горячие клав.";
                LegacyHotkeysRadioButton.Text = "Оригинальный";
                AnisoCheckBox.Text = "Анизотропная фильтрация";
                CameosStandardRadioButton.Text = LegacyHotkeysRadioButton.Text = ControlBarStandardRadioButton.Text = "Стандарт.";
                ControlBarLabel.Text = "Панель упр.";
                IconQualityLabel.Text = "Кач. иконок";
                ExtraBuildingPropsCheckBox.Text = "Доп. объекты к зданиям";
                NoPreviewText.Text = "Предварительный просмотр недоступен";
                AcceptBtn.Text = "ПРИМЕНИТЬ";
                CloseBtn.Text = "ОТМЕНА";
            }
            else if (Globals.UA_Checked == true)
            {
                labelResolution.Text = "Розрішення:";
                Shadows3DCheckBox.Text = "3D Тіні";
                Shadows2DCheckBox.Text = "2D Тіні";
                CloudShadowsCheckBox.Text = "Тіні хмар";
                ExtraGroundLightingCheckBox.Text = "Освітлення землі";
                SmoothWaterBordersCheckBox.Text = "Рівні краї води";
                BehindBuildingsCheckBox.Text = "Юніти через будинки";
                ShowPropsCheckBox.Text = "Маленькі об'єкти";
                ExtraAnimationsCheckBox.Text = "Додаткова анімація";
                DisableDynamicLODCheckBox.Text = "Вимк. дин. рівень детал.";
                HeatEffectsCheckBox.Text = "Теплові ефекти";
                FogCheckBox.Text = "Ефект туману";
                LangFilterCheckBox.Text = "Мовний фільтр";
                WaterEffectsCheckBox.Text = "Водний ефект";
                CameraHeightLabel.Text = "Висота камери: ?";
                HotkeyStyleLabel.Text = "Гарячі клавіші";
                LegacyHotkeysRadioButton.Text = "Оригінальний";
                AnisoCheckBox.Text = "Анізотропна фільтрація";
                CameosStandardRadioButton.Text = LegacyHotkeysRadioButton.Text = ControlBarStandardRadioButton.Text = "Стандарт.";
                ControlBarLabel.Text = "Панель кер.";
                IconQualityLabel.Text = "Якість ікон";
                ExtraBuildingPropsCheckBox.Text = "Дод. об'єкти до будівель";
                NoPreviewText.Text = "Попередній перегляд недоступний";
                AcceptBtn.Text = "ЗАСТОСУВАТИ";
                CloseBtn.Text = "ВІДМІНА";
            }
            else if (Globals.BG_Checked == true)
            {
                labelResolution.Text = "Резолюция:";
                Shadows3DCheckBox.Text = "3D сенки";
                Shadows2DCheckBox.Text = "2D сенки";
                CloudShadowsCheckBox.Text = "Облачни сенки";
                ExtraGroundLightingCheckBox.Text = "Осветление на земята";
                SmoothWaterBordersCheckBox.Text = "Гладки водни граници";
                BehindBuildingsCheckBox.Text = "Единици през сгради";
                ShowPropsCheckBox.Text = "Малки обекти";
                ExtraAnimationsCheckBox.Text = "Допълнит. анимации";
                DisableDynamicLODCheckBox.Text = "Изкл. дин. ниво на дет.";
                HeatEffectsCheckBox.Text = "Топлинни ефекти";
                FogCheckBox.Text = "Мъглявинен ефект";
                LangFilterCheckBox.Text = "Езиков филтър";
                WaterEffectsCheckBox.Text = "Водни ефекти";
                CameraHeightLabel.Text = "Вис. на камерата: ?";
                HotkeyStyleLabel.Text = "Горещи клав.";
                LegacyHotkeysRadioButton.Text = "Оригинален";
                AnisoCheckBox.Text = "Анизотропно филтриране";
                CameosStandardRadioButton.Text = LegacyHotkeysRadioButton.Text = ControlBarStandardRadioButton.Text = "Стандарт.";
                ControlBarLabel.Text = "Контр. лента";
                IconQualityLabel.Text = "Кач. на икон.";
                ExtraBuildingPropsCheckBox.Text = "Доп. елементи на сградите";
                NoPreviewText.Text = "Няма налична визуализация";
                AcceptBtn.Text = "ПРИЕМИ";
                CloseBtn.Text = "ОТКАЗ";
            }
            else if (Globals.DE_Checked == true)
            {
                labelResolution.Text = "Auflцsung:";
                Shadows3DCheckBox.Text = "3D-Schatten";
                Shadows2DCheckBox.Text = "2D-Schatten";
                CloudShadowsCheckBox.Text = "Schatten der Wolken";
                ExtraGroundLightingCheckBox.Text = "Bodenbeleuchtung";
                SmoothWaterBordersCheckBox.Text = "Glatte Wasserränder";
                BehindBuildingsCheckBox.Text = "Einheiten hinter Gebäuden";
                ShowPropsCheckBox.Text = "Kleine Objekte";
                ExtraAnimationsCheckBox.Text = "Zusätzliche Animation";
                DisableDynamicLODCheckBox.Text = "Dyn. Detailebene deaktiv.";
                HeatEffectsCheckBox.Text = "Wärmeeffekte";
                FogCheckBox.Text = "Nebel Effekte";
                LangFilterCheckBox.Text = "Sprache Filter";
                WaterEffectsCheckBox.Text = "Wassereffekt";
                CameraHeightLabel.Text = "Kamerahöhe: ?";
                HotkeyStyleLabel.Text = "Hotkey-Stil";
                LegacyHotkeysRadioButton.Text = "Original";
                AnisoCheckBox.Text = "Anisotrope Filterung";
                CameosStandardRadioButton.Text = LegacyHotkeysRadioButton.Text = ControlBarStandardRadioButton.Text = "Standard";
                ControlBarLabel.Text = "Kontrollleiste";
                IconQualityLabel.Text = "Cameo-Qual.";
                ExtraBuildingPropsCheckBox.Text = "Zusätzliche Gebäudeobjekte";
                NoPreviewText.Text = "Keine Vorschau vorhanden";
                AcceptBtn.Text = "AKZEPTIEREN";
                CloseBtn.Text = "SCHLIESSEN";
            }
            //TextureResLabel.Text = Messages.GenerateMessage("TextureRes", Globals.currentLanguage);

            // Load settings from Options.ini to display them in our Options form
            if (Directory.Exists(Globals.myDocPath))
            {
                string s = File.ReadAllText(Globals.myDocPath + "Options.ini");
                {
                    if (s.ToLower().Contains("staticgamelod = low")
                        || s.ToLower().Contains("staticgamelod = medium")
                        || s.ToLower().Contains("staticgamelod = high"))
                    {
                        File.WriteAllText(Globals.myDocPath + "Options.ini",
                            Regex.Replace(s,
                            "\r?\nStaticGameLOD = .*",
                            "\r\nStaticGameLOD = Custom" + "\r"));
                    }

                    // Check/uncheck our checkboxes depending on values from the file
                    if (s.ToLower().Contains("useshadowvolumes = no")) Shadows3DCheckBox.Checked = false;
                    else if (s.ToLower().Contains("useshadowvolumes = yes")) Shadows3DCheckBox.Checked = true;
                    if (s.ToLower().Contains("useshadowdecals = no")) Shadows2DCheckBox.Checked = false;
                    else if (s.ToLower().Contains("useshadowdecals = yes")) Shadows2DCheckBox.Checked = true;
                    if (s.ToLower().Contains("usecloudmap = no")) CloudShadowsCheckBox.Checked = false;
                    else if (s.ToLower().Contains("usecloudmap = yes")) CloudShadowsCheckBox.Checked = true;
                    if (s.ToLower().Contains("uselightmap = no")) ExtraGroundLightingCheckBox.Checked = false;
                    else if (s.ToLower().Contains("uselightmap = yes")) ExtraGroundLightingCheckBox.Checked = true;
                    if (s.ToLower().Contains("showsoftwateredge = no")) SmoothWaterBordersCheckBox.Checked = false;
                    else if (s.ToLower().Contains("showsoftwateredge = yes")) SmoothWaterBordersCheckBox.Checked = true;
                    if (s.ToLower().Contains("buildingocclusion = no")) BehindBuildingsCheckBox.Checked = false;
                    else if (s.ToLower().Contains("buildingocclusion = yes")) BehindBuildingsCheckBox.Checked = true;
                    if (s.ToLower().Contains("showtrees = no")) ShowPropsCheckBox.Checked = false;
                    else if (s.ToLower().Contains("showtrees = yes")) ShowPropsCheckBox.Checked = true;
                    if (s.ToLower().Contains("extraanimations = no")) ExtraAnimationsCheckBox.Checked = false;
                    else if (s.ToLower().Contains("extraanimations = yes")) ExtraAnimationsCheckBox.Checked = true;
                    if (s.ToLower().Contains("dynamiclod = no")) DisableDynamicLODCheckBox.Checked = true;
                    else if (s.ToLower().Contains("dynamiclod = yes")) DisableDynamicLODCheckBox.Checked = false;

                    if (s.ToLower().Contains("heateffects = no"))
                    {
                        HeatEffectsCheckBox.Checked = false;
                        heatEffectsCheckBoxIsClicked = false;
                    }
                    else if (s.ToLower().Contains("heateffects = yes"))
                    {
                        HeatEffectsCheckBox.Checked = true;
                        heatEffectsCheckBoxIsClicked = false; // Yes, false
                    }
                }

                // Get and display numeric values from the file
                List<string> found = new List<string>();
                string line;
                using (StringReader file = new StringReader(s))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        // Get current resolution
                        if (line.ToLower().Contains("resolution ="))
                        {
                            found.Add(line);
                            s = line;
                            s = s.Substring(s.IndexOf('=') + 2);
                            s = s.TrimEnd();
                            string s2 = s.Replace(" ", "x");
                            resolutionComboBox.Text = s2;
                            Properties.Settings.Default.Res = s2;
                        }
                        // Get current particle cap
                        if (line.ToLower().Contains("maxparticlecount ="))
                        {
                            found.Add(line);
                            s = line;
                            s = s.Substring(s.IndexOf('=') + 2);
                            s = s.TrimEnd();
                            ParticleCapTrackBar.Value = Convert.ToInt32(s);
                        }
                        // Get current texture resolution
                        if (line.ToLower().Contains("texturereduction ="))
                        {
                            found.Add(line);
                            s = line;
                            s = s.Substring(s.IndexOf('=') + 2);
                            s = s.TrimEnd();
                            if (Convert.ToInt32(s) == 0)
                            {
                                TextureResTrackBar.Value = 3;
                                TextureResLabel.Text = Messages.GenerateMessage("TextureRes", Globals.currentLanguage) + Messages.GenerateMessage("High", Globals.currentLanguage);
                            }
                            else if (Convert.ToInt32(s) == 1)
                            {
                                TextureResTrackBar.Value = 2;
                                TextureResLabel.Text = Messages.GenerateMessage("TextureRes", Globals.currentLanguage) + Messages.GenerateMessage("Medium", Globals.currentLanguage);
                            }
                            else if (Convert.ToInt32(s) == 2)
                            {
                                TextureResTrackBar.Value = 1;
                                TextureResLabel.Text = Messages.GenerateMessage("TextureRes", Globals.currentLanguage) + Messages.GenerateMessage("Low", Globals.currentLanguage);
                            }
                        }
                    }
                }
            }
            else Messages.GenerateMessageBox("E_NotFound_OptionsIni", Globals.currentLanguage);


            FogCheckBox.Checked = Properties.Settings.Default.Fog;
            LangFilterCheckBox.Checked = Properties.Settings.Default.LangF;
            WaterEffectsCheckBox.Checked = Properties.Settings.Default.WaterEffects;
            ExtraBuildingPropsCheckBox.Checked = Properties.Settings.Default.ExtraBuildingProps;
            ControlBarProRadioButton.Checked = Properties.Settings.Default.ControlBarPro;
            ControlBarContraRadioButton.Checked = Properties.Settings.Default.ControlBarContra;
            ControlBarStandardRadioButton.Checked = Properties.Settings.Default.ControlBarStandard;
            CameosDoubleRadioButton.Checked = Properties.Settings.Default.CameosDouble;
            CameosStandardRadioButton.Checked = Properties.Settings.Default.CameosStandard;
            LeikezeHotkeysRadioButton.Checked = Properties.Settings.Default.LeikezeHotkeys;
            LegacyHotkeysRadioButton.Checked = Properties.Settings.Default.LegacyHotkeys;
            AnisoCheckBox.Checked = Properties.Settings.Default.Anisotropic;

            // Get current camera height
            if (File.Exists("!" + MainForm.betaPrefix + "_GameData.big"))
            {
                try {ReadCameraHeight(File.ReadAllText("!" + MainForm.betaPrefix + "_GameData.big"));}
                catch (IOException) {Messages.GenerateMessageBox("E_CloseGameDataP3", Globals.currentLanguage);}
            }
        }

        private void ReadCameraHeight(string fileToRead)
        {
            List<string> found = new List<string>();
            string line;
            using (StringReader file = new StringReader(fileToRead))
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains(" MaxCameraHeight ="))
                    {
                        found.Add(line);
                        line = line.Substring(0, line.IndexOf(".") + 1);
                        line = Regex.Replace(line, @"[^\d]", "");
                        if (AspectRatio(x, y) == "16:9")
                        {
                            int value;
                            value = Convert.ToInt32(line);
                            CameraHeightTrackBar.Value = value + 110;
                        }
                        else
                        {
                            CameraHeightTrackBar.Value = Convert.ToInt32(line);
                        }
                        CameraHeightLabel.Text = Messages.GenerateMessage("CameraHeightString", Globals.currentLanguage) + CameraHeightTrackBar.Value.ToString() + ".0";
                    }
                }
            }
        }

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

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        private void ExitBtnSm_MouseEnter(object sender, EventArgs e)
        {
            ExitBtnSm.BackgroundImage = Properties.Resources._button_sm_exit_tr;
        }
        private void ExitBtnSm_MouseLeave(object sender, EventArgs e)
        {
            ExitBtnSm.BackgroundImage = Properties.Resources._button_sm_exit;
        }
        private void ExitBtnSm_Click(object sender, EventArgs e)
        {
            Close(); //OnApplicationExit(sender, e);
        }

        private void MinBtnSm_MouseEnter(object sender, EventArgs e)
        {
            MinBtnSm.BackgroundImage = Properties.Resources._button_sm_min_tr;
        }
        private void MinBtnSm_MouseLeave(object sender, EventArgs e)
        {
            MinBtnSm.BackgroundImage = Properties.Resources._button_sm_min;
        }
        private void MinBtnSm_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        public static Tuple<int, int> getScreenResolution() => Tuple.Create(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        int x = getScreenResolution().Item1;
        int y = getScreenResolution().Item2;

        public string AspectRatio(int x, int y)
        {
            double value = (double)x / y;
            if (value > 1.7)
                return "16:9";
            else
                return "4:3";
        }

        private void ChangeCamHeight()
        {
            //if (File.Exists("!!!!Contra009Final_Patch3.big") || File.Exists("!!!!Contra009Final_Patch3.ctr"))
            //{
                CamHeightRegexReplace("!" + MainForm.betaPrefix + "_GameData.big", "E_NotFound_GameDataP3");
            //}
            //else Messages.GenerateMessageBox("E_MissingFiles_CouldNotChangeCamHeight", Globals.currentLanguage);
        }

        private void CamHeightRegexReplace(string gameDataFilename, string gameDataNotFoundMsg)
        {
            if (File.Exists(gameDataFilename))
            {
                Encoding encoding = Encoding.GetEncoding("windows-1252");
                var regex = ""; var regex2 = "";

                if (AspectRatio(x, y) == "16:9")
                {
                    regex = Regex.Replace(File.ReadAllText(gameDataFilename, encoding),
                        "  MaxCameraHeight = .*\r?\n",
                        "  MaxCameraHeight = " + (CameraHeightTrackBar.Value - 110) + ".0" + " ;350.0\r\n");
                }
                else
                {
                    regex = Regex.Replace(File.ReadAllText(gameDataFilename, encoding),
                        "  MaxCameraHeight = .*\r?\n",
                        "  MaxCameraHeight = " + CameraHeightTrackBar.Value + ".0" + " ;350.0\r\n");
                }
                File.WriteAllText(gameDataFilename, regex, encoding);

                if (CameraHeightTrackBar.Value > 392)
                {
                    regex2 = Regex.Replace(File.ReadAllText(gameDataFilename, encoding),
                        "  DrawEntireTerrain = No\r?\n",
                        "  DrawEntireTerrain = Yes\r\n");
                }
                else
                {
                    regex2 = Regex.Replace(File.ReadAllText(gameDataFilename, encoding),
                        "  DrawEntireTerrain = Yes\r?\n",
                        "  DrawEntireTerrain = No\r\n");
                }
                File.WriteAllText(gameDataFilename, regex2, encoding);
            }
            else Messages.GenerateMessageBox(gameDataNotFoundMsg, Globals.currentLanguage);
        }

        private void CameraHeightTrackBar_Scroll(object sender, EventArgs e)
        {
            CameraHeightLabel.Text = Messages.GenerateMessage("CameraHeightString", Globals.currentLanguage) + CameraHeightTrackBar.Value.ToString() + ".0";
        }

        public void IsGeneralsRunning()
        {
            if (ActiveForm == this)
            {
                Process[] genByName = Process.GetProcessesByName("generals");
                if (genByName.Length > 0)
                    Messages.GenerateMessageBox("W_ChangesAfterGameRestart", Globals.currentLanguage);
            }
        }

        private void HeatEffectsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!HeatEffectsCheckBox.Checked)
                heatEffectsCheckBoxIsClicked = false;
            else
                heatEffectsCheckBoxIsClicked = true;
        }
        private void HeatEffectsCheckBox_Click(object sender, EventArgs e)
        {
            if (heatEffectsCheckBoxIsClicked == true)
                Messages.GenerateMessageBox("W_BlackScreen", Globals.currentLanguage);
        }

        private void AcceptBtn_MouseDown(object sender, MouseEventArgs e)
        {
            AcceptBtn.BackgroundImage = Properties.Resources._button_big_down;
            AcceptBtn.ForeColor = Globals.buttonHighlight;
            AcceptBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void AcceptBtn_MouseEnter(object sender, EventArgs e)
        {
            AcceptBtn.BackgroundImage = Properties.Resources._button_big_hover;
            AcceptBtn.ForeColor = Globals.buttonHighlight;
            AcceptBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void AcceptBtn_MouseLeave(object sender, EventArgs e)
        {
            AcceptBtn.BackgroundImage = Properties.Resources._button_big;
            AcceptBtn.ForeColor = SystemColors.ButtonHighlight;
            AcceptBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void EnableDisableSettings(string settingName, string settingValueCaseOne)
        {
            string settingValueCaseTwo;
            if (settingValueCaseOne == "Yes") settingValueCaseTwo = "No";
            else settingValueCaseTwo = "Yes";

            File.WriteAllText(Globals.myDocPath + "Options.ini",
                Regex.Replace(File.ReadAllText(Globals.myDocPath + "Options.ini"),
                $"\r?\n{settingName} = {settingValueCaseOne}",
                $"\r\n{settingName} = {settingValueCaseTwo}",
                RegexOptions.IgnoreCase));
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            // Apply .ini-file options
            if (Directory.Exists(Globals.myDocPath))
            {
                if (!Shadows3DCheckBox.Checked) EnableDisableSettings("UseShadowVolumes", "Yes");
                else EnableDisableSettings("UseShadowVolumes", "No");
                if (!Shadows2DCheckBox.Checked) EnableDisableSettings("UseShadowDecals", "Yes");
                else EnableDisableSettings("UseShadowDecals", "No");
                if (!CloudShadowsCheckBox.Checked) EnableDisableSettings("UseCloudMap", "Yes");
                else EnableDisableSettings("UseCloudMap", "No");
                if (!ExtraGroundLightingCheckBox.Checked) EnableDisableSettings("UseLightMap", "Yes");
                else EnableDisableSettings("UseLightMap", "No");
                if (!SmoothWaterBordersCheckBox.Checked) EnableDisableSettings("ShowSoftWaterEdge", "Yes");
                else EnableDisableSettings("ShowSoftWaterEdge", "No");
                if (!BehindBuildingsCheckBox.Checked) EnableDisableSettings("BuildingOcclusion", "Yes");
                else EnableDisableSettings("BuildingOcclusion", "No");
                if (!ShowPropsCheckBox.Checked) EnableDisableSettings("ShowTrees", "Yes");
                else EnableDisableSettings("ShowTrees", "No");
                if (!ExtraAnimationsCheckBox.Checked) EnableDisableSettings("ExtraAnimations", "Yes");
                else EnableDisableSettings("ExtraAnimations", "No");
                if (DisableDynamicLODCheckBox.Checked) EnableDisableSettings("DynamicLOD", "Yes");
                else EnableDisableSettings("DynamicLOD", "No");

                if (!HeatEffectsCheckBox.Checked)
                {
                    heatEffectsCheckBoxIsClicked = false;
                    Properties.Settings.Default.HeatEffects = false;
                    EnableDisableSettings("HeatEffects", "Yes");
                }
                else
                {
                    heatEffectsCheckBoxIsClicked = true;
                    Properties.Settings.Default.HeatEffects = true;
                    EnableDisableSettings("HeatEffects", "No");
                }

                // Apply resolution
                if (Regex.IsMatch(resolutionComboBox.Text, @"^[0-9]{3,4}x[0-9]{3,4}$"))
                {
                    string fixedText = resolutionComboBox.Text.Replace("x", " ");
                    File.WriteAllText(Globals.myDocPath + "Options.ini",
                        Regex.Replace(File.ReadAllText(Globals.myDocPath + "Options.ini"),
                        "\r?\nResolution =.*", "\r\nResolution = " + fixedText + "\r",
                        RegexOptions.IgnoreCase));
                }
                else
                {
                    Messages.GenerateMessageBox("E_InvalidRes", Globals.currentLanguage);
                    return;
                }

                // Apply Particle Cap
                File.WriteAllText(Globals.myDocPath + "Options.ini",
                    Regex.Replace(File.ReadAllText(Globals.myDocPath + "Options.ini"),
                    "\r?\nMaxParticleCount =.*",
                    "\r\nMaxParticleCount = " + ParticleCapTrackBar.Value + "\r",
                    RegexOptions.IgnoreCase));

                // Apply Texture Resolution
                if (TextureResTrackBar.Value == 1)
                {
                    File.WriteAllText(Globals.myDocPath + "Options.ini",
                        Regex.Replace(File.ReadAllText(Globals.myDocPath + "Options.ini"),
                        "\r?\nTextureReduction =.*",
                        "\r\nTextureReduction = 2\r",
                        RegexOptions.IgnoreCase));
                }
                else if (TextureResTrackBar.Value == 2)
                {
                    File.WriteAllText(Globals.myDocPath + "Options.ini",
                        Regex.Replace(File.ReadAllText(Globals.myDocPath + "Options.ini"),
                        "\r?\nTextureReduction =.*",
                        "\r\nTextureReduction = 1\r",
                        RegexOptions.IgnoreCase));
                }
                else
                {
                    File.WriteAllText(Globals.myDocPath + "Options.ini",
                        Regex.Replace(File.ReadAllText(Globals.myDocPath + "Options.ini"),
                        "\r?\nTextureReduction =.*",
                        "\r\nTextureReduction = 0\r",
                        RegexOptions.IgnoreCase));
                }
            }
            else Messages.GenerateMessageBox("E_NotFound_OptionsIni", Globals.currentLanguage);

            // Apply Camera Height
            try { ChangeCamHeight(); }
            catch (IOException)
            {
                if (File.Exists("!ContraXBeta_GameData.big")) Messages.GenerateMessageBox("E_CloseGameDataP3", Globals.currentLanguage);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

            // .big-file Options
            if (FogCheckBox.Checked)
                Properties.Settings.Default.Fog = true;
            else Properties.Settings.Default.Fog = false;

            if (WaterEffectsCheckBox.Checked)
                Properties.Settings.Default.WaterEffects = true;
            else Properties.Settings.Default.WaterEffects = false;

            if (LangFilterCheckBox.Checked)
                Properties.Settings.Default.LangF = true;
            else Properties.Settings.Default.LangF = false;

            if (ExtraBuildingPropsCheckBox.Checked)
                Properties.Settings.Default.ExtraBuildingProps = true;
            else Properties.Settings.Default.ExtraBuildingProps = false;

            if (ControlBarProRadioButton.Checked)
            {
                Properties.Settings.Default.ControlBarPro = true;
                Properties.Settings.Default.ControlBarContra = false;
                Properties.Settings.Default.ControlBarStandard = false;
            }
            else if (ControlBarContraRadioButton.Checked)
            {
                Properties.Settings.Default.ControlBarPro = false;
                Properties.Settings.Default.ControlBarContra = true;
                Properties.Settings.Default.ControlBarStandard = false;
            }
            else
            {
                Properties.Settings.Default.ControlBarPro = false;
                Properties.Settings.Default.ControlBarContra = false;
                Properties.Settings.Default.ControlBarStandard = true;
            }

            if (CameosDoubleRadioButton.Checked)
            {
                Properties.Settings.Default.CameosDouble = true;
                Properties.Settings.Default.CameosStandard = false;
            }
            else
            {
                Properties.Settings.Default.CameosDouble = false;
                Properties.Settings.Default.CameosStandard = true;
            }

            if (LeikezeHotkeysRadioButton.Checked)
            {
                Properties.Settings.Default.LeikezeHotkeys = true;
                Properties.Settings.Default.LegacyHotkeys = false;
            }
            else
            {
                Properties.Settings.Default.LeikezeHotkeys = false;
                Properties.Settings.Default.LegacyHotkeys = true;
            }

            // Enable ENB Series distribution for Anisotropic Filtering
            if (AnisoCheckBox.Checked)
                Properties.Settings.Default.Anisotropic = true;
            else Properties.Settings.Default.Anisotropic = false;

            IsGeneralsRunning();
            Properties.Settings.Default.Save();
            Close();
        }

        private void CloseBtn_MouseDown(object sender, MouseEventArgs e)
        {
            CloseBtn.BackgroundImage = Properties.Resources._button_big_down;
            CloseBtn.ForeColor = Globals.buttonHighlight;
            CloseBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void CloseBtn_MouseEnter(object sender, EventArgs e)
        {
            CloseBtn.BackgroundImage = Properties.Resources._button_big_hover;
            CloseBtn.ForeColor = Globals.buttonHighlight;
            CloseBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void CloseBtn_MouseLeave(object sender, EventArgs e)
        {
            CloseBtn.BackgroundImage = Properties.Resources._button_big;
            CloseBtn.ForeColor = SystemColors.ButtonHighlight;
            CloseBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowGraphicsInfo(string header, string performance, string label, bool haspreview = true)
        {
            if (haspreview == true) NoPreviewText.Visible = false;
            else NoPreviewText.Visible = true;
            GraphicsInfoHeaderLabel.Visible = true;
            GraphicsInfoPerformanceLabel.Visible = true;
            GraphicsInfoDescriptionLabel.Visible = true;
            GraphicsInfoHeaderLabel.Text = header.ToUpper();
            GraphicsInfoPerformanceLabel.Text = performance;
            GraphicsInfoDescriptionLabel.Text = label;
        }

        private void Shadows3DCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_3d;
            ShowGraphicsInfo(Shadows3DCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectHigh", Globals.currentLanguage),
                Messages.GenerateMessage("Shadows3DDescription", Globals.currentLanguage));
        }
        private void Shadows2DCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_2d;
            ShowGraphicsInfo(Shadows2DCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectLow", Globals.currentLanguage),
                Messages.GenerateMessage("Shadows2DDescription", Globals.currentLanguage));
        }
        private void CloudShadowsCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_cloud;
            ShowGraphicsInfo(CloudShadowsCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectLow", Globals.currentLanguage),
                Messages.GenerateMessage("CloudShadowsDescription", Globals.currentLanguage));
        }
        private void ExtraGroundLightingCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_light;
            ShowGraphicsInfo(ExtraGroundLightingCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectLow", Globals.currentLanguage),
                Messages.GenerateMessage("ExtraGroundLightingDescription", Globals.currentLanguage));
        }
        private void SmoothWaterBordersCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_watbord;
            ShowGraphicsInfo(SmoothWaterBordersCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectLow", Globals.currentLanguage),
                Messages.GenerateMessage("SmoothWaterBordersDescription", Globals.currentLanguage));
        }
        private void BehindBuildingsCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_behindbldg;
            ShowGraphicsInfo(BehindBuildingsCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectLow", Globals.currentLanguage),
                Messages.GenerateMessage("BehindBuildingsDescription", Globals.currentLanguage));
        }
        private void ShowPropsCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_props;
            ShowGraphicsInfo(ShowPropsCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectLow", Globals.currentLanguage),
                Messages.GenerateMessage("ShowPropsDescription", Globals.currentLanguage));
        }
        private void ExtraAnimationsCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_extraanim;
            ShowGraphicsInfo(ExtraAnimationsCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectLow", Globals.currentLanguage),
                Messages.GenerateMessage("ExtraAnimationsDescription", Globals.currentLanguage));
        }
        private void DisableDynamicLODCheckBox_MouseHover(object sender, EventArgs e)
        {
            ShowGraphicsInfo(DisableDynamicLODCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectHigh", Globals.currentLanguage),
                Messages.GenerateMessage("DisableDynamicLODDescription", Globals.currentLanguage), false);
        }
        private void HeatEffectsCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_heat;
            ShowGraphicsInfo(HeatEffectsCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectLow", Globals.currentLanguage),
                Messages.GenerateMessage("HeatEffectsDescription", Globals.currentLanguage));
        }
        private void FogCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_fog;
            ShowGraphicsInfo(FogCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectLow", Globals.currentLanguage),
                Messages.GenerateMessage("FogDescription", Globals.currentLanguage));
        }
        private void WaterEffectsCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_waterfx;
            ShowGraphicsInfo(WaterEffectsCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectHigh", Globals.currentLanguage),
                Messages.GenerateMessage("WaterEffectsDescription", Globals.currentLanguage));
        }
        private void LangFilterCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_langfilter;
            ShowGraphicsInfo(LangFilterCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectNone", Globals.currentLanguage),
                Messages.GenerateMessage("LangFilterDescription", Globals.currentLanguage));
        }
        private void AnisoCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_aniso;
            ShowGraphicsInfo(AnisoCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectLow", Globals.currentLanguage),
                Messages.GenerateMessage("AnisoDescription", Globals.currentLanguage));
        }

        private void ExtraBuildingPropsCheckBox_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_extraprops;
            ShowGraphicsInfo(ExtraBuildingPropsCheckBox.Text,
                Messages.GenerateMessage("PerformanceEffectMedium", Globals.currentLanguage),
                Messages.GenerateMessage("ExtraBuildingPropsDescription", Globals.currentLanguage));
        }
        private void ControlBarProRadioButton_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_barpro;
            ShowGraphicsInfo(Messages.GenerateMessage("ControlBarPro", Globals.currentLanguage),
                Messages.GenerateMessage("ControlBarProDescription", Globals.currentLanguage), "");
        }
        private void ControlBarContraRadioButton_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_barctr;
            ShowGraphicsInfo(Messages.GenerateMessage("ControlBarContra", Globals.currentLanguage),
                Messages.GenerateMessage("ControlBarContraDescription", Globals.currentLanguage), "");
        }
        private void ControlBarStandardRadioButton_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_barstandard;
            ShowGraphicsInfo(Messages.GenerateMessage("ControlBarStandard", Globals.currentLanguage),
                Messages.GenerateMessage("ControlBarStandardDescription", Globals.currentLanguage), "");
        }
        private void CameosDoubleRadioButton_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_iconshd;
            ShowGraphicsInfo(Messages.GenerateMessage("IconQualityDouble", Globals.currentLanguage),
                Messages.GenerateMessage("IconQualityDoubleDescription", Globals.currentLanguage), "");
        }
        private void CameosStandardRadioButton_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_iconsstandard;
            ShowGraphicsInfo(Messages.GenerateMessage("IconQualityStandard", Globals.currentLanguage),
                Messages.GenerateMessage("IconQualityStandardDescription", Globals.currentLanguage), "");
        }
        private void LeikezeHotkeysRadioButton_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_hotkeysleikeze;
            ShowGraphicsInfo(Messages.GenerateMessage("HotkeysLeikeze", Globals.currentLanguage),
                Messages.GenerateMessage("HotkeysLeikezeDescription", Globals.currentLanguage), "");
        }
        private void LegacyHotkeysRadioButton_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_hotkeysstandard;
            ShowGraphicsInfo(Messages.GenerateMessage("HotkeysStandard", Globals.currentLanguage),
                Messages.GenerateMessage("HotkeysStandardDescription", Globals.currentLanguage), "");
        }
        private void CameraHeightTrackBar_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_height;
            ShowGraphicsInfo(Messages.GenerateMessage("CameraHeight", Globals.currentLanguage),
                Messages.GenerateMessage("PerformanceEffectHigh", Globals.currentLanguage),
                Messages.GenerateMessage("CameraHeightDescription", Globals.currentLanguage));
        }
        private void TextureResTrackBar_MouseHover(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = Properties.Resources.comp_textureres;
            ShowGraphicsInfo(Messages.GenerateMessage("TextureResTwo", Globals.currentLanguage),
                Messages.GenerateMessage("PerformanceEffectLow", Globals.currentLanguage),
                Messages.GenerateMessage("TextureResDescription", Globals.currentLanguage));
        }
        private void ParticleCapTrackBar_MouseHover(object sender, EventArgs e)
        {
            ShowGraphicsInfo(Messages.GenerateMessage("ParticleCap", Globals.currentLanguage),
                Messages.GenerateMessage("PerformanceEffectHigh", Globals.currentLanguage),
                Messages.GenerateMessage("ParticleCapDescription", Globals.currentLanguage), false);
        }

        private void ParticleCapTrackBar_Scroll(object sender, EventArgs e)
        {
            ParticleCapLabel.Text = Messages.GenerateMessage("ParticleCap", Globals.currentLanguage) + ParticleCapTrackBar.Value.ToString();
        }

        private void TextureResTrackBar_Scroll(object sender, EventArgs e)
        {
            if (TextureResTrackBar.Value == 1) TextureResLabel.Text = Messages.GenerateMessage("TextureRes", Globals.currentLanguage)
                    + Messages.GenerateMessage("Low", Globals.currentLanguage);
            else if (TextureResTrackBar.Value == 2) TextureResLabel.Text = Messages.GenerateMessage("TextureRes", Globals.currentLanguage)
                    + Messages.GenerateMessage("Medium", Globals.currentLanguage);
            else TextureResLabel.Text = Messages.GenerateMessage("TextureRes", Globals.currentLanguage)
                    + Messages.GenerateMessage("High", Globals.currentLanguage);
        }

        private void Option_MouseLeave(object sender, EventArgs e)
        {
            GraphicsInfoPictureBox.Image = null;
            GraphicsInfoHeaderLabel.Visible = false;
            GraphicsInfoPerformanceLabel.Visible = false;
            GraphicsInfoDescriptionLabel.Visible = false;
            NoPreviewText.Visible = true;
        }

        private void ControlBarProRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                if (!MainForm.isGentoolInstalled("d3d8.dll") || MainForm.isGentoolOutdated("d3d8.dll", 85))
                {
                    DialogResult dialogResult = MessageBox.Show(Messages.GenerateMessage("W_GenToolNotInstalledOrNotUpToDate", Globals.currentLanguage),
                    Messages.GenerateMessage("Warning", Globals.currentLanguage), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                        MainForm.Url_open("https://www.gentool.net/");
                    return;
                }
            }
        }
    }
}