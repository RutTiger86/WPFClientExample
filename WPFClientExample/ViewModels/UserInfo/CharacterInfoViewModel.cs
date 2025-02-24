using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFClientExample.Commons.Messages.UserInfo;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Services;

namespace WPFClientExample.ViewModels.UserInfo
{
    public interface ICharacterInfoViewModel
    {
        CharacterInfo? SelectedCharacter { get; set; }

        CharacterDetailInfo? TargetCharacterDetailInfo { get; set; }
    }

    public partial class CharacterInfoViewModel:ObservableObject, ICharacterInfoViewModel, IRecipient<SelectedCharacterMessage>
    {
        private readonly IGameLogService gameLogService;

        [ObservableProperty]
        private CharacterInfo? selectedCharacter;

        [ObservableProperty]
        private CharacterDetailInfo? targetCharacterDetailInfo;

        public CharacterInfoViewModel(IGameLogService gameLogService)
        {
            this.gameLogService = gameLogService;
            SettingMessage();
        }

        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<SelectedCharacterMessage>(this);

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
                TargetCharacterDetailInfo = await gameLogService.GetCharacterInfoDetailInfoAsync(SelectedCharacter.CharacterId);
            }
        }

        private async Task SetEquippedItemsAsync()
        {

        }
        private async Task SetRecenChatAsync()
        {

        }

        private async Task SetQuestListAsync()
        {

        }
    }
}
