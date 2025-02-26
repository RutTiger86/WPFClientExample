using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
