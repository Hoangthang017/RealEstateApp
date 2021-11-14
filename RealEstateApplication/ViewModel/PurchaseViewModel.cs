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
    public class PurchaseViewModel : BaseViewModel
    {
        // command mẫu để copy
        public ICommand LoadedMainWindowCommand { get; set; }
        public PurchaseViewModel()
        {
            LoadedMainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
               
            });
        }
    }
}
