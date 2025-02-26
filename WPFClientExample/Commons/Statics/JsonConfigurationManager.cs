using System;
using System.IO;
using System.Text.Json;
using static WPFClientExample.Commons.Enums.SettingEnum;

namespace WPFClientExample.Commons.Statics
{
    public class ClientSettings
    {
        public ClientLanguage Language { get; set; } = ClientLanguage.ENGLISH;
        public ClientTheme Theme { get; set; } = ClientTheme.DEFAULT;
        public string FontFamily { get; set; } = "Arial";
    }
    public static class JsonConfigurationManager
    {
        private static readonly string ConfigFilePath = "appsettings.json";
        private static ClientSettings settings;

        static JsonConfigurationManager()
        {
            LoadConfiguration();
        }

        private static void LoadConfiguration()
        {
            if (!File.Exists(ConfigFilePath))
            {
                settings = new ClientSettings();
                SaveConfiguration();
            }
            else
            {
                try
                {
                    string json = File.ReadAllText(ConfigFilePath);
                    settings = JsonSerializer.Deserialize<ClientSettings>(json) ?? new ClientSettings();
                }
                catch
                {
                    settings = new ClientSettings(); // JSON 파싱 오류 시 기본값 사용
                }
            }
        }

        private static void SaveConfiguration()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(ConfigFilePath, json);
        }

        // 설정값 가져오기
        public static ClientLanguage GetLanguage() => settings.Language;
        public static ClientTheme GetTheme() => settings.Theme;
        public static string GetFontFamily() => settings.FontFamily;

        // 설정값 변경 후 저장
        public static void SetLanguage(ClientLanguage language)
        {
            settings.Language = language;
            SaveConfiguration();
        }

        public static void SetTheme(ClientTheme theme)
        {
            settings.Theme = theme;
            SaveConfiguration();
        }

        public static void SetFontFamily(string fontFamily)
        {
            settings.FontFamily = fontFamily;
            SaveConfiguration();
        }
    }
}
