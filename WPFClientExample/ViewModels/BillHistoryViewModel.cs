using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models.Billing;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Services;

namespace WPFClientExample.ViewModels
{
    public interface IBillHistoryViewModel
    {
        List<BillHistoryInfo>? BillHistoryInfos { get; set; }
        DateTime SearchStartDate { get; set; }
        DateTime SearchEndDate { get; set; }
        IAsyncRelayCommand SearchCommand { get; }
        KeyValuePair<USER_SEARCH_TYPE, string>[]? SearchType { get; set; }
         USER_SEARCH_TYPE SelectedSearchType { get; set; }
        string SearchText { get; set; }
    }

    public partial class BillHistoryViewModel:ObservableObject, IBillHistoryViewModel, IRecipient<LoginMessage>
    {
        private readonly IBillingService billingService;

        [ObservableProperty]
        List<BillHistoryInfo>? billHistoryInfos;

        [ObservableProperty]
        DateTime searchStartDate;

        [ObservableProperty]
        DateTime searchEndDate;

        [ObservableProperty]
        KeyValuePair<USER_SEARCH_TYPE, string>[]? searchType;

        [ObservableProperty]
        private USER_SEARCH_TYPE selectedSearchType;

        [ObservableProperty]
        private string searchText = string.Empty;

        public BillHistoryViewModel(IBillingService billingService)
        {
            this.billingService = billingService;
            Initialize();
            SettingMessage();
        }
        private void Initialize()
        {
            SearchType =
            [
                new(USER_SEARCH_TYPE.ID, "Account ID"),
                new(USER_SEARCH_TYPE.NAME, "Account Name")
            ];

            SelectedSearchType = SearchType.First().Key;
        }

        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);
        }

        public void Receive(LoginMessage message)
        {
            InitSetting();
        }

        private void InitSetting()
        {
            BillHistoryInfos = null;
            SearchStartDate = DateTime.Now.AddDays(-30);
            SearchEndDate = DateTime.Now;
            SelectedSearchType = SearchType.First().Key;
        }

        [RelayCommand]
        private async Task Search()
        {
            BillHistoryInfos = null;
            BillHistoryInfos = await Task.Run(() =>
            billingService.GetBillHistoryInfoAsync(SelectedSearchType, SearchText, SearchStartDate, SearchEndDate)
            ).ConfigureAwait(false);
        }
    }
}
