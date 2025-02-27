using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFClientExample.Services
{
    public interface ILocalizationService
    {
        void ChangeLanguage(string culture);
        string GetString(string key);
    }

    public class LocalizationService : ILocalizationService
    {
        private ResourceDictionary resourceDictionary;

        public LocalizationService()
        {
            LoadLanguage("en-US"); // 기본 언어 설정
        }

        public void ChangeLanguage(string culture)
        {
            LoadLanguage(culture);
            OnLanguageChanged?.Invoke();
        }

        public string GetString(string key)
        {
            return resourceDictionary.Contains(key) ? resourceDictionary[key].ToString() : key;
        }

        private void LoadLanguage(string culture)
        {
            string resourcePath = "Resources/Localization/Strings.xaml"; 
            if (culture == "ko-KR")
                resourcePath = "Resources/Localization/Strings.kr.xaml";

            var existingResource = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Strings"));

            if (existingResource != null)
                Application.Current.Resources.MergedDictionaries.Remove(existingResource);

            resourceDictionary = new ResourceDictionary { Source = new Uri(resourcePath, UriKind.Relative) };
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }


        public event Action? OnLanguageChanged;
    }

}
