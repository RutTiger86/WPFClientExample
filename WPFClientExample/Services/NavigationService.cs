using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFClientExample.Views;

namespace WPFClientExample.Services
{
    public interface INavigationService
    {
        void NavigateTo(string viewName);
        UserControl? GetCurrentView();

        UserControl? GetView(string viewName);
        
        event Action<UserControl?>? OnViewChanged;
    }

    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Dictionary<string, UserControl> views;
        private UserControl? currentView;

        public event Action<UserControl?>? OnViewChanged;

        public NavigationService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            views = new Dictionary<string, UserControl>
            {
                { "Login", serviceProvider.GetRequiredService<LoginView>() },
                { "Search", serviceProvider.GetRequiredService<SearchView>() },
                { "Detail", serviceProvider.GetRequiredService<DetailView>() },
                { "Report", serviceProvider.GetRequiredService<ReportView>() },
                { "Settings", serviceProvider.GetRequiredService<SettingsView>() },
                { "NotificationLog", serviceProvider.GetRequiredService<NotificationLogView>() },
                { "Admin", serviceProvider.GetRequiredService<AdminView>() }
            };

        }

        public UserControl? GetView(string viewName)
        {
            return views[viewName];
        }

        public void NavigateTo(string viewName)
        {
            if (views.ContainsKey(viewName))
            {
                currentView = views[viewName];
                OnViewChanged?.Invoke(currentView);
            }
        }

        public UserControl? GetCurrentView()
        {
            return currentView;
        }

    }
}
