using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFClientExample.Services;

namespace WPFClientExample
{
    public partial class MainVeiwModel :ObservableObject
    {
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private UserControl? currentView;

        [ObservableProperty]
        private bool isAuthenticated = false;


        private const string loginViewName = "Login";

        public MainVeiwModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            CurrentView = navigationService.GetView(loginViewName);
            navigationService.OnViewChanged += view => CurrentView = view;
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
        private void CompleteLogin()
        {
            IsAuthenticated = true;
            navigationService.NavigateTo("Search");
        }

        [RelayCommand]
        private void Logout()
        {
            IsAuthenticated = false; 
            navigationService.NavigateTo("Login");
        }
    }
   
}
