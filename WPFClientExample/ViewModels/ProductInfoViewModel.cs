using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.ViewModels
{   
    public interface IProductInfoViewModel
    {

    }

    public partial class ProductInfoViewModel:ObservableObject, IProductInfoViewModel
    {
    }
}
