using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using RealEstateApplication.Model;
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
                // 1 lần duy nhất
                OpenUC.GridOpenUC = (p as Grid);

                // truyền dữ liệu
                var newHomePage = new Homepage();
                var datacontext = (newHomePage.DataContext as HomepageViewModel);
                datacontext.Query = "Test query";
                OpenUC.OpenChildUC(newHomePage);

                //
                Filter.CreateListData();
            });

            OpenHomepageUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenUC.OpenChildUC(new Homepage()); });
            OpenPurchaseUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenUC.OpenChildUC(new PuchaseUC()); });
            OpenRentUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenUC.OpenChildUC(new RentUC()); });
            OpenProjectUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenUC.OpenChildUC(new ProjectUC()); });
        }
    }
}
