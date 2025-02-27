using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Media;
using WPFClientExample.Commons.Statics;
using WPFClientExample.Services;
using static WPFClientExample.Commons.Enums.SettingEnum;

namespace WPFClientExample.ViewModels
{
    public interface IClientSettingsViewModel
    {
        List<KeyValuePair<ClientLanguage, string>> Language { get; }
        List<KeyValuePair<ClientTheme, string>> Theme { get; }
        List<FontFamily> FontFamilies { get; }
        ClientTheme SelectedTheme { get; set; }
        ClientLanguage SelectedLanguage { get; set; }
        FontFamily SelectedFontFamily { get; set; }
        IRelayCommand SaveCommand { get; }
    }

    public partial class ClientSettingsViewModel : ObservableObject, IClientSettingsViewModel
    {
        private readonly ILocalizationService localizationService;

        [ObservableProperty]
        List<KeyValuePair<ClientLanguage, string>> language = [];

        [ObservableProperty]
        List<KeyValuePair<ClientTheme, string>> theme = [];

        [ObservableProperty]
        List<FontFamily> fontFamilies = [];

        [ObservableProperty]
        ClientLanguage selectedLanguage;

        [ObservableProperty]
        ClientTheme selectedTheme;

        [ObservableProperty]
        FontFamily selectedFontFamily = new();

        public ClientSettingsViewModel(ILocalizationService localizationService)
        {
            this.localizationService = localizationService;
            Initialize();
        }

        private void Initialize()
        {
            Language =
            [
                new(ClientLanguage.ENGLISH, "English"),
                new(ClientLanguage.KOREAN, "한국어")
            ];

            Theme =
            [
                new(ClientTheme.DEFAULT, "Default"),
                new(ClientTheme.DARK, "Dark")
            ];

            FontFamilies = [.. Fonts.SystemFontFamilies];

            SelectedLanguage = JsonConfigurationManager.GetLanguage();
            SelectedTheme = JsonConfigurationManager.GetTheme();
            string savedFont = JsonConfigurationManager.GetFontFamily();
            SelectedFontFamily = FontFamilies.FirstOrDefault(f => f.Source == savedFont) ?? new FontFamily("Arial");
        }

        [RelayCommand]
        private void Save()
        {
            ApplySettings();
        }

        private void ApplySettings()
        {
            JsonConfigurationManager.SetLanguage(SelectedLanguage);
            JsonConfigurationManager.SetTheme(SelectedTheme);
            JsonConfigurationManager.SetFontFamily(SelectedFontFamily.Source);

            localizationService.ChangeLanguage(SelectedLanguage == ClientLanguage.ENGLISH ? "en-US" : "ko-KR");

            var dictionary = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source?.OriginalString == "Resources/Styles.xaml");

            if (dictionary != null)
            {
                dictionary["GlobalFont"] = SelectedFontFamily;
            }

            var themeDictionary = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme"));

            if (themeDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(themeDictionary);
            }

            string newTheme = SelectedTheme == ClientTheme.DARK ? "Resources/Themes/DarkTheme.xaml" : "Resources/Themes/DefaultTheme.xaml";
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(newTheme, UriKind.Relative) });
        }
    }
}
