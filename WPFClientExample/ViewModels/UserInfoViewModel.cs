using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        KeyValuePair<UserSearchType, string>[]? SearchType { get; set; }
        UserSearchType SelectedSearchType { get; set; }

        ObservableCollection<CharacterInfo>? CharacterInfos { get; set; }
        string SearchText { get; set; }

        CharacterInfoView TabItemCharacterInfoView { get; set; }
        InventoryLogView TabItemInventoryLogView { get; set; }

        IRelayCommand SearchCommand { get; }
        IRelayCommand<CharacterInfo> CharcterSelectionChagedCommand { get; }
    }

    public partial class UserInfoViewModel:ObservableObject, IUserInfoViewModel
    {
        private readonly IGameLogService gameLogService;
        private readonly IServiceProvider serviceProvider;

        [ObservableProperty]
        private KeyValuePair<UserSearchType, string>[]? searchType;

        [ObservableProperty]
        private UserSearchType selectedSearchType;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<CharacterInfo>? characterInfos;


        [ObservableProperty]
        private CharacterInfoView? tabItemCharacterInfoView; 

        [ObservableProperty]
        private InventoryLogView? tabItemInventoryLogView; 


        public UserInfoViewModel(IGameLogService gameLogService, IServiceProvider serviceProvider)
        {
            this.gameLogService = gameLogService;
            this.serviceProvider = serviceProvider;

            Initialize();
        }

        private void Initialize()
        {
            SearchType =
            [
                new(UserSearchType.AccountId, "계정 ID"),
                new(UserSearchType.AccountName, "계정 명")
            ];

            SelectedSearchType = SearchType.First().Key;

            TabItemCharacterInfoView = serviceProvider.GetRequiredService<CharacterInfoView>();
            TabItemInventoryLogView = serviceProvider.GetRequiredService<InventoryLogView>();
        }

        [RelayCommand]
        private void Search()
        {
            try
            {
                CharacterInfos = new ObservableCollection<CharacterInfo>(gameLogService.GetCharacterInfoList(SelectedSearchType, SearchText));
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

    }
}
