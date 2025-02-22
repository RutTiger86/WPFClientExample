using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models;
using WPFClientExample.Repositories;
using WPFClientExample.Services;
using WPFClientExample.ViewModels;

namespace WPFClientExample
{
    public partial class MainViewModel :ObservableObject, IRecipient<LoginMessage>
    {
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private UserControl? currentView;

        [ObservableProperty]
        private UserInfo? loginUser;

        public ObservableCollection<TreeViewItem> TreeViewItems => navigationService.TreeViewItems;

        [ObservableProperty]
        private MenuItemModel? selectedMenuItem; // 선택된 메뉴 항목

        
        public MainViewModel(INavigationService navigationService, IMenuRepository menuRepository)
        {
            this.navigationService = navigationService;
            navigationService.OnViewChanged += NavigationService_OnViewChanged;
            SettingMessage();
        }

        private void NavigationService_OnViewChanged(UserControl? obj)
        {
            CurrentView = obj;
        }

        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);
        }

        public void Receive(LoginMessage message)
        {            
            LoginUser = message.Value;
            NavigateTo(0);
        }

        [RelayCommand]
        private void Logout()
        {
            LoginUser = null;
            WeakReferenceMessenger.Default.Send(new LogoutMessage(true));
        }

        [RelayCommand]
        private void WindowClosed()
        {
            WeakReferenceMessenger.Default.Send(new ProgramShutDownMessage(true));
        }

        [RelayCommand]
        private void NavigateTo(int menuId)
        {
            navigationService.NavigateTo(menuId);
        }
    }
   
}
