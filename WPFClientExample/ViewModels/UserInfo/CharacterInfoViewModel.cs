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
            Task.FromResult(SetCharacterInfoAsync());
        }

        private async Task SetCharacterInfoAsync()
        {
            if (SelectedCharacter != null)
            {
                var characterDetailTask = Task.Run(() => gameLogService.GetCharacterInfoDetailInfo(SelectedCharacter.CharacterId));
                var characterEquipeedsTask = Task.Run(() => gameLogService.GetCharacterEquipeedInfo(SelectedCharacter.CharacterId));
                var chatLogInfosTask = Task.Run(() => gameLogService.GetChatLogInfoByCharacterId(SelectedCharacter.CharacterId));
                var characterQuestsTask = Task.Run(() => gameLogService.GetCharacterQuestInfoByCharacterId(SelectedCharacter.CharacterId));

                await Task.WhenAll(
                    characterDetailTask,
                    characterEquipeedsTask,
                    chatLogInfosTask,
                    characterQuestsTask
                );

                Application.Current.Dispatcher.Invoke(() =>
                {
                    TargetCharacterDetailInfo = characterDetailTask.Result;
                    TargetCharacterEquipeedInfo = [.. characterEquipeedsTask.Result];
                    TargetChatLogInfo = [.. chatLogInfosTask.Result];
                    TargetCharacterQuestInfo = [.. characterQuestsTask.Result];
                });
            }
        }

        [RelayCommand]
        private void ItemDetailShow(long itemID)
        {
            MessageBox.Show(localizationService.GetString("MessageItemDetails"));
        }
    }
}
