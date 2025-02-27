using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Repositories;
using WPFClientExample.Views;

namespace WPFClientExample.Services
{
    public interface INavigationService
    {
        ObservableCollection<TreeViewItem> TreeViewItems { get; }
        void NavigateTo(int menuId);

        event Action<UserControl?>? OnViewChanged;

        void InitializeTreeViewItems();
    }

    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMenuRepository menuRepository;

        private readonly Dictionary<int, UserControl> views;
        private Dictionary<int, TreeViewItem> treeViews;

        public ObservableCollection<TreeViewItem> TreeViewItems { get; } = new();


        public event Action<UserControl?>? OnViewChanged;

        public NavigationService(IServiceProvider serviceProvider, IMenuRepository menuRepository)
        {
            this.serviceProvider = serviceProvider;
            this.menuRepository = menuRepository;
            views = new Dictionary<int, UserControl>
            {
                { 2, serviceProvider.GetRequiredService<UserInfoView>() },
                { 5, serviceProvider.GetRequiredService<ChatLogView>() },
                { 6, serviceProvider.GetRequiredService<CcuMonitoringView>() },
                { 8, serviceProvider.GetRequiredService<BillHistoryView>() },
                { 11, serviceProvider.GetRequiredService<ClientSettingsView>() },
            };
            treeViews = [];
        }

        public void InitializeTreeViewItems()
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
            if (menuId == 0)
            {
                var firstView = views.FirstOrDefault();
                ExpandToRoot(treeViews[firstView.Key]);
                treeViews[firstView.Key].IsSelected = true;
                OnViewChanged?.Invoke(firstView.Value);
            }
            else if (views.ContainsKey(menuId))
            {
                ExpandToRoot(treeViews[menuId]);
                treeViews[menuId].IsSelected = true;
                WeakReferenceMessenger.Default.Send(new TokenCancelMessage());
                OnViewChanged?.Invoke(views[menuId]);
            }


        }

        private void ExpandToRoot(TreeViewItem? item)
        {
            while (item != null)
            {
                item.IsExpanded = true;
                item = GetParent(item); // 부모 노드를 찾는 메서드
            }
        }

        private TreeViewItem? GetParent(TreeViewItem item)
        {
            foreach (var parentItem in treeViews.Values)
            {
                if (parentItem.Items.Contains(item))
                {
                    return parentItem;
                }
            }
            return null;
        }
    }
}
