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
    public class RentViewModel : BaseViewModel
    {
        //command mẫu đễ copy
        public ICommand LoadedMainWindowCommand { get; set; }
        public RentViewModel()
        {
            LoadedMainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
            });
        }
    }
}
