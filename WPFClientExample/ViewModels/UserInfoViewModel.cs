using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Commons.Messages.UserInfo;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Services;
using WPFClientExample.Views.UserInfo;

namespace WPFClientExample.ViewModels
{
    public interface IUserInfoViewModel
    {
        KeyValuePair<USER_SEARCH_TYPE, string>[]? SearchType { get; set; }
        USER_SEARCH_TYPE SelectedSearchType { get; set; }
        ObservableCollection<CharacterInfo>? CharacterInfos { get; }
        string SearchText { get; }
        CharacterInfoView TabItemCharacterInfoView { get; set; }
        InventoryLogView TabItemInventoryLogView { get; set; }
        AccountInfo? TargetAccountInfo { get; set; }
        IAsyncRelayCommand SearchCommand { get; }
        IRelayCommand AccountRestrictionCommand { get; }
        IRelayCommand<CharacterInfo> CharcterSelectionChagedCommand { get; }
    }

    public partial class UserInfoViewModel : ObservableObject, IUserInfoViewModel, IRecipient<LoginMessage>
    {
        private readonly IGameLogService gameLogService;
        private readonly IServiceProvider serviceProvider;
        private readonly ILocalizationService localizationService;

        [ObservableProperty]
        private KeyValuePair<USER_SEARCH_TYPE, string>[]? searchType;

        [ObservableProperty]
        private USER_SEARCH_TYPE selectedSearchType;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<CharacterInfo>? characterInfos;

        [ObservableProperty]
        private CharacterInfoView? tabItemCharacterInfoView;

        [ObservableProperty]
        private InventoryLogView? tabItemInventoryLogView;

        [ObservableProperty]
        private AccountInfo? targetAccountInfo;

        public UserInfoViewModel(IGameLogService gameLogService, IServiceProvider serviceProvider, ILocalizationService localizationService)
        {
            this.gameLogService = gameLogService;
            this.serviceProvider = serviceProvider;
            this.localizationService = localizationService;

            SettingMessage();
            Initialize();
        }
        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);
        }

        public void Receive(LoginMessage message)
        {
            CharacterInfos?.Clear();
            TargetAccountInfo = null;
            SearchText = string.Empty;
            SelectedSearchType = USER_SEARCH_TYPE.ID;
        }

        private void Initialize()
        {
            SearchType =
            [
                new(USER_SEARCH_TYPE.ID, localizationService.GetString("AccountID")),
                new(USER_SEARCH_TYPE.NAME, localizationService.GetString("AccountName"))
            ];

            SelectedSearchType = SearchType.First().Key;

            TabItemCharacterInfoView = serviceProvider.GetRequiredService<CharacterInfoView>();
            TabItemInventoryLogView = serviceProvider.GetRequiredService<InventoryLogView>();
        }

        [RelayCommand]
        private async Task Search()
        {
            try
            {
                await Task.Run(() =>
                {
                    TargetAccountInfo = gameLogService.GetAccountInfo(SelectedSearchType, SearchText);

                    if (TargetAccountInfo != null)
                    {
                        CharacterInfos = [.. gameLogService.GetCharacterInfoList(TargetAccountInfo.AccountId)];
                    }
                }
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, localizationService.GetString("ErrorCaption"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void CharcterSelectionChaged(CharacterInfo selectedValue)
        {
            if (selectedValue is null) return;
            WeakReferenceMessenger.Default.Send(new SelectedCharacterMessage(selectedValue));
        }

        [RelayCommand]
        private void AccountRestriction()
        {
            if (TargetAccountInfo != null)
            {
                MessageBox.Show(localizationService.GetString("MessageAccountRestriction"));
            }
            else
            {
                MessageBox.Show(localizationService.GetString("MessageAccountIsNull"));
            }
        }

    }
}
