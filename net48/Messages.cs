using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Contra
{
    public class Messages
    {
        private static string ChooseByLanguage(string lang, string en, string ru, string ua, string bg, string de)
        {
            switch (lang)
            {
                case "RU": return ru;
                case "UA": return ua;
                case "BG": return bg;
                case "DE": return de;
                default: return en;
            }
        }

        private static MessageBoxIcon GetIconForType(string type)
        {
            if (type.StartsWith("E_")) return MessageBoxIcon.Error;
            if (type.StartsWith("W_")) return MessageBoxIcon.Warning;
            if (type.StartsWith("I_")) return MessageBoxIcon.Information;
            return MessageBoxIcon.None;
        }

        private static string GetCaptionForType(string type, string lang)
        {
            if (type.StartsWith("E_")) return GenerateMessage("Error", lang);
            if (type.StartsWith("W_")) return GenerateMessage("Warning", lang);
            if (type.StartsWith("I_")) return GenerateMessage("Information", lang);
            return string.Empty;
        }

        private static readonly Dictionary<string, Func<string, string>> FastBodies = new Dictionary<string, Func<string, string>>(StringComparer.Ordinal)
        {
            ["E_NotFound_OptionsIni"] = lang => ChooseByLanguage(lang,
                "Options.ini not found!",
                "Файл \"Options.ini\" не найден!",
                "Файл Options.ini не знайдений!",
                "Options.ini не беше намерен!",
                "Options.ini nicht gefunden!"),

            ["E_NotFound_WB"] = lang => ChooseByLanguage(lang,
                "World Builder was not found.",
                "World Builder не найден.",
                "World Builder не знайдено.",
                "World Builder не бе намерен.",
                "World Builder wurde nicht gefunden."),

            ["E_NotFound_GameDataP3"] = lang => ChooseByLanguage(lang,
                "\"!ContraXBeta_GameData.big\" file not found!",
                "Файл \"!ContraXBeta_GameData.big\" не найден!",
                "Файл \"!ContraXBeta_GameData.big\" не знайдений!",
                "Файлът \"!ContraXBeta_GameData.big\" не беше намерен!",
                "\"!ContraXBeta_GameData.big\" nicht gefunden!"),

            ["E_CloseGameDataP3"] = lang => ChooseByLanguage(lang,
                "Please close !ContraXBeta_GameData.big in order to change camera height.",
                "Пожалуйста, закройте !ContraXBeta_GameData.big, чтобы изменить высоту камеры.",
                "Будь ласка, закрийте !ContraXBeta_GameData.big, щоб змінити висоту камери.",
                "Моля, затворете !ContraXBeta_GameData.big, за да промените височината на камерата.",
                "Bitte schließen Sie !ContraXBeta_GameData.big, um die Kamerahöhe zu ändern."),

            ["E_InvalidRes"] = lang => ChooseByLanguage(lang,
                "This resolution is not valid.",
                "Это разрешение экрана не является действительным.",
                "Це розширення не є дійсним.",
                "Тази резолюция не е валидна.",
                "Diese Auflцsung ist nicht gьltig."),

            ["W_ChangesAfterGameRestart"] = lang => ChooseByLanguage(lang,
                "Changes will take effect after game restart.",
                "Изменения вступят в силу после перезапуска игры.",
                "Зміни набудуть чинності після перезапуску гри.",
                "Промените ще влязат в сила след рестартиране на играта.",
                "Änderungen werden nach dem Neustart des Spiels wirksam."),

            ["W_BlackScreen"] = lang => ChooseByLanguage(lang,
                "Heat Effects are known to cause black screen in the game on certain systems. If you get this problem, turn this setting off.",
                "Известно, что тепловые эффекты вызывают черный экран в игре на некоторых системах. Если у вас возникла эта проблема, отключите этот параметр.",
                "Відомо, що теплові ефекти викликають чорний екран у грі на певних системах. Якщо у вас виникла ця проблема, вимкніть це налаштування.",
                "Известно е, че топлинните ефекти причиняват черен екран в играта на определени системи. Ако имате този проблем, изключете тази настройка.",
                "Es ist bekannt, dass Hitzeeffekte auf bestimmten Systemen einen schwarzen Bildschirm im Spiel verursachen. Wenn dieses Problem auftritt, schalten Sie diese Einstellung aus."),

            ["W_WBCouldNotUnloadMod"] = lang => ChooseByLanguage(lang,
                "Mod files could not be unloaded since they are currently in use by World Builder. If you want to unload mod files, close World Builder and run the launcher again. Closing the launcher anyway.",
                "Файлы мода не могут быть выгружены, так как они в настоящее время используются World Builder. Если вы хотите выгрузить файлы мода, закройте World Builder и снова запустите лаунчер. Закрытие лаунчера в любом случае.",
                "Файли моду не могли бути розвантажені, оскільки вони в даний час використовуються World Builder. Якщо ви хочете завантажити файли моду, закрийте World Builder і знову запустіть лаунчер. Закриття лаунчера в будь-якому випадку.",
                "Contra файловете не можаха да бъдат деактивирани, тъй като се използват от World Builder. Ако искате да деактивирате Contra, затворете World Builder и стартирайте launcher-а отново.",
                "Mod dateien konnten nicht entladen werden, da sie momentan im World Builder benutzt werden. Falls du die mod dateien entladen wilst, schlieЯe den World Builder und starte den Launcher erneut. SchlieЯt den Launcher sowieso."),

            ["E_MissingFiles_CouldNotChangeCamHeight"] = lang => ChooseByLanguage(lang,
                "Could not change camera height because of missing files!",
                "Не удалось изменить высоту камеры из-за отсутствия файлов!",
                "Не вдалося змінити висоту камери через відсутні файли!",
                "Височината на камерата не можа да се промени поради липсващи файлове!",
                "Die Kamerahöhe konnte wegen fehlender Dateien nicht geändert werden!"),

            ["I_WeakCPU"] = lang => ChooseByLanguage(lang,
                "We have detected that your CPU's single-core performance is not good enough for the most demanding graphical effects.\n\nNote: C&C Generals runs on an old game engine which does not benefit from multiple CPU cores/threads.\n\nWe have set 3D Shadows and Water Effects OFF for optimal performance.\nWe recommend to keep them off for the best experience.",
                "Мы обнаружили, что одноядерная производительность вашего процессора недостаточна для самых требовательных графических эффектов.\n\nПримечание: C&C Generals работает на старом игровом движке, который не использует несколько ядер/потоков ЦП.\n\nМы отключили 3D-тени и водные эффекты для оптимальной производительности.\nМы рекомендуем держать их выключенными.",
                "Ми виявили, що одноядерна продуктивність вашого ЦП недостатня для найвимогливіших графічних ефектів.\n\nПримітка: C&C Generals працює на старому ігровому движку, який не має переваг від кількох ядер/потоків ЦП.\n\nМи вимкнули тривимірні тіні та водяні ефекти для оптимальної роботи.\nМи рекомендуємо тримати їх вимкненими.",
                "Установихме, че едноядрената производителност на вашия процесор не е достатъчно добра за някои графични ефекти.\n\nЗабележка: C&C Generals работи на стар двигател (game engine), който не се възползва от множество процесорни ядра/нишки.\n\nИзключили сме 3D сенки и водни ефекти за оптимална производителност.\nПрепоръчваме да ги оставите изключени.",
                "Wir haben festgestellt, dass die Single-Core-Leistung Ihrer CPU für die anspruchsvollsten Grafikeffekte nicht gut genug ist.\n\nHinweis: C&C Generals läuft auf einer alten Spiel-Engine, die nicht von mehreren CPU-Kernen/Threads profitiert.\n\nWir haben 3D-Schatten und Wassereffekte für eine optimale Leistung deaktiviert.\nWir empfehlen, sie für das beste Erlebnis fernzuhalten."),

            ["I_WelcomeMessage"] = lang => ChooseByLanguage(lang,
                "Welcome to Contra X Beta!\n\nSince this is a Beta version, it is not fully polished and you may encounter content that does not match quality standards.\nWe are continuously working towards improving Contra in all aspects.\nYou can find any information about Contra on our Discord.\n\nWe hope you will appreciate and enjoy the changes we have made in this version so far!",
                "Добро пожаловать в бета-версию Contra X!\n\nПоскольку это бета-версия, она не полностью завершена, и вы можете столкнуться с контентом, не соответствующим стандартам качества.\nМы постоянно работаем над улучшением Contra во всех аспектах.\nВы можете найти любую информацию о Contra в нашем Discord.\n\nМы надеемся, что вы оцените и насладитесь изменениями, которые мы сделали в этой версии!",
                "Ласкаво просимо до Contra X Beta!\n\nОскільки це бета-версія, вона не повністю завершена, і ви можете натрапити на вміст, який не відповідає стандартам якості.\nМи постійно працюємо над покращенням Contra в усіх аспектах.\nВи можете знайти будь-яку інформацію про Contra на нашому Discord.\n\nМи сподіваємося, що ви оціните та насолодитеся змінами, які ми внесли в цю версію досі!",
                "Добре дошли в Contra X Beta!\n\nТъй като това е бета версия, тя не е напълно доработена и може да срещнете съдържание, което не отговаря на стандартите за качество.\nНие работим непрекъснато за подобряване на Contra във всички аспекти.\nМожете да намерите всякаква информация за Contra в нашия Discord.\n\nНадяваме се, че ще оцените и ще се насладите на промените, които успяхме да направим в тази версия!",
                "Willkommen zur Contra X Beta!\n\nDa es sich um eine Beta-Version handelt, ist sie nicht vollständig ausgefeilt und es kann sein, dass Sie auf Inhalte stoßen, die nicht den Qualitätsstandards entsprechen.\nWir arbeiten kontinuierlich daran, Contra in allen Aspekten zu verbessern.\nAlle Informationen zu Contra finden Sie auf unserem Discord.\n\nWir hoffen, dass Sie die Änderungen, die wir bisher in dieser Version vorgenommen haben, zu schätzen wissen und genießen werden!"),

            ["W_NotFound_ContraOnDesktop"] = lang => ChooseByLanguage(lang,
                "Contra Launcher could not find Contra installed on your desktop. Locate the game folder where you installed Contra and make a shortcut (not a copy) of Contra Launcher instead.",
                "Contra Launcher не может найти установленная Contra на вашем рабочем столе. Найдите папку с игрой, в которую вы установили Contra, и вместо этого сделайте ярлык (не копию) Contra Launcher.",
                "Contra Launcher не може знайти встановлена Contra на вашому робочому столі. Знайдіть папку з грою, в яку ви встановили Contra, і замість цього зробіть ярлик (не копію) Contra Launcher.",
                "Contra Launcher не можа да открие инсталирана Contra на вашия работен плот. Намерете директорията на играта, където сте инсталирали Contra и създайте пряк път (не копие) на Contra Launcher вместо това.",
                "Contra Launcher konnte Contra nicht auf Ihrem Desktop installiert finden. Suchen Sie den Spielordner, in dem Sie Contra installiert haben, und erstellen Sie stattdessen eine Verknüpfung (keine Kopie) von Contra Launcher."),

            ["W_NotFound_Contra009Final"] = lang => ChooseByLanguage(lang,
                "\"!Contra009Final.ctr\" is missing!\nPlease, install 009 Final first, or the mod will not start!",
                "\"!Contra009Final.ctr\" отсутствует!\nПожалуйста, сначала установите 009 Final, или мод не запустится!",
                "\"!Contra009Final.ctr\" відсутня!\nБудь ласка, спочатку встановіть 009 Final, або мод не запуститься!",
                "\"!Contra009Final.ctr\" не беше намерен!\nМоля, първо инсталирайте 009 Final, или модът няма да стартира!",
                "\"!Contra009Final.ctr\" wird vermisst!\nBitte installieren Sie zuerst 009 Final, oder der mod startet nicht!")
        };

        private static readonly Dictionary<string, Func<string, string>> FastMessages = new Dictionary<string, Func<string, string>>(StringComparer.Ordinal)
        {
            ["Error"] = lang => ChooseByLanguage(lang, "Error", "Ошибка", "Помилка", "Грешка", "Fehler"),
            
            ["Warning"] = lang => ChooseByLanguage(lang, "Warning", "Предупреждение", "Попередження", "Предупреждение", "Warnung"),
            
            ["Information"] = lang => ChooseByLanguage(lang, "Information", "Информация", "Інформація", "Информация", "Information"),
            
            ["CameraHeightString"] = lang => ChooseByLanguage(lang, "Camera Height: ", "Высота камеры: ", "Висота камери: ", "Вис. на камерата: ", "Kamerahöhe: "),
            
            ["ParticleCap"] = lang => ChooseByLanguage(lang, "Max Particle Count: ", "Количество частиц: ", "Кількість частинок: ", "Количество частици: ", "Partikelzahl: "),
            
            ["TextureRes"] = lang => ChooseByLanguage(lang, "Texture Resolution: ", "Разр. текстур: ", "Розр. текстур: ", "Текстурна резол.: ", "Texturauflösung: "),
            
            ["Low"] = lang => ChooseByLanguage(lang, "Low", "Низкое", "Низьке", "Ниска", "Niedrige"),
            
            ["Medium"] = lang => ChooseByLanguage(lang, "Medium", "Среднее", "Середнє", "Средна", "Mittlere"),
            
            ["High"] = lang => ChooseByLanguage(lang, "High", "Высокое", "Висока", "Висока", "Hohe"),
            
            ["PerformanceEffectNone"] = lang => ChooseByLanguage(lang, "Effect on performance: None", "Влияние на производительность: Нет", "Вплив на продуктивність: Немає", "Ефект върху производителността: Няма", "Auswirkung auf die Leistung: Keiner"),
            
            ["PerformanceEffectLow"] = lang => ChooseByLanguage(lang, "Effect on performance: Low", "Влияние на производительность: Слабое", "Вплив на продуктивність: Низький", "Ефект върху производителността: Нисък", "Auswirkung auf die Leistung: Geringe"),
            
            ["PerformanceEffectMedium"] = lang => ChooseByLanguage(lang, "Effect on performance: Medium", "Влияние на производительность: Средное", "Вплив на продуктивність: Середнє", "Ефект върху производителността: Среден", "Auswirkung auf die Leistung: Mittel"),
            
            ["PerformanceEffectHigh"] = lang => ChooseByLanguage(lang, "Effect on performance: High", "Влияние на производительность: Сильное", "Вплив на продуктивність: Високий", "Ефект върху производителността: Висок", "Auswirkung auf die Leistung: Hohe"),
            
            ["CameraHeight"] = lang => ChooseByLanguage(lang, "CAMERA HEIGHT", "ВЫСОТА КАМЕРЫ", "ВИСОТА КАМЕРИ", "ВИСОЧИНА НА КАМЕРАТА", "KAMERAHÖHE"),
            
            ["CameraHeightDescription"] = lang => ChooseByLanguage(lang, 
                "Changes the default and maximum player\nview distance in-game.",
                "Изменяет стандартное и максимальное\nрасстояние поле зрения игрока.",
                "Змінює стандартну та максимальну\nвідстань огляду гравця в грі.",
                "Променя стандартното и максималното\nразстояние на изглед на играча.",
                "Ändert die standardmäßige und maximale\nSichtweite des Spielers im Spiel."),
            
            ["TextureResTwo"] = lang => ChooseByLanguage(lang, "TEXTURE RESOLUTION", "РАЗРЕШЕНИЕ ТЕКСТУРЫ", "РОЗРІШЕННЯ ТЕКСТУРИ", "ТЕКСТУРНА РЕЗОЛЮЦИЯ", "TEXTURAUFLÖSUNG"),
            
            ["TextureResDescription"] = lang => ChooseByLanguage(lang, "Changes the texture resolution.", "Изменяет разрешение текстуры.", "Змінює роздільну здатність текстури.", "Променя резолюцията на текстурите.", "Ändert die Texturauflösung."),
            
            ["ParticleCapTwo"] = lang => ChooseByLanguage(lang, "PARTICLE CAP", "ПОДСЧЕТ ЧАСТИЦ", "КІЛЬКІСТЬ ЧАСТИНОК", "БРОЙ ЧАСТИЦИ", "PARTIKELZAHL"),
            
            ["ParticleCapDescription"] = lang => ChooseByLanguage(lang, "Changes the particle effects limit.", "Изменяет лимит эффектов частиц.", "Змінює обмеження ефектів частинок.", "Променя лимита на частиците.", "Ändert das Limit der Partikeleffekte."),
            
            ["Shadows3DDescription"] = lang => ChooseByLanguage(lang, 
                "Shows 3D shadows projected\nby world objects.",
                "Показывает 3D-тени.",
                "Показує тривимірні тіні.",
                "Показва 3D сенки.",
                "Zeigt 3D-Schatten an."),
            
            ["Shadows2DDescription"] = lang => ChooseByLanguage(lang, "Shows 2D shadows and area decals.", "Показывает 2D-тени и метки областей.", "Показує 2D тіні та наклейки області.", "Показва 2D сенки и\nплощни стикери.", "Zeigt 2D-Schatten und\nFlächenabziehbilder an."),
            
            ["CloudShadowsDescription"] = lang => ChooseByLanguage(lang, "Shows moving cloud shadows.", "Показывает движущиеся тени облаков.", "Показує рухомі тіні хмар.", "Показва движещи се облачни сенки.", "Zeigt sich bewegende Wolkenschatten."),
            
            ["ExtraGroundLightingDescription"] = lang => ChooseByLanguage(lang, 
                "Shows more detailed ground lighting.",
                "Показывает более детальное\nосвещение земли.",
                "Показує більш детальне освітлення землі.",
                "Показва по-подробно осветление\nна земята.",
                "Zeigt eine detailliertere Bodenbeleuchtung."),
            
            ["SmoothWaterBordersDescription"] = lang => ChooseByLanguage(lang, "Smoothens water borders along the shores.", "Сглаживает границы воды.", "Згладжує межі води.", "Изглажда водните граници.", "Glättet Wasserränder."),
            
            ["BehindBuildingsDescription"] = lang => ChooseByLanguage(lang, 
                "Shows the silhouettes of units\nbehind buildings.",
                "Показывает силуэты юнитов за зданиями.",
                "Показує силуети підрозділів за будівлями.",
                "Показва силуети на единиците\nпрез сградите.",
                "Zeigt Einheiten hinter Gebäuden an."),
            
            ["ShowPropsDescription"] = lang => ChooseByLanguage(lang, "Shows props.", "Показывает мелкие предметы.", "Показує дрібні предмети.", "Показва малки обекти.", "Zeigt kleine Objekte."),
            
            ["ExtraAnimationsDescription"] = lang => ChooseByLanguage(lang, 
                "Shows additional animations\nsuch as tree sway.",
                "Показывает дополнительные анимации,\nтакие как покачивания деревьев.",
                "Показує додаткові анімації,\nтакі як похитування дерев.",
                "Показва допълнителни анимации,\nкато люлеене на дървета.",
                "Zeigt zusätzliche Animationen\nwie Baumschwingen."),
            
            ["DisableDynamicLODDescription"] = lang => ChooseByLanguage(lang, 
                "Disables automatic detail adjustment.\nUncheck for better performance.",
                "Отключает автоматическую настройку\nдеталей. Отмените выбор для\nповышения производительности.",
                "Вимикає автоматичне nналаштування\nдеталей. Зніміть вибір для nкращої\nпродуктивності.",
                "Деактивира автоматичната настройка\nна детайлите. Премахнете отметката за\nпо-добра производителност.",
                "Deaktiviert die automatische\nDetailanpassung. Deaktivieren Sie für\neine bessere Leistung."),
            
            ["HeatEffectsDescription"] = lang => ChooseByLanguage(lang, 
                "Distorts the space around\nhot particle effects.",
                "Искажает пространство вокруг\nэффектов горячих частиц.",
                "Спотворює простір навколо ефектів\nгарячих частинок.",
                "Изкривява пространството около\nефектите на горещи частици.",
                "Verzerrt den Raum um\nheiße Partikeleffekte."),
            
            ["FogDescription"] = lang => ChooseByLanguage(lang, 
                "Adds a gradient layer on the screen\ndepending on the map conditions.",
                "Добавляет градиентный слой на экран\nв зависимости от условий карты.",
                "Додає градієнтний шар на екран\nзалежно від умов карти.",
                "Добавя градиентен слой на екрана\nв зависимост от условията на картата.",
                "Fügt je nach Kartenbedingungen eine\nVerlaufsebene auf dem Bildschirm hinzu."),
            
            ["WaterEffectsDescription"] = lang => ChooseByLanguage(lang, 
                "Simulates realistic sun and cloud\nreflections on water.",
                "Имитирует реалистичные отражения\nсолнца и облаков на воде.",
                "Імітує реалістичне відображення\nсонця та хмар на воді.",
                "Симулира реалистични отражения на\nслънцето и облаците върху водата.",
                "Simuliert realistische Sonnen- und\nWolkenreflexionen auf dem Wasser."),
            
            ["LangFilterDescription"] = lang => ChooseByLanguage(lang, "Hides offensive words in chat.", "Скрывает оскорбительные слова в чате\n(только на английском).", "Приховує образливі слова в чаті\n(тільки англійською).", "Скрива обидните думи в чата\n(само на английски).", "Versteckt böse Worte im Chat\n(Nur auf Englisch verfügbar)."),
            
            ["AnisoDescription"] = lang => ChooseByLanguage(lang, 
                "Improves texture quality of surfaces\nwhen viewed at an angle.",
                "Улучшает качество текстур\nповерхностей при просмотре под углом.",
                "Покращує якість текстури поверхонь\nпри погляді під кутом.",
                "Подобрява качеството на текстурите,\nкогато се наблюдават под ъгъл.",
                "Verbessert die Texturqualität von\nOberflächen bei schräger Betrachtung."),
            
            ["ExtraBuildingPropsDescription"] = lang => ChooseByLanguage(lang, 
                "Shows extra building props\nunique per each general.",
                "Показывает доп. косметические\nэлементы на зданиях разных генералов.",
                "Показує дод. косметичні елементи на\nбудинках різних генералів.",
                "Показва допълнителни сградни\nреквизити, уникални за всеки генерал.",
                "Zeigt zusätzliche Gebäudeobjekte an,\ndie für jeden General einzigartig sind."),
            
            ["ControlBarPro"] = lang => ChooseByLanguage(lang, "CONTROL BAR PRO", "CONTROL BAR PRO", "CONTROL BAR PRO", "CONTROL BAR PRO", "CONTROL BAR PRO"),
            
            ["ControlBarProDescription"] = lang => ChooseByLanguage(lang, 
                "Control Bar Pro by FAS and xezon.\nRecommended for 16:9\naspect ratio displays.",
                "Панель управления Pro от FAS и xezon.\nРекомендуется для дисплеев\nс соотношением сторон 16:9.",
                "Control Bar Pro від FAS і xezon.\nРекомендовано для дисплеїв зі\nспіввідношенням сторін 16:9.",
                "Контролна лента Pro от FAS и xezon.\nПрепоръчва се за дисплеи със\nсъотношение 16:9.",
                "Control Bar Pro von FAS und xezon.\nEmpfohlen für Displays mit einem\nSeitenverhältnis von 16:9."),
            
            ["ControlBarContra"] = lang => ChooseByLanguage(lang, "CONTROL BAR CONTRA", "CONTROL BAR CONTRA", "CONTROL BAR CONTRA", "CONTROL BAR CONTRA", "CONTROL BAR CONTRA"),
            
            ["ControlBarContraDescription"] = lang => ChooseByLanguage(lang, 
                "Contra's own control bar from past versions.\nRecommended for 4:3\naspect ratio displays.",
                "Панель управления Contra из прошлых\nверсий. Рекомендуется для дисплеев\nс соотношением сторон 4:3.",
                "Панель керування Contra з попередніх\nверсій. Рекомендовано для дисплеїв зі\nспіввідношенням сторін 4:3.",
                "Контролна лента Contra от пред. версии.\nПрепоръчва се за дисплеи със\nсъотношение 4:3.",
                "Contras Steuerleiste aus früheren\nVersionen. Empfohlen für Displays mit\neinem Seitenverhältnis von 4:3."),
            
            ["ControlBarStandard"] = lang => ChooseByLanguage(lang, "CONTROL BAR STANDARD", "CONTROL BAR STANDARD", "CONTROL BAR STANDARD", "CONTROL BAR STANDARD", "CONTROL BAR STANDARD"),
            
            ["ControlBarStandardDescription"] = lang => ChooseByLanguage(lang, 
                "Standard Zero Hour control bar.\nRecommended for 4:3\naspect ratio displays.",
                "Стандартная панель управления Zero\nHour. Рекомендуется для дисплеев\nс соотношением сторон 4:3.",
                "Стандартна панель керування Zero Hour.\nРекомендовано для дисплеїв зі\nспіввідношенням сторін 4:3.",
                "Стандартната контр. лента от Zero Hour.\nПрепоръчва се за дисплеи със\nсъотношение 4:3.",
                "Standard-Zero-Hour-Steuerleiste.\nEmpfohlen für Displays mit einem\nSeitenverhältnis von 4:3."),
            
            ["IconQualityDouble"] = lang => ChooseByLanguage(lang, "DOUBLE ICON QUALITY", "ДВОЙНОЕ КАЧЕСТВО ИКОН", "ПІДВІЙНА ЯКІСТЬ ІКОНОК", "ДВОЙНО КАЧЕСТВО НА ИКОНИТЕ", "DOPPELTE CAMEO-QUALITÄT"),
            
            ["IconQualityDoubleDescription"] = lang => ChooseByLanguage(lang, 
                "Small buttons will use larger\nicon/cameo resolution. Best\nlooking on large displays.",
                "Иконки будут в высоком разрешении.\nЛучше смотрится на больших дисплеях.",
                "Іконки будуть у високій якості.\nНайкраще виглядає на великих\nдисплеях.",
                "Малките бутони ще ползват по-висока\nрезолюция на иконите. Препоръчва\nсе за големи дисплеи.",
                "Kleine Schaltflächen verwenden\neine größere Symbolauflösung. Sieht\nam besten auf großen Displays aus."),
            
            ["IconQualityStandard"] = lang => ChooseByLanguage(lang, "STANDARD ICON QUALITY", "СТАНДАРТНОЕ КАЧЕСТВО ИКОН", "СТАНДАРТНА ЯКІСТЬ ІКОНОК", "СТАНДАРТНО КАЧЕСТВО НА ИКОНИТЕ", "STANDARD-CAMEO-QUALITÄT"),
            
            ["IconQualityStandardDescription"] = lang => ChooseByLanguage(lang, 
                "Small buttons will use standard\nicon/cameo resolution. Best\nlooking on small displays.",
                "Иконки будут в стандартном разрешении.\nЛучше смотрится на маленьких дисплеях.",
                "Іконки будуть у стандартному дозволі.\nНайкраще виглядає на невеликих дисплеях.",
                "Малките бутони ще ползват стандартна\nрезолюция на иконите. Препоръчва\nсе за малки дисплеи.",
                "Kleine Schaltflächen verwenden\ndie Standardsymbolauflösung. Sieht\nam besten auf kleinen Displays aus."),
            
            ["HotkeysLeikeze"] = lang => ChooseByLanguage(lang, "LEIKEZE HOTKEYS", "ГОРЯЧИЕ КЛАВИШИ LEIKEZE", "ГАРЯЧІ КЛАВІШІ LEIKEZE", "БЪРЗИ КЛАВИШИ LEIKEZE", "LEIKEZE-HOTKEYS"),
            
            ["HotkeysLeikezeDescription"] = lang => ChooseByLanguage(lang, "Modern hotkeys with the same pattern for all generals.", "Современные хоткеи с одинаковым\nпаттерном для всех генералов.", "Сучасні хоткеї з однаковим патерном\nдля всіх генералів.", "Модерни клавишни комбинации с\nеднакъв модел за всички генерали.", "Moderne Hotkeys mit dem gleichen\nMuster für alle Generäle."),
            
            ["HotkeysStandard"] = lang => ChooseByLanguage(lang, "STANDARD HOTKEYS", "СТАНДАРТНЫЕ ГОРЯЧИЕ КЛАВИШИ", "СТАНДАРТНІ ГАРЯЧІ КЛАВІШІ", "СТАНДАРТНИ БЪРЗИ КЛАВИШИ", "STANDARD-HOTKEYS"),
            
            ["HotkeysStandardDescription"] = lang => ChooseByLanguage(lang, "Standard hotkeys by object names.", "Стандартные хоткеи по названиям\nобъектов.", "Стандартні хоткеї за назвами об'єктів.", "Стандартни клавишни комбинации,\nпо имената на обектите.", "Standard-Hotkeys anhand von Objektnamen."),
            
            ["E_NotFound_DLLs"] = lang => ChooseByLanguage(lang,
                "There are missing base game files in the current directory - please install the mod into the main folder of the game (C&C Generals Zero Hour)!\n\nWould you like to visit a page with installation instructions?",
                "В текущем каталоге отсутствуют файлы базовой игры - установите мод в основную папку игры (C&C Generals Zero Hour)!\n\nХотите посетить страницу с инструкциями по установке?",
                "У поточному каталозі відсутні файли базової гри — будь ласка, встановіть мод в основну папку гри (C&C Generals Zero Hour)!\n\nБажаєте відвідати сторінку з інструкціями щодо встановлення?",
                "В текущата директория липсват файлове на основната игра - моля, инсталирайте мода в основната папка на играта (C&C Generals Zero Hour)!\n\nИскате ли да посетите страница с инструкции за инсталиране?",
                "Im aktuellen Verzeichnis fehlen Basisspieldateien. Bitte installieren Sie den Mod im Hauptordner des Spiels (C&C Generals Zero Hour)!\n\nMöchten Sie eine Seite mit Installationsanweisungen besuchen?"),
            
            ["E_NotFound_GeneralsEXE"] = lang => ChooseByLanguage(lang, 
                "The \"generals.exe\" file was not found. Make sure you have Zero Hour installed, and Contra is installed into Zero Hour's main folder!\n\nWould you like to visit a page with installation instructions?",
                "Файл \"generals.exe\" не найден. Убедитесь, что у вас установлена Zero Hour, а Contra установлен в основную папку Zero Hour!\n\nХотите посетить страницу с инструкциями по установке?",
                "Файл \"generals.exe\" не знайдено. Переконайтеся, що у вас встановлена Zero Hour, а Contra встановлений в головну папку Zero Hour!\n\nБажаєте відвідати сторінку з інструкціями щодо встановлення?",
                "Файлът \"generals.exe\" не беше намерен. Уверете се, че Zero Hour е инсталирана и Contra е инсталиран в основната й папка!\n\nИскате ли да посетите страница с инструкции за инсталиране?",
                "Die Datei \"generals.exe\" wurde nicht gefunden. Stellen Sie sicher, dass Sie Zero Hour installiert haben und Contra im Hauptordner von Zero Hour installiert ist!\n\nMöchten Sie eine Seite mit Installationsanweisungen besuchen?"),
            
            ["E_GeneralsAlreadyRunningButNotResponding"] = lang => ChooseByLanguage(lang, 
                "Generals.exe is already running but not responding.\n\nWould you like the Launcher to kill the process and then run the game?",
                "Generals.exe уже запущен, но не отвечает.\n\nВы хотите, чтобы Launcher остановил процесс, а затем запустил игру?",
                "Generals.exe вже запущено, але не відповідає.\n\nЧи бажаєте ви, щоб програма запуску зупинила процес, а потім запустила гру?",
                "Generals.exe е вече стартиран, но не отговаря.\n\nЖелаете ли Launcher-а да спре процеса и тогава да стартира играта?",
                "Generals.exe ist bereits gestartet, reagiert jedoch nicht.\n\nMöchten Sie, dass der Launcher den Vorgang stoppt und dann das Spiel ausführt?"),

            ["E_Cannot_Delete_INIZH"] = lang => ChooseByLanguage(lang,
                "There is an INIZH.big file in your Data\\INI folder which will prevent mods from running!\n\nWould you like to visit the folder and delete the file manually?",
                "В папке Data\\INI находится файл INIZH.big, который препятствует запуску модов!\n\nХотите зайти в папку и удалить файл вручную?",
                "У вашій папці Data\\INI є файл INIZH.big, який запобігатиме запуску модів!\n\nВи хочете відвідати папку та видалити файл вручну?",
                "В папката Data\\INI има файл INIZH.big, който ще попречи на изпълнението на модове!\n\nИскате ли да посетите папката и да изтриете файла ръчно?",
                "In Ihrem Data\\INI-Ordner befindet sich eine INIZH.big-Datei, die die Ausführung von Mods verhindert!\n\nMöchten Sie den Ordner aufrufen und die Datei manuell löschen?"),

            ["W_NotFound_GeneralsCTR"] = lang => ChooseByLanguage(lang, 
                "\"generals.ctr\" not found or checksum mismatch! Please, extract it from the Contra X Beta archive if you want camera height setting to work and to see players in network lobby.\n\nWould you like to start the game anyway?",
                "\"generals.ctr\" не найден или несоответствие контрольной суммы! Извлеките его из архива Contra X Beta, если вы хотите, чтобы настройка высоты камеры работала и чтобы видеть игроков в сетевом лобби.\n\nХотели бы вы начать игру?",
                "\"generals.ctr\" не знайден або невідповідність контрольної суми! Будь ласка, витягніть його з архіву Contra X Beta, якщо ви хочете, щоб налаштування висоти камери працювало і бачити гравців у лобі мережі.\n\nВи хочете все-таки почати гру?",
                "\"generals.ctr\" не е намерен или има несъответствие в контролната сума! Моля, извлечете го от архива Contra X Beta, ако искате настройката на височината на камерата да работи и да виждате играчи в мрежовото лоби.\n\nЖелаете ли да стартирате играта въпреки това?",
                "\"generals.ctr\" nicht gefunden oder Prüfsummeninkongruenz! Bitte extrahieren Sie es aus dem Archiv Contra X Beta, wenn die Einstellung der Kamerahöhe funktionieren soll und Spieler in der Netzwerk-Lobby zu sehen.\n\nMöchten Sie das Spiel trotzdem starten?"),
            
            ["W_FoundIniFiles"] = lang => ChooseByLanguage(lang, 
                "Found .ini files in the \"Data\\INI\" directory. They may corrupt the mod or cause mismatch online.\n\nWould you like to visit this folder now?",
                "Найдены файлы .ini в каталоге \"Data\\INI\". Они могут повредить мод или вызвать несоответствие в сети.\n\nХотите посетить эту папку сейчас?",
                "Знайдено файли .ini в каталозі \"Data\\INI\". Вони можуть пошкодити мод або призвести до невідповідності в Інтернеті.\n\nБажаєте відвідати цю папку зараз?",
                "Намерени .ini файлове в директорията \"Data\\INI\". Те могат да повредят мода или да причинят несъответствие онлайн.\n\nИскате ли да посетите тази папка сега?",
                "Es wurden INI-Dateien im Verzeichnis \"Data\\INI\" gefunden. Sie können den Mod beschädigen oder online zu Unstimmigkeiten führen.\n\nMöchten Sie diesen Ordner jetzt aufrufen?"),
            
            ["W_FoundW3DFiles"] = lang => ChooseByLanguage(lang, 
                "Found .W3D files in the \"Art\\W3D\" directory. They may corrupt the mod or cause mismatch online.\n\nWould you like to visit this folder now?",
                "Найдены файлы .W3D в каталоге \"Art\\W3D\". Они могут повредить мод или вызвать несоответствие в сети.\n\nХотите посетить эту папку сейчас?",
                "Знайдено файли .W3D в каталозі \"Art\\W3D\". Вони можуть пошкодити мод або призвести до невідповідності в Інтернеті.\n\nБажаєте відвідати цю папку зараз?",
                "Намерени .W3D файлове в директорията \"Art\\W3D\". Те могат да повредят мода или да причинят несъответствие онлайн.\n\nИскате ли да посетите тази папка сега?",
                "Es wurden W3D-Dateien im Verzeichnis \"Art\\W3D\" gefunden. Sie können den Mod beschädigen oder online zu Unstimmigkeiten führen.\n\nMöchten Sie diesen Ordner jetzt aufrufen?"),
            
            ["W_FoundWndFiles"] = lang => ChooseByLanguage(lang, 
                "Found .wnd files in the \"Window\" directory. They may corrupt the mod.\n\nWould you like to visit this folder now?",
                "Найдены файлы .wnd в каталоге \"Window\". Они могут повредить мод.\n\nХотите посетить эту папку сейчас?",
                "Знайдено файли .wnd в каталозі \"Window\". Вони можуть пошкодити мод.\n\nБажаєте відвідати цю папку зараз?",
                "Намерени .wnd файлове в директорията \"Window\". Те могат да повредят мода.\n\nИскате ли да посетите тази папка сега?",
                "Es wurden WND-Dateien im Verzeichnis \"Window\" gefunden. Sie können den Mod beschädigen.\n\nMöchten Sie diesen Ordner jetzt aufrufen?"),
            
            ["W_FoundStrFiles"] = lang => ChooseByLanguage(lang, 
                "Found .str file(s) in the \"Data\" directory. They may corrupt the mod's text strings.\n\nWould you like to visit this folder now?",
                "Найдены файлы .str в каталоге \"Data\". Они могут повредить текстовые строки мода.\n\nХотите посетить эту папку сейчас?",
                "Знайдено файли .str в каталозі \"Data\". Вони можуть пошкодити текст мода.\n\nБажаєте відвідати цю папку зараз?",
                "Намерени .str файлове в директорията \"Data\". Те могат да повредят текста в играта.\n\nИскате ли да посетите тази папка сега?",
                "Es wurden STR-Dateien im Verzeichnis \"Data\" gefunden. Sie können die Textzeichenfolgen des Mods beschädigen.\n\nMöchten Sie diesen Ordner jetzt aufrufen?"),
            
            ["W_FoundCsfFiles"] = lang => ChooseByLanguage(lang, 
                "Found .csf file(s) in the \"Data\\English\" directory. They may corrupt the mod's text strings.\n\nWould you like to visit this folder now?",
                "Найдены файлы .csf в каталоге \"Data\\English\". Они могут повредить текстовые строки мода.\n\nХотите посетить эту папку сейчас?",
                "Знайдено файли .csf в каталозі \"Data\\English\". Вони можуть пошкодити текст мода.\n\nБажаєте відвідати цю папку зараз?",
                "Намерени .csf файлове в директорията \"Data\\English\". Те могат да повредят текста в играта.\n\nИскате ли да посетите тази папка сега?",
                "Es wurden CSF-Dateien im Verzeichnis \"Data\\English\" gefunden. Sie können die Textzeichenfolgen des Mods beschädigen.\n\nMöchten Sie diesen Ordner jetzt aufrufen?"),
            
            ["W_FoundIniFilesInEnglishFolder"] = lang => ChooseByLanguage(lang, 
                "Found .ini file(s) in the \"Data\\English\" directory. They may corrupt the mod.\n\nWould you like to visit this folder now?",
                "Найдены файлы .ini в каталоге \"Data\\English\". Они могут повредить мод.\n\nХотите посетить эту папку сейчас?",
                "Знайдено файли .ini в каталозі \"Data\\English\". Вони можуть пошкодити мод.\n\nБажаєте відвідати цю папку зараз?",
                "Намерени .ini файлове в директорията \"Data\\English\". Те могат да повредят мода.\n\nИскате ли да посетите тази папка сега?",
                "Es wurden INI-Dateien im Verzeichnis \"Data\\English\" gefunden. Sie können den Mod beschädigen.\n\nMöchten Sie diesen Ordner jetzt aufrufen?"),
            
            ["W_PreferencesMayNotLoad"] = lang => ChooseByLanguage(lang, 
                "Voice, language and hotkey preferences may not load correctly since World Builder is running.\n\nWould you like to start the game anyway?",
                "Настройки голоса, языка и горячих клавиш могут загружаться неправильно, так как World Builder запущен.\n\nВы все равно хотите начать игру?",
                "Налаштування голосу, мови та гарячих клавіш можуть завантажуватися неправильно, оскільки працює World Builder.\n\nВи все-таки хотіли б почати гру?",
                "Предпочитанията за глас, език и горещи клавиши може да не се заредят правилно, тъй като World Builder е стартиран.\n\nИскате ли да стартирате играта все пак?",
                "Stimmen der Einheit, Spielsprache und Hotkey-Einstellungen werden möglicherweise nicht richtig geladen, da World Builder ausgeführt wird.\n\nMöchten Sie das Spiel trotzdem starten?"),
            
            ["W_GenToolNotInstalledOrNotUpToDate"] = lang => ChooseByLanguage(lang, 
                "GenTool is not installed or not up to date. That means you will have a black line behind the Control Bar Pro.\n\nWould you like to visit https://www.gentool.net/ and install the latest version?\nIf not, we recommend you to select a different control bar for its proper display.",
                "GenTool не установлен или не обновлен. Это означает, что вы увидите черную линию за панелью управления Pro.\n\nХотите посетить https://www.gentool.net/ и установить последнюю версию?\nЕсли нет, мы рекомендуем вам выбрать другую панель управления для ее правильного отображения.",
                "GenTool не встановлено або не оновлено. Це означає, що ви побачите чорну лінію позаду Control Bar Pro.\n\nБажаєте відвідати https://www.gentool.net/ і встановити останню версію?\nЯкщо ні, рекомендуємо вибрати іншу панель керування для її належного відображення.",
                "GenTool не е инсталиран или не е обновен. Това означава, че ще виждате черна линия зад Control Bar Pro.\n\nЖелаете ли да посетите https://www.gentool.net/ и да инсталирате последната версия?\nВ противен случай, препоръчваме Ви да изберете друга контролна лента за правилното й изображение.",
                "GenTool ist nicht installiert oder nicht auf dem neuesten Stand. Das bedeutet, dass Sie hinter der Control Bar Pro eine schwarze Linie haben.\n\nMöchten Sie https://www.gentool.net/ besuchen und die neueste Version installieren?\nnWenn nicht, empfehlen wir Ihnen, für die korrekte Anzeige eine andere Steuerleiste auszuwählen."),
            
            ["I_GeneralsAlreadyRunning"] = lang => ChooseByLanguage(lang, "Generals.exe is already running.", "Generals.exe уже запущен.", "Generals.exe вже працює.", "Generals.exe е вече стартиран.", "Generals.exe ist bereits gestartet."),
            
            ["I_Patch1NotFound"] = lang => ChooseByLanguage(lang,
                "Contra X Beta 2 Patch 1 is not installed.\n\nThis patch contains important updates and fixes for Contra X Beta 2.\n\nWould you like to visit the downloads page to get the latest patch?",
                "Contra X Beta 2 Patch 1 не установлен.\n\nЭтот патч содержит важные обновления и исправления для Contra X Beta 2.\n\nХотите ли вы посетить страницу загрузок, чтобы получить последний патч?",
                "Contra X Beta 2 Patch 1 не встановлений.\n\nЦей патч містить важливі оновлення та виправлення для Contra X Beta 2.\n\nЧи хочете ви відвідати сторінку завантажень, щоб отримати останній патч?",
                "Contra X Beta 2 Patch 1 не е инсталиран.\n\nТози патч съдържа важни актуализации и корекции за Contra X Beta 2.\n\nИскате ли да посетите страницата за изтегляне, за да получите най-новия патч?",
                "Contra X Beta 2 Patch 1 ist nicht installiert.\n\nDieser Patch enthält wichtige Updates und Fehlerbehebungen für Contra X Beta 2.\n\nMöchten Sie die Download-Seite besuchen, um den neuesten Patch zu erhalten?")
        };

        public static void GenerateMessageBox(string type, string lang)
        {
            if (FastBodies.TryGetValue(type, out var bodyFactory))
            {
                string body = bodyFactory(lang);
                string caption = GetCaptionForType(type, lang);
                var icon = GetIconForType(type);
                MessageBox.Show(body, caption, MessageBoxButtons.OK, icon);
                return;
            }
        }

        public static string GenerateMessage(string type, string lang)
        {
            if (FastMessages.TryGetValue(type, out var messageFactory))
            {
                return messageFactory(lang);
            }
            return "";
        }
    }
}