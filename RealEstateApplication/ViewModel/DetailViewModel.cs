using RealEstateApplication.Model;
using RealEstateApplication.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RealEstateApplication.ViewModel
{
    class DetailViewModel : BaseViewModel
    {
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
        public DetailViewModel()
        {
            // load data
            LoadedUserControlsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // thuê nhà thì k hiện nút vay
                if (BackupListRE.Container.Purchase)
                {
                    VisibilityButtonRent = Visibility.Visible;
                }
                else
                {
                    VisibilityButtonRent = Visibility.Collapsed;
                }

                // đổ dữ liệu vào
                DisplayRE = BackupListRE.Container.ViewRE;
            });

            // nút quay trở lại
            ClickComebackCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var check = BackupListRE.Container;
                var child = new PuchaseUC();
                child.DataContext = new PurchaseViewModel();
                OpenUC.OpenChildUC(child);
            });

            // nút mở giao diện vay tiền
            ClickOpenLoanWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (BackupListRE.Container != null)
                {
                    BackupListRE.Container.PriceLoan = DisplayRE.price;
                }
                else
                {
                    BackupListRE.Container = new Container()
                    {
                        PriceLoan = DisplayRE.price
                    };
                }
                LoanWindow newLoanWindow = new LoanWindow();
                newLoanWindow.DataContext = new LoanViewModel();
                newLoanWindow.ShowDialog();
            });
        }
    }
}
