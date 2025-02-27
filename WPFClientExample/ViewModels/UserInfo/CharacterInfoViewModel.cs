using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
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
        List<CharacterEquipeedInfo>? TargetCharacterEquipeedInfo { get; set; }
        List<ChatLogInfo>? TargetChatLogInfo { get; set; }
        List<CharacterQuestInfo>? TargetCharacterQuestInfo { get; set; }

        IRelayCommand<long> ItemDetailShowCommand { get; }
    }

    public partial class CharacterInfoViewModel:ObservableObject, ICharacterInfoViewModel, IRecipient<SelectedCharacterMessage>, IRecipient<LoginMessage>
    {
        private readonly IGameLogService gameLogService;
        private readonly ILocalizationService localizationService;

        [ObservableProperty]
        private CharacterInfo? selectedCharacter;

        [ObservableProperty]
        private CharacterDetailInfo? targetCharacterDetailInfo;

        [ObservableProperty]
        private List<CharacterEquipeedInfo>? targetCharacterEquipeedInfo;

        [ObservableProperty]
        List<ChatLogInfo>? targetChatLogInfo;

        [ObservableProperty]
        List<CharacterQuestInfo>? targetCharacterQuestInfo;

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
            TargetCharacterEquipeedInfo = null;
            TargetChatLogInfo = null;
        }

        public void Receive(SelectedCharacterMessage message)
        {
            SelectedCharacter = message.Value;
            SetCharacterInfoAsync();
        }

        private async void SetCharacterInfoAsync()
        {
            // Task.WhenAll을 이용하여 병렬 처리
            await Task.WhenAll(
                SetCharacterDetailInfo(),
                SetEquippedItemsAsync(),
                SetRecenChatAsync(),
                SetQuestListAsync()
            );
        }

        private async Task SetCharacterDetailInfo()
        {
            if (SelectedCharacter!= null)
            {
                TargetCharacterDetailInfo = await Task.Run(() => gameLogService.GetCharacterInfoDetailInfoAsync(SelectedCharacter.CharacterId)
                ).ConfigureAwait(false);
            }
        }

        private async Task SetEquippedItemsAsync()
        {
            if (SelectedCharacter != null)
            {
                TargetCharacterEquipeedInfo = await Task.Run(() => gameLogService.GetCharacterEquipeedInfoAsync(SelectedCharacter.CharacterId)
                ).ConfigureAwait(false);
            }
        }
        private async Task SetRecenChatAsync()
        {
            if (SelectedCharacter != null)
            {
                TargetChatLogInfo = await Task.Run(() => gameLogService.GetChatLogInfoByCharacterIdAsync(SelectedCharacter.CharacterId)
                ).ConfigureAwait(false);
            }
        }

        private async Task SetQuestListAsync()
        {
            if (SelectedCharacter != null)
            {
                TargetCharacterQuestInfo = await Task.Run(() => gameLogService.GetCharacterQuestInfoByCharacterIdAsync(SelectedCharacter.CharacterId)
                ).ConfigureAwait(false);
            }
        }

        [RelayCommand] 
        private  void ItemDetailShow(long itemID)
        {
            MessageBox.Show(localizationService.GetString("MessageItemDetails"));
        }
    }
}
