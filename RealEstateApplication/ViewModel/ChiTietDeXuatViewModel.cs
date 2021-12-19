using RealEstateApplication.Model;
using RealEstateApplication.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RealEstateApplication.ViewModel
{
    class ChiTietDeXuatViewModel : BaseViewModel
    {
        // lưu dữ liệu back up
        private Container _BackupContainer;
        public Container BackupContainer { get => _BackupContainer; set { _BackupContainer = value; OnPropertyChanged(); } }

        // list data
        private RealEstateInfo _DisplayRE;
        public RealEstateInfo DisplayRE { get => _DisplayRE; set { _DisplayRE = value; OnPropertyChanged(); } }

        // visiblle vay tiền
        private Visibility _VisibilityButtonRent;
        public Visibility VisibilityButtonRent { get => _VisibilityButtonRent; set { _VisibilityButtonRent = value; OnPropertyChanged(); } }

        // command
        public ICommand LoadedUserControlsCommand { get; set; }
        public ICommand ClickComebackCommand { get; set; }
        public ICommand ClickOpenLoanWindowCommand { get; set; }
        public ChiTietDeXuatViewModel()
        {
            // load data
            LoadedUserControlsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // thuê nhà thì k hiện nút vay
                //if (passDataDetailRE.cellRealEstateInfo.Purchase)
                //{
                //    VisibilityButtonRent = Visibility.Visible;
                //}
                //else
                //{
                //    VisibilityButtonRent = Visibility.Collapsed;
                //}

                // đổ dữ liệu vào
                DisplayRE = passDataDetailRE.cellRealEstateInfo; 
            });

            // nút quay trở lại
            ClickComebackCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                OpenUC.OpenChildUC(new DeXuatUC());
            });

            // nút mở giao diện vay tiền
            ClickOpenLoanWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                BackupListRE.Container = new Container()
                {
                    PriceLoan = DisplayRE.price
                };
                LoanWindow newLoanWindow = new LoanWindow();
                newLoanWindow.Show();
            });
        }
    }
}
