using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Text;
using System.Diagnostics;

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

            //// Make CTR Options.ini active
            //try
            //{
            //    if (File.Exists(UserDataLeafName() + "Options_CTR.ini"))
            //    {
            //        File.SetAttributes(UserDataLeafName() + "Options.ini", FileAttributes.Normal);
            //        File.SetAttributes(UserDataLeafName() + "Options_CTR.ini", FileAttributes.Normal);
            //        File.SetAttributes(UserDataLeafName() + "Options_ZH.ini", FileAttributes.Normal);
            //        File.Copy(UserDataLeafName() + "Options.ini", UserDataLeafName() + "Options_ZH.ini", true);
            //        File.Copy(UserDataLeafName() + "Options_CTR.ini", UserDataLeafName() + "Options.ini", true);
            //    }
            //    else if (File.Exists(myDocPath + "Options_CTR.ini"))
            //    {
            //        File.SetAttributes(myDocPath + "Options.ini", FileAttributes.Normal);
            //        File.SetAttributes(myDocPath + "Options_CTR.ini", FileAttributes.Normal);
            //        File.SetAttributes(myDocPath + "Options_ZH.ini", FileAttributes.Normal);
            //        File.Copy(myDocPath + "Options.ini", myDocPath + "Options_ZH.ini", true);
            //        File.Copy(myDocPath + "Options_CTR.ini", myDocPath + "Options.ini", true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            // Read from Options.ini and check/uncheck settings depending on values there
            if (Directory.Exists(UserDataLeafName()))
            {
                string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                {
                    if (text.Contains("StaticGameLOD = Low") || text.Contains("StaticGameLOD = Medium") || text.Contains("StaticGameLOD = High"))
                    {
                        File.WriteAllText(UserDataLeafName() + "Options.ini", Regex.Replace(File.ReadAllText(UserDataLeafName() + "Options.ini"), "\r?\nStaticGameLOD = .*", "\r\nStaticGameLOD = Custom" + "\r"));
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
                    }
                    else if (text.Contains("HeatEffects = Yes") || text.Contains("HeatEffects = yes"))
                    {
                        HeatEffectsCheckBox.Checked = true;
                    }
                }
            }
            else if (Directory.Exists(myDocPath))
            {
                string text = File.ReadAllText(myDocPath + "Options.ini");
                {
                    if (text.Contains("StaticGameLOD = Low") || text.Contains("StaticGameLOD = Medium") || text.Contains("StaticGameLOD = High"))
                    {
                        File.WriteAllText(myDocPath + "Options.ini", Regex.Replace(File.ReadAllText(myDocPath + "Options.ini"), "\r?\nStaticGameLOD = .*", "\r\nStaticGameLOD = Custom" + "\r"));
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
                    }
                    else if (text.Contains("HeatEffects = Yes") || text.Contains("HeatEffects = yes"))
                    {
                        HeatEffectsCheckBox.Checked = true;
                    }
                }
            }


            // Get current camera height
            if (File.Exists("!!!!Contra009Final_Patch3_GameData.big"))
            {
                try
                {
                    string s = File.ReadAllText("!!!!Contra009Final_Patch3_GameData.big");
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
                        }
                    }
                }
                catch (IOException)
                {
                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("Please close !!!!Contra009Final_Patch3_GameData.big in order to change camera height.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("Пожалуйста, закройте !!!!Contra009Final_Patch3_GameData.big, чтобы изменить высоту камеры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("Будь ласка, закрийте !!!!Contra009Final_Patch3_GameData.big, щоб змінити висоту камери.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("Моля, затворете !!!!Contra009Final_Patch3_GameData.big, за да промените височината на камерата.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("Bitte schließen Sie !!!!Contra009Final_Patch3_GameData.big, um die Kamerahöhe zu ändern.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (File.Exists("!!!Contra009Final_Patch2_GameData.big"))
            {
                try
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
                        }
                    }
                }
                catch (IOException)
                {
                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("Please close !!!Contra009Final_Patch2_GameData.big in order to change camera height.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("Пожалуйста, закройте !!!Contra009Final_Patch2_GameData.big, чтобы изменить высоту камеры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("Будь ласка, закрийте !!!Contra009Final_Patch2_GameData.big, щоб змінити висоту камери.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("Моля, затворете !!!Contra009Final_Patch2_GameData.big, за да промените височината на камерата.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("Bitte schließen Sie !!!Contra009Final_Patch2_GameData.big, um die Kamerahöhe zu ändern.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


            // Get current resolution
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
                LangFilterCheckBox.Checked = Properties.Settings.Default.LangF;
                WaterEffectsCheckBox.Checked = Properties.Settings.Default.WaterEffects;
                LeikezeHotkeysRadioButton.Checked = Properties.Settings.Default.LeikezeHotkeys;
                LegacyHotkeysRadioButton.Checked = Properties.Settings.Default.LegacyHotkeys;
                Shadows3DCheckBox.Checked = Properties.Settings.Default.Shadows3D;
                DisableDynamicLODCheckBox.Checked = Properties.Settings.Default.DisableDynamicLOD;
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
                    MessageBox.Show("Options.ini nicht gefunden! Aktuelle Auflцsung konnte nicht geladen werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.Close(); //OnApplicationExit(sender, e);
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
                        IsGeneralsRunning();
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
            button18.BackgroundImage = Properties.Resources.exit11;
        }

        private void button18_MouseLeave(object sender, EventArgs e)
        {
            button18.BackgroundImage = Properties.Resources.exit1;
        }

        private void button17_MouseEnter(object sender, EventArgs e)
        {
            button17.BackgroundImage = Properties.Resources.min11;
        }

        private void button17_MouseLeave(object sender, EventArgs e)
        {
            button17.BackgroundImage = Properties.Resources.min;
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
            IsGeneralsRunning();
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
            IsGeneralsRunning();
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
            IsGeneralsRunning();
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
                if (File.Exists("!!!!Contra009Final_Patch3_GameData.big"))
                {
                    Encoding encoding = Encoding.GetEncoding("windows-1252");
                    var regex = "";
                    if (AspectRatio(x, y) == "16:9")
                    {
                        regex = Regex.Replace(File.ReadAllText("!!!!Contra009Final_Patch3_GameData.big"), "  MaxCameraHeight = .*\r?\n", "  MaxCameraHeight = " + (camTrackBar.Value - 110) + ".0" + " ;350.0\r\n");
                    }
                    else
                    {
                        regex = Regex.Replace(File.ReadAllText("!!!!Contra009Final_Patch3_GameData.big"), "  MaxCameraHeight = .*\r?\n", "  MaxCameraHeight = " + camTrackBar.Value + ".0" + " ;350.0\r\n");
                    }
                    string read = File.ReadAllText("!!!!Contra009Final_Patch3_GameData.big", encoding);
                    File.WriteAllText("!!!!Contra009Final_Patch3_GameData.big", regex, encoding);

                    if (camTrackBar.Value > 392)
                    {
                        var regex2 = Regex.Replace(File.ReadAllText("!!!!Contra009Final_Patch3_GameData.big"), "  DrawEntireTerrain = No\r?\n", "  DrawEntireTerrain = Yes\r\n");
                        string read2 = File.ReadAllText("!!!!Contra009Final_Patch3_GameData.big", encoding);
                        File.WriteAllText("!!!!Contra009Final_Patch3_GameData.big", regex2, encoding);
                    }
                    else
                    {
                        var regex2 = Regex.Replace(File.ReadAllText("!!!!Contra009Final_Patch3_GameData.big"), "  DrawEntireTerrain = Yes\r?\n", "  DrawEntireTerrain = No\r\n");
                        string read2 = File.ReadAllText("!!!!Contra009Final_Patch3_GameData.big", encoding);
                        File.WriteAllText("!!!!Contra009Final_Patch3_GameData.big", regex2, encoding);
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
                        MessageBox.Show("\"!!!!Contra009Final_Patch3_GameData.big\" file not found!", "Error");
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("Файл \"!!!!Contra009Final_Patch3_GameData.big\" не найден!", "Ошибка");
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("Файл \"!!!!Contra009Final_Patch3_GameData.big\" не знайдений!", "Помилка");
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("Файлът \"!!!!Contra009Final_Patch3_GameData.big\" не беше намерен!", "Грешка");
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("\"!!!!Contra009Final_Patch3_GameData.big\" nicht gefunden!", "Fehler");
                    }
                }
            }
            else if (File.Exists("!!!Contra009Final_Patch2.big") || File.Exists("!!!Contra009Final_Patch2.ctr"))
            {
                if (File.Exists("!!!Contra009Final_Patch2_GameData.big"))
                {
                    Encoding encoding = Encoding.GetEncoding("windows-1252");
                    var regex = "";
                    if (AspectRatio(x, y) == "16:9")
                    {
                        regex = Regex.Replace(File.ReadAllText("!!!Contra009Final_Patch2_GameData.big"), "  MaxCameraHeight = .*\r?\n", "  MaxCameraHeight = " + (camTrackBar.Value - 110) + ".0" + " ;350.0\r\n");
                    }
                    else
                    {
                        regex = Regex.Replace(File.ReadAllText("!!!Contra009Final_Patch2_GameData.big"), "  MaxCameraHeight = .*\r?\n", "  MaxCameraHeight = " + camTrackBar.Value + ".0" + " ;350.0\r\n");
                    }
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
                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("Please close !!!!Contra009Final_Patch3_GameData.big in order to change camera height.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("Пожалуйста, закройте !!!!Contra009Final_Patch3_GameData.big, чтобы изменить высоту камеры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("Будь ласка, закрийте !!!!Contra009Final_Patch3_GameData.big, щоб змінити висоту камери.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("Моля, затворете !!!!Contra009Final_Patch3_GameData.big, за да промените височината на камерата.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("Bitte schließen Sie !!!!Contra009Final_Patch3_GameData.big, um die Kamerahöhe zu ändern.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (File.Exists("!!!Contra009Final_Patch2_GameData.big"))
                {
                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("Please close !!!Contra009Final_Patch2_GameData.big in order to change camera height.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("Пожалуйста, закройте !!!Contra009Final_Patch2_GameData.big, чтобы изменить высоту камеры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("Будь ласка, закрийте !!!Contra009Final_Patch2_GameData.big, щоб змінити висоту камери.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("Моля, затворете !!!Contra009Final_Patch2_GameData.big, за да промените височината на камерата.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("Bitte schließen Sie !!!Contra009Final_Patch2_GameData.big, um die Kamerahöhe zu ändern.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
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

        public void IsGeneralsRunning()
        {
            if (Form.ActiveForm == this)
            {
                Process[] genByName = Process.GetProcessesByName("generals");
                if (genByName.Length > 0) //if the game is already running
                {
                    if (Globals.GB_Checked == true)
                    {
                        MessageBox.Show("Changes will take effect after game restart.", "Warning");
                    }
                    else if (Globals.RU_Checked == true)
                    {
                        MessageBox.Show("Изменения вступят в силу после перезапуска игры.", "Предупреждение");
                    }
                    else if (Globals.UA_Checked == true)
                    {
                        MessageBox.Show("Зміни набудуть чинності після перезапуску гри.", "Попередження");
                    }
                    else if (Globals.BG_Checked == true)
                    {
                        MessageBox.Show("Промените ще влязат в сила след рестартиране на играта.", "Предупреждение");
                    }
                    else if (Globals.DE_Checked == true)
                    {
                        MessageBox.Show("Änderungen werden nach dem Neustart des Spiels wirksam.", "Warnung");
                    }
                }
            }
        }

        private void Shadows3DCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!Shadows3DCheckBox.Checked)
            {
                Properties.Settings.Default.Shadows3D = false;
                Properties.Settings.Default.Save();

                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("UseShadowVolumes = Yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseShadowVolumes = Yes", "UseShadowVolumes = No"));
                        }
                        else if (text.Contains("UseShadowVolumes = yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseShadowVolumes = yes", "UseShadowVolumes = No"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("UseShadowVolumes = Yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseShadowVolumes = Yes", "UseShadowVolumes = No"));
                        }
                        else if (text.Contains("UseShadowVolumes = yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseShadowVolumes = yes", "UseShadowVolumes = No"));
                        }
                    }
                }
            }
            else
            {
                Properties.Settings.Default.Shadows3D = true;
                Properties.Settings.Default.Save();

                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("UseShadowVolumes = No"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseShadowVolumes = No", "UseShadowVolumes = Yes"));
                        }
                        else if (text.Contains("UseShadowVolumes = no"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseShadowVolumes = no", "UseShadowVolumes = Yes"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("UseShadowVolumes = No"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseShadowVolumes = No", "UseShadowVolumes = Yes"));
                        }
                        else if (text.Contains("UseShadowVolumes = no"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseShadowVolumes = no", "UseShadowVolumes = Yes"));
                        }
                    }
                }
            }
            IsGeneralsRunning();
        }

        private void Shadows2DCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!Shadows2DCheckBox.Checked)
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("UseShadowDecals = Yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseShadowDecals = Yes", "UseShadowDecals = No"));
                        }
                        else if (text.Contains("UseShadowDecals = yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseShadowDecals = yes", "UseShadowDecals = No"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("UseShadowDecals = Yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseShadowDecals = Yes", "UseShadowDecals = No"));
                        }
                        else if (text.Contains("UseShadowDecals = yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseShadowDecals = yes", "UseShadowDecals = No"));
                        }
                    }
                }
            }
            else
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("UseShadowDecals = No"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseShadowDecals = No", "UseShadowDecals = Yes"));
                        }
                        else if (text.Contains("UseShadowDecals = no"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseShadowDecals = no", "UseShadowDecals = Yes"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("UseShadowDecals = No"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseShadowDecals = No", "UseShadowDecals = Yes"));
                        }
                        else if (text.Contains("UseShadowDecals = no"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseShadowDecals = no", "UseShadowDecals = Yes"));
                        }
                    }
                }
            }
            IsGeneralsRunning();
        }

        private void CloudShadowsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!CloudShadowsCheckBox.Checked)
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("UseCloudMap = Yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseCloudMap = Yes", "UseCloudMap = No"));
                        }
                        else if (text.Contains("UseCloudMap = yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseCloudMap = yes", "UseCloudMap = No"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("UseCloudMap = Yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseCloudMap = Yes", "UseCloudMap = No"));
                        }
                        else if (text.Contains("UseCloudMap = yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseCloudMap = yes", "UseCloudMap = No"));
                        }
                    }
                }
            }
            else
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("UseCloudMap = No"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseCloudMap = No", "UseCloudMap = Yes"));
                        }
                        else if (text.Contains("UseCloudMap = no"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseCloudMap = no", "UseCloudMap = Yes"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("UseCloudMap = No"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseCloudMap = No", "UseCloudMap = Yes"));
                        }
                        else if (text.Contains("UseCloudMap = no"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseCloudMap = no", "UseCloudMap = Yes"));
                        }
                    }
                }
            }
            IsGeneralsRunning();
        }

        private void ExtraGroundLightingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!ExtraGroundLightingCheckBox.Checked)
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("UseLightMap = Yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseLightMap = Yes", "UseLightMap = No"));
                        }
                        else if (text.Contains("UseLightMap = yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseLightMap = yes", "UseLightMap = No"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("UseLightMap = Yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseLightMap = Yes", "UseLightMap = No"));
                        }
                        else if (text.Contains("UseLightMap = yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseLightMap = yes", "UseLightMap = No"));
                        }
                    }
                }
            }
            else
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("UseLightMap = No"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseLightMap = No", "UseLightMap = Yes"));
                        }
                        else if (text.Contains("UseLightMap = no"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("UseLightMap = no", "UseLightMap = Yes"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("UseLightMap = No"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseLightMap = No", "UseLightMap = Yes"));
                        }
                        else if (text.Contains("UseLightMap = no"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("UseLightMap = no", "UseLightMap = Yes"));
                        }
                    }
                }
            }
            IsGeneralsRunning();
        }

        private void SmoothWaterBordersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!SmoothWaterBordersCheckBox.Checked)
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("ShowSoftWaterEdge = Yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ShowSoftWaterEdge = Yes", "ShowSoftWaterEdge = No"));
                        }
                        else if (text.Contains("ShowSoftWaterEdge = yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ShowSoftWaterEdge = yes", "ShowSoftWaterEdge = No"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("ShowSoftWaterEdge = Yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ShowSoftWaterEdge = Yes", "ShowSoftWaterEdge = No"));
                        }
                        else if (text.Contains("ShowSoftWaterEdge = yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ShowSoftWaterEdge = yes", "ShowSoftWaterEdge = No"));
                        }
                    }
                }
            }
            else
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("ShowSoftWaterEdge = No"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ShowSoftWaterEdge = No", "ShowSoftWaterEdge = Yes"));
                        }
                        else if (text.Contains("ShowSoftWaterEdge = no"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ShowSoftWaterEdge = no", "ShowSoftWaterEdge = Yes"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("ShowSoftWaterEdge = No"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ShowSoftWaterEdge = No", "ShowSoftWaterEdge = Yes"));
                        }
                        else if (text.Contains("ShowSoftWaterEdge = no"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ShowSoftWaterEdge = no", "ShowSoftWaterEdge = Yes"));
                        }
                    }
                }
            }
            IsGeneralsRunning();
        }

        private void BehindBuildingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!BehindBuildingsCheckBox.Checked)
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("BuildingOcclusion = Yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("BuildingOcclusion = Yes", "BuildingOcclusion = No"));
                        }
                        else if (text.Contains("BuildingOcclusion = yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("BuildingOcclusion = yes", "BuildingOcclusion = No"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("BuildingOcclusion = Yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("BuildingOcclusion = Yes", "BuildingOcclusion = No"));
                        }
                        else if (text.Contains("BuildingOcclusion = yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("BuildingOcclusion = yes", "BuildingOcclusion = No"));
                        }
                    }
                }
            }
            else
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("BuildingOcclusion = No"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("BuildingOcclusion = No", "BuildingOcclusion = Yes"));
                        }
                        else if (text.Contains("BuildingOcclusion = no"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("BuildingOcclusion = no", "BuildingOcclusion = Yes"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("BuildingOcclusion = No"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("BuildingOcclusion = No", "BuildingOcclusion = Yes"));
                        }
                        else if (text.Contains("BuildingOcclusion = no"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("BuildingOcclusion = no", "BuildingOcclusion = Yes"));
                        }
                    }
                }
            }
            IsGeneralsRunning();
        }

        private void ShowPropsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!ShowPropsCheckBox.Checked)
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("ShowTrees = Yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ShowTrees = Yes", "ShowTrees = No"));
                        }
                        else if (text.Contains("ShowTrees = yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ShowTrees = yes", "ShowTrees = No"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("ShowTrees = Yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ShowTrees = Yes", "ShowTrees = No"));
                        }
                        else if (text.Contains("ShowTrees = yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ShowTrees = yes", "ShowTrees = No"));
                        }
                    }
                }
            }
            else
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("ShowTrees = No"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ShowTrees = No", "ShowTrees = Yes"));
                        }
                        else if (text.Contains("ShowTrees = no"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ShowTrees = no", "ShowTrees = Yes"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("ShowTrees = No"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ShowTrees = No", "ShowTrees = Yes"));
                        }
                        else if (text.Contains("ShowTrees = no"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ShowTrees = no", "ShowTrees = Yes"));
                        }
                    }
                }
            }
            IsGeneralsRunning();
        }

        private void ExtraAnimationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!ExtraAnimationsCheckBox.Checked)
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("ExtraAnimations = Yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ExtraAnimations = Yes", "ExtraAnimations = No"));
                        }
                        else if (text.Contains("ExtraAnimations = yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ExtraAnimations = yes", "ExtraAnimations = No"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("ExtraAnimations = Yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ExtraAnimations = Yes", "ExtraAnimations = No"));
                        }
                        else if (text.Contains("ExtraAnimations = yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ExtraAnimations = yes", "ExtraAnimations = No"));
                        }
                    }
                }
            }
            else
            {
                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("ExtraAnimations = No"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ExtraAnimations = No", "ExtraAnimations = Yes"));
                        }
                        else if (text.Contains("ExtraAnimations = no"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("ExtraAnimations = no", "ExtraAnimations = Yes"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("ExtraAnimations = No"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ExtraAnimations = No", "ExtraAnimations = Yes"));
                        }
                        else if (text.Contains("ExtraAnimations = no"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("ExtraAnimations = no", "ExtraAnimations = Yes"));
                        }
                    }
                }
            }
            IsGeneralsRunning();
        }

        private void DisableDynamicLODCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DisableDynamicLODCheckBox.Checked)
            {
                Properties.Settings.Default.DisableDynamicLOD = true;
                Properties.Settings.Default.Save();

                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("DynamicLOD = Yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("DynamicLOD = Yes", "DynamicLOD = No"));
                        }
                        else if (text.Contains("DynamicLOD = yes"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("DynamicLOD = yes", "DynamicLOD = No"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("DynamicLOD = Yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("DynamicLOD = Yes", "DynamicLOD = No"));
                        }
                        else if (text.Contains("DynamicLOD = yes"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("DynamicLOD = yes", "DynamicLOD = No"));
                        }
                    }
                }
            }
            else
            {
                Properties.Settings.Default.DisableDynamicLOD = false;
                Properties.Settings.Default.Save();

                if (Directory.Exists(UserDataLeafName()))
                {
                    string text = File.ReadAllText(UserDataLeafName() + "Options.ini");
                    {
                        if (text.Contains("DynamicLOD = No"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("DynamicLOD = No", "DynamicLOD = Yes"));
                        }
                        else if (text.Contains("DynamicLOD = no"))
                        {
                            File.WriteAllText(UserDataLeafName() + "Options.ini", File.ReadAllText(UserDataLeafName() + "Options.ini").Replace("DynamicLOD = no", "DynamicLOD = Yes"));
                        }
                    }
                }
                else if (Directory.Exists(myDocPath))
                {
                    string text = File.ReadAllText(myDocPath + "Options.ini");
                    {
                        if (text.Contains("DynamicLOD = No"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("DynamicLOD = No", "DynamicLOD = Yes"));
                        }
                        else if (text.Contains("DynamicLOD = no"))
                        {
                            File.WriteAllText(myDocPath + "Options.ini", File.ReadAllText(myDocPath + "Options.ini").Replace("DynamicLOD = no", "DynamicLOD = Yes"));
                        }
                    }
                }
            }
            IsGeneralsRunning();
        }

        private void HeatEffectsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!HeatEffectsCheckBox.Checked)
            {
                Properties.Settings.Default.HeatEffects = false;
                Properties.Settings.Default.Save();

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
                    }
                }
            }
            else
            {
                Properties.Settings.Default.HeatEffects = true;
                Properties.Settings.Default.Save();

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
                    }
                }
            }
            IsGeneralsRunning();
        }

        private void LeikezeHotkeysRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!LeikezeHotkeysRadioButton.Checked)
            {
                Properties.Settings.Default.LegacyHotkeys = false;
                Properties.Settings.Default.Save();
            }
            else Properties.Settings.Default.LegacyHotkeys = true;
            Properties.Settings.Default.Save();
            IsGeneralsRunning();
        }

        private void LegacyHotkeysRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!LegacyHotkeysRadioButton.Checked)
            {
                Properties.Settings.Default.LeikezeHotkeys = false;
                Properties.Settings.Default.Save();
            }
            else Properties.Settings.Default.LeikezeHotkeys = true;
            Properties.Settings.Default.Save();
            IsGeneralsRunning();
        }
    }
}