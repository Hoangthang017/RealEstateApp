using RealEstateApplication.Model;
using RealEstateApplication.UserControls;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RealEstateApplication.ViewModel
{
    public class HomepageViewModel : BaseViewModel
    {
        // list data
        private List<RealEstateInfo> _ListViewRE;
        public List<RealEstateInfo> ListViewRE { get => _ListViewRE; set { _ListViewRE = value; OnPropertyChanged(); } }

        // list data
        private List<RealEstateInfo> _ListViewQueryRE;
        public List<RealEstateInfo> ListViewQueryRE { get => _ListViewQueryRE; set { _ListViewQueryRE = value; OnPropertyChanged(); } }

        private List<string> _ListTypeSellRE;
        public List<string> ListTypeSellRE { get => _ListTypeSellRE; set { _ListTypeSellRE = value; OnPropertyChanged(); } }

        private List<string> _ListLocation;
        public List<string> ListLocation { get => _ListLocation; set { _ListLocation = value; OnPropertyChanged(); } }

        private List<string> _ListPrice;
        public List<string> ListPrice { get => _ListPrice; set { _ListPrice = value; OnPropertyChanged(); } }

        private List<string> _ListArea;
        public List<string> ListArea { get => _ListArea; set { _ListArea = value; OnPropertyChanged(); } }

        private Visibility _VisibleGridMoreFilter;
        public Visibility VisibleGridMoreFilter { get => _VisibleGridMoreFilter; set { _VisibleGridMoreFilter = value; OnPropertyChanged(); } }

        // choose purchase or rent
        // background
        private Brush _BackgroudPurchase;
        public Brush BackgroudPurchase { get => _BackgroudPurchase; set { _BackgroudPurchase = value; OnPropertyChanged(); } }

        private Brush _BackgroudRent;
        public Brush BackgroudRent { get => _BackgroudRent; set { _BackgroudRent = value; OnPropertyChanged(); } }

        // foreground
        private Brush _ForegroundPurchase;
        public Brush ForegroundPurchase { get => _ForegroundPurchase; set { _ForegroundPurchase = value; OnPropertyChanged(); } }

        private Brush _ForegroudRent;
        public Brush ForegroudRent { get => _ForegroudRent; set { _ForegroudRent = value; OnPropertyChanged(); } }

        // mode
        private string _Mode;
        public string Mode { get => _Mode; set { _Mode = value; OnPropertyChanged(); } }

        // display
        private string _Query;
        public string Query { get => _Query; set { _Query = value; OnPropertyChanged(); } }

        private string _DisplayTypeRE;
        public string DisplayTypeRE { get => _DisplayTypeRE; set { _DisplayTypeRE = value; OnPropertyChanged(); } }

        private string _DisplayCity;
        public string DisplayCity { get => _DisplayCity; set { _DisplayCity = value; OnPropertyChanged(); } }

        private string _DisplayPrice;
        public string DisplayPrice { get => _DisplayPrice; set { _DisplayPrice = value; OnPropertyChanged(); } }

        private string _DisplayArea;
        public string DisplayArea { get => _DisplayArea; set { _DisplayArea = value; OnPropertyChanged(); } }

        // command để copy cho dễ 
        public ICommand LoadedUserControlsCommand { get; set; }
        public ICommand ClickMoreFilterCommand { get; set; }
        public ICommand ClickChangeEnableButtonCommand { get; set; }
        public ICommand SelectionChangedTypeRECommand { get; set; }
        public ICommand TextChangedQueryCommand { get; set; }
        public ICommand SelectionChangedCityCommand { get; set; }
        public ICommand SelectionChangedPriceCommand { get; set; }
        public ICommand SelectionChangedAreaCommand { get; set; }
        public ICommand ClickSearchRECommand { get; set; }
        public HomepageViewModel()
        {
            // load user controls
            LoadedUserControlsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // load list data
                ListTypeSellRE = new List<string>();
                ListTypeSellRE = Filter.ListTypeSellRE;
                ListLocation = new List<string>();
                ListLocation = Filter.ListAddressRE;
                ListPrice = new List<string>();
                ListPrice = Filter.ListPriceRE;
                ListArea = new List<string>();
                ListArea = Filter.ListAreaRE;

                // hide more filter
                VisibleGridMoreFilter = Visibility.Collapsed;

                // enable purchase button
                var bc = new BrushConverter();
                BackgroudPurchase = (Brush)bc.ConvertFromString("#E5000000");
                BackgroudRent = (Brush)bc.ConvertFromString("#FFF5F5DC");

                ForegroundPurchase = (Brush)bc.ConvertFromString("#DDFFFFFF");
                ForegroudRent = (Brush)bc.ConvertFromString("#DD000000");

                Mode = "Bán";
                ListViewRE = Filter.FilterTypeRE(Mode, DisplayTypeRE);
                ListViewQueryRE = ListViewRE;
            });

            // click mở filter thêm
            ClickMoreFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (VisibleGridMoreFilter == Visibility.Collapsed)
                {
                    VisibleGridMoreFilter = Visibility.Visible;
                }
                else
                {
                    VisibleGridMoreFilter = Visibility.Collapsed;
                }
            });

            // click đổi trạng thái mua hoặc thuê
            ClickChangeEnableButtonCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var bc = new BrushConverter();
                if (BackgroudPurchase.ToString() == "#E5000000")
                {
                    BackgroudRent = (Brush)bc.ConvertFromString("#E5000000");
                    BackgroudPurchase = (Brush)bc.ConvertFromString("#FFF5F5DC");
                    ForegroudRent = (Brush)bc.ConvertFromString("#DDFFFFFF");
                    ForegroundPurchase = (Brush)bc.ConvertFromString("#DD000000");
                    Mode = "Cho thuê";
                    ListTypeSellRE = Filter.ListTypeRentRE;
                }
                else
                {
                    BackgroudPurchase = (Brush)bc.ConvertFromString("#E5000000");
                    BackgroudRent = (Brush)bc.ConvertFromString("#FFF5F5DC");
                    ForegroundPurchase = (Brush)bc.ConvertFromString("#DDFFFFFF");
                    ForegroudRent = (Brush)bc.ConvertFromString("#DD000000");
                    Mode = "Bán";
                    ListTypeSellRE = Filter.ListTypeSellRE;
                }
            });

            // sự kiện thay đổi lựu chọn loại bất động sản
            SelectionChangedTypeRECommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListViewRE = Filter.FilterTypeRE(Mode, DisplayTypeRE);
                ListViewQueryRE = ListViewRE;
            });

            // sự kiện thay đổi lựu chọn thành phố
            SelectionChangedCityCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FilterListViewRE();
            });

            // sự kiện thay đổi lựu chọn giá
            SelectionChangedPriceCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FilterListViewRE();
            });

            // sự kiện thay đổi lựu chọn diện tích
            SelectionChangedAreaCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FilterListViewRE();
            });

            // bấm nút tìm kiếm
            ClickSearchRECommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                BackupListRE.Container = new Container()
                {
                    BackupViewRE = ListViewQueryRE,
                    BackupViewQueryRE = ListViewQueryRE,
                    DisplayArea = DisplayArea,
                    DisplayCity = DisplayCity,
                    DisplayPrice = DisplayPrice,
                    DisplayFilter = DisplayPrice,
                    DisplayTypeRE = DisplayTypeRE
                };
                UserControl child = null;
                if (Mode == "Bán")
                {
                    child = new PuchaseUC();
                }
                else
                {
                    child = new RentUC();
                }
                OpenUC.OpenChildUC(child);
            });
        }

        private void FilterListViewRE()
        {
            ListViewQueryRE = ListViewRE;

            if (string.IsNullOrEmpty(DisplayCity) == false)
            {
                ListViewQueryRE = Filter.FilterCity(DisplayCity, ListViewQueryRE.ToList());
            }

            if (string.IsNullOrEmpty(DisplayPrice) == false)
            {
                ListViewQueryRE = Filter.FilterPrice(ListPrice.IndexOf(DisplayPrice), ListViewQueryRE.ToList());
            }

            if (string.IsNullOrEmpty(DisplayArea) == false)
            {
                ListViewQueryRE = Filter.FilterArea(ListArea.IndexOf(DisplayArea), ListViewQueryRE.ToList());
            }

            if (string.IsNullOrEmpty(Query) == false)
            {
                ListViewQueryRE = ListViewQueryRE.Where(x => x.title.ToLower().Contains(Query) == true).ToList();
            }
        }
    }
}
