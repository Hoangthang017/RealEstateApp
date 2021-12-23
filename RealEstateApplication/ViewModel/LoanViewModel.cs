using RealEstateApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RealEstateApplication.ViewModel
{
    public class ViewPayPerMonth
    {
        public int STT { get; set; }
        public string RatePrice { get; set; }
        public string RootPrice { get; set; }
        public string PayPrice { get; set; }
        public string SumPrice { get; set; }
    }

    public class LoanViewModel : BaseViewModel
    {

        // list data thống kê
        private ObservableCollection<ViewPayPerMonth> _ListPayPerMonth;
        public ObservableCollection<ViewPayPerMonth> ListPayPerMonth { get => _ListPayPerMonth; set { _ListPayPerMonth = value; OnPropertyChanged(); } }

        // checked bank
        private bool _DisplayIsCheckedBIDV;
        public bool DisplayIsCheckedBIDV { get => _DisplayIsCheckedBIDV; set { _DisplayIsCheckedBIDV = value; OnPropertyChanged(); } }

        private bool _DisplayIsCheckedVietinbank;
        public bool DisplayIsCheckedVietinbank { get => _DisplayIsCheckedVietinbank; set { _DisplayIsCheckedVietinbank = value; OnPropertyChanged(); } }

        private bool _DisplayIsCheckedVietcombank;
        public bool DisplayIsCheckedVietcombank { get => _DisplayIsCheckedVietcombank; set { _DisplayIsCheckedVietcombank = value; OnPropertyChanged(); } }

        private bool _DisplayIsCheckedACB;
        public bool DisplayIsCheckedACB { get => _DisplayIsCheckedACB; set { _DisplayIsCheckedACB = value; OnPropertyChanged(); } }

        // display price
        private string _DisplayRootPrice;
        public string DisplayRootPrice { get => _DisplayRootPrice; set { _DisplayRootPrice = value; OnPropertyChanged(); } }

        private string _DisplayLoanPrice;
        public string DisplayLoanPrice { get => _DisplayLoanPrice; set { _DisplayLoanPrice = value; OnPropertyChanged(); } }

        // display month
        private int _DisplayMonth;
        public int DisplayMonth { get => _DisplayMonth; set { _DisplayMonth = value; OnPropertyChanged(); } }

        private int _DisplayMaxMonth;
        public int DisplayMaxMonth { get => _DisplayMaxMonth; set { _DisplayMaxMonth = value; OnPropertyChanged(); } }

        // display rate
        private double _DisplayRate;
        public double DisplayRate { get => _DisplayRate; set { _DisplayRate = value; OnPropertyChanged(); } }

        // display calculate per month
        private string _DisplaySumPayRate;
        public string DisplaySumPayRate { get => _DisplaySumPayRate; set { _DisplaySumPayRate = value; OnPropertyChanged(); } }

        private string _DisplayPayAll;
        public string DisplayPayAll { get => _DisplayPayAll; set { _DisplayPayAll = value; OnPropertyChanged(); } }

        private string _DisplayPayFirstMonth;
        public string DisplayPayFirstMonth { get => _DisplayPayFirstMonth; set { _DisplayPayFirstMonth = value; OnPropertyChanged(); } }

        // command
        public ICommand LoadedLoanWindowCommand { get; set; }
        public ICommand CheckedBankCommand { get; set; }
        public ICommand TextChangedMonthCommand { get; set; }
        public ICommand ClickCalcRateCommand { get; set; }
        public ICommand ClickOpenLinkCommand { get; set; }

        public LoanViewModel()
        {
            //hàm load dữ liệu
            LoadedLoanWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (string.IsNullOrEmpty(BackupListRE.Container.PriceLoan) == false)
                {
                    DisplayRootPrice = BackupListRE.Container.PriceLoan;
                }
            });

            //hàm load dữ liệu
            CheckedBankCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    var _nameCheckBox = (p as CheckBox).Content.ToString();
                    if (_nameCheckBox == "BIDV")
                    {
                        DisplayIsCheckedACB = false;
                        DisplayIsCheckedVietcombank = false;
                        DisplayIsCheckedVietinbank = false;

                        // tính lãi suất
                        DisplayLoanPrice = (double.Parse(DisplayRootPrice.Split(' ')[0]) * 100d / 100d).ToString() + " " + DisplayRootPrice.Split(' ')[1];
                        DisplayRate = 7.3d;
                        DisplayMaxMonth = 180;
                    }
                    else if (_nameCheckBox == "Vietinbank")
                    {
                        DisplayIsCheckedACB = false;
                        DisplayIsCheckedVietcombank = false;
                        DisplayIsCheckedBIDV = false;

                        // tính lãi suất
                        DisplayLoanPrice = (double.Parse(DisplayRootPrice.Split(' ')[0]) * 70d / 100d).ToString() + " " + DisplayRootPrice.Split(' ')[1];
                        DisplayRate = 8.62d;
                        DisplayMaxMonth = 180;
                    }
                    else if (_nameCheckBox == "Vietcombank")
                    {
                        DisplayIsCheckedACB = false;
                        DisplayIsCheckedVietinbank = false;
                        DisplayIsCheckedBIDV = false;

                        // tính lãi suất
                        DisplayLoanPrice = (double.Parse(DisplayRootPrice.Split(' ')[0]) * 70d / 100d).ToString() + " " + DisplayRootPrice.Split(' ')[1];
                        DisplayRate = 7.4d;
                        DisplayMaxMonth = 180;
                    }
                    else if (_nameCheckBox == "ACB")
                    {
                        DisplayIsCheckedBIDV = false;
                        DisplayIsCheckedVietcombank = false;
                        DisplayIsCheckedVietinbank = false;

                        // tính lãi suất
                        DisplayLoanPrice = (double.Parse(DisplayRootPrice.Split(' ')[0]) * 100d / 100d).ToString() + " " + DisplayRootPrice.Split(' ')[1];
                        DisplayRate = 7.5d;
                        DisplayMaxMonth = 180;
                    }
                }
            });

            //hàm nhập số tháng
            TextChangedMonthCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (DisplayMaxMonth <= 0)
                {
                    MessageBox.Show("Vui lòng chọn ngân hàng vay!!");
                    DisplayMonth = 0;
                }
                if (DisplayMonth > DisplayMaxMonth)
                {
                    MessageBox.Show("Vui lòng nhập nhỏ hơn số tháng tối đa!!!");
                    DisplayMonth = 0;
                }
            });

            //hàm tính lãi suất
            ClickCalcRateCommand = new RelayCommand<object>((p) => {
                if (DisplayMonth <= 0)
                    return false;
                return true;
            }, (p) => {
                if (string.IsNullOrEmpty(DisplayLoanPrice) == false)
                {
                    double SumRatePrice = 0;
                    double SumPayPrice = 0;
                    double loanPrice = 50000000;
                    
                    if (DisplayLoanPrice.ToLower().Contains("tỷ"))
                    {
                        loanPrice = double.Parse(DisplayLoanPrice.Split(' ')[0]) * 1000000000;
                    }
                    else
                    {
                        loanPrice = double.Parse(DisplayLoanPrice.Split(' ')[0]) * 1000000;
                    }

                    ListPayPerMonth = new ObservableCollection<ViewPayPerMonth>();
                    double _RootPrice = Math.Round(loanPrice / DisplayMonth);
                    var value = (Math.Round(loanPrice / DisplayMonth + ((loanPrice / DisplayMonth) * DisplayRate / 100)));
                    DisplayPayFirstMonth = string.Format(new CultureInfo("vi-VN"), "{0:0,0}", value);
                    for (int i = 1; i <= DisplayMonth; i++)
                    {
                        double _SumPayPrice = 0;
                        if (i != DisplayMonth)
                        {
                            _SumPayPrice = loanPrice - _RootPrice;
                        }
                        else
                        {
                            _RootPrice = loanPrice;
                        }
                        var newViewPay = new ViewPayPerMonth();
                        newViewPay.RootPrice = string.Format(new CultureInfo("vi-VN"), "{0:0,0}", _RootPrice);
                        newViewPay.STT = i;
                        double _RatePrice = Math.Round((loanPrice / DisplayMonth) * DisplayRate / 100);
                        double _PayPrice = _RatePrice + _RootPrice;
                        newViewPay.RatePrice = string.Format(new CultureInfo("vi-VN"), "{0:0,0}", (_RatePrice));
                        newViewPay.PayPrice = string.Format(new CultureInfo("vi-VN"), "{0:0,0}", _PayPrice);
                        newViewPay.SumPrice = string.Format(new CultureInfo("vi-VN"), "{0:0,0}", (_SumPayPrice));
                        loanPrice -= _RootPrice;
                        SumPayPrice += _PayPrice;
                        SumRatePrice += _RatePrice;
                        ListPayPerMonth.Add(newViewPay);

                    }
                    DisplaySumPayRate = string.Format(new CultureInfo("vi-VN"), "{0:0,0}", SumRatePrice);
                    DisplayPayAll = string.Format(new CultureInfo("vi-VN"), "{0:0,0}", SumPayPrice);
                }

            });

            //hàm load dữ liệu
            ClickOpenLinkCommand = new RelayCommand<object>((p) => {
                if (DisplayIsCheckedBIDV == false && DisplayIsCheckedACB == false && DisplayIsCheckedVietcombank == false && DisplayIsCheckedVietinbank == false)
                    return false;
                return true;
            }, (p) => {
                if (DisplayIsCheckedBIDV)
                {
                    System.Diagnostics.Process.Start("https://www.bidv.com.vn/bidv/ca-nhan/khuyen-mai/san-pham-vay/bidv-tung-goi-cho-vay-mua-nha-mua-xe-san-xuat-kinh-doanh-lai-suat-uu-dai");
                }
                else if (DisplayIsCheckedVietcombank)
                {
                    System.Diagnostics.Process.Start("https://portal.vietcombank.com.vn/Personal/Loan/CVMBDS/Pages/Cho-vay-mua-nha-dat.aspx?devicechannel=default");
                }
                else if (DisplayIsCheckedVietinbank)
                {
                    System.Diagnostics.Process.Start("https://www.vietinbank.vn/vn/ca-nhan/premium/san-pham/cho-vay-nha-o");
                }
                else if (DisplayIsCheckedACB)
                {
                    System.Diagnostics.Process.Start("https://www.acb.com.vn/vn/personal/cho-vay/vay-mua-nha/vay-mua-nha-dat");
                }
            });
        }
    }
}
