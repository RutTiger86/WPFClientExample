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
        public static readonly DependencyProperty SelectedCharacterProperty =
            DependencyProperty.Register(
        nameof(SelectedCharacter),
                typeof(CharacterInfo),
                typeof(CharacterInfoView),
                new PropertyMetadata(null, OnSelectedCharacterChanged));

        public object? SelectedCharacter
        {
            get => (CharacterInfo?)GetValue(SelectedCharacterProperty);
            set => SetValue(SelectedCharacterProperty, value);

        }

        public CharacterInfoView(ICharacterInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private static void OnSelectedCharacterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CharacterInfoView view && view.DataContext is CharacterInfoViewModel viewModel)
            {
                viewModel.SelectedCharacter = e.NewValue as CharacterInfo;
            }
        }

    }
}
