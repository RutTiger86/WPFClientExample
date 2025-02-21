using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using System.Windows.Controls;
using WPFClientExample.Services;

namespace WPFClientExample.ViewModels
{
    public interface ILoginViewModel
    {
        string Username { get; set; }
        IRelayCommand<PasswordBox> LoginCommand { get; }
        IRelayCommand<PasswordBox> LoadedCommand { get; }
        event Action<bool>? OnLoginSuccess; // 로그인 성공 시 MainViewModel에서 처리
    }

    public partial class LoginViewModel : ObservableObject, ILoginViewModel
    {
        private readonly IAuthService authService;

        [ObservableProperty]
        private string username = "";

        public event Action<bool>? OnLoginSuccess;

        public LoginViewModel(IAuthService authService)
        {
            this.authService = authService;
        }

        [RelayCommand]
        private void Login(PasswordBox param)
        {
           
            if (authService.Authenticate(Username, param.Password)) // ✅ Password 값이 정상적으로 업데이트됨
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                OnLoginSuccess?.Invoke(true);
            }
            else
            {
                MessageBox.Show("Invalid Username or Password!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void Loaded(PasswordBox param)
        {
            Username = string.Empty;
            param.Password = string.Empty;
        }
    }
}
