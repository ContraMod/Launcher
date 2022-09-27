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
            resOkButton.TabStop = false;

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
            this.resolutionComboBox.DataSource = noDupes;

            if (Globals.GB_Checked == true)
            {
                toolTip3.SetToolTip(Shadows3DCheckBox, "Toggle showing 3D shadows in game.\nTurn off for improved performance."); ;
                toolTip3.SetToolTip(Shadows2DCheckBox, "Toggle showing 2D shadows in game.\nTurn off for improved performance.");
                toolTip3.SetToolTip(CloudShadowsCheckBox, "Toggle showing cloud shadows on terrain.\nTurn off for improved performance.");
                toolTip3.SetToolTip(ExtraGroundLightingCheckBox, "Toggle showing detailed lighting on terrain.\nTurn off for improved performance.");
                toolTip3.SetToolTip(SmoothWaterBordersCheckBox, "Toggle smoothing of water borders.\nTurn off for improved performance.");
                toolTip3.SetToolTip(BehindBuildingsCheckBox, "Toggle showing units behind buildings.\nTurn off for improved performance.");
                toolTip3.SetToolTip(ShowPropsCheckBox, "Toggle displaying game props.\nTurn off for improved performance.");
                toolTip3.SetToolTip(ExtraAnimationsCheckBox, "Toggle showing optional animations like tree sway.\nTurn off for improved performance.");
                toolTip3.SetToolTip(DisableDynamicLODCheckBox, "Disable automatic detail adjustment.\nTurn off for improved performance.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Toggle showing heat distortion effects.\nTurn this off if your screen randomly turns black while playing.");
                toolTip3.SetToolTip(FogCheckBox, "Toggle fog (depth of field) effects on/off.\nThis effect adds a color layer at the top of the screen, depending on the map.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Disabling the language filter will show bad words written by players in chat.");
                toolTip3.SetToolTip(WaterEffectsCheckBox, "Toggle water effects on/off.\nThis effect simulates water reflection.");
                toolTip3.SetToolTip(camHeightLabel, "The camera height setting changes the default and maximum player view distance in-game.\nThe higher this value is, the further away the view will be.");
            }
            else if (Globals.RU_Checked == true)
            {
                toolTip3.SetToolTip(Shadows3DCheckBox, "Переключить отображение трехмерных теней в игре.\nВыключите для повышения производительности.");
                toolTip3.SetToolTip(Shadows2DCheckBox, "Переключить отображение двумерных теней в игре.\nВыключите для повышения производительности.");
                toolTip3.SetToolTip(CloudShadowsCheckBox, "Переключить отображение тени облаков на местности.\nВыключите для повышения производительности.");
                toolTip3.SetToolTip(ExtraGroundLightingCheckBox, "Переключить детализированного освещения земли.\nВыключите для повышения производительности.");
                toolTip3.SetToolTip(SmoothWaterBordersCheckBox, "Переключить сглаживание границ воды.\nВыключите для повышения производительности.");
                toolTip3.SetToolTip(BehindBuildingsCheckBox, "Переключить отображение единицы за зданиями.\nВыключите для повышения производительности.");
                toolTip3.SetToolTip(ShowPropsCheckBox, "Переключить отображение маленькие объекты.\nВыключите для повышения производительности.");
                toolTip3.SetToolTip(ExtraAnimationsCheckBox, "Переключить отображение дополнительных анимаций, таких как раскачивание деревьев.\nВыключите для повышения производительности.");
                toolTip3.SetToolTip(DisableDynamicLODCheckBox, "Переключить автоматическую регулировку деталей.\nВыключите для повышения производительности.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Переключите отображение тепловых эффектов.\nВыключите, если ваш экран случайно становится черным во время игры.");
                toolTip3.SetToolTip(FogCheckBox, "Эффекты переключения тумана (глубина поля) вкл\\выкл.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Отключение языкового фильтра покажет плохие слова, написанные игроками в чате.");
                toolTip3.SetToolTip(WaterEffectsCheckBox, "Эффекты переключения отражение воды вкл\\выкл.");
                toolTip3.SetToolTip(camHeightLabel, "Настройка высоты камеры изменяет стандартное и максимальное расстояние поле зрения игрока.\nЧем выше это значение, тем дальше будет поле зрения.");

                labelResolution.Text = "Разрешение экрана:";
                Shadows3DCheckBox.Text = "3D Тени";
                Shadows2DCheckBox.Text = "2D Тени";
                CloudShadowsCheckBox.Text = "Тени облаков";
                ExtraGroundLightingCheckBox.Text = "Дополнит. освещение земли";
                SmoothWaterBordersCheckBox.Text = "Ровные края воды";
                BehindBuildingsCheckBox.Text = "Единицы за зданиями";
                ShowPropsCheckBox.Text = "Показывать маленькие объекты";
                ExtraAnimationsCheckBox.Text = "Дополнительные анимации";
                DisableDynamicLODCheckBox.Text = "Откл. Динам. Уровень Детализации";
                HeatEffectsCheckBox.Text = "Тепловые эффекты";
                FogCheckBox.Text = "Эффект тумана";
                LangFilterCheckBox.Text = "Языковый фильтр";
                WaterEffectsCheckBox.Text = "Эффект воды";
                camHeightLabel.Text = "Высота камеры: ?";
                HotkeyStyleGroupBox.Text = "Стиль горячих клавиш";
                LegacyHotkeysRadioButton.Text = "Оригинальный";
            }
            else if (Globals.UA_Checked == true)
            {
                toolTip3.SetToolTip(Shadows3DCheckBox, "Переключити відображення тривимірних тіней в грі.\nВиключіте для підвищення продуктивності.");
                toolTip3.SetToolTip(Shadows2DCheckBox, "Переключити відображення двовимірних тіней в грі.\nВиключіте для підвищення продуктивності.");
                toolTip3.SetToolTip(CloudShadowsCheckBox, "Переключити відображення тіні хмар на місцевості.\nВиключіте для підвищення продуктивності.");
                toolTip3.SetToolTip(ExtraGroundLightingCheckBox, "Переключити детального висвітлення землі.\nВиключіте для підвищення продуктивності.");
                toolTip3.SetToolTip(SmoothWaterBordersCheckBox, "Переключити згладжування меж води.\nВиключіте для підвищення продуктивності.");
                toolTip3.SetToolTip(BehindBuildingsCheckBox, "Переключити відображення одиниці за будівлями.\nВиключіте для підвищення продуктивності.");
                toolTip3.SetToolTip(ShowPropsCheckBox, "Переключити відображення маленькі об'єкти.\nВиключіте для підвищення продуктивності.");
                toolTip3.SetToolTip(ExtraAnimationsCheckBox, "Переключити відображення додаткових анімацій, таких як розгойдування дерев.\nВиключіте для підвищення продуктивності.");
                toolTip3.SetToolTip(DisableDynamicLODCheckBox, "Переключити автоматичне регулювання деталей.\nВиключіте для підвищення продуктивності.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Переключення відображення теплових ефектів.\nВимкніть цю функцію, якщо екран у випадковому режимі стане чорним під час відтворення.");
                toolTip3.SetToolTip(FogCheckBox, "Ефекти перемикання туману (глибина поля) вкл\\викл.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Вимкнення мовного фільтра покаже погані слова, написані гравцями в чаті.");
                toolTip3.SetToolTip(WaterEffectsCheckBox, "Ефекти перемикання відображення води вкл\\викл.");
                toolTip3.SetToolTip(camHeightLabel, "Налаштування висоти камери змінює стандартне і максимальна відстань поле зору гравця.\nЧем вище це значення, тим далі буде поле зору.");

                labelResolution.Text = "Роздільна здатність:";
                Shadows3DCheckBox.Text = "3D Тіні";
                Shadows2DCheckBox.Text = "2D Тіні";
                CloudShadowsCheckBox.Text = "Тіні хмар";
                ExtraGroundLightingCheckBox.Text = "Додаткове освітлення землі";
                SmoothWaterBordersCheckBox.Text = "Рівні краї води";
                BehindBuildingsCheckBox.Text = "Одиниці за будівлями";
                ShowPropsCheckBox.Text = "Показувати маленькі об'єкти";
                ExtraAnimationsCheckBox.Text = "Додаткова анімація";
                DisableDynamicLODCheckBox.Text = "Вимкнути Динам. Рівень Деталізації";
                HeatEffectsCheckBox.Text = "Теплові ефекти";
                FogCheckBox.Text = "Ефект туману";
                LangFilterCheckBox.Text = "Мовний фільтр";
                WaterEffectsCheckBox.Text = "Водний ефект";
                camHeightLabel.Text = "Висота камери: ?";
                HotkeyStyleGroupBox.Text = "Стиль гарячих клавіш";
                LegacyHotkeysRadioButton.Text = "Оригінальний";
            }
            else if (Globals.BG_Checked == true)
            {
                toolTip3.SetToolTip(Shadows3DCheckBox, "Превключете показването на 3D сенки в играта.\nИзключете за по-добра производителност.");
                toolTip3.SetToolTip(Shadows2DCheckBox, "Превключете показването на 2D сенки в играта.\nИзключете за по-добра производителност.");
                toolTip3.SetToolTip(CloudShadowsCheckBox, "Превключете показването на облачни сенки по земята.\nИзключете за по-добра производителност.");
                toolTip3.SetToolTip(ExtraGroundLightingCheckBox, "Превключете показването на детайлно осветление по земята.\nИзключете за по-добра производителност.");
                toolTip3.SetToolTip(SmoothWaterBordersCheckBox, "Превключете изглаждане на водните граници.\nИзключете за по-добра производителност.");
                toolTip3.SetToolTip(BehindBuildingsCheckBox, "Превключете показването на единици зад сградите.\nИзключете за по-добра производителност.");
                toolTip3.SetToolTip(ShowPropsCheckBox, "Превключете показването на малки обекти.\nИзключете за по-добра производителност.");
                toolTip3.SetToolTip(ExtraAnimationsCheckBox, "Превключете показването на допълнителни анимации като например люлеене на дърветата.\nИзключете за по-добра производителност.");
                toolTip3.SetToolTip(DisableDynamicLODCheckBox, "Превключете автоматичното регулиране на детайлите.\nИзключете за по-добра производителност.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Превключете показването на топлинните ефекти.\nИзключете ги, ако вашият екран става черен, докато играете.");
                toolTip3.SetToolTip(FogCheckBox, "Превключете ефекта \"дълбочина на рязкост\".\nТози ефект добавя цветен слой на върха на екрана, зависещ от атмосферата на картата. Например, мъгла.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Изключването на езиковия филтър ще спре да скрива лошите думи, написани от играчите.");
                toolTip3.SetToolTip(WaterEffectsCheckBox, "Превключете водните ефекти (симулация на слънчево и облачно отражение).");
                toolTip3.SetToolTip(camHeightLabel, "Настройката за височина на камерата променя стандартното и максималното разстояние на изглед на играча.\nКолкото по-висока е тази стойност, толкова по-далеч ще бъде изгледът.");

                labelResolution.Text = "Резолюция:";
                Shadows3DCheckBox.Text = "3D сенки";
                Shadows2DCheckBox.Text = "2D сенки";
                CloudShadowsCheckBox.Text = "Облачни сенки";
                ExtraGroundLightingCheckBox.Text = "Допълн. осветление на земята";
                SmoothWaterBordersCheckBox.Text = "Гладки граници на водата";
                BehindBuildingsCheckBox.Text = "Единици зад сгради";
                ShowPropsCheckBox.Text = "Показвай малки обекти";
                ExtraAnimationsCheckBox.Text = "Допълнителни анимации";
                DisableDynamicLODCheckBox.Text = "Изкл. Динам. Ниво на Детайлност";
                HeatEffectsCheckBox.Text = "Топлинни ефекти";
                FogCheckBox.Text = "Мъглявинен ефект";
                LangFilterCheckBox.Text = "Езиков филтър";
                WaterEffectsCheckBox.Text = "Водни ефекти";
                camHeightLabel.Text = "Височина на камерата: ?";
                HotkeyStyleGroupBox.Text = "Стил на горещи клавиши";
                LegacyHotkeysRadioButton.Text = "Оригинален";
            }
            else if (Globals.DE_Checked == true)
            {
                toolTip3.SetToolTip(Shadows3DCheckBox, "Schaltet das Anzeigen von 3D-Schatten im Spiel um.\nSchalten Sie das Gerät aus, um die Leistung zu verbessern.");
                toolTip3.SetToolTip(Shadows2DCheckBox, "Schaltet das Anzeigen von 2D-Schatten im Spiel um.\nSchalten Sie das Gerät aus, um die Leistung zu verbessern.");
                toolTip3.SetToolTip(CloudShadowsCheckBox, "Umschalten der Anzeige von Wolkenschatten im Gelände.\nSchalten Sie das Gerät aus, um die Leistung zu verbessern.");
                toolTip3.SetToolTip(ExtraGroundLightingCheckBox, "Umschalten der Anzeige der detaillierten Beleuchtung im Gelände.\nSchalten Sie das Gerät aus, um die Leistung zu verbessern.");
                toolTip3.SetToolTip(SmoothWaterBordersCheckBox, "Glättung der Wassergrenzen umschalten.\nSchalten Sie das Gerät aus, um die Leistung zu verbessern.");
                toolTip3.SetToolTip(BehindBuildingsCheckBox, "Umschalten der Anzeige von Einheiten hinter Gebäuden.\nSchalten Sie das Gerät aus, um die Leistung zu verbessern.");
                toolTip3.SetToolTip(ShowPropsCheckBox, "Anzeige der Spielrequisiten umschalten.\nSchalten Sie das Gerät aus, um die Leistung zu verbessern.");
                toolTip3.SetToolTip(ExtraAnimationsCheckBox, "Optionale Animationen wie Baumschwankungen anzeigen.\nSchalten Sie das Gerät aus, um die Leistung zu verbessern.");
                toolTip3.SetToolTip(DisableDynamicLODCheckBox, "Deaktiviert die automatische Detailanpassung.\nSchalten Sie das Gerät aus, um die Leistung zu verbessern.");
                toolTip3.SetToolTip(HeatEffectsCheckBox, "Schalten Sie die Anzeige der thermischen Effekte.\nDeaktivieren Sie diese Option, wenn der Bildschirm während des Spiels zufällig schwarz wird.");
                toolTip3.SetToolTip(FogCheckBox, "Schalte Nebel (Tiefenschдrfe) Effekte An/Aus.");
                toolTip3.SetToolTip(LangFilterCheckBox, "Das ausschalten vom Sprache Filter zeigt bцse Wцrter von anderen Spielern im Chat an.");
                toolTip3.SetToolTip(WaterEffectsCheckBox, "Schalte Wasser Reflexion Effekte An/Aus.");
                toolTip3.SetToolTip(camHeightLabel, "Mit der Einstellung für die Kamerahöhe werden die Standard- und die maximale Anzeigedistanz des Players geändert.\nJe höher dieser Wert ist, desto weiter entfernt ist die Ansicht.");

                labelResolution.Text = "Auflцsung:";
                Shadows3DCheckBox.Text = "3D-Schatten";
                Shadows2DCheckBox.Text = "2D-Schatten";
                CloudShadowsCheckBox.Text = "Schatten der Wolken";
                ExtraGroundLightingCheckBox.Text = "Zusätzliche Bodenbeleuchtung";
                SmoothWaterBordersCheckBox.Text = "Flache Ränder des Wassers";
                BehindBuildingsCheckBox.Text = "Einheiten hinter Gebäuden";
                ShowPropsCheckBox.Text = "Kleine Objekte anzeigen";
                ExtraAnimationsCheckBox.Text = "Zusätzliche Animation";
                DisableDynamicLODCheckBox.Text = "Dynamische Detailebene deaktivieren";
                HeatEffectsCheckBox.Text = "Wärmeeffekte";
                FogCheckBox.Text = "Nebel Effekte";
                LangFilterCheckBox.Text = "Sprache Filter";
                WaterEffectsCheckBox.Text = "Wassereffekt";
                camHeightLabel.Text = "Kamerahöhe: ?";
                HotkeyStyleGroupBox.Text = "Hotkey-Stil";
                LegacyHotkeysRadioButton.Text = "Original";
            }

            // Read the settings from Options.ini and check/uncheck our checkboxes depending on values from the file
            if (Directory.Exists(Globals.myDocPath))
            {
                string text = File.ReadAllText(Globals.myDocPath + "Options.ini");
                {
                    if (text.Contains("StaticGameLOD = Low") || text.Contains("StaticGameLOD = Medium") || text.Contains("StaticGameLOD = High"))
                    {
                        File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(text, "\r?\nStaticGameLOD = .*", "\r\nStaticGameLOD = Custom" + "\r"));
                    }

                    if (text.Contains("UseShadowVolumes = No") || text.Contains("UseShadowVolumes = no"))
                    {
                        Shadows3DCheckBox.Checked = false;
                    }
                    else if (text.Contains("UseShadowVolumes = Yes") || text.Contains("UseShadowVolumes = yes"))
                    {
                        Shadows3DCheckBox.Checked = true;
                    }

                    if (text.Contains("UseShadowDecals = No") || text.Contains("UseShadowDecals = no"))
                    {
                        Shadows2DCheckBox.Checked = false;
                    }
                    else if (text.Contains("UseShadowDecals = Yes") || text.Contains("UseShadowDecals = yes"))
                    {
                        Shadows2DCheckBox.Checked = true;
                    }

                    if (text.Contains("UseCloudMap = No") || text.Contains("UseCloudMap = no"))
                    {
                        CloudShadowsCheckBox.Checked = false;
                    }
                    else if (text.Contains("UseCloudMap = Yes") || text.Contains("UseCloudMap = yes"))
                    {
                        CloudShadowsCheckBox.Checked = true;
                    }

                    if (text.Contains("UseLightMap = No") || text.Contains("UseLightMap = no"))
                    {
                        ExtraGroundLightingCheckBox.Checked = false;
                    }
                    else if (text.Contains("UseLightMap = Yes") || text.Contains("UseLightMap = yes"))
                    {
                        ExtraGroundLightingCheckBox.Checked = true;
                    }

                    if (text.Contains("ShowSoftWaterEdge = No") || text.Contains("ShowSoftWaterEdge = no"))
                    {
                        SmoothWaterBordersCheckBox.Checked = false;
                    }
                    else if (text.Contains("ShowSoftWaterEdge = Yes") || text.Contains("ShowSoftWaterEdge = yes"))
                    {
                        SmoothWaterBordersCheckBox.Checked = true;
                    }

                    if (text.Contains("BuildingOcclusion = No") || text.Contains("BuildingOcclusion = no"))
                    {
                        BehindBuildingsCheckBox.Checked = false;
                    }
                    else if (text.Contains("BuildingOcclusion = Yes") || text.Contains("BuildingOcclusion = yes"))
                    {
                        BehindBuildingsCheckBox.Checked = true;
                    }

                    if (text.Contains("ShowTrees = No") || text.Contains("ShowTrees = no"))
                    {
                        ShowPropsCheckBox.Checked = false;
                    }
                    else if (text.Contains("ShowTrees = Yes") || text.Contains("ShowTrees = yes"))
                    {
                        ShowPropsCheckBox.Checked = true;
                    }

                    if (text.Contains("ExtraAnimations = No") || text.Contains("ExtraAnimations = no"))
                    {
                        ExtraAnimationsCheckBox.Checked = false;
                    }
                    else if (text.Contains("ExtraAnimations = Yes") || text.Contains("ExtraAnimations = yes"))
                    {
                        ExtraAnimationsCheckBox.Checked = true;
                    }

                    if (text.Contains("DynamicLOD = No") || text.Contains("DynamicLOD = no"))
                    {
                        DisableDynamicLODCheckBox.Checked = true;
                    }
                    else if (text.Contains("DynamicLOD = Yes") || text.Contains("DynamicLOD = yes"))
                    {
                        DisableDynamicLODCheckBox.Checked = false;
                    }

                    if (text.Contains("HeatEffects = No") || text.Contains("HeatEffects = no"))
                    {
                        HeatEffectsCheckBox.Checked = false;
                        heatEffectsCheckBoxIsClicked = false;
                    }
                    else if (text.Contains("HeatEffects = Yes") || text.Contains("HeatEffects = yes"))
                    {
                        HeatEffectsCheckBox.Checked = true;
                        heatEffectsCheckBoxIsClicked = false; // Yes, false
                    }
                }
            }

            // Get current camera height
            if (File.Exists("!!!!Contra009Final_Patch3_GameData.big"))
            {
                string fileToRead = File.ReadAllText("!!!!Contra009Final_Patch3_GameData.big");

                try
                {
                    ReadCameraHeight(fileToRead);
                }
                catch (IOException)
                {
                    Messages.GenerateMessageBox("E_CloseGameDataP3", Globals.currentLanguage);
                }
            }
            else if (File.Exists("!!!Contra009Final_Patch2_GameData.big"))
            {
                string fileToRead = File.ReadAllText("!!!Contra009Final_Patch2_GameData.big");

                try
                {
                    ReadCameraHeight(fileToRead);
                }
                catch (IOException)
                {
                    Messages.GenerateMessageBox("E_CloseGameDataP2", Globals.currentLanguage);
                }
            }

            // Get current resolution
            if (Directory.Exists(Globals.myDocPath))
            {
                string s = File.ReadAllText(Globals.myDocPath + "Options.ini");
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
                            Properties.Settings.Default.Res = s2;
                            Properties.Settings.Default.Save();
                        }
                    }
                }
            }
            resolutionComboBox.Text = Properties.Settings.Default.Res;

            FogCheckBox.Checked = Properties.Settings.Default.Fog;
            LangFilterCheckBox.Checked = Properties.Settings.Default.LangF;
            WaterEffectsCheckBox.Checked = Properties.Settings.Default.WaterEffects;
            LeikezeHotkeysRadioButton.Checked = Properties.Settings.Default.LeikezeHotkeys;
            LegacyHotkeysRadioButton.Checked = Properties.Settings.Default.LegacyHotkeys;

            if (!File.Exists(Globals.myDocPath + "Options.ini"))
            {
                Messages.GenerateMessageBox("E_CouldNotLoadRes", Globals.currentLanguage);
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
                            camTrackBar.Value = value + 110;
                        }
                        else
                        {
                            camTrackBar.Value = Convert.ToInt32(line);
                        }
                        camHeightLabel.Text = Messages.GenerateMessage("CameraHeightString", Globals.currentLanguage) + camTrackBar.Value.ToString() + ".0";
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
            this.Close();
        }

        private void resOkButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Globals.myDocPath))
            {
                string text = File.ReadAllText(Globals.myDocPath + "Options.ini");
                {
                    if (!Regex.IsMatch(resolutionComboBox.Text, @"^[0-9]{3,4}x[0-9]{3,4}$")) //if selected res doesn't match valid input (input must match the regex)
                    {
                        Messages.GenerateMessageBox("E_InvalidRes", Globals.currentLanguage);
                        //return;
                    }
                    else
                    {
                        string fixedText = resolutionComboBox.Text.Replace("x", " ");
                        File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(text, "\r?\nResolution =.*", "\r\nResolution = " + fixedText + "\r"));
                        Messages.GenerateMessageBox("I_ResChanged", Globals.currentLanguage);
                        IsGeneralsRunning();
                    }
                }
            }
 
            if (!File.Exists(Globals.myDocPath + "Options.ini"))
            {
                Messages.GenerateMessageBox("E_CouldNotSetRes", Globals.currentLanguage);
            }
        }

        private void resOkButton_MouseDown(object sender, MouseEventArgs e)
        {
            resOkButton.BackgroundImage = Properties.Resources.btnOk3a;
            resOkButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }
        private void resOkButton_MouseLeave(object sender, EventArgs e)
        {
            resOkButton.BackgroundImage = Properties.Resources.btnOk3;
            resOkButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void ExitBtnSm_MouseEnter(object sender, EventArgs e)
        {
            ExitBtnSm.BackgroundImage = Properties.Resources._button_sm_exit;
        }
        private void ExitBtnSm_MouseLeave(object sender, EventArgs e)
        {
            ExitBtnSm.BackgroundImage = Properties.Resources._button_sm_exit;
        }
        private void ExitBtnSm_Click(object sender, EventArgs e)
        {
            this.Close(); //OnApplicationExit(sender, e);
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
            this.WindowState = FormWindowState.Minimized;
        }

        private void FogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!FogCheckBox.Checked)
            {
                Properties.Settings.Default.Fog = false;
            }
            else Properties.Settings.Default.Fog = true;
            Properties.Settings.Default.Save();
            IsGeneralsRunning();
        }

        private void LangFilterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!LangFilterCheckBox.Checked)
            {
                Properties.Settings.Default.LangF = false;
            }
            else Properties.Settings.Default.LangF = true;
            Properties.Settings.Default.Save();
            IsGeneralsRunning();
        }

        private void WaterEffectsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!WaterEffectsCheckBox.Checked)
            {
                Properties.Settings.Default.WaterEffects = false;
            }
            else Properties.Settings.Default.WaterEffects = true;
            Properties.Settings.Default.Save();
            IsGeneralsRunning();
        }

        private void LeikezeHotkeysRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!LeikezeHotkeysRadioButton.Checked)
            {
                Properties.Settings.Default.LeikezeHotkeys = false;
            }
            else Properties.Settings.Default.LeikezeHotkeys = true;
            Properties.Settings.Default.Save();
            IsGeneralsRunning();
        }

        private void LegacyHotkeysRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!LegacyHotkeysRadioButton.Checked)
            {
                Properties.Settings.Default.LegacyHotkeys = false;
            }
            else Properties.Settings.Default.LegacyHotkeys = true;
            Properties.Settings.Default.Save();
            IsGeneralsRunning();
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
            if (File.Exists("!!!!Contra009Final_Patch3.big") || File.Exists("!!!!Contra009Final_Patch3.ctr"))
            {
                CamHeightRegexReplace("!!!!Contra009Final_Patch3_GameData.big", "E_NotFound_GameDataP3");
            }
            else if (File.Exists("!!!Contra009Final_Patch2.big") || File.Exists("!!!Contra009Final_Patch2.ctr"))
            {
                CamHeightRegexReplace("!!!Contra009Final_Patch2_GameData.big", "E_NotFound_GameDataP2");
            }
            else
            {
                Messages.GenerateMessageBox("E_MissingFiles_CouldNotChangeCamHeight", Globals.currentLanguage);
            }
        }

        private void CamHeightRegexReplace(string gameDataFilename, string gameDataNotFoundMsg)
        {
            if (File.Exists(gameDataFilename))
            {
                Encoding encoding = Encoding.GetEncoding("windows-1252");
                var regex = ""; var regex2 = "";

                if (AspectRatio(x, y) == "16:9")
                {
                    regex = Regex.Replace(File.ReadAllText(gameDataFilename, encoding), "  MaxCameraHeight = .*\r?\n", "  MaxCameraHeight = " + (camTrackBar.Value - 110) + ".0" + " ;350.0\r\n");
                }
                else
                {
                    regex = Regex.Replace(File.ReadAllText(gameDataFilename, encoding), "  MaxCameraHeight = .*\r?\n", "  MaxCameraHeight = " + camTrackBar.Value + ".0" + " ;350.0\r\n");
                }
                File.WriteAllText(gameDataFilename, regex, encoding);

                if (camTrackBar.Value > 392)
                {
                    regex2 = Regex.Replace(File.ReadAllText(gameDataFilename, encoding), "  DrawEntireTerrain = No\r?\n", "  DrawEntireTerrain = Yes\r\n");
                }
                else
                {
                    regex2 = Regex.Replace(File.ReadAllText(gameDataFilename, encoding), "  DrawEntireTerrain = Yes\r?\n", "  DrawEntireTerrain = No\r\n");
                }
                File.WriteAllText(gameDataFilename, regex2, encoding);

                Messages.GenerateMessageBox("I_CameraHeightChanged", Globals.currentLanguage);
            }
            else
            {
                Messages.GenerateMessageBox(gameDataNotFoundMsg, Globals.currentLanguage);
            }
        }

        private void camOkButton_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeCamHeight();
            }
            catch (IOException)
            {
                if (File.Exists("!!!!Contra009Final_Patch3_GameData.big"))
                {
                    Messages.GenerateMessageBox("E_CloseGameDataP3", Globals.currentLanguage);
                }
                else if (File.Exists("!!!Contra009Final_Patch2_GameData.big"))
                {
                    Messages.GenerateMessageBox("E_CloseGameDataP2", Globals.currentLanguage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void camTrackBar_Scroll(object sender, EventArgs e)
        {
            camHeightLabel.Text = Messages.GenerateMessage("CameraHeightString", Globals.currentLanguage) + camTrackBar.Value.ToString() + ".0";
        }

        public void IsGeneralsRunning()
        {
            if (Form.ActiveForm == this)
            {
                Process[] genByName = Process.GetProcessesByName("generals");
                if (genByName.Length > 0)
                {
                    Messages.GenerateMessageBox("W_ChangesAfterGameRestart", Globals.currentLanguage);
                }
            }
        }

        private void Shadows3DCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string optionsIniText = File.ReadAllText(Globals.myDocPath + "Options.ini");
            if (!Shadows3DCheckBox.Checked)
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nUseShadowVolumes = Yes", "\r\nUseShadowVolumes = No", RegexOptions.IgnoreCase));
            }
            else
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nUseShadowVolumes = No", "\r\nUseShadowVolumes = Yes", RegexOptions.IgnoreCase));
            }
            IsGeneralsRunning();
        }

        private void Shadows2DCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string optionsIniText = File.ReadAllText(Globals.myDocPath + "Options.ini");
            if (!Shadows2DCheckBox.Checked)
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nUseShadowDecals = Yes", "\r\nUseShadowDecals = No", RegexOptions.IgnoreCase));
            }
            else
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nUseShadowDecals = No", "\r\nUseShadowDecals = Yes", RegexOptions.IgnoreCase));
            }
            IsGeneralsRunning();
        }

        private void CloudShadowsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string optionsIniText = File.ReadAllText(Globals.myDocPath + "Options.ini");
            if (!CloudShadowsCheckBox.Checked)
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nUseCloudMap = Yes", "\r\nUseCloudMap = No", RegexOptions.IgnoreCase));
            }
            else
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nUseCloudMap = No", "\r\nUseCloudMap = Yes", RegexOptions.IgnoreCase));
            }
            IsGeneralsRunning();
        }

        private void ExtraGroundLightingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string optionsIniText = File.ReadAllText(Globals.myDocPath + "Options.ini");
            if (!ExtraGroundLightingCheckBox.Checked)
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nUseLightMap = Yes", "\r\nUseLightMap = No", RegexOptions.IgnoreCase));
            }
            else
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nUseLightMap = No", "\r\nUseLightMap = Yes", RegexOptions.IgnoreCase));
            }
            IsGeneralsRunning();
        }

        private void SmoothWaterBordersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string optionsIniText = File.ReadAllText(Globals.myDocPath + "Options.ini");
            if (!SmoothWaterBordersCheckBox.Checked)
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nShowSoftWaterEdge = Yes", "\r\nShowSoftWaterEdge = No", RegexOptions.IgnoreCase));
            }
            else
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nShowSoftWaterEdge = No", "\r\nShowSoftWaterEdge = Yes", RegexOptions.IgnoreCase));
            }
            IsGeneralsRunning();
        }

        private void BehindBuildingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string optionsIniText = File.ReadAllText(Globals.myDocPath + "Options.ini");
            if (!BehindBuildingsCheckBox.Checked)
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nBuildingOcclusion = Yes", "\r\nBuildingOcclusion = No", RegexOptions.IgnoreCase));
            }
            else
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nBuildingOcclusion = No", "\r\nBuildingOcclusion = Yes", RegexOptions.IgnoreCase));
            }
            IsGeneralsRunning();
        }

        private void ShowPropsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string optionsIniText = File.ReadAllText(Globals.myDocPath + "Options.ini");
            if (!ShowPropsCheckBox.Checked)
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nShowTrees = Yes", "\r\nShowTrees = No", RegexOptions.IgnoreCase));
            }
            else
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nShowTrees = No", "\r\nShowTrees = Yes", RegexOptions.IgnoreCase));
            }
            IsGeneralsRunning();
        }

        private void ExtraAnimationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string optionsIniText = File.ReadAllText(Globals.myDocPath + "Options.ini");
            if (!ExtraAnimationsCheckBox.Checked)
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nExtraAnimations = Yes", "\r\nExtraAnimations = No", RegexOptions.IgnoreCase));
            }
            else
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nExtraAnimations = No", "\r\nExtraAnimations = Yes", RegexOptions.IgnoreCase));
            }
            IsGeneralsRunning();
        }

        private void DisableDynamicLODCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string optionsIniText = File.ReadAllText(Globals.myDocPath + "Options.ini");
            if (DisableDynamicLODCheckBox.Checked)
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nDynamicLOD = Yes", "\r\nDynamicLOD = No", RegexOptions.IgnoreCase));
            }
            else
            {
                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nDynamicLOD = No", "\r\nDynamicLOD = Yes", RegexOptions.IgnoreCase));
            }
            IsGeneralsRunning();
        }

        private void HeatEffectsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string optionsIniText = File.ReadAllText(Globals.myDocPath + "Options.ini");
            if (!HeatEffectsCheckBox.Checked)
            {
                heatEffectsCheckBoxIsClicked = false;
                Properties.Settings.Default.HeatEffects = false;
                Properties.Settings.Default.Save();

                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nHeatEffects = Yes", "\r\nHeatEffects = No", RegexOptions.IgnoreCase));
            }
            else
            {
                heatEffectsCheckBoxIsClicked = true;
                Properties.Settings.Default.HeatEffects = true;
                Properties.Settings.Default.Save();

                File.WriteAllText(Globals.myDocPath + "Options.ini", Regex.Replace(optionsIniText, "\r?\nHeatEffects = No", "\r\nHeatEffects = Yes", RegexOptions.IgnoreCase));
            }
            IsGeneralsRunning();
        }
        private void HeatEffectsCheckBox_Click(object sender, EventArgs e)
        {
            if (heatEffectsCheckBoxIsClicked == true)
            {
                Messages.GenerateMessageBox("W_BlackScreen", Globals.currentLanguage);
            }
        }
    }
}