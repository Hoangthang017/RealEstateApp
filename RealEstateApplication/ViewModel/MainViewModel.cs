using RealEstateApplication.Model;
using RealEstateApplication.UserControls;
using System.Windows;
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
        public ICommand MoIconCommand { get; set; }
        public ICommand DongIconCommand { get; set; }
        public ICommand MoDanhChoBanUCCommand { get; set; }

        //Mở đề xuất theo khoảng cách
        private Visibility _ismotab;
        public Visibility isMoTab { get => _ismotab; set { _ismotab = value; OnPropertyChanged(); } }

        //Đóng đề xuất theo khoảng cách
        private Visibility _isdongtab;
        public Visibility isDongTab { get => _isdongtab; set { _isdongtab = value; OnPropertyChanged(); } }

        //Danh cho bạn
        private Visibility _isdanhchoban;
        public Visibility isDanhChoBan { get => _isdanhchoban; set { _isdanhchoban = value; OnPropertyChanged(); } }

        //Danh cho bạn
        private string _nhaptinhthanhpho;
        public string NhapTinhThanhPho { get => _nhaptinhthanhpho; set { _nhaptinhthanhpho = value; OnPropertyChanged(); } }

        //public string NhapTinhThanhPho;

        //Danh cho bạn
        private string _nhapquanhuyen;
        public string NhapQuanHuyen { get => _nhapquanhuyen; set { _nhapquanhuyen = value; OnPropertyChanged(); } }

        //Danh cho bạn
        private string _nhapxaphuong;
        public string NhapXaPhuong { get => _nhapxaphuong; set { _nhapxaphuong = value; OnPropertyChanged(); } }

        //Danh cho bạn
        private string _nhapSoNha;
        public string NhapSoNha { get => _nhapSoNha; set { _nhapSoNha = value; OnPropertyChanged(); } }

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

                //
                isMoTab = Visibility.Visible;
                isDongTab = Visibility.Collapsed;
                isDanhChoBan = Visibility.Collapsed;
            });

            OpenHomepageUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenUC.OpenChildUC(new Homepage()); });
            OpenPurchaseUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenUC.OpenChildUC(new PuchaseUC()); });
            OpenRentUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenUC.OpenChildUC(new RentUC()); });
            OpenProjectUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenUC.OpenChildUC(new ProjectUC()); });
            MoDanhChoBanUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                passData.passTinhThanhPho = NhapTinhThanhPho;
                passData.passQuanHuyen = NhapQuanHuyen;
                passData.passXaHuyen = NhapXaPhuong;
                passData.passSoNha = NhapSoNha;
                OpenUC.OpenChildUC(new DeXuatUC());
                isMoTab = Visibility.Visible;
                isDongTab = Visibility.Collapsed;
                isDanhChoBan = Visibility.Collapsed;
            });

            //
            MoIconCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                isMoTab = Visibility.Collapsed;
                isDongTab = Visibility.Visible;
                isDanhChoBan = Visibility.Visible;
            });

            DongIconCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                isMoTab = Visibility.Visible;
                isDongTab = Visibility.Collapsed;
                isDanhChoBan = Visibility.Collapsed;
            });
        }
    }
}
