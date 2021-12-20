using RealEstateApplication.Model;
using RealEstateApplication.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RealEstateApplication.ViewModel
{
    public class RentViewModel : BaseViewModel
    {
        #region khai báo các biến
        // list data
        private ObservableCollection<RealEstateInfo> _ListViewRE;
        public ObservableCollection<RealEstateInfo> ListViewRE { get => _ListViewRE; set { _ListViewRE = value; OnPropertyChanged(); } }

        // list data
        private ObservableCollection<RealEstateInfo> _ListViewQueryRE;
        public ObservableCollection<RealEstateInfo> ListViewQueryRE { get => _ListViewQueryRE; set { _ListViewQueryRE = value; OnPropertyChanged(); } }

        private List<string> _ListTypeSellRE;
        public List<string> ListTypeSellRE { get => _ListTypeSellRE; set { _ListTypeSellRE = value; OnPropertyChanged(); } }

        private List<string> _ListLocation;
        public List<string> ListLocation { get => _ListLocation; set { _ListLocation = value; OnPropertyChanged(); } }

        private List<string> _ListPrice;
        public List<string> ListPrice { get => _ListPrice; set { _ListPrice = value; OnPropertyChanged(); } }

        private List<string> _ListArea;
        public List<string> ListArea { get => _ListArea; set { _ListArea = value; OnPropertyChanged(); } }

        private List<string> _ListFilter;
        public List<string> ListFilter { get => _ListFilter; set { _ListFilter = value; OnPropertyChanged(); } }

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

        private string _DisplaySquareRoot;
        public string DisplaySquareRoot { get => _DisplaySquareRoot; set { _DisplaySquareRoot = value; OnPropertyChanged(); } }

        private string _DisplayFilter;
        public string DisplayFilter { get => _DisplayFilter; set { _DisplayFilter = value; OnPropertyChanged(); } }

        private RealEstateInfo _DisplayRE;
        public RealEstateInfo DisplayRE { get => _DisplayRE; set { _DisplayRE = value; OnPropertyChanged(); } }

        #endregion

        #region khai báo command
        public ICommand LoadedUserControlsCommand { get; set; }
        public ICommand SelectionChangedCityCommand { get; set; }
        public ICommand SelectionChangedPriceCommand { get; set; }
        public ICommand SelectionChangedAreaCommand { get; set; }
        public ICommand SelectionChangedTypeRECommand { get; set; }
        public ICommand ClickSearchRECommand { get; set; }
        public ICommand SelectionChangedFilterCommand { get; set; }
        public ICommand SelectedCellsChangedRECommand { get; set; }
        public ICommand DeleteFilterCommand { get; set; }
        #endregion

        public RentViewModel()
        {
            #region cài đặt command
            // load user control
            LoadedUserControlsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // create 
                Reset();

                // list filter
                ListFilter = new List<string>()
                {
                    "Mới Nhất",
                    "Giá Cao Nhất",
                    "Giá Thấp Nhất"
                };
                // mũ bình phương
                DisplaySquareRoot = "\u00b2";

                // load list data
                ListTypeSellRE = new List<string>();
                ListTypeSellRE = Filter.ListTypeRentRE;
                ListLocation = new List<string>();
                ListLocation = Filter.ListAddressRE;
                ListPrice = new List<string>();
                ListPrice = Filter.ListPriceRentRE;
                ListArea = new List<string>();
                ListArea = Filter.ListAreaRE;

                // load data
                if (BackupListRE.Container != null)
                {
                    ListViewRE = new ObservableCollection<RealEstateInfo>(BackupListRE.Container.BackupViewRE);
                    ListViewQueryRE = new ObservableCollection<RealEstateInfo>(BackupListRE.Container.BackupViewQueryRE);
                    DisplayArea = BackupListRE.Container.DisplayArea;
                    DisplayCity = BackupListRE.Container.DisplayCity;
                    DisplayPrice = BackupListRE.Container.DisplayPrice;
                    DisplayFilter = BackupListRE.Container.DisplayFilter;
                    DisplayTypeRE = BackupListRE.Container.DisplayTypeRE;
                    BackupListRE.Clear();
                }

                if (ListViewRE == null || ListViewRE.Count == 0)
                {
                    ListViewRE = new ObservableCollection<RealEstateInfo>(DataProvider.Ins.DB.RealEstateInfoes.Where(x => x.type.Contains("Cho thuê ") == true).ToList());
                    DisplayTypeRE = null;
                    DisplayPrice = null;
                    DisplayArea = null;
                    DisplayCity = null;
                    Query = null;
                    ListViewQueryRE = ListViewRE;
                }

            });

            // sự kiện thay đổi lựu chọn loại bất động sản
            SelectionChangedTypeRECommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //if (_Backup == false)
                //{
                //    DisplayFilter = null;
                //    DisplayCity = null;
                //    DisplayArea = null;
                //    DisplayPrice = null;
                //}
                //else
                //{
                //    _Backup = false;
                //}
                if (string.IsNullOrEmpty(DisplayTypeRE) == false)
                {
                    ListViewRE = new ObservableCollection<RealEstateInfo>(Filter.FilterTypeRE("Cho thuê", DisplayTypeRE));
                    ListViewQueryRE = ListViewRE;
                    FilterListViewRE();
                }
            });

            // sự kiện thay đổi lựu chọn thành phố
            DeleteFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DisplayTypeRE = null;
                DisplayFilter = null;
                DisplayCity = null;
                DisplayArea = null;
                DisplayPrice = null;
                ListViewRE = new ObservableCollection<RealEstateInfo>(DataProvider.Ins.DB.RealEstateInfoes.Where(x => x.type.Contains("Cho thuê") == true).ToList());
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

            // sự kiện tìm kiếm
            ClickSearchRECommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FilterListViewRE();
            });

            // sự kiện tìm kiếm 11/16/2021 3:38:00 PM
            SelectionChangedFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FilterListViewRE();
            });

            // sự kiện thay đổi cell datagrid
            SelectedCellsChangedRECommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DisplayRE != null)
                {
                    var newContainer = new Container()
                    {
                        Purchase = false,
                        BackupViewRE = ListViewRE.ToList(),
                        BackupViewQueryRE = ListViewQueryRE.ToList(),
                        DisplayArea = DisplayArea,
                        DisplayCity = DisplayCity,
                        DisplayPrice = DisplayPrice,
                        DisplayFilter = DisplayPrice,
                        DisplayTypeRE = DisplayTypeRE,
                        ViewRE = DisplayRE
                    };
                    BackupListRE.Container = newContainer;
                    var child = new DetailUC();
                    child.DataContext = new DetailViewModel();
                    OpenUC.OpenChildUC(child);
                }
            });

            #endregion
        }

        // function filter
        private void FilterListViewRE()
        {
            ListViewQueryRE = ListViewRE;

            if (string.IsNullOrEmpty(DisplayCity) == false)
            {
                ListViewQueryRE = new ObservableCollection<RealEstateInfo>(Filter.FilterCity(DisplayCity, ListViewQueryRE.ToList()));
            }

            if (string.IsNullOrEmpty(DisplayPrice) == false)
            {
                ListViewQueryRE = new ObservableCollection<RealEstateInfo>(Filter.FilterPriceRent(ListPrice.IndexOf(DisplayPrice), ListViewQueryRE.ToList()));
            }

            if (string.IsNullOrEmpty(DisplayArea) == false)
            {
                ListViewQueryRE = new ObservableCollection<RealEstateInfo>(Filter.FilterArea(ListArea.IndexOf(DisplayArea), ListViewQueryRE.ToList()));
            }

            if (string.IsNullOrEmpty(Query) == false)
            {
                ListViewQueryRE = new ObservableCollection<RealEstateInfo>(ListViewQueryRE.Where(x => x.title.ToLower().Contains(Query) == true).ToList());
            }

            if (string.IsNullOrEmpty(DisplayFilter) == false)
            {
                var sortListViewQueryRE = ListViewQueryRE.ToList();
                if (DisplayFilter == "Mới Nhất")
                {
                    sortListViewQueryRE.Sort(delegate (RealEstateInfo re1, RealEstateInfo re2) { return re1.timepost.CompareTo(re2.timepost); });
                    sortListViewQueryRE.Reverse();
                }
                else if (DisplayFilter == "Giá Cao Nhất")
                {
                    var sortPriceTrieu = sortListViewQueryRE.Where(x => x.price.ToLower().Contains("triệu") == true).ToList();
                    var sortPriceTy = sortListViewQueryRE.Where(x => x.price.ToLower().Contains("tỷ") == true).ToList();
                    var sortPriceTT = sortListViewQueryRE.Where(x => x.price.ToLower().Contains("thoả thuận") == true).ToList();

                    sortPriceTrieu.Sort(delegate (RealEstateInfo re1, RealEstateInfo re2) { return double.Parse(re1.price.Replace(" Triệu/tháng", "")).CompareTo(double.Parse(re2.price.Replace(" Triệu/tháng", ""))); });
                    sortPriceTy.Sort(delegate (RealEstateInfo re1, RealEstateInfo re2) { return double.Parse(re1.price.Replace(" Tỷ/tháng", "")).CompareTo(double.Parse(re2.price.Replace(" Tỷ/tháng", ""))); });

                    sortListViewQueryRE = sortPriceTT.Concat(sortPriceTrieu).Concat(sortPriceTy).ToList();
                    sortListViewQueryRE.Reverse();
                }
                else
                {
                    var sortPriceTrieu = sortListViewQueryRE.Where(x => x.price.ToLower().Contains("triệu") == true).ToList();
                    var sortPriceTy = sortListViewQueryRE.Where(x => x.price.ToLower().Contains("tỷ") == true).ToList();
                    var sortPriceTT = sortListViewQueryRE.Where(x => x.price.ToLower().Contains("thoả thuận") == true).ToList();

                    sortPriceTrieu.Sort(delegate (RealEstateInfo re1, RealEstateInfo re2) { return double.Parse(re1.price.Replace(" Triệu/tháng", "")).CompareTo(double.Parse(re2.price.Replace(" Triệu/tháng", ""))); });
                    sortPriceTy.Sort(delegate (RealEstateInfo re1, RealEstateInfo re2) { return double.Parse(re1.price.Replace(" Tỷ/tháng", "")).CompareTo(double.Parse(re2.price.Replace(" Tỷ/tháng", ""))); });

                    sortListViewQueryRE = sortPriceTrieu.Concat(sortPriceTy).Concat(sortPriceTT).ToList();
                }
                ListViewQueryRE = new ObservableCollection<RealEstateInfo>(sortListViewQueryRE);
            }

        }

        // reset
        private void Reset()
        {
            DisplayTypeRE = null;
            DisplayFilter = null;
            DisplayCity = null;
            DisplayArea = null;
            DisplayPrice = null;
            ListViewRE = new ObservableCollection<RealEstateInfo>();
            ListViewQueryRE = new ObservableCollection<RealEstateInfo>();
        }
    }
}
