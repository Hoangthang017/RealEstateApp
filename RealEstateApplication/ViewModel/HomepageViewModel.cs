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

        private List<string> _ListDistrict;
        public List<string> ListDistrict { get => _ListDistrict; set { _ListDistrict = value; OnPropertyChanged(); } }

        private List<string> _ListWard;
        public List<string> ListWard { get => _ListWard; set { _ListWard = value; OnPropertyChanged(); } }

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

        private string _DisplayDistrict;
        public string DisplayDistrict { get => _DisplayDistrict; set { _DisplayDistrict = value; OnPropertyChanged(); } }

        private string _DisplayWard;
        public string DisplayWard { get => _DisplayWard; set { _DisplayWard = value; OnPropertyChanged(); } }

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
        public ICommand SelectionChangedDistrictCommand { get; set; }
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

                VisibleGridMoreFilter = Visibility.Collapsed;
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


            // sự kiện thay đổi lựu chọn loại bất động sản
            SelectionChangedDistrictCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(DisplayDistrict) == false)
                {
                    var newListWard = new List<string>();
                    var _District = DataProvider.Ins.DB.devvn_quanhuyen.Where(x => x.name.Contains(DisplayDistrict) == true).FirstOrDefault();
                    if (_District != null)
                    {
                        var _Wards = DataProvider.Ins.DB.devvn_xaphuongthitran.Where(x => x.maqh == _District.maqh).ToList();
                        foreach (var war in _Wards)
                        {
                            newListWard.Add(war.nameXa.Replace(war.typeXa, ""));
                        }
                        ListWard = newListWard;
                    }
                    
                }
            });


            // sự kiện thay đổi lựu chọn thành phố
            SelectionChangedCityCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FilterListViewRE();
                if (string.IsNullOrEmpty(DisplayCity) == false)
                {
                    var newListDistrict = new List<string>();
                    var check = DataProvider.Ins.DB.devvn_tinhthanhpho.ToList()[0];
                    string chooseTP = "";
                    if (DisplayCity == " Hà Nội")
                    {
                        chooseTP = "hà nội";
                    }
                    else if (DisplayCity == " Bà Rịa Vũng Tàu")
                    {
                        chooseTP = "vũng tàu";
                    }
                    else
                    {
                        chooseTP = DisplayCity.ToLower();
                    }
                    var currentTP = DataProvider.Ins.DB.devvn_tinhthanhpho.Where(x => x.nameTP.ToLower().Contains(chooseTP) == true).FirstOrDefault();
                    if (currentTP != null)
                    {
                        var _District = DataProvider.Ins.DB.devvn_quanhuyen.Where(x => x.matp == currentTP.matp).ToList();
                        foreach (var district in _District)
                        {
                            if (district.typeQH != "Quận")
                            {
                                newListDistrict.Add(district.name.Replace(district.typeQH, ""));
                            }
                            else
                            {
                                newListDistrict.Add(district.name);
                            }
                        }
                        ListDistrict = newListDistrict;
                    }
                   
                }
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
                    DisplayTypeRE = DisplayTypeRE,
                    DisplayDistrict = DisplayDistrict,
                    DisplayWard = DisplayWard
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
