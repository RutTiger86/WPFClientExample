using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Services;

namespace WPFClientExample
{
    public interface IMainWindowModel
    {
        UserControl? CurrentView { get; set; }

        AuthAccount? LoginAuthUser { get; set; }

        ObservableCollection<TreeViewItem> TreeViewItems { get; }

        MenuItemModel? SelectedMenuItem { get; set; }

        IRelayCommand LogoutCommand { get; }

        IRelayCommand WindowClosedCommand { get; }

        IRelayCommand<int> NavigateToCommand { get; }
    }

    public partial class MainWindowModel : ObservableObject, IMainWindowModel, IRecipient<LoginMessage>
    {
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private UserControl? currentView;

        [ObservableProperty]
        private AuthAccount? loginAuthUser;

        [ObservableProperty]
        public ObservableCollection<TreeViewItem> treeViewItems;

        [ObservableProperty]
        private MenuItemModel? selectedMenuItem;

        public MainWindowModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            TreeViewItems = navigationService.TreeViewItems;
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
            LoginAuthUser = message.Value;
            NavigateTo(0);
        }

        [RelayCommand]
        private void Logout()
        {
            LoginAuthUser = null;
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
