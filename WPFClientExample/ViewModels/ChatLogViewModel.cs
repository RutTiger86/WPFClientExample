using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Commons.Messages.UserInfo;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Views.UserInfo;
using WPFClientExample.Services;
using System.Windows;
using WPFClientExample.Models.Monitoring;

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
    public partial class ChatLogViewModel:ObservableObject, IChatLogViewModel, IRecipient<LoginMessage>
    {
        private readonly IMonitoringService monitoringService;

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

        public ChatLogViewModel(IMonitoringService monitoringService)
        {
            this.monitoringService = monitoringService;
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
                new(USER_SEARCH_TYPE.ID, "Character ID"),
                new(USER_SEARCH_TYPE.NAME, "Character Name")
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
