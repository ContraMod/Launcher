using System.Windows.Forms;

namespace Contra
{
    public class Messages
    {
        public static void GenerateMessageBox(string type, string lang)
        {
            switch (lang)
            {
                case "EN":
                    switch (type)
                    {
                        case "E_NotFound_OptionsIni":
                            MessageBox.Show("Options.ini not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_WB":
                            MessageBox.Show("World Builder was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_GameDataP3":
                            MessageBox.Show("\"!ContraXBeta_GameData.big\" file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP3":
                            MessageBox.Show("Please close !ContraXBeta_GameData.big in order to change camera height.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_InvalidRes":
                            MessageBox.Show("This resolution is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_MissingFiles_CouldNotChangeCamHeight":
                            MessageBox.Show("Could not change camera height because of missing files!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "I_WeakCPU":
                            MessageBox.Show("We have detected that your CPU's single-core performance is not good enough for the most demanding graphical effects." +
                                "\n\nNote: C&C Generals runs on an old game engine which does not benefit from multiple CPU cores/threads." +
                                "\n\nWe have set 3D Shadows and Water Effects OFF for optimal performance." +
                                "\nWe recommend to keep them off for the best experience.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_WelcomeMessage":
                            MessageBox.Show("Welcome to Contra X Beta! " +
                                "Since this is a Beta version, it is not fully polished and you may encounter content that does not match quality standards. " +
                                "We are continuously working towards improving Contra in all aspects. You can find any information about Contra on our Discord. " +
                                "We hope you will appreciate and enjoy the changes we have made in this version so far!", "Welcome!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "W_WBCouldNotUnloadMod":
                            MessageBox.Show("Mod files could not be unloaded since they are currently in use by World Builder. " +
                            "If you want to unload mod files, close World Builder and run the launcher again. Closing the launcher anyway.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_NotFound_ContraOnDesktop":
                            MessageBox.Show("Contra Launcher could not find Contra installed on your desktop. " +
                                "Locate the game folder where you installed Contra and make a shortcut (not a copy) " +
                                "of Contra Launcher instead.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_NotFound_Contra009Final":
                            MessageBox.Show("\"!Contra009Final.ctr\" is missing!" +
                                "\nPlease, install 009 Final first, or the mod will not start!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_ChangesAfterGameRestart":
                            MessageBox.Show("Changes will take effect after game restart.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_BlackScreen":
                            MessageBox.Show("Heat Effects are known to cause black screen in the game on certain systems. " +
                                "If you get this problem, turn this setting off.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }
                    break;
                case "RU":
                    switch (type)
                    {
                        case "E_NotFound_OptionsIni":
                            MessageBox.Show("Файл \"Options.ini\" не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_WB":
                            MessageBox.Show("World Builder не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_GameDataP3":
                            MessageBox.Show("Файл \"!ContraXBeta_GameData.big\" не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP3":
                            MessageBox.Show("Пожалуйста, закройте !ContraXBeta_GameData.big, чтобы изменить высоту камеры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_InvalidRes":
                            MessageBox.Show("Это разрешение экрана не является действительным.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_MissingFiles_CouldNotChangeCamHeight":
                            MessageBox.Show("Не удалось изменить высоту камеры из-за отсутствия файлов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "I_WeakCPU":
                            MessageBox.Show("Мы обнаружили, что одноядерная производительность вашего процессора недостаточна для самых требовательных графических эффектов." +
                                "\n\nПримечание: C&C Generals работает на старом игровом движке, который не использует несколько ядер/потоков ЦП." +
                                "\n\nМы отключили 3D-тени и водные эффекты для оптимальной производительности." +
                                "\nМы рекомендуем держать их выключенными.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_WelcomeMessage":
                            MessageBox.Show("Добро пожаловать в бета-версию Contra X! " +
                                "Поскольку это бета-версия, она не полностью завершена, и вы можете столкнуться с контентом, не соответствующим стандартам качества. " +
                                "Мы постоянно работаем над улучшением Contra во всех аспектах. Вы можете найти любую информацию о Contra в нашем Discord. " +
                                "Мы надеемся, что вы оцените и насладитесь изменениями, которые мы сделали в этой версии!", "Добро пожаловать!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "W_WBCouldNotUnloadMod":
                            MessageBox.Show("Файлы мода не могут быть выгружены, так как они в настоящее время используются World Builder. " +
                                "Если вы хотите выгрузить файлы мода, закройте World Builder и снова запустите лаунчер. Закрытие лаунчера в любом случае.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_NotFound_ContraOnDesktop":
                            MessageBox.Show("Contra Launcher не может найти установленная Contra на вашем рабочем столе. " +
                                "Найдите папку с игрой, в которую вы установили Contra, и вместо этого сделайте ярлык (не копию) " +
                                "Contra Launcher.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_NotFound_Contra009Final":
                            MessageBox.Show("\"!Contra009Final.ctr\" отсутствует!" +
                                "\nПожалуйста, сначала установите 009 Final, или мод не запустится!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_ChangesAfterGameRestart":
                            MessageBox.Show("Изменения вступят в силу после перезапуска игры.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_BlackScreen":
                            MessageBox.Show("Известно, что тепловые эффекты вызывают черный экран в игре на некоторых системах. " +
                                "Если у вас возникла эта проблема, отключите этот параметр.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }
                    break;
                case "UA":
                    switch (type)
                    {
                        case "E_NotFound_OptionsIni":
                            MessageBox.Show("Файл Options.ini не знайдений!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_WB":
                            MessageBox.Show("World Builder не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_GameDataP3":
                            MessageBox.Show("Файл \"!ContraXBeta_GameData.big\" не знайдений!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP3":
                            MessageBox.Show("Будь ласка, закрийте !ContraXBeta_GameData.big, щоб змінити висоту камери.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_InvalidRes":
                            MessageBox.Show("Це розширення не є дійсним.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_MissingFiles_CouldNotChangeCamHeight":
                            MessageBox.Show("Не вдалося змінити висоту камери через відсутні файли!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "I_WeakCPU":
                            MessageBox.Show("Ми виявили, що одноядерна продуктивність вашого ЦП недостатня для найвимогливіших графічних ефектів." +
                                "\n\nПримітка: C&C Generals працює на старому ігровому движку, який не має переваг від кількох ядер/потоків ЦП." +
                                "\n\nМи вимкнули тривимірні тіні та водяні ефекти для оптимальної роботи." +
                                "\nМи рекомендуємо тримати їх вимкненими.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_WelcomeMessage":
                            MessageBox.Show("Ласкаво просимо до Contra X Beta! " +
                                "Оскільки це бета-версія, вона не повністю завершена, і ви можете натрапити на вміст, який не відповідає стандартам якості. " +
                                "Ми постійно працюємо над покращенням Contra в усіх аспектах. Ви можете знайти будь-яку інформацію про Contra на нашому Discord. " +
                                "Ми сподіваємося, що ви оціните та насолодитеся змінами, які ми внесли в цю версію досі!", "Ласкаво просимо!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "W_WBCouldNotUnloadMod":
                            MessageBox.Show("Файли моду не могли бути розвантажені, оскільки вони в даний час використовуються World Builder. " +
                                "Якщо ви хочете завантажити файли моду, закрийте World Builder і знову запустіть лаунчер. Закриття лаунчера в будь-якому випадку.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_NotFound_ContraOnDesktop":
                            MessageBox.Show("Contra Launcher не може знайти встановлена Contra на вашому робочому столі. " +
                                "Знайдіть папку з грою, в яку ви встановили Contra, і замість цього зробіть ярлик (не копію) " +
                                "Contra Launcher.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_NotFound_Contra009Final":
                            MessageBox.Show("\"!Contra009Final.ctr\" відсутня!" +
                                "\nБудь ласка, спочатку встановіть 009 Final, або мод не запуститься!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_ChangesAfterGameRestart":
                            MessageBox.Show("Зміни набудуть чинності після перезапуску гри.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_BlackScreen":
                            MessageBox.Show("Відомо, що теплові ефекти викликають чорний екран у грі на певних системах. " +
                                "Якщо у вас виникла ця проблема, вимкніть це налаштування.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }
                    break;
                case "BG":
                    switch (type)
                    {
                        case "E_NotFound_OptionsIni":
                            MessageBox.Show("Options.ini не беше намерен!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_WB":
                            MessageBox.Show("World Builder не бе намерен.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_GameDataP3":
                            MessageBox.Show("Файлът \"!ContraXBeta_GameData.big\" не беше намерен!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP3":
                            MessageBox.Show("Моля, затворете !ContraXBeta_GameData.big, за да промените височината на камерата.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_InvalidRes":
                            MessageBox.Show("Тази резолюция не е валидна.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_MissingFiles_CouldNotChangeCamHeight":
                            MessageBox.Show("Височината на камерата не можа да се промени поради липсващи файлове!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "I_WeakCPU":
                            MessageBox.Show("Установихме, че едноядрената производителност на вашия процесор не е достатъчно добра за някои графични ефекти." +
                                "\n\nЗабележка: C&C Generals работи на стар двигател (game engine), който не се възползва от множество процесорни ядра/нишки." +
                                "\n\nИзключили сме 3D сенки и водни ефекти за оптимална производителност." +
                                "\nПрепоръчваме да ги оставите изключени.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_WelcomeMessage":
                            MessageBox.Show("Добре дошли в Contra X Beta! " +
                                "Тъй като това е бета версия, тя не е напълно доработена и може да срещнете съдържание, което не отговаря на стандартите за качество. " +
                                "Ние непрекъснато работим за подобряване на Contra във всички аспекти. Можете да намерите всякаква информация за Contra в нашия Discord. " +
                                "Надяваме се, че ще оцените и ще се насладите на промените, които успяхме да направим в тази версия!", "Добре дошли!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "W_WBCouldNotUnloadMod":
                            MessageBox.Show("Contra файловете не можаха да бъдат деактивирани, тъй като се използват от World Builder. " +
                                "Ако искате да деактивирате Contra, затворете World Builder и стартирайте launcher-а отново.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_NotFound_ContraOnDesktop":
                            MessageBox.Show("Contra Launcher не можа да открие инсталирана Contra на вашия работен плот. " +
                                "Намерете директорията на играта, където сте инсталирали Contra и създайте пряк път (не копие) " +
                                "на Contra Launcher вместо това.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_NotFound_Contra009Final":
                            MessageBox.Show("\"!Contra009Final.ctr\" не беше намерен!" +
                                "\nМоля, първо инсталирайте 009 Final, или модът няма да стартира!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_ChangesAfterGameRestart":
                            MessageBox.Show("Промените ще влязат в сила след рестартиране на играта.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_BlackScreen":
                            MessageBox.Show("Известно е, че топлинните ефекти причиняват черен екран в играта на определени системи. " +
                                "Ако имате този проблем, изключете тази настройка.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }
                    break;
                case "DE":
                    switch (type)
                    {
                        case "E_NotFound_OptionsIni":
                            MessageBox.Show("Options.ini nicht gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_WB":
                            MessageBox.Show("World Builder wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_GameDataP3":
                            MessageBox.Show("\"!ContraXBeta_GameData.big\" nicht gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP3":
                            MessageBox.Show("Bitte schließen Sie !ContraXBeta_GameData.big, um die Kamerahöhe zu ändern.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_InvalidRes":
                            MessageBox.Show("Diese Auflцsung ist nicht gьltig.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_MissingFiles_CouldNotChangeCamHeight":
                            MessageBox.Show("Die Kamerahöhe konnte wegen fehlender Dateien nicht geändert werden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "I_WeakCPU":
                            MessageBox.Show("Wir haben festgestellt, dass die Single-Core-Leistung Ihrer CPU für die anspruchsvollsten Grafikeffekte nicht gut genug ist." +
                                "\n\nHinweis: C&C Generals läuft auf einer alten Spiel-Engine, die nicht von mehreren CPU-Kernen/Threads profitiert." +
                                "\n\nWir haben 3D-Schatten und Wassereffekte für eine optimale Leistung deaktiviert." +
                                "\nWir empfehlen, sie für das beste Erlebnis fernzuhalten.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_WelcomeMessage":
                            MessageBox.Show("Willkommen zur Contra X Beta! " +
                                "Da es sich um eine Beta-Version handelt, ist sie nicht vollständig ausgefeilt und es kann sein, dass Sie auf Inhalte stoßen, " +
                                "die nicht den Qualitätsstandards entsprechen. Wir arbeiten kontinuierlich daran, Contra in allen Aspekten zu verbessern. " +
                                "Alle Informationen zu Contra finden Sie auf unserem Discord. Wir hoffen, dass Sie die Änderungen, die wir bisher in dieser " +
                                "Version vorgenommen haben, zu schätzen wissen und genießen werden!", "Willkommen!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "W_WBCouldNotUnloadMod":
                            MessageBox.Show("Mod dateien konnten nicht entladen werden, da sie momentan im World Builder benutzt werden. " +
                                "Falls du die mod dateien entladen wilst, schlieЯe den World Builder und starte den Launcher erneut. SchlieЯt den Launcher sowieso.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_NotFound_ContraOnDesktop":
                            MessageBox.Show("Contra Launcher konnte Contra nicht auf Ihrem Desktop installiert finden. " +
                                "Suchen Sie den Spielordner, in dem Sie Contra installiert haben, und erstellen Sie stattdessen eine Verknüpfung (keine Kopie) " +
                                "von Contra Launcher.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_NotFound_Contra009Final":
                            MessageBox.Show("\"!Contra009Final.ctr\" wird vermisst!" +
                                "\nBitte installieren Sie zuerst 009 Final, oder der mod startet nicht!", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_ChangesAfterGameRestart":
                            MessageBox.Show("Änderungen werden nach dem Neustart des Spiels wirksam.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case "W_BlackScreen":
                            MessageBox.Show("Es ist bekannt, dass Hitzeeffekte auf bestimmten Systemen einen schwarzen Bildschirm im Spiel verursachen. " +
                                "Wenn dieses Problem auftritt, schalten Sie diese Einstellung aus.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }
                    break;
            }
        }

        public static string GenerateMessage(string type, string lang)
        {
            switch (lang)
            {
                case "EN":
                    switch (type)
                    {
                        case "Error":
                            return "Error";

                        case "Warning":
                            return "Warning";

                        case "Information":
                            return "Information";

                        case "CameraHeightString":
                            return "Camera Height: ";

                        case "ParticleCap":
                            return "Max Particle Count: ";

                        case "TextureRes":
                            return "Texture Resolution: ";

                        case "Low":
                            return "Low";

                        case "Medium":
                            return "Medium";

                        case "High":
                            return "High";

                        case "PerformanceEffectNone":
                            return "Effect on performance: None";

                        case "PerformanceEffectLow":
                            return "Effect on performance: Low";

                        case "PerformanceEffectMedium":
                            return "Effect on performance: Medium";

                        case "PerformanceEffectHigh":
                            return "Effect on performance: High";

                        case "CameraHeight":
                            return "CAMERA HEIGHT";

                        case "CameraHeightDescription":
                            return "Changes the default and maximum player" +
                                "\nview distance in-game.";

                        case "TextureResTwo":
                            return "TEXTURE RESOLUTION";

                        case "TextureResDescription":
                            return "Changes the texture resolution.";

                        case "ParticleCapTwo":
                            return "PARTICLE CAP";

                        case "ParticleCapDescription":
                            return "Changes the particle effects limit.";

                        case "Shadows3DDescription":
                            return "Shows 3D shadows projected" +
                               "\nby world objects.";

                        case "Shadows2DDescription":
                            return "Shows 2D shadows and area decals.";

                        case "CloudShadowsDescription":
                            return "Shows moving cloud shadows.";

                        case "ExtraGroundLightingDescription":
                            return "Shows more detailed ground lighting.";

                        case "SmoothWaterBordersDescription":
                            return "Smoothens water borders along the shores.";

                        case "BehindBuildingsDescription":
                            return "Shows the silhouettes of units" +
                                "\nbehind buildings.";

                        case "ShowPropsDescription":
                            return "Shows props.";

                        case "ExtraAnimationsDescription":
                            return "Shows additional animations" +
                               "\nsuch as tree sway.";

                        case "DisableDynamicLODDescription":
                            return "Disables automatic detail adjustment." +
                                "\nUncheck for better performance.";

                        case "HeatEffectsDescription":
                            return "Distorts the space around" +
                                "\nhot particle effects.";

                        case "FogDescription":
                            return "Adds a gradient layer on the screen" +
                                "\ndepending on the map conditions.";

                        case "WaterEffectsDescription":
                            return "Simulates realistic sun and cloud" +
                                 "\nreflections on water.";

                        case "LangFilterDescription":
                            return "Hides bad words in chat.";

                        case "AnisoDescription":
                            return "Improves texture quality of surfaces" +
                                "\nwhen viewed at an angle.";

                        case "ExtraBuildingPropsDescription":
                            return "Shows extra building props" +
                                "\nunique per each general.";

                        case "ControlBarPro":
                            return "CONTROL BAR PRO";

                        case "ControlBarProDescription":
                            return "Control Bar Pro by FAS and xezon." +
                                "\nRecommended for 16:9" +
                                "\naspect ratio displays.";

                        case "ControlBarContra":
                            return "CONTROL BAR CONTRA";

                        case "ControlBarContraDescription":
                            return "Contra's own control bar from past versions." +
                                "\nRecommended for 4:3" +
                                "\naspect ratio displays.";

                        case "ControlBarStandard":
                            return "CONTROL BAR STANDARD";

                        case "ControlBarStandardDescription":
                            return "Standard Zero Hour control bar." +
                                "\nRecommended for 4:3" +
                                "\naspect ratio displays.";

                        case "IconQualityDouble":
                            return "DOUBLE ICON QUALITY";

                        case "IconQualityDoubleDescription":
                            return "Small buttons will use larger" +
                                "\nicon/cameo resolution. Best" +
                                "\nlooking on large displays.";

                        case "IconQualityStandard":
                            return "STANDARD ICON QUALITY";

                        case "IconQualityStandardDescription":
                            return "Small buttons will use standard" +
                                "\nicon/cameo resolution. Best" +
                                "\nlooking on small displays.";

                        case "HotkeysLeikeze":
                            return "LEIKEZE HOTKEYS";

                        case "HotkeysLeikezeDescription":
                            return "Use hotkeys by Leikeze.";

                        case "HotkeysStandard":
                            return "STANDARD HOTKEYS";

                        case "HotkeysStandardDescription":
                            return "Use standard hotkeys.";

                        case "E_NotFound_DLLs":
                            return "The game cannot start because the \"binkw32.dll\" and/or \"mss32.dll\" file(s) were not found " +
                                "in the current directory. Contra must be installed in Zero Hour's main game folder." +
                                "\n\nWould you like to visit a page with installation instructions?";

                        case "E_NotFound_GeneralsEXE":
                            return "The \"generals.exe\" file was not found. " +
                                "Make sure you have Zero Hour installed, and Contra is installed into Zero Hour's main folder." +
                                "\n\nWould you like to visit a page with installation instructions?";

                        case "E_GeneralsAlreadyRunningButNotResponding":
                            return "Generals.exe is already running but not responding." +
                                "\n\nWould you like the Launcher to kill the process and then run the game?";

                        case "W_NotFound_GeneralsCTR":
                            return "\"generals.ctr\" not found or checksum mismatch! " +
                                "Please, extract it from the Contra X Beta archive if you want " +
                                "camera height setting to work and to see players in network lobby." +
                                "\n\nWould you like to start the game anyway?";

                        case "W_FoundIniFiles":
                            return "Found .ini files in the \"Data\\INI\" directory. They may corrupt the mod or cause mismatch online." +
                                "\n\nWould you like to visit this folder now?";

                        case "W_FoundW3DFiles":
                            return "Found .W3D files in the \"Art\\W3D\" directory. They may corrupt the mod or cause mismatch online." +
                                "\n\nWould you like to visit this folder now?";

                        case "W_FoundWndFiles":
                            return "Found .wnd files in the \"Window\" directory. They may corrupt the mod." +
                                "\n\nWould you like to visit this folder now?";

                        case "W_FoundStrFiles":
                            return "Found .str file(s) in the \"Data\" directory. They may corrupt the mod's text strings." +
                                "\n\nWould you like to visit this folder now?";

                        case "W_FoundCsfFiles":
                            return "Found .csf file(s) in the \"Data\\English\" directory. They may corrupt the mod's text strings." +
                                "\n\nWould you like to visit this folder now?";

                        case "W_FoundIniFilesInEnglishFolder":
                            return "Found .ini file(s) in the \"Data\\English\" directory. They may corrupt the mod." +
                                "\n\nWould you like to visit this folder now?";

                        case "W_PreferencesMayNotLoad":
                            return "Voice, language and hotkey preferences may not load correctly since World Builder is running." +
                                "\n\nWould you like to start the game anyway?";

                        case "W_GenToolNotInstalledOrNotUpToDate":
                            return "GenTool is not installed or not up to date. That means you will have a black line behind the Control Bar Pro." +
                                "\n\nWould you like to visit https://www.gentool.net/ and install the latest version?" +
                                "\nIf not, we recommend you to select a different control bar for its proper display.";

                        case "I_GeneralsAlreadyRunning":
                            return "Generals.exe is already running.";
                    }
                    break;
                case "RU":
                    switch (type)
                    {
                        case "Error":
                            return "Ошибка";

                        case "Warning":
                            return "Предупреждение";

                        case "Information":
                            return "Информация";

                        case "CameraHeightString":
                            return "Высота камеры: ";

                        case "ParticleCap":
                            return "Количество частиц: ";

                        case "TextureRes":
                            return "Разр. текстуры: ";

                        case "Low":
                            return "Низкое";

                        case "Medium":
                            return "Среднее";

                        case "High":
                            return "Высокое";

                        case "PerformanceEffectNone":
                            return "Влияние на производительность: Нет";

                        case "PerformanceEffectLow":
                            return "Влияние на производительность: Слабое";

                        case "PerformanceEffectMedium":
                            return "Влияние на производительность: Средное";

                        case "PerformanceEffectHigh":
                            return "Влияние на производительность: Сильное";

                        case "CameraHeight":
                            return "ВЫСОТА КАМЕРЫ";

                        case "CameraHeightDescription":
                            return "Изменяет стандартное и максимальное" +
                                "\nрасстояние поле зрения игрока.";

                        case "TextureResTwo":
                            return "РАЗРЕШЕНИЕ ТЕКСТУРЫ";

                        case "TextureResDescription":
                            return "Изменяет разрешение текстуры.";

                        case "ParticleCapTwo":
                            return "ПОДСЧЕТ ЧАСТИЦ";

                        case "ParticleCapDescription":
                            return "Изменяет лимит эффектов частиц.";

                        case "Shadows3DDescription":
                            return "Показывает 3D-тени.";

                        case "Shadows2DDescription":
                            return "Показывает 2D-тени и метки областей.";

                        case "CloudShadowsDescription":
                            return "Показывает движущиеся тени облаков.";

                        case "ExtraGroundLightingDescription":
                            return "Показывает более детальное" +
                                "\nосвещение земли.";

                        case "SmoothWaterBordersDescription":
                            return "Сглаживает границы воды.";

                        case "BehindBuildingsDescription":
                            return "Показывает силуэты юнитов за зданиями.";

                        case "ShowPropsDescription":
                            return "Показывает мелкие предметы.";

                        case "ExtraAnimationsDescription":
                            return "Показывает дополнительные анимации," +
                                "\nтакие как раскачивание дерева.";

                        case "DisableDynamicLODDescription":
                            return "Отключает автоматическую настройку" +
                                "\nдеталей. Отмените выбор для" +
                                "\nповышения производительности.";

                        case "HeatEffectsDescription":
                            return "Искажает пространство вокруг" +
                                "\nэффектов горячих частиц.";

                        case "FogDescription":
                            return "Добавляет градиентный слой на экран" +
                                "\nв зависимости от условий карты.";

                        case "WaterEffectsDescription":
                            return "Имитирует реалистичные отражения" +
                                "\nсолнца и облаков на воде.";

                        case "LangFilterDescription":
                            return "Скрывает плохие слова в чате.";

                        case "AnisoDescription":
                            return "Улучшает качество текстур" +
                                "\nповерхностей при просмотре под углом.";

                        case "ExtraBuildingPropsDescription":
                            return "Показывает дополнит. строительные" +
                                "\nрекв., уникальные для каждого генерала.";

                        case "ControlBarPro":
                            return "CONTROL BAR PRO";

                        case "ControlBarProDescription":
                            return "Панель управления Pro от FAS и xezon." +
                                "\nРекомендуется для дисплеев" +
                                "\nс соотношением сторон 16:9.";

                        case "ControlBarContra":
                            return "CONTROL BAR CONTRA";

                        case "ControlBarContraDescription":
                            return "Панель управления Contra из прошлых" +
                                "\nверсий. Рекомендуется для дисплеев" +
                                "\nс соотношением сторон 4:3.";

                        case "ControlBarStandard":
                            return "CONTROL BAR STANDARD";

                        case "ControlBarStandardDescription":
                            return "Стандартная панель управления Zero" +
                                "\nHour. Рекомендуется для дисплеев" +
                                "\nс соотношением сторон 4:3.";

                        case "IconQualityDouble":
                            return "ДВОЙНОЕ КАЧЕСТВО ИКОН";

                        case "IconQualityDoubleDescription":
                            return "Маленькие кнопки будут использовать" +
                                "\nбольшее разрешение значков." +
                                "\nЛучше смотрится на больших дисплеях.";

                        case "IconQualityStandard":
                            return "СТАНДАРТНОЕ КАЧЕСТВО ИКОН";

                        case "IconQualityStandardDescription":
                            return "Маленькие кнопки будут использовать" +
                                "\nстандартное разрешение значков." +
                                "\nЛучше смотрится на маленьких дисплеях.";

                        case "HotkeysLeikeze":
                            return "ГОРЯЧИЕ КЛАВИШИ LEIKEZE";

                        case "HotkeysLeikezeDescription":
                            return "Используйте горячие клавиши от Leikeze.";

                        case "HotkeysStandard":
                            return "СТАНДАРТНЫЕ ГОРЯЧИЕ КЛАВИШИ";

                        case "HotkeysStandardDescription":
                            return "Используйте стандартные горячие" +
                                "\nклавиши.";

                        case "E_NotFound_DLLs":
                            return "Игра не может запуститься, потому что файлы \"binkw32.dll\" и/или \"mss32.dll\" не найдены " +
                                "в текущей папке. Contra должна быть установлена в основную папку игры Zero Hour." +
                                "\n\nХотите посетить страницу с инструкциями по установке?";

                        case "E_NotFound_GeneralsEXE":
                            return "Файл \"generals.exe\" не найден. " +
                                "Убедитесь, что у вас установлена Zero Hour, а Contra установлен в основную папку Zero Hour." +
                                "\n\nХотите посетить страницу с инструкциями по установке?";

                        case "E_GeneralsAlreadyRunningButNotResponding":
                            return "Generals.exe уже запущен, но не отвечает." +
                                "\n\nВы хотите, чтобы Launcher остановил процесс, а затем запустил игру?";

                        case "W_NotFound_GeneralsCTR":
                            return "\"generals.ctr\" не найден или несоответствие контрольной суммы! " +
                                "Извлеките его из архива Contra X Beta, если вы хотите, " +
                                "чтобы настройка высоты камеры работала и чтобы видеть игроков в сетевом лобби." +
                                "\n\nХотели бы вы начать игру?";

                        case "W_FoundIniFiles":
                            return "Найдены файлы .ini в каталоге \"Data\\INI\". Они могут повредить мод или вызвать несоответствие в сети." +
                                "\n\nХотите посетить эту папку сейчас?";

                        case "W_FoundW3DFiles":
                            return "Найдены файлы .W3D в каталоге \"Art\\W3D\". Они могут повредить мод или вызвать несоответствие в сети." +
                                "\n\nХотите посетить эту папку сейчас?";

                        case "W_FoundWndFiles":
                            return "Найдены файлы .wnd в каталоге \"Window\". Они могут повредить мод." +
                                "\n\nХотите посетить эту папку сейчас?";

                        case "W_FoundStrFiles":
                            return "Найдены файлы .str в каталоге \"Data\". Они могут повредить текстовые строки мода." +
                                "\n\nХотите посетить эту папку сейчас?";

                        case "W_FoundCsfFiles":
                            return "Найдены файлы .csf в каталоге \"Data\\English\". Они могут повредить текстовые строки мода." +
                                "\n\nХотите посетить эту папку сейчас?";

                        case "W_FoundIniFilesInEnglishFolder":
                            return "Найдены файлы .ini в каталоге \"Data\\English\". Они могут повредить мод." +
                                "\n\nХотите посетить эту папку сейчас?";

                        case "W_PreferencesMayNotLoad":
                            return "Настройки голоса, языка и горячих клавиш могут загружаться неправильно, так как World Builder запущен." +
                                "\n\nВы все равно хотите начать игру?";

                        case "W_GenToolNotInstalledOrNotUpToDate":
                            return "GenTool не установлен или не обновлен. Это означает, что вы увидите черную линию за панелью управления Pro." +
                                "\n\nХотите посетить https://www.gentool.net/ и установить последнюю версию?" +
                                "\nЕсли нет, мы рекомендуем вам выбрать другую панель управления для ее правильного отображения.";

                        case "I_GeneralsAlreadyRunning":
                            return "Generals.exe уже запущен.";
                    }
                    break;
                case "UA":
                    switch (type)
                    {
                        case "Error":
                            return "Помилка";

                        case "Warning":
                            return "Попередження";

                        case "Information":
                            return "Інформація";

                        case "CameraHeightString":
                            return "Висота камери: ";

                        case "ParticleCap":
                            return "Кількість частинок: ";

                        case "TextureRes":
                            return "Розр. текстури: ";

                        case "Low":
                            return "Низьке";

                        case "Medium":
                            return "Середнє";

                        case "High":
                            return "Висока";

                        case "PerformanceEffectNone":
                            return "Вплив на продуктивність: Немає";

                        case "PerformanceEffectLow":
                            return "Вплив на продуктивність: Низький";

                        case "PerformanceEffectMedium":
                            return "Вплив на продуктивність: Середнє";

                        case "PerformanceEffectHigh":
                            return "Вплив на продуктивність: Високий";

                        case "CameraHeight":
                            return "ВИСОТА КАМЕРИ";

                        case "CameraHeightDescription":
                            return "Змінює стандартну та максимальну" +
                                "\nвідстань огляду гравця в грі.";

                        case "TextureResTwo":
                            return "РОЗРІШЕННЯ ТЕКСТУРИ";

                        case "TextureResDescription":
                            return "Змінює роздільну здатність текстури.";

                        case "ParticleCapTwo":
                            return "КІЛЬКІСТЬ ЧАСТИНОК";

                        case "ParticleCapDescription":
                            return "Змінює обмеження ефектів частинок.";

                        case "Shadows3DDescription":
                            return "Показує тривимірні тіні.";

                        case "Shadows2DDescription":
                            return "Показує 2D тіні та наклейки області.";

                        case "CloudShadowsDescription":
                            return "Показує рухомі тіні хмар.";

                        case "ExtraGroundLightingDescription":
                            return "Показує більш детальне освітлення землі.";

                        case "SmoothWaterBordersDescription":
                            return "Згладжує межі води.";

                        case "BehindBuildingsDescription":
                            return "Показує силуети підрозділів за будівлями.";

                        case "ShowPropsDescription":
                            return "Показує дрібні предмети.";

                        case "ExtraAnimationsDescription":
                            return "Показує додаткові анімації," +
                                "\nнаприклад коливання дерев.";

                        case "DisableDynamicLODDescription":
                            return "Вимикає автоматичне nналаштування" +
                                "\nдеталей. Зніміть вибір для nкращої" +
                                "\nпродуктивності.";

                        case "HeatEffectsDescription":
                            return "Спотворює простір навколо ефектів" +
                                "\nгарячих частинок.";

                        case "FogDescription":
                            return "Додає градієнтний шар на екран" +
                                "\nзалежно від умов карти.";

                        case "WaterEffectsDescription":
                            return "Імітує реалістичне відображення" +
                                "\nсонця та хмар на воді.";

                        case "LangFilterDescription":
                            return "Приховує погані слова в чаті.";

                        case "AnisoDescription":
                            return "Покращує якість текстури поверхонь" +
                                "\nпри погляді під кутом.";

                        case "ExtraBuildingPropsDescription":
                            return "Показує додатковий будівельний реквізит," +
                                "\nунікальний для кожного генерала.";

                        case "ControlBarPro":
                            return "CONTROL BAR PRO";

                        case "ControlBarProDescription":
                            return "Control Bar Pro від FAS і xezon." +
                                "\nРекомендовано для дисплеїв зі" +
                                "\nспіввідношенням сторін 16:9.";

                        case "ControlBarContra":
                            return "CONTROL BAR CONTRA";

                        case "ControlBarContraDescription":
                            return "Панель керування Contra з попередніх" +
                                "\nверсій. Рекомендовано для дисплеїв зі" +
                                "\nспіввідношенням сторін 4:3.";

                        case "ControlBarStandard":
                            return "CONTROL BAR STANDARD";

                        case "ControlBarStandardDescription":
                            return "Стандартна панель керування Zero Hour." +
                                "\nРекомендовано для дисплеїв зі" +
                                "\nспіввідношенням сторін 4:3.";

                        case "IconQualityDouble":
                            return "ПІДВІЙНА ЯКІСТЬ ІКОНОК";

                        case "IconQualityDoubleDescription":
                            return "Маленькі кнопки використовуватимуть" +
                                "\nбільшу роздільну здатність значків." +
                                "\nНайкраще виглядає на великих" +
                                "\nдисплеях.";

                        case "IconQualityStandard":
                            return "СТАНДАРТНА ЯКІСТЬ ІКОНОК";

                        case "IconQualityStandardDescription":
                            return "Маленькі кнопки використовуватимуть" +
                                "\nстандартну роздільну здатність" +
                                "\nзначків. Найкраще виглядає на" +
                                "\nневеликих дисплеях.";

                        case "HotkeysLeikeze":
                            return "ГАРЯЧІ КЛАВІШІ LEIKEZE";

                        case "HotkeysLeikezeDescription":
                            return "Використовуйте гарячі клавіші від Leikeze.";

                        case "HotkeysStandard":
                            return "СТАНДАРТНІ ГАРЯЧІ КЛАВІШІ";

                        case "HotkeysStandardDescription":
                            return "Використовуйте стандартні гарячі клавіші.";

                        case "E_NotFound_DLLs":
                            return "Гра не може запуститися, оскільки файли \"binkw32.dll\" та/або \"mss32.dll\" не знайдені " +
                                "у поточній папці. Contra має бути встановленa в головній папці гри Zero Hour." +
                                "\n\nБажаєте відвідати сторінку з інструкціями щодо встановлення?";

                        case "E_NotFound_GeneralsEXE":
                            return "Файл \"generals.exe\" не знайдено. " +
                                "Переконайтеся, що у вас встановлена Zero Hour, а Contra встановлений в головну папку Zero Hour." +
                                "\n\nБажаєте відвідати сторінку з інструкціями щодо встановлення?";

                        case "E_GeneralsAlreadyRunningButNotResponding":
                            return "Generals.exe вже запущено, але не відповідає." +
                                "\n\nЧи бажаєте ви, щоб програма запуску зупинила процес, а потім запустила гру?";

                        case "W_NotFound_GeneralsCTR":
                            return "\"generals.ctr\" не знайден або невідповідність контрольної суми! " +
                                "Будь ласка, витягніть його з архіву Contra X Beta, якщо ви хочете, " +
                                "щоб налаштування висоти камери працювало і бачити гравців у лобі мережі." +
                                "\n\nВи хочете все-таки почати гру?";

                        case "W_FoundIniFiles":
                            return "Знайдено файли .ini в каталозі \"Data\\INI\". Вони можуть пошкодити мод або призвести до невідповідності в Інтернеті." +
                                "\n\nБажаєте відвідати цю папку зараз?";

                        case "W_FoundW3DFiles":
                            return "Знайдено файли .W3D в каталозі \"Art\\W3D\". Вони можуть пошкодити мод або призвести до невідповідності в Інтернеті." +
                                "\n\nБажаєте відвідати цю папку зараз?";

                        case "W_FoundWndFiles":
                            return "Знайдено файли .wnd в каталозі \"Window\". Вони можуть пошкодити мод." +
                                "\n\nБажаєте відвідати цю папку зараз?";

                        case "W_FoundStrFiles":
                            return "Знайдено файли .str в каталозі \"Data\". Вони можуть пошкодити текст мода." +
                                "\n\nБажаєте відвідати цю папку зараз?";

                        case "W_FoundCsfFiles":
                            return "Знайдено файли .csf в каталозі \"Data\\English\". Вони можуть пошкодити текст мода." +
                                "\n\nБажаєте відвідати цю папку зараз?";

                        case "W_FoundIniFilesInEnglishFolder":
                            return "Знайдено файли .ini в каталозі \"Data\\English\". Вони можуть пошкодити мод." +
                                "\n\nБажаєте відвідати цю папку зараз?";

                        case "W_PreferencesMayNotLoad":
                            return "Налаштування голосу, мови та гарячих клавіш можуть завантажуватися неправильно, оскільки працює World Builder." +
                                "\n\nВи все-таки хотіли б почати гру?";

                        case "W_GenToolNotInstalledOrNotUpToDate":
                            return "GenTool не встановлено або не оновлено. Це означає, що ви побачите чорну лінію позаду Control Bar Pro." +
                                "\n\nБажаєте відвідати https://www.gentool.net/ і встановити останню версію?" +
                                "\nЯкщо ні, рекомендуємо вибрати іншу панель керування для її належного відображення.";

                        case "I_GeneralsAlreadyRunning":
                            return "Generals.exe вже працює.";
                    }
                    break;
                case "BG":
                    switch (type)
                    {
                        case "Error":
                            return "Грешка";

                        case "Warning":
                            return "Предупреждение";

                        case "Information":
                            return "Информация";

                        case "CameraHeightString":
                            return "Вис. на камерата: ";

                        case "ParticleCap":
                            return "Количество частици: ";

                        case "TextureRes":
                            return "Текстурна резол.: ";

                        case "Low":
                            return "Ниска";

                        case "Medium":
                            return "Средна";

                        case "High":
                            return "Висока";

                        case "PerformanceEffectNone":
                            return "Ефект върху производителността: Няма";

                        case "PerformanceEffectLow":
                            return "Ефект върху производителността: Нисък";

                        case "PerformanceEffectMedium":
                            return "Ефект върху производителността: Среден";

                        case "PerformanceEffectHigh":
                            return "Ефект върху производителността: Висок";

                        case "CameraHeight":
                            return "ВИСОЧИНА НА КАМЕРАТА";

                        case "CameraHeightDescription":
                            return "Променя стандартното и максималното" +
                                "\nразстояние на изглед на играча.";

                        case "TextureResTwo":
                            return "ТЕКСТУРНА РЕЗОЛЮЦИЯ";

                        case "TextureResDescription":
                            return "Променя резолюцията на текстурите.";

                        case "ParticleCapTwo":
                            return "БРОЙ ЧАСТИЦИ";

                        case "ParticleCapDescription":
                            return "Променя лимита на частиците.";

                        case "Shadows3DDescription":
                            return "Показва 3D сенки.";

                        case "Shadows2DDescription":
                            return "Показва 2D сенки и" +
                                "\nплощни стикери.";

                        case "CloudShadowsDescription":
                            return "Показва движещи се облачни сенки.";

                        case "ExtraGroundLightingDescription":
                            return "Показва по-подробно осветление" +
                                "\nна земята.";

                        case "SmoothWaterBordersDescription":
                            return "Изглажда водните граници.";

                        case "BehindBuildingsDescription":
                            return "Показва силуети на единиците" +
                                "\nпрез сградите.";

                        case "ShowPropsDescription":
                            return "Показва малки обекти.";

                        case "ExtraAnimationsDescription":
                            return "Показва допълнителни анимации," +
                                "\nкато люлеене на дървета.";

                        case "DisableDynamicLODDescription":
                            return "Деактивира автоматичната настройка" +
                                "\nна детайлите. Премахнете отметката за" +
                                "\nпо-добра производителност.";

                        case "HeatEffectsDescription":
                            return "Изкривява пространството около" +
                                "\nефектите на горещи частици.";

                        case "FogDescription":
                            return "Добавя градиентен слой на екрана" +
                                "\nв зависимост от условията на картата.";

                        case "WaterEffectsDescription":
                            return "Симулира реалистични отражения на" +
                                "\nслънцето и облаците върху водата.";

                        case "LangFilterDescription":
                            return "Скрива лошите думи в чата.";

                        case "AnisoDescription":
                            return "Подобрява качеството на текстурите," +
                                "\nкогато се наблюдават под ъгъл.";

                        case "ExtraBuildingPropsDescription":
                            return "Показва допълнителни сградни" +
                                "\nреквизити, уникални за всеки генерал.";

                        case "ControlBarPro":
                            return "CONTROL BAR PRO";

                        case "ControlBarProDescription":
                            return "Контролна лента Pro от FAS и xezon." +
                                "\nПрепоръчва се за дисплеи със" +
                                "\nсъотношение 16:9.";

                        case "ControlBarContra":
                            return "CONTROL BAR CONTRA";

                        case "ControlBarContraDescription":
                            return "Контролна лента Contra от пред. версии." +
                                "\nПрепоръчва се за дисплеи със" +
                                "\nсъотношение 4:3.";

                        case "ControlBarStandard":
                            return "CONTROL BAR STANDARD";

                        case "ControlBarStandardDescription":
                            return "Стандартната контр. лента от Zero Hour." +
                                "\nПрепоръчва се за дисплеи със" +
                                "\nсъотношение 4:3.";

                        case "IconQualityDouble":
                            return "ДВОЙНО КАЧЕСТВО НА ИКОНИТЕ";

                        case "IconQualityDoubleDescription":
                            return "Малките бутони ще ползват по-висока" +
                                "\nрезолюция на иконите. Препоръчва" +
                                "\nсе за големи дисплеи.";

                        case "IconQualityStandard":
                            return "СТАНДАРТНО КАЧЕСТВО НА ИКОНИТЕ";

                        case "IconQualityStandardDescription":
                            return "Малките бутони ще ползват стандартна" +
                                "\nрезолюция на иконите. Препоръчва" +
                                "\nсе за малки дисплеи.";

                        case "HotkeysLeikeze":
                            return "БЪРЗИ КЛАВИШИ LEIKEZE";

                        case "HotkeysLeikezeDescription":
                            return "Използвайте бързи клавиши от Leikeze.";

                        case "HotkeysStandard":
                            return "СТАНДАРТНИ БЪРЗИ КЛАВИШИ";

                        case "HotkeysStandardDescription":
                            return "Използвайте стандартни бързи клавиши.";

                        case "E_NotFound_DLLs":
                            return "Играта не може да се стартира, тъй като не са намерени файловете \"binkw32.dll\" и/или \"mss32.dll\" " +
                                "в текущата директория. Contra трябва да се инсталира в основната папка на Zero Hour." +
                                "\n\nИскате ли да посетите страница с инструкции за инсталиране?";

                        case "E_NotFound_GeneralsEXE":
                            return "Файлът \"generals.exe\" не беше намерен. " +
                                "Уверете се, че Zero Hour е инсталирана и Contra е инсталиран в основната й папка." +
                                "\n\nИскате ли да посетите страница с инструкции за инсталиране?";

                        case "E_GeneralsAlreadyRunningButNotResponding":
                            return "Generals.exe е вече стартиран, но не отговаря." +
                                "\n\nЖелаете ли Launcher-а да спре процеса и тогава да стартира играта?";

                        case "W_NotFound_GeneralsCTR":
                            return "\"generals.ctr\" не е намерен или има несъответствие в контролната сума! " +
                                "Моля, извлечете го от архива Contra X Beta, ако искате настройката " +
                                "на височината на камерата да работи и да виждате играчи в мрежовото лоби." +
                                "\n\nЖелаете ли да стартирате играта въпреки това?";

                        case "W_FoundIniFiles":
                            return "Намерени .ini файлове в директорията \"Data\\INI\". Те могат да повредят мода или да причинят несъответствие онлайн." +
                                "\n\nИскате ли да посетите тази папка сега?";

                        case "W_FoundW3DFiles":
                            return "Намерени .W3D файлове в директорията \"Art\\W3D\". Те могат да повредят мода или да причинят несъответствие онлайн." +
                                "\n\nИскате ли да посетите тази папка сега?";

                        case "W_FoundWndFiles":
                            return "Намерени .wnd файлове в директорията \"Window\". Те могат да повредят мода." +
                                "\n\nИскате ли да посетите тази папка сега?";

                        case "W_FoundStrFiles":
                            return "Намерени .str файлове в директорията \"Data\". Те могат да повредят текста в играта." +
                                "\n\nИскате ли да посетите тази папка сега?";

                        case "W_FoundCsfFiles":
                            return "Намерени .csf файлове в директорията \"Data\\English\". Те могат да повредят текста в играта." +
                                "\n\nИскате ли да посетите тази папка сега?";

                        case "W_FoundIniFilesInEnglishFolder":
                            return "Намерени .ini файлове в директорията \"Data\\English\". Те могат да повредят мода." +
                                "\n\nИскате ли да посетите тази папка сега?";

                        case "W_PreferencesMayNotLoad":
                            return "Предпочитанията за глас, език и горещи клавиши може да не се заредят правилно, тъй като World Builder е стартиран." +
                                "\n\nИскате ли да стартирате играта все пак?";

                        case "W_GenToolNotInstalledOrNotUpToDate":
                            return "GenTool не е инсталиран или не е обновен. Това означава, че ще виждате черна линия зад Control Bar Pro." +
                                "\n\nЖелаете ли да посетите https://www.gentool.net/ и да инсталирате последната версия?" +
                                "\nВ противен случай, препоръчваме Ви да изберете друга контролна лента за правилното й изображение.";

                        case "I_GeneralsAlreadyRunning":
                            return "Generals.exe е вече стартиран.";
                    }
                    break;
                case "DE":
                    switch (type)
                    {
                        case "Error":
                            return "Fehler";

                        case "Warning":
                            return "Warnung";

                        case "Information":
                            return "Information";

                        case "CameraHeightString":
                            return "Kamerahöhe: ";

                        case "ParticleCap":
                            return "Partikelzahl: ";

                        case "TextureRes":
                            return "Texturauflösung: ";

                        case "Low":
                            return "Niedrige";

                        case "Medium":
                            return "Mittlere";

                        case "High":
                            return "Hohe";

                        case "PerformanceEffectNone":
                            return "Auswirkung auf die Leistung: Keiner";

                        case "PerformanceEffectLow":
                            return "Auswirkung auf die Leistung: Geringe";

                        case "PerformanceEffectMedium":
                            return "Auswirkung auf die Leistung: Mittel";

                        case "PerformanceEffectHigh":
                            return "Auswirkung auf die Leistung: Hohe";

                        case "CameraHeight":
                            return "KAMERAHÖHE";

                        case "CameraHeightDescription":
                            return "Ändert die standardmäßige und maximale" +
                                "\nSichtweite des Spielers im Spiel.";

                        case "TextureResTwo":
                            return "TEXTURAUFLÖSUNG";

                        case "TextureResDescription":
                            return "Ändert die Texturauflösung.";

                        case "ParticleCapTwo":
                            return "PARTIKELZAHL";

                        case "ParticleCapDescription":
                            return "Ändert das Limit der Partikeleffekte.";

                        case "Shadows3DDescription":
                            return "Zeigt 3D-Schatten an.";

                        case "Shadows2DDescription":
                            return "Zeigt 2D-Schatten und" +
                                "\nFlächenabziehbilder an.";

                        case "CloudShadowsDescription":
                            return "Zeigt sich bewegende Wolkenschatten.";

                        case "ExtraGroundLightingDescription":
                            return "Zeigt eine detailliertere Bodenbeleuchtung.";

                        case "SmoothWaterBordersDescription":
                            return "Glättet Wasserränder.";

                        case "BehindBuildingsDescription":
                            return "Zeigt Einheiten hinter Gebäuden an.";

                        case "ShowPropsDescription":
                            return "Zeigt kleine Objekte.";

                        case "ExtraAnimationsDescription":
                            return "Zeigt zusätzliche Animationen" +
                                "\nwie Baumschwingen.";

                        case "DisableDynamicLODDescription":
                            return "Deaktiviert die automatische" +
                                "\nDetailanpassung. Deaktivieren Sie für" +
                                "\neine bessere Leistung.";

                        case "HeatEffectsDescription":
                            return "Verzerrt den Raum um" +
                                "\nheiße Partikeleffekte.";

                        case "FogDescription":
                            return "Fügt je nach Kartenbedingungen eine" +
                                "\nVerlaufsebene auf dem Bildschirm hinzu.";

                        case "WaterEffectsDescription":
                            return "Simuliert realistische Sonnen- und" +
                                "\nWolkenreflexionen auf dem Wasser.";

                        case "LangFilterDescription":
                            return "Versteckt böse Worte im Chat.";

                        case "AnisoDescription":
                            return "Verbessert die Texturqualität von" +
                                "\nOberflächen bei schräger Betrachtung.";

                        case "ExtraBuildingPropsDescription":
                            return "Zeigt zusätzliche Gebäudeobjekte an," +
                                "\ndie für jeden General einzigartig sind.";

                        case "ControlBarPro":
                            return "CONTROL BAR PRO";

                        case "ControlBarProDescription":
                            return "Control Bar Pro von FAS und xezon." +
                                "\nEmpfohlen für Displays mit einem" +
                                "\nSeitenverhältnis von 16:9.";

                        case "ControlBarContra":
                            return "CONTROL BAR CONTRA";

                        case "ControlBarContraDescription":
                            return "Contras Steuerleiste aus früheren" +
                                "\nVersionen. Empfohlen für Displays mit" +
                                "\neinem Seitenverhältnis von 4:3.";

                        case "ControlBarStandard":
                            return "CONTROL BAR STANDARD";

                        case "ControlBarStandardDescription":
                            return "Standard-Zero-Hour-Steuerleiste." +
                                "\nEmpfohlen für Displays mit einem" +
                                "\nSeitenverhältnis von 4:3.";

                        case "IconQualityDouble":
                            return "DOPPELTE CAMEO-QUALITÄT";

                        case "IconQualityDoubleDescription":
                            return "Kleine Schaltflächen verwenden" +
                                "\neine größere Symbolauflösung. Sieht" +
                                "\nam besten auf großen Displays aus.";

                        case "IconQualityStandard":
                            return "STANDARD-CAMEO-QUALITÄT";

                        case "IconQualityStandardDescription":
                            return "Kleine Schaltflächen verwenden" +
                                "\ndie Standardsymbolauflösung. Sieht" +
                                "\nam besten auf kleinen Displays aus.";

                        case "HotkeysLeikeze":
                            return "LEIKEZE-HOTKEYS";

                        case "HotkeysLeikezeDescription":
                            return "Verwenden Sie Hotkeys von Leikeze.";

                        case "HotkeysStandard":
                            return "STANDARD-HOTKEYS";

                        case "HotkeysStandardDescription":
                            return "Verwenden Sie Standard-Hotkeys.";

                        case "E_NotFound_DLLs":
                            return "Das Spiel kann nicht gestartet werden, da die Dateien \"binkw32.dll\" und /oder \"mss32.dll\" " +
                                "nicht im aktuellen Verzeichnis gefunden wurden. Contra muss im Hauptspielordner von Zero Hour installiert werden." +
                                "\n\nMöchten Sie eine Seite mit Installationsanweisungen besuchen?";

                        case "E_NotFound_GeneralsEXE":
                            return "Die Datei \"generals.exe\" wurde nicht gefunden. " +
                                "Stellen Sie sicher, dass Sie Zero Hour installiert haben und Contra im Hauptordner von Zero Hour installiert ist." +
                                "\n\nMöchten Sie eine Seite mit Installationsanweisungen besuchen?";

                        case "E_GeneralsAlreadyRunningButNotResponding":
                            return "Generals.exe ist bereits gestartet, reagiert jedoch nicht." +
                                "\n\nMöchten Sie, dass der Launcher den Vorgang stoppt und dann das Spiel ausführt?";

                        case "W_NotFound_GeneralsCTR":
                            return "\"generals.ctr\" nicht gefunden oder Prüfsummeninkongruenz! " +
                                "Bitte extrahieren Sie es aus dem Archiv Contra X Beta, wenn die Einstellung " +
                                "der Kamerahöhe funktionieren soll und Spieler in der Netzwerk-Lobby zu sehen." +
                                "\n\nMöchten Sie das Spiel trotzdem starten?";

                        case "W_FoundIniFiles":
                            return "Es wurden INI-Dateien im Verzeichnis \"Data\\INI\" gefunden. Sie können den Mod beschädigen oder online zu Unstimmigkeiten führen." +
                                "\n\nMöchten Sie diesen Ordner jetzt aufrufen?";

                        case "W_FoundW3DFiles":
                            return "Es wurden W3D-Dateien im Verzeichnis \"Art\\W3D\" gefunden. Sie können den Mod beschädigen oder online zu Unstimmigkeiten führen." +
                                "\n\nMöchten Sie diesen Ordner jetzt aufrufen?";

                        case "W_FoundWndFiles":
                            return "Es wurden WND-Dateien im Verzeichnis \"Window\" gefunden. Sie können den Mod beschädigen." +
                                "\n\nMöchten Sie diesen Ordner jetzt aufrufen?";

                        case "W_FoundStrFiles":
                            return "Es wurden STR-Dateien im Verzeichnis \"Data\" gefunden. Sie können die Textzeichenfolgen des Mods beschädigen." +
                                "\n\nMöchten Sie diesen Ordner jetzt aufrufen?";

                        case "W_FoundCsfFiles":
                            return "Es wurden CSF-Dateien im Verzeichnis \"Data\\English\" gefunden. Sie können die Textzeichenfolgen des Mods beschädigen." +
                                "\n\nMöchten Sie diesen Ordner jetzt aufrufen?";

                        case "W_FoundIniFilesInEnglishFolder":
                            return "Es wurden INI-Dateien im Verzeichnis \"Data\\English\" gefunden. Sie können den Mod beschädigen." +
                                "\n\nMöchten Sie diesen Ordner jetzt aufrufen?";

                        case "W_PreferencesMayNotLoad":
                            return "Stimmen der Einheit, Spielsprache und Hotkey-Einstellungen werden möglicherweise nicht richtig geladen, da World Builder ausgeführt wird." +
                                "\n\nMöchten Sie das Spiel trotzdem starten?";

                        case "W_GenToolNotInstalledOrNotUpToDate":
                            return "GenTool ist nicht installiert oder nicht auf dem neuesten Stand. Das bedeutet, dass Sie hinter der Control Bar Pro eine schwarze Linie haben." +
                                "\n\nMöchten Sie https://www.gentool.net/ besuchen und die neueste Version installieren?" +
                                "nWenn nicht, empfehlen wir Ihnen, für die korrekte Anzeige eine andere Steuerleiste auszuwählen.";

                        case "I_GeneralsAlreadyRunning":
                            return "Generals.exe ist bereits gestartet.";
                    }
                    break;
            }
            return "";
        }
    }
}