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
    public class MainViewModel : BaseViewModel
    {
        // command 
        public ICommand LoadedMainWindowCommand { get; set; }
        public ICommand OpenHomepageUCCommand { get; set; }
        public ICommand OpenPurchaseUCCommand { get; set; }
        public ICommand OpenRentUCCommand { get; set; }
        public ICommand OpenProjectUCCommand { get; set; }

        // phương thức khởi tạo
        public MainViewModel()
        {   

            // các commnad
            LoadedMainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenUC(p as Grid, new Homepage());
            });

            OpenHomepageUCCommand = new RelayCommand<object>((p) => { return true; }, (p) =>{OpenUC(p as Grid, new Homepage());});
            OpenPurchaseUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenUC(p as Grid, new PuchaseUC()); });
            OpenRentUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenUC(p as Grid, new RentUC()); });
            OpenProjectUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenUC(p as Grid, new ProjectUC()); });
        }

        // hàm mở user controls
        void OpenUC(Grid gridContent, UserControl uc)
        {
            gridContent.Children.Clear();
            gridContent.Children.Add(uc);
        }
    }
}
