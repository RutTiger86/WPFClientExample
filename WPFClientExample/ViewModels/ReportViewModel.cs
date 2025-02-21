using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.ViewModels
{
    public partial class ReportViewModel:ObservableObject
    {
        [ObservableProperty]
        private string title = "Report Page";
    }
}
