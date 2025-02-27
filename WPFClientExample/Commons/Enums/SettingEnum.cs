using System.Text.Json.Serialization;

namespace WPFClientExample.Commons.Enums
{
    public class SettingEnum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ClientTheme
        {
            DEFAULT,
            DARK
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ClientLanguage
        {
            ENGLISH,
            KOREAN
        }
    }
}
