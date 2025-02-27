using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models.Billing;
using WPFClientExample.Services;

namespace WPFClientExample.ViewModels
{
    public interface IBillHistoryViewModel
    {
        ObservableCollection<BillHistoryInfo> BillHistoryInfos { get; }
        DateTime SearchStartDate { get; set; }
        DateTime SearchEndDate { get; set; }
        IAsyncRelayCommand SearchCommand { get; }
        KeyValuePair<USER_SEARCH_TYPE, string>[] SearchType { get; }
        USER_SEARCH_TYPE SelectedSearchType { get; set; }
        string SearchText { get; set; }
    }

    public partial class BillHistoryViewModel : ObservableObject, IBillHistoryViewModel, IRecipient<LoginMessage>, IRecipient<TokenCancelMessage>
    {
        private readonly IBillingService billingService;
        private readonly ILocalizationService localizationService;


        [ObservableProperty]
        ObservableCollection<BillHistoryInfo> billHistoryInfos = [];

        [ObservableProperty]
        DateTime searchStartDate;

        [ObservableProperty]
        DateTime searchEndDate;

        [ObservableProperty]
        KeyValuePair<USER_SEARCH_TYPE, string>[] searchType = [];

        [ObservableProperty]
        private USER_SEARCH_TYPE selectedSearchType;

        [ObservableProperty]
        private string searchText = string.Empty;

        private CancellationTokenSource? cancelToken;

        public BillHistoryViewModel(IBillingService billingService, ILocalizationService localizationService)
        {
            this.billingService = billingService;
            this.localizationService = localizationService;
            Initialize();
            SettingMessage();
        }
        private void Initialize()
        {
            SearchType =
            [
                new(USER_SEARCH_TYPE.ID, localizationService.GetString("AccountID")),
                new(USER_SEARCH_TYPE.NAME, localizationService.GetString("AccountName"))
            ];

            SelectedSearchType = SearchType.First().Key;
        }

        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);
            WeakReferenceMessenger.Default.Register<TokenCancelMessage>(this);

        }

        public void Receive(LoginMessage message)
        {
            InitSetting();
        }

        public void Receive(TokenCancelMessage message)
        {
            if (cancelToken != null && !cancelToken.Token.IsCancellationRequested)
            {
                cancelToken?.Cancel();
            }
        }

        private void InitSetting()
        {
            BillHistoryInfos?.Clear();
            SearchStartDate = DateTime.Now.AddDays(-30);
            SearchEndDate = DateTime.Now;
            SelectedSearchType = SearchType.First().Key;
        }

        [RelayCommand]
        private async Task Search()
        {
            try
            {
                BillHistoryInfos?.Clear();
                cancelToken?.Cancel();
                cancelToken = new CancellationTokenSource();
                var token = cancelToken.Token;

                var result = await Task.Run(() =>
                billingService.GetBillHistoryInfo(SelectedSearchType, SearchText, SearchStartDate, SearchEndDate, token)
                );

                Application.Current.Dispatcher.Invoke(() =>
                {
                    BillHistoryInfos = [.. result];
                });
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Task Was Cancelled.");
            }
        }
    }
}
