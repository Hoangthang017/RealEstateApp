using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RealEstateApplication.Model;
using RealEstateApplication.UserControls;

namespace RealEstateApplication.ViewModel
{
    public class HomepageViewModel : BaseViewModel
    {
        private string _Query;
        public string Query { get => _Query; set { _Query = value; OnPropertyChanged(); } }

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

        // command để copy cho dễ 
        public ICommand LoadedUserControlsCommand { get; set; }
        public ICommand ClickMoreFilterCommand { get; set; }
        public HomepageViewModel()
        {
            LoadedUserControlsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var test = Query;

                //
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
            });

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
        }
    }
}
