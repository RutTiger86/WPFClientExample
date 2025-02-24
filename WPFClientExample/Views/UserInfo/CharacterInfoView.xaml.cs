using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFClientExample.Models.GameLog;
using WPFClientExample.ViewModels.UserInfo;

namespace WPFClientExample.Views.UserInfo
{
    /// <summary>
    /// CharacterInfoView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CharacterInfoView : UserControl
    {
        public CharacterInfoView(ICharacterInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
