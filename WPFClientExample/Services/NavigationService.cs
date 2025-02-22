using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFClientExample.Models;
using WPFClientExample.Repositories;
using WPFClientExample.Views;

namespace WPFClientExample.Services
{
    public interface INavigationService
    {
        ObservableCollection<TreeViewItem> TreeViewItems { get; }
        void NavigateTo(int menuId);

        event Action<UserControl?>? OnViewChanged;
    }

    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMenuRepository menuRepository;

        private readonly Dictionary<int, UserControl> views;
        private Dictionary<int, TreeViewItem> treeViews ;

        public ObservableCollection<TreeViewItem> TreeViewItems { get; } = new();


        public event Action<UserControl?>? OnViewChanged;

        public NavigationService(IServiceProvider serviceProvider, IMenuRepository menuRepository)
        {
            this.serviceProvider = serviceProvider;
            this.menuRepository = menuRepository;
            views = new Dictionary<int, UserControl>
            {
                { 1, serviceProvider.GetRequiredService<SearchView>() },
                { 2, serviceProvider.GetRequiredService<ReportView>() },
                { 3, serviceProvider.GetRequiredService<SettingsView>() },
                { 5, serviceProvider.GetRequiredService<NotificationLogView>() },
                { 6, serviceProvider.GetRequiredService<AdminView>() }
            };
            treeViews = [];
            InitializeTreeViewItems();
        }

        private void InitializeTreeViewItems()
        {
            var menuItems = menuRepository.GetMenuItems();
            TreeViewItems.Clear();
            foreach (var menuItem in menuItems)
            {
                TreeViewItems.Add(CreateTreeViewItem(menuItem));
            }
        }

        private TreeViewItem CreateTreeViewItem(MenuItemModel menuItem)
        {
            var treeViewItem = new TreeViewItem
            {
                Header = menuItem.Title,
                Tag = menuItem.Id               
            };
            treeViews.Add(menuItem.Id, treeViewItem);
            foreach (var child in menuItem.Children)
            {
                treeViewItem.Items.Add(CreateTreeViewItem(child));
            }

            return treeViewItem;
        }

        public void NavigateTo(int menuId)
        {
            if(menuId == 0)
            {
                treeViews[views.FirstOrDefault().Key].IsSelected = true;
                OnViewChanged?.Invoke(views.FirstOrDefault().Value);
            } else if (views.ContainsKey(menuId))
            {
                treeViews[menuId].IsSelected = true;
                OnViewChanged?.Invoke(views[menuId]);
            }
        }
    }
}
