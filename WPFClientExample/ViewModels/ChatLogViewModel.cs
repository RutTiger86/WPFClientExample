using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models;
using WPFClientExample.Services;

namespace WPFClientExample.ViewModels
{
    public interface IChatLogViewModel
    {
        KeyValuePair<USER_SEARCH_TYPE, string>[]? SearchType { get; set; }
        USER_SEARCH_TYPE SelectedSearchType { get; set; }
        string SearchText { get; set; }
        DateTime SearchStartDate { get; set; }
        DateTime SearchEndDate { get; set; }
        List<ChatLogInfo>? TargetChatLogInfo { get; set; }
        IAsyncRelayCommand SearchCommand { get; }

    }
    public partial class ChatLogViewModel : ObservableObject, IChatLogViewModel, IRecipient<LoginMessage>
    {
        private readonly IMonitoringService monitoringService;
        private readonly ILocalizationService localizationService;

        [ObservableProperty]
        private KeyValuePair<USER_SEARCH_TYPE, string>[]? searchType;

        [ObservableProperty]
        private USER_SEARCH_TYPE selectedSearchType;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        DateTime searchStartDate;

        [ObservableProperty]
        DateTime searchEndDate;

        [ObservableProperty]
        List<ChatLogInfo>? targetChatLogInfo;

        public ChatLogViewModel(IMonitoringService monitoringService, ILocalizationService localizationService)
        {
            this.monitoringService = monitoringService;
            this.localizationService = localizationService;
            Initialize();
            SettingMessage();
        }

        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);
        }

        public void Receive(LoginMessage message)
        {
            SearchStartDate = DateTime.Now.AddDays(-1);
            SearchEndDate = DateTime.Now.AddDays(1); ;
            SelectedSearchType = SearchType.First().Key;
            SearchText = string.Empty;
            TargetChatLogInfo = null;
        }

        private void Initialize()
        {
            SearchType =
            [
                new(USER_SEARCH_TYPE.ID,  localizationService.GetString("CharacterID")),
                new(USER_SEARCH_TYPE.NAME, localizationService.GetString("CharacterName"))
            ];

            SelectedSearchType = SearchType.First().Key;
        }

        [RelayCommand]
        private async Task Search()
        {
            try
            {
                TargetChatLogInfo = null;
                TargetChatLogInfo = await Task.Run(() => monitoringService.GetChatLogInfosAsync(SelectedSearchType, SearchText, SearchStartDate, SearchEndDate)
                ).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, localizationService.GetString("ErrorCaption"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
