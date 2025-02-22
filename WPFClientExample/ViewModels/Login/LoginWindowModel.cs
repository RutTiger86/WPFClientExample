using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using System;
using System.Windows;
using System.Windows.Controls;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models;
using WPFClientExample.Services;

namespace WPFClientExample.ViewModels.Login
{
    public interface ILoginWindowModel
    {
        string Username { get; set; }
        IRelayCommand<PasswordBox> LoginCommand { get; }
        IRelayCommand WindowClosedCommand { get; }
    }

    public partial class LoginWindowModel : ObservableObject, ILoginWindowModel
    {
        private readonly IAuthService authService;

        [ObservableProperty]
        private string username = "";

        public LoginWindowModel(IAuthService authService)
        {
            this.authService = authService;
        }

        [RelayCommand]
        private void Login(PasswordBox param)
        {

            (bool authResult , UserInfo? user ) = authService.Authenticate(Username, param.Password);
            if (authResult)
            {
                WeakReferenceMessenger.Default.Send(new LoginMessage(user));
            }
            else
            {
                MessageBox.Show("Invalid Username or Password!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Username = string.Empty;
            param.Password = string.Empty;
        }

        [RelayCommand]
        private void WindowClosed()
        {
            WeakReferenceMessenger.Default.Send(new ProgramShutDownMessage(true));
        }
    }
}
