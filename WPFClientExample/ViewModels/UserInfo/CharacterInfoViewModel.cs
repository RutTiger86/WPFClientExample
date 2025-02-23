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

namespace WPFClientExample.ViewModels.UserInfo
{
    public interface ICharacterInfoViewModel
    {
        CharacterInfo? SelectedCharacter { get; set; }
    }

    public partial class CharacterInfoViewModel:ObservableObject, ICharacterInfoViewModel, IRecipient<SelectedCharacterMessage>
    {
        [ObservableProperty]
        private CharacterInfo? selectedCharacter;

        public CharacterInfoViewModel()
        {
            SettingMessage();
        }

        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<SelectedCharacterMessage>(this);

        }

        public void Receive(SelectedCharacterMessage message)
        {
            MessageBox.Show(message.Value?.CharacterName);
        }

        partial void OnSelectedCharacterChanged(CharacterInfo? value)
        {
            if (value != null)
            {
                MessageBox.Show($"SelectedCharacter changed: {value.CharacterName}");
            }
        }

    }
}
