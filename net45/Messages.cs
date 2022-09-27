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
                            MessageBox.Show("\"!!!!Contra009Final_Patch3_GameData.big\" file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_GameDataP2":
                            MessageBox.Show("\"!!!Contra009Final_Patch2_GameData.big\" file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP3":
                            MessageBox.Show("Please close !!!!Contra009Final_Patch3_GameData.big in order to change camera height.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP2":
                            MessageBox.Show("Please close !!!Contra009Final_Patch2_GameData.big in order to change camera height.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CouldNotLoadRes":
                            MessageBox.Show("Options.ini not found! Could not load current resolution.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CouldNotSetRes":
                            MessageBox.Show("Options.ini not found! Could not set new resolution.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_InvalidRes":
                            MessageBox.Show("This resolution is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_MissingFiles_CouldNotChangeCamHeight":
                            MessageBox.Show("Could not change camera height because of missing files!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "I_ResChanged":
                            MessageBox.Show("Resolution changed successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_CameraHeightChanged":
                            MessageBox.Show("Camera height successfully changed!" +
                                "\n\nNOTE: High camera height values will decrease performance.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_WeakCPU":
                            MessageBox.Show("We have detected that your CPU's single-core performance is not good enough for the most demanding graphical effects." +
                                "\n\nNote: C&C Generals runs on an old game engine which does not benefit from multiple CPU cores/threads." +
                                "\n\nWe have set 3D Shadows and Water Effects OFF for optimal performance." +
                                "\nWe recommend to keep them off for the best experience.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show("Файл \"!!!!Contra009Final_Patch3_GameData.big\" не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_GameDataP2":
                            MessageBox.Show("Файл \"!!!Contra009Final_Patch2_GameData.big\" не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP3":
                            MessageBox.Show("Пожалуйста, закройте !!!!Contra009Final_Patch3_GameData.big, чтобы изменить высоту камеры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP2":
                            MessageBox.Show("Пожалуйста, закройте !!!Contra009Final_Patch2_GameData.big, чтобы изменить высоту камеры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CouldNotLoadRes":
                            MessageBox.Show("Файл \"Options.ini\" не найден! Не удалось загрузить текущее разрешение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CouldNotSetRes":
                            MessageBox.Show("Файл \"Options.ini\" не найден! Не удалось установить новое разрешение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_InvalidRes":
                            MessageBox.Show("Это разрешение экрана не является действительным.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_MissingFiles_CouldNotChangeCamHeight":
                            MessageBox.Show("Не удалось изменить высоту камеры из-за отсутствия файлов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "I_ResChanged":
                            MessageBox.Show("Разрешение экрана успешно изменено!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_CameraHeightChanged":
                            MessageBox.Show("Высота камеры изменена!" +
                                "\n\nЗаметка. Высокие значения высоты камеры снижают производительность.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_WeakCPU":
                            MessageBox.Show("Мы обнаружили, что одноядерная производительность вашего процессора недостаточна для самых требовательных графических эффектов." +
                                "\n\nПримечание: C&C Generals работает на старом игровом движке, который не использует несколько ядер/потоков ЦП." +
                                "\n\nМы отключили 3D-тени и водные эффекты для оптимальной производительности." +
                                "\nМы рекомендуем держать их выключенными.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show("Файл \"!!!!Contra009Final_Patch3_GameData.big\" не знайдений!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_GameDataP2":
                            MessageBox.Show("Файл \"!!!Contra009Final_Patch2_GameData.big\" не знайдений!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP3":
                            MessageBox.Show("Будь ласка, закрийте !!!!Contra009Final_Patch3_GameData.big, щоб змінити висоту камери.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP2":
                            MessageBox.Show("Будь ласка, закрийте !!!Contra009Final_Patch2_GameData.big, щоб змінити висоту камери.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CouldNotLoadRes":
                            MessageBox.Show("Файл Options.ini не знайдений! Не вдалося завантажити поточну роздільну здатність.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CouldNotSetRes":
                            MessageBox.Show("Файл Options.ini не знайдений! Не вдалося встановити нову роздільну здатність.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_InvalidRes":
                            MessageBox.Show("Це розширення не є дійсним.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_MissingFiles_CouldNotChangeCamHeight":
                            MessageBox.Show("Не вдалося змінити висоту камери через відсутні файли!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "I_ResChanged":
                            MessageBox.Show("Розширення успішно змінено!", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_CameraHeightChanged":
                            MessageBox.Show("Висота камери змінилася!" +
                                "\n\nПримітка. Високі значення висоти камери знижують продуктивність.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_WeakCPU":
                            MessageBox.Show("Ми виявили, що одноядерна продуктивність вашого ЦП недостатня для найвимогливіших графічних ефектів." +
                                "\n\nПримітка: C&C Generals працює на старому ігровому движку, який не має переваг від кількох ядер/потоків ЦП." +
                                "\n\nМи вимкнули тривимірні тіні та водяні ефекти для оптимальної роботи." +
                                "\nМи рекомендуємо тримати їх вимкненими.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show("Файлът \"!!!!Contra009Final_Patch3_GameData.big\" не беше намерен!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_GameDataP2":
                            MessageBox.Show("Файлът \"!!!Contra009Final_Patch2_GameData.big\" не беше намерен!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP3":
                            MessageBox.Show("Моля, затворете !!!!Contra009Final_Patch3_GameData.big, за да промените височината на камерата.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP2":
                            MessageBox.Show("Моля, затворете !!!Contra009Final_Patch2_GameData.big, за да промените височината на камерата.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CouldNotLoadRes":
                            MessageBox.Show("Options.ini не беше намерен! Не можа да се зареди текущата резолюция.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CouldNotSetRes":
                            MessageBox.Show("Options.ini не беше намерен! Не можа да се приложи избраната резолюция.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_InvalidRes":
                            MessageBox.Show("Тази резолюция не е валидна.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_MissingFiles_CouldNotChangeCamHeight":
                            MessageBox.Show("Височината на камерата не можа да се промени поради липсващи файлове!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "I_ResChanged":
                            MessageBox.Show("Резолюцията беше променена успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_CameraHeightChanged":
                            MessageBox.Show("Височината на камерата е променена!" +
                                "\n\nЗабележка: Високите стойности на височината на камерата ще намалят производителността.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_WeakCPU":
                            MessageBox.Show("Установихме, че едноядрената производителност на вашия процесор не е достатъчно добра за някои графични ефекти." +
                                "\n\nЗабележка: C&C Generals работи на стар двигател (game engine), който не се възползва от множество процесорни ядра/нишки." +
                                "\n\nИзключили сме 3D сенки и водни ефекти за оптимална производителност." +
                                "\nПрепоръчваме да ги оставите изключени.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show("\"!!!!Contra009Final_Patch3_GameData.big\" nicht gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_NotFound_GameDataP2":
                            MessageBox.Show("\"!!!Contra009Final_Patch2_GameData.big\" nicht gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP3":
                            MessageBox.Show("Bitte schließen Sie !!!!Contra009Final_Patch3_GameData.big, um die Kamerahöhe zu ändern.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CloseGameDataP2":
                            MessageBox.Show("Bitte schließen Sie !!!Contra009Final_Patch2_GameData.big, um die Kamerahöhe zu ändern.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CouldNotLoadRes":
                            MessageBox.Show("Options.ini nicht gefunden! Aktuelle Auflцsung konnte nicht geladen werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_CouldNotSetRes":
                            MessageBox.Show("Options.ini nicht gefunden! Neue Auflцsung konnte nicht eingestellt werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_InvalidRes":
                            MessageBox.Show("Diese Auflцsung ist nicht gьltig.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "E_MissingFiles_CouldNotChangeCamHeight":
                            MessageBox.Show("Die Kamerahöhe konnte wegen fehlender Dateien nicht geändert werden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "I_ResChanged":
                            MessageBox.Show("Auflцsung erfolgreich geдndert!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_CameraHeightChanged":
                            MessageBox.Show("Kamerahöhe geändert!" +
                                "\n\nHinweis: Hohe Werte für die Kamerahöhe verringern die Leistung.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case "I_WeakCPU":
                            MessageBox.Show("Wir haben festgestellt, dass die Single-Core-Leistung Ihrer CPU für die anspruchsvollsten Grafikeffekte nicht gut genug ist." +
                                "\n\nHinweis: C&C Generals läuft auf einer alten Spiel-Engine, die nicht von mehreren CPU-Kernen/Threads profitiert." +
                                "\n\nWir haben 3D-Schatten und Wassereffekte für eine optimale Leistung deaktiviert." +
                                "\nWir empfehlen, sie für das beste Erlebnis fernzuhalten.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        case "CameraHeightString":
                            return "Camera Height: ";
                        case "OptionsSuggestion":
                            return "You can adjust many options including camera height here.";
                        case "E_NotFound_DLLs":
                            return "The game cannot start because the \"binkw32.dll\" and/or \"mss32.dll\" file(s) were not found " +
                                "in the current directory. Contra must be installed in Zero Hour's main game folder." +
                                "\n\nWould you like to visit a page with installation instructions?";
                        case "E_NotFound_GeneralsEXE":
                            return "The \"generals.exe\" file was not found. " +
                                "Make sure you have Zero Hour installed, and Contra is installed into Zero Hour's main folder." +
                                "\n\nWould you like to visit a page with installation instructions?";
                        case "W_NotFound_GeneralsCTR":
                            return "\"generals.ctr\" not found or checksum mismatch! " +
                                "Please, extract it from the \"Contra009FinalPatch2\" archive if you want " +
                                "camera height setting to work and to see players in network lobby." +
                                "\n\nWould you like to start the game anyway?";
                        case "W_FoundIniFiles":
                            return "Found .ini files in the Data\\INI directory. They may corrupt the mod or cause mismatch online." +
                                "\n\nWould you like to visit this folder now?";
                        case "W_FoundWndFiles":
                            return "Found .wnd files in the Window directory. They may corrupt the mod." +
                                "\n\nWould you like to visit this folder now?";
                        case "W_PreferencesMayNotLoad":
                            return "Voice, language and hotkey preferences may not load correctly since World Builder is running." +
                                "\n\nWould you like to start the game anyway?";
                    }
                    break;
                case "RU":
                    switch (type)
                    {
                        case "Error":
                            return "Ошибка";
                        case "Warning":
                            return "Предупреждение";
                        case "CameraHeightString":
                            return "Высота камеры: ";
                        case "OptionsSuggestion":
                            return "Здесь вы можете настроить множество параметров, включая высоту камеры.";
                        case "E_NotFound_DLLs":
                            return "Игра не может запуститься, потому что файлы \"binkw32.dll\" и/или \"mss32.dll\" не найдены " +
                                "в текущей папке. Contra должна быть установлена в основную папку игры Zero Hour." +
                                "\n\nХотите посетить страницу с инструкциями по установке?";
                        case "E_NotFound_GeneralsEXE":
                            return "Файл \"generals.exe\" не найден. " +
                                "Убедитесь, что у вас установлена Zero Hour, а Contra установлен в основную папку Zero Hour." +
                                "\n\nХотите посетить страницу с инструкциями по установке?";
                        case "W_NotFound_GeneralsCTR":
                            return "\"generals.ctr\" не найден или несоответствие контрольной суммы! " +
                                "Извлеките его из архива \"Contra009FinalPatch2\", если вы хотите, " +
                                "чтобы настройка высоты камеры работала и чтобы видеть игроков в сетевом лобби." +
                                "\n\nХотели бы вы начать игру?";
                        case "W_FoundIniFiles":
                            return "Найдены файлы .ini в каталоге Data\\INI. Они могут повредить мод или вызвать несоответствие в сети." +
                                "\n\nХотите посетить эту папку сейчас?";
                        case "W_FoundWndFiles":
                            return "Найдены файлы .wnd в каталоге Window. Они могут повредить мод." +
                                "\n\nХотите посетить эту папку сейчас?";
                        case "W_PreferencesMayNotLoad":
                            return "Настройки голоса, языка и горячих клавиш могут загружаться неправильно, так как World Builder запущен." +
                                "\n\nВы все равно хотите начать игру?";
                    }
                    break;
                case "UA":
                    switch (type)
                    {
                        case "Error":
                            return "Помилка";
                        case "Warning":
                            return "Попередження";
                        case "CameraHeightString":
                            return "Висота камери: ";
                        case "OptionsSuggestion":
                            return "Тут можна налаштувати багато варіантів, включаючи висоту камери.";
                        case "E_NotFound_DLLs":
                            return "Гра не може запуститися, оскільки файли \"binkw32.dll\" та/або \"mss32.dll\" не знайдені " +
                                "у поточній папці. Contra має бути встановленa в головній папці гри Zero Hour." +
                                "\n\nБажаєте відвідати сторінку з інструкціями щодо встановлення?";
                        case "E_NotFound_GeneralsEXE":
                            return "Файл \"generals.exe\" не знайдено. " +
                                "Переконайтеся, що у вас встановлена Zero Hour, а Contra встановлений в головну папку Zero Hour." +
                                "\n\nБажаєте відвідати сторінку з інструкціями щодо встановлення?";
                        case "W_NotFound_GeneralsCTR":
                            return "\"generals.ctr\" не знайден або невідповідність контрольної суми! " +
                                "Будь ласка, витягніть його з архіву \"Contra009FinalPatch2\", якщо ви хочете, " +
                                "щоб налаштування висоти камери працювало і бачити гравців у лобі мережі." +
                                "\n\nВи хочете все-таки почати гру?";
                        case "W_FoundIniFiles":
                            return "Знайдено файли .ini в каталозі Data\\INI. Вони можуть пошкодити мод або призвести до невідповідності в Інтернеті." +
                                "\n\nБажаєте відвідати цю папку зараз?";
                        case "W_FoundWndFiles":
                            return "Знайдено файли .wnd в каталозі Window. Вони можуть пошкодити мод." +
                                "\n\nБажаєте відвідати цю папку зараз?";
                        case "W_PreferencesMayNotLoad":
                            return "Налаштування голосу, мови та гарячих клавіш можуть завантажуватися неправильно, оскільки працює World Builder." +
                                "\n\nВи все-таки хотіли б почати гру?";
                    }
                    break;
                case "BG":
                    switch (type)
                    {
                        case "Error":
                            return "Грешка";
                        case "Warning":
                            return "Предупреждение";
                        case "CameraHeightString":
                            return "Височина на камерата: ";
                        case "OptionsSuggestion":
                            return "Тук можете да настроите много опции, включително височина на камерата.";
                        case "E_NotFound_DLLs":
                            return "Играта не може да се стартира, тъй като не са намерени файловете \"binkw32.dll\" и/или \"mss32.dll\" " +
                                "в текущата директория. Contra трябва да се инсталира в основната папка на Zero Hour." +
                                "\n\nИскате ли да посетите страница с инструкции за инсталиране?";
                        case "E_NotFound_GeneralsEXE":
                            return "Файлът \"generals.exe\" не беше намерен. " +
                                "Уверете се, че Zero Hour е инсталирана и Contra е инсталиран в основната й папка." +
                                "\n\nИскате ли да посетите страница с инструкции за инсталиране?";
                        case "W_NotFound_GeneralsCTR":
                            return "\"generals.ctr\" не е намерен или има несъответствие в контролната сума! " +
                                "Моля, извлечете го от архива \"Contra009FinalPatch2\", ако искате настройката " +
                                "на височината на камерата да работи и да виждате играчи в мрежовото лоби." +
                                "\n\nЖелаете ли да стартирате играта въпреки това?";
                        case "W_FoundIniFiles":
                            return "Намерени .ini файлове в директорията Data\\INI. Те могат да повредят мода или да причинят несъответствие онлайн." +
                                "\n\nИскате ли да посетите тази папка сега?";
                        case "W_FoundWndFiles":
                            return "Намерени .wnd файлове в директорията Window. Те могат да повредят мода." +
                                "\n\nИскате ли да посетите тази папка сега?";
                        case "W_PreferencesMayNotLoad":
                            return "Предпочитанията за глас, език и горещи клавиши може да не се заредят правилно, тъй като World Builder е стартиран." +
                                "\n\nИскате ли да стартирате играта все пак?";
                    }
                    break;
                case "DE":
                    switch (type)
                    {
                        case "Error":
                            return "Fehler";
                        case "Warning":
                            return "Warnung";
                        case "CameraHeightString":
                            return "Kamerahöhe: ";
                        case "OptionsSuggestion":
                            return "Sie können hier viele Optionen anpassen, einschließlich der Kamerahöhe.";
                        case "E_NotFound_DLLs":
                            return "Das Spiel kann nicht gestartet werden, da die Dateien \"binkw32.dll\" und /oder \"mss32.dll\" " +
                                "nicht im aktuellen Verzeichnis gefunden wurden. Contra muss im Hauptspielordner von Zero Hour installiert werden." +
                                "\n\nMöchten Sie eine Seite mit Installationsanweisungen besuchen?";
                        case "E_NotFound_GeneralsEXE":
                            return "Die Datei \"generals.exe\" wurde nicht gefunden. " +
                                "Stellen Sie sicher, dass Sie Zero Hour installiert haben und Contra im Hauptordner von Zero Hour installiert ist." +
                                "\n\nMöchten Sie eine Seite mit Installationsanweisungen besuchen?";
                        case "W_NotFound_GeneralsCTR":
                            return "\"generals.ctr\" nicht gefunden oder Prüfsummeninkongruenz! " +
                                "Bitte extrahieren Sie es aus dem Archiv \"Contra009FinalPatch2\", wenn die Einstellung " +
                                "der Kamerahöhe funktionieren soll und Spieler in der Netzwerk-Lobby zu sehen." +
                                "\n\nMöchten Sie das Spiel trotzdem starten?";
                        case "W_FoundIniFiles":
                            return "Es wurden INI-Dateien im Verzeichnis \"Data\\INI\" gefunden. Sie können den Mod beschädigen oder online zu Unstimmigkeiten führen." +
                                "\n\nMöchten Sie diesen Ordner jetzt aufrufen?";
                        case "W_FoundWndFiles":
                            return "Es wurden WND-Dateien im Verzeichnis \"Window\" gefunden. Sie können den Mod beschädigen." +
                                "\n\nMöchten Sie diesen Ordner jetzt aufrufen?";
                        case "W_PreferencesMayNotLoad":
                            return "Stimmen der Einheit, Spielsprache und Hotkey-Einstellungen werden möglicherweise nicht richtig geladen, da World Builder ausgeführt wird." +
                                "\n\nMöchten Sie das Spiel trotzdem starten?";
                    }
                    break;
            }
            return "";
        }
    }
}