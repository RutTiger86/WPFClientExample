using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Models.DataBase;

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
            return
            [
                new MenuItemModel
                {
                    Id = 1,
                    Title = "Game Logs",
                    Children =
                    [
                        new() { Id = 2, Title = "User Info", ViewName = "UserInfo" },
                    ]
                },
                new MenuItemModel
                {
                    Id = 4,
                    Title = "Monitoring",
                    Children =
                    [
                        new () { Id = 5, Title = "ChatLog", ViewName = "ChatLog" },
                        new () { Id = 6, Title = "CCU", ViewName = "CCUMonitoring" }
                    ]
                },
                new MenuItemModel
                {
                    Id = 7,
                    Title = "Billing",
                    Children =
                    [
                        new() { Id = 8, Title = "Bill History", ViewName = "BillHistory" },
                    ]
                },new MenuItemModel
                {
                    Id = 10,
                    Title = "Setting",
                    Children =
                    [
                        new() { Id = 11, Title = "Client Setting", ViewName = "Setting" },
                    ]
                }
            ];
        }
    }
}
