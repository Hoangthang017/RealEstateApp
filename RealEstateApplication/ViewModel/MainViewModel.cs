using RealEstateApplication.Model;
using RealEstateApplication.UserControls;
using System.Collections.Generic;
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
        public ICommand ClickDeleteFilterCommand { get; set; }

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

        //Danh cho bạn
        private string _GioiTinh;
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }

        //Danh cho bạn
        private int _NamSinh;
        public int NamSinh { get => _NamSinh; set { _NamSinh = value; OnPropertyChanged(); } }

        //Danh cho bạn
        private List<string> _ListGioiTinh;
        public List<string> ListGioiTinh { get => _ListGioiTinh; set { _ListGioiTinh = value; OnPropertyChanged(); } }

        //Danh cho bạn
        private List<string> _ListLoaiHinh;
        public List<string> ListLoaiHinh { get => _ListLoaiHinh; set { _ListLoaiHinh = value; OnPropertyChanged(); } }
        //Danh cho bạn
        private string _LoaiHinh;
        public string LoaiHinh { get => _LoaiHinh; set { _LoaiHinh = value; OnPropertyChanged(); } }

        //Danh cho bạn
        private List<string> _ListSoNguoi;
        public List<string> ListSoNguoi { get => _ListSoNguoi; set { _ListSoNguoi = value; OnPropertyChanged(); } }
        //Danh cho bạn
        private string _SoNguoi;
        public string SoNguoi { get => _SoNguoi; set { _SoNguoi = value; OnPropertyChanged(); } }

        //Danh cho bạn
        private double _MucLuong;
        public double MucLuong { get => _MucLuong; set { _MucLuong = value; OnPropertyChanged(); } }


        // phương thức khởi tạo
        public MainViewModel()
        {

            // các commnad
            LoadedMainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // 1 lần duy nhất
                OpenUC.BackupGridOpenUC = (p as Grid);

                // truyền dữ liệu
                var child = new Homepage();
                child.DataContext = new HomepageViewModel();
                OpenUC.OpenChildUC(new Homepage());

                //
                Filter.CreateListData();

                //
                isMoTab = Visibility.Visible;
                isDongTab = Visibility.Collapsed;
                isDanhChoBan = Visibility.Collapsed;

                ListGioiTinh = new List<string>()
                {
                    "Nam","Nữ"
                };

                ListLoaiHinh = new List<string>()
                {
                    "Mua Nhà","Thuê Nhà"
                };

                ListSoNguoi = new List<string>()
                {
                    "1","2","3","4","5+"
                };
            });
            OpenHomepageUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                var child = new Homepage();
                child.DataContext = new HomepageViewModel();
                OpenUC.OpenChildUC(child);
            });
            ClickDeleteFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                NhapTinhThanhPho = null;
                NhapQuanHuyen = null;
                NhapXaPhuong = null;
                NhapSoNha = null;

                GioiTinh = null;
                NamSinh = 0;  
                LoaiHinh = null;     
                SoNguoi=null;
                MucLuong=0;

            });
            OpenPurchaseUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                var child = new PuchaseUC();
                child.DataContext = new PurchaseViewModel();
                OpenUC.OpenChildUC(child); 
            });
            OpenRentUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                var child = new RentUC();
                child.DataContext = new RentViewModel();
                OpenUC.OpenChildUC(new RentUC()); 
            });
            OpenProjectUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                var child = new ProjectUC();
                child.DataContext = new ProjectViewModel();
                OpenUC.OpenChildUC(child); 
            });
            MoDanhChoBanUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                passData.Clear();

                passData.passTinhThanhPho = NhapTinhThanhPho;
                passData.passQuanHuyen = NhapQuanHuyen;
                passData.passXaHuyen = NhapXaPhuong;
                passData.passSoNha = NhapSoNha;

                passData.GioiTinh = GioiTinh;
                passData.NamSinh = NamSinh;
                passData.LoaiHinh = LoaiHinh;
                passData.SoNguoi = SoNguoi;
                passData.MucLuong = MucLuong;

                OpenUC.BackupGridOpenUC = p as Grid;
                var child = new DeXuatUC();
                child.DataContext = new DeXuatViewModel();
                OpenUC.OpenChildUC(child);
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
