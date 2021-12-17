using RealEstateApplication.Model;
using RealEstateApplication.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

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
                OpenUC.BackupGridOpenUC = (p as Grid);

                // truyền dữ liệu
                OpenUC.OpenChildUC(new Homepage());

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
