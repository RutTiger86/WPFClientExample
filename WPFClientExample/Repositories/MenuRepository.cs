using System.Collections.ObjectModel;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Services;
using WPFClientExample.Views;

namespace WPFClientExample.Repositories
{
    public interface IMenuRepository
    {
        ObservableCollection<MenuItemModel> GetMenuItems();
    }
    public class MenuRepository : IMenuRepository
    {
        private readonly ILocalizationService localizationService;

        public MenuRepository(ILocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        public ObservableCollection<MenuItemModel> GetMenuItems()
        {

            return
            [
                new MenuItemModel
                {
                    Id = 1,
                    Title = localizationService.GetString("MenuGameLogs"),
                    Children =
                    [
                        new() { Id = 2, Title = localizationService.GetString("MenuUserInfo"), ViewName =  typeof(UserInfoView).Name },
                    ]
                },
                new MenuItemModel
                {
                    Id = 4,
                    Title = localizationService.GetString("MenuMonitoring"),
                    Children =
                    [
                        new () { Id = 5, Title = localizationService.GetString("MenuChatLog"), ViewName = typeof(ChatLogView).Name },
                        new () { Id = 6, Title = localizationService.GetString("MenuCCU"), ViewName = typeof(CcuMonitoringView).Name}
                    ]
                },
                new MenuItemModel
                {
                    Id = 7,
                    Title = localizationService.GetString("MenuBilling"),
                    Children =
                    [
                        new() { Id = 8, Title = localizationService.GetString("MenuBillHistory"), ViewName = typeof(BillHistoryView).Name},
                    ]
                },new MenuItemModel
                {
                    Id = 10,
                    Title = localizationService.GetString("MenuSetting"),
                    Children =
                    [
                        new() { Id = 11, Title = localizationService.GetString("MenuClientSetting"), ViewName = typeof(ClientSettingsView).Name},
                    ]
                }
            ];
        }
    }
}
