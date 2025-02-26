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
                    settings = new ClientSettings(); // JSON �Ľ� ���� �� �⺻�� ���
                }
            }
        }

        private static void SaveConfiguration()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(ConfigFilePath, json);
        }

        // ������ ��������
        public static ClientLanguage GetLanguage() => settings.Language;
        public static ClientTheme GetTheme() => settings.Theme;
        public static string GetFontFamily() => settings.FontFamily;

        // ������ ���� �� ����
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
