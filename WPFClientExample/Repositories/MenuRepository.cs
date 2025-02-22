using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Models;

namespace WPFClientExample.Repositories
{
    public interface IMenuRepository
    {
        ObservableCollection<MenuItemModel> GetMenuItems();
    }
    public class MenuRepository : IMenuRepository
    {
        public ObservableCollection<MenuItemModel> GetMenuItems()
        {
            return new ObservableCollection<MenuItemModel>
            {
                new MenuItemModel { Id = 1, Title = "Search", ViewName = "Search"},
                new MenuItemModel { Id = 2, Title = "Report", ViewName = "Report" },
                new MenuItemModel { Id = 3, Title = "Notifications", ViewName = "NotificationLog" },
                new MenuItemModel
                {
                    Id = 4,
                    Title = "Settings",
                    Children = new ObservableCollection<MenuItemModel>
                    {
                        new MenuItemModel { Id = 5, Title = "General", ViewName = "Settings" },
                        new MenuItemModel { Id = 6, Title = "Admin", ViewName = "Admin" }
                    }
                }
            };
        }

    }
}
