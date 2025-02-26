using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Commons.Statics;
using WPFClientExample.Views;
using static WPFClientExample.Commons.Enums.SettingEnum;

namespace WPFClientExample.ViewModels
{
    public interface IClientSettingsViewModel
    {
        List<KeyValuePair<ClientLanguage, string>> Language { get; set; }
        List<KeyValuePair<ClientTheme, string>> Theme { get; set; }
        List<FontFamily> FontFamilies { get; set; }

        ClientTheme SelectedTheme { get; set; }

        ClientLanguage SelectedLanguage { get; set; }

        FontFamily SelectedFontFamily { get; set; }

        IRelayCommand SaveCommand { get; }
    }

    public partial class ClientSettingsViewModel:ObservableObject, IClientSettingsViewModel
    {
        [ObservableProperty]
        List<KeyValuePair<ClientLanguage, string>> language;
        [ObservableProperty]
        List<KeyValuePair<ClientTheme, string>> theme;
        [ObservableProperty]
        List<FontFamily> fontFamilies;

        [ObservableProperty]
        ClientLanguage selectedLanguage;
        [ObservableProperty]
        ClientTheme selectedTheme;
        [ObservableProperty]
        FontFamily selectedFontFamily;


        [ObservableProperty]
        ObservableCollection<KeyValuePair<String, UserControl>> categories;


        public ClientSettingsViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            Language  =
            [
                new(ClientLanguage.ENGLISH, "English"),
                new(ClientLanguage.KOREAN, "한국어")
            ];

            Theme =
            [
                new(ClientTheme.DEFAULT, "Default"),
                new(ClientTheme.DARK, "Dark")
            ];

            FontFamilies = Fonts.SystemFontFamilies.ToList();

            // 저장된 설정을 불러오기
            SelectedLanguage = JsonConfigurationManager.GetLanguage();
            SelectedTheme = JsonConfigurationManager.GetTheme();
            string savedFont = JsonConfigurationManager.GetFontFamily();
            SelectedFontFamily = FontFamilies.FirstOrDefault(f => f.Source == savedFont) ?? new FontFamily("Arial");


        }

        [RelayCommand]
        private void Save()
        {
            JsonConfigurationManager.SetLanguage(SelectedLanguage);
            JsonConfigurationManager.SetTheme(SelectedTheme);
            JsonConfigurationManager.SetFontFamily(SelectedFontFamily.Source);

            ApplySettings();
        }

        private void ApplySettings()
        {
            // 글로벌 폰트 적용
            var dictionary = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source?.OriginalString == "Resources/Styles.xaml");
            if (dictionary != null)
            {
                dictionary["GlobalFont"] = SelectedFontFamily;
            }

            var themeDictionary = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme"));

            if (themeDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(themeDictionary);
            }

            // 테마 변경 적용
            string newTheme = SelectedTheme == ClientTheme.DARK ? "Resources/Themes/DarkTheme.xaml" : "Resources/Themes/DefaultTheme.xaml";
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(newTheme, UriKind.Relative) });
        }
    }
}
