using System.Collections.ObjectModel;

namespace WPFClientExample.Models.DataBase
{
    public partial class MenuItemModel
    {
        public int Id { get; set; } // 메뉴 ID
        public string Title { get; set; } = string.Empty;  // 메뉴 이름
        public string? ViewName { get; set; }  // 해당하는 View (없으면 하위 메뉴)
        public ObservableCollection<MenuItemModel> Children { get; set; } = new(); // 하위 메뉴 리스트
    }
}
