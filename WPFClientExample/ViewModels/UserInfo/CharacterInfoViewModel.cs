using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Commons.Messages.UserInfo;
using WPFClientExample.Models;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Services;

namespace WPFClientExample.ViewModels.UserInfo
{
    public interface ICharacterInfoViewModel
    {
        CharacterInfo? SelectedCharacter { get; set; }
        CharacterDetailInfo? TargetCharacterDetailInfo { get; set; }
        ObservableCollection<CharacterEquipeedInfo> TargetCharacterEquipeedInfo { get; set; }
        ObservableCollection<ChatLogInfo> TargetChatLogInfo { get; set; }
        ObservableCollection<CharacterQuestInfo> TargetCharacterQuestInfo { get; set; }
        IRelayCommand<long> ItemDetailShowCommand { get; }
    }

    public partial class CharacterInfoViewModel : ObservableObject, ICharacterInfoViewModel, IRecipient<SelectedCharacterMessage>, IRecipient<LoginMessage>
    {
        private readonly IGameLogService gameLogService;
        private readonly ILocalizationService localizationService;

        [ObservableProperty]
        CharacterInfo? selectedCharacter;

        [ObservableProperty]
        CharacterDetailInfo? targetCharacterDetailInfo;

        [ObservableProperty]
        ObservableCollection<CharacterEquipeedInfo> targetCharacterEquipeedInfo = [];

        [ObservableProperty]
        ObservableCollection<ChatLogInfo> targetChatLogInfo = [];

        [ObservableProperty]
        ObservableCollection<CharacterQuestInfo> targetCharacterQuestInfo = [];

        public CharacterInfoViewModel(IGameLogService gameLogService, ILocalizationService localizationService)
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
            SelectedCharacter = null;
            TargetCharacterDetailInfo = null;
            TargetCharacterEquipeedInfo?.Clear();
            TargetChatLogInfo?.Clear();
            TargetCharacterQuestInfo?.Clear();
        }

        public void Receive(SelectedCharacterMessage message)
        {
            SelectedCharacter = message.Value;
            SetCharacterInfoAsync();
        }

        private async void SetCharacterInfoAsync()
        {
            await Task.WhenAll(
                SetCharacterDetailInfo(),
                SetEquippedItemsAsync(),
                SetRecenChatAsync(),
                SetQuestListAsync()
            );
        }

        private async Task SetCharacterDetailInfo()
        {
            if (SelectedCharacter != null)
            {
                TargetCharacterDetailInfo = await Task.Run(() => gameLogService.GetCharacterInfoDetailInfoAsync(SelectedCharacter.CharacterId));
            }
        }

        private async Task SetEquippedItemsAsync()
        {
            if (SelectedCharacter != null)
            {
                TargetCharacterEquipeedInfo = [.. await Task.Run(() => gameLogService.GetCharacterEquipeedInfoAsync(SelectedCharacter.CharacterId))];
            }
        }
        private async Task SetRecenChatAsync()
        {
            if (SelectedCharacter != null)
            {
                TargetChatLogInfo = [.. await Task.Run(() => gameLogService.GetChatLogInfoByCharacterIdAsync(SelectedCharacter.CharacterId))];
            }
        }

        private async Task SetQuestListAsync()
        {
            if (SelectedCharacter != null)
            {
                TargetCharacterQuestInfo = [.. await Task.Run(() => gameLogService.GetCharacterQuestInfoByCharacterIdAsync(SelectedCharacter.CharacterId))];
            }
        }

        [RelayCommand]
        private void ItemDetailShow(long itemID)
        {
            MessageBox.Show(localizationService.GetString("MessageItemDetails"));
        }
    }
}
