using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using System;
using System.Windows;
using System.Windows.Controls;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Services;

namespace WPFClientExample.ViewModels.Login
{
    public interface ILoginWindowModel
    {
        string AuthUserId { get; set; }
        IRelayCommand<PasswordBox> LoginCommand { get; }
        IRelayCommand WindowClosedCommand { get; }
    }

    public partial class LoginWindowModel : ObservableObject, ILoginWindowModel
    {
        private readonly IAuthService authService;
        private readonly ILocalizationService localizationService;

        [ObservableProperty]
        private string authUserId = "";

        public LoginWindowModel(IAuthService authService, ILocalizationService localizationService)
        {
            this.authService = authService;
            this.localizationService = localizationService;
        }

        [RelayCommand]
        private void Login(PasswordBox param)
        {

            (bool authResult , AuthAccount? user ) = authService.Authenticate(AuthUserId, param.Password);
            if (authResult)
            {
                WeakReferenceMessenger.Default.Send(new LoginMessage(user));
            }
            else
            {
                MessageBox.Show(localizationService.GetString("MessageLogInFailed"), localizationService.GetString("MessageLogInFailedCaption"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            AuthUserId = string.Empty;
            param.Password = string.Empty;
        }

        [RelayCommand]
        private void WindowClosed()
        {
            WeakReferenceMessenger.Default.Send(new ProgramShutDownMessage(true));
        }
    }
}
