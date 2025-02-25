using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Commons.Messages.UserInfo;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Services;
using WPFClientExample.Views.UserInfo;

namespace WPFClientExample.ViewModels
{
    public interface IUserInfoViewModel
    {
        KeyValuePair<USER_SEARCH_TYPE, string>[]? SearchType { get; set; }
        USER_SEARCH_TYPE SelectedSearchType { get; set; }
        ObservableCollection<CharacterInfo>? CharacterInfos { get; set; }
        string SearchText { get; set; }
        CharacterInfoView TabItemCharacterInfoView { get; set; }
        InventoryLogView TabItemInventoryLogView { get; set; }
        AccountInfo? TargetAccountInfo { get; set; }

        IAsyncRelayCommand SearchCommand { get; }
        IRelayCommand AccountRestrictionCommand { get; }
        IRelayCommand<CharacterInfo> CharcterSelectionChagedCommand { get; }
    }

    public partial class UserInfoViewModel:ObservableObject, IUserInfoViewModel, IRecipient<LoginMessage>
    {
        private readonly IGameLogService gameLogService;
        private readonly IServiceProvider serviceProvider;

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

        public UserInfoViewModel(IGameLogService gameLogService, IServiceProvider serviceProvider)
        {
            this.gameLogService = gameLogService;
            this.serviceProvider = serviceProvider;

            SettingMessage();
            Initialize();
        }
        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);
        }

        public void Receive(LoginMessage message)
        {
            CharacterInfos = null;
            TargetAccountInfo = null;
            SearchText = string.Empty;
            SelectedSearchType = USER_SEARCH_TYPE.Id;
        }

        private void Initialize()
        {
            SearchType =
            [
                new(USER_SEARCH_TYPE.Id, "Account ID"),
                new(USER_SEARCH_TYPE.Name, "Account Name")
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
                TargetAccountInfo =  await gameLogService.GetAccountInfoAsync(SelectedSearchType, SearchText);

                if(TargetAccountInfo != null)
                {
                    var charcterInfo = await gameLogService.GetCharacterInfoListAsync(TargetAccountInfo.AccountId);
                    CharacterInfos = new ObservableCollection<CharacterInfo>(charcterInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SearchError", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void CharcterSelectionChaged(CharacterInfo selectedValue)
        {
            if(selectedValue!= null)
            {
                WeakReferenceMessenger.Default.Send(new SelectedCharacterMessage(selectedValue));
            }
        }

        [RelayCommand]
        private void AccountRestriction()
        {
            if (TargetAccountInfo != null)
            {
                MessageBox.Show($"[ AccountId : {TargetAccountInfo.AccountId}]{Environment.NewLine}The account restriction view function is under development.");
            }
            else
            {
                MessageBox.Show($"Account Is Null, please Account Search First");
            }
        }

    }
}
