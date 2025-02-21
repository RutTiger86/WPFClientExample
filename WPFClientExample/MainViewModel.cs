using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFClientExample.Services;
using WPFClientExample.ViewModels;

namespace WPFClientExample
{
    public partial class MainViewModel :ObservableObject
    {
        private readonly INavigationService navigationService;
        private readonly ILoginViewModel loginViewModel;

        [ObservableProperty]
        private UserControl? currentView;

        [ObservableProperty]
        private bool isAuthenticated = false;

        private const string loginViewName = "Login";

        public MainViewModel(INavigationService navigationService, ILoginViewModel loginViewModel)
        {
            this.navigationService = navigationService;
            this.loginViewModel = loginViewModel;

            CurrentView = navigationService.GetView(loginViewName);
            loginViewModel.OnLoginSuccess += HandleLoginSuccess;
            navigationService.OnViewChanged += view => CurrentView = view;
        }
        private void HandleLoginSuccess(bool success)
        {
            if (success)
            {
                IsAuthenticated = true; 
                navigationService.NavigateTo("Search"); 
            }
        }

        [RelayCommand]
        private void NavigateTo(string viewName)
        {
            if (IsAuthenticated)
            {
                navigationService.NavigateTo(viewName);
            }
        }

        [RelayCommand]
        private void Logout()
        {
            IsAuthenticated = false; 
            navigationService.NavigateTo("Login");
        }
    }
   
}
