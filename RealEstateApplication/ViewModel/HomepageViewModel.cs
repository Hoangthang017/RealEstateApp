using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using RealEstateApplication.UserControls;

namespace RealEstateApplication.ViewModel
{
    public class HomepageViewModel : BaseViewModel
    {
        // command để copy cho dễ 
        public ICommand LoadedMainWindowCommand { get; set; }
        public HomepageViewModel()
        {
            LoadedMainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });
        }
    }
}
