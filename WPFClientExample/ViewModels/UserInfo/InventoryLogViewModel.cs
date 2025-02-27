using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Commons.Messages.UserInfo;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Services;

namespace WPFClientExample.ViewModels.UserInfo
{
    public interface IInventoryLogViewModel
    {
        DateTime SearchStartDate { get; set; }
        DateTime SearchEndDate { get; set; }
        ObservableCollection<InventoryHistoryLogInfo> TargetInventoryHistory { get; }
        IAsyncRelayCommand SearchCommand { get; }

    }
    public partial class InventoryLogViewModel : ObservableObject, IInventoryLogViewModel, IRecipient<LoginMessage>, IRecipient<SelectedCharacterMessage>
    {
        private readonly IGameLogService gameLogService;
        private readonly ILocalizationService localizationService;

        [ObservableProperty]
        DateTime searchStartDate;

        [ObservableProperty]
        DateTime searchEndDate;

        [ObservableProperty]
        ObservableCollection<InventoryHistoryLogInfo> targetInventoryHistory = [];

        private CharacterInfo? selectedCharacterInfo;

        public InventoryLogViewModel(IGameLogService gameLogService, ILocalizationService localizationService)
        {
            this.gameLogService = gameLogService;
            this.localizationService = localizationService;
            SettingMessage();
        }

        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<SelectedCharacterMessage>(this);
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);
        }

        public void Receive(LoginMessage message)
        {
            InitData();
        }

        public void Receive(SelectedCharacterMessage message)
        {
            InitData();
            selectedCharacterInfo = message.Value;
        }

        public void InitData()
        {
            SearchStartDate = DateTime.Now.AddDays(-1);
            SearchEndDate = DateTime.Now.AddDays(1); ;
            TargetInventoryHistory?.Clear();
            selectedCharacterInfo = null;
        }

        [RelayCommand]
        private async Task Search()
        {
            try
            {
                if (selectedCharacterInfo != null)
                {
                    TargetInventoryHistory?.Clear();
                    TargetInventoryHistory = [.. await gameLogService.GetInventoryHistoryLogAsync(selectedCharacterInfo.CharacterId, SearchStartDate, SearchEndDate)];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, localizationService.GetString("ErrorCaption"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
