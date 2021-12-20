using RealEstateApplication.Model;
using RealEstateApplication.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace RealEstateApplication.ViewModel
{
    public class TinhKhoangCach
    {
        public ImageSource Anh { get; set; }
        public RealEstateInfo realestateinfo { get; set; }
        public float KhoangCach { get; set; }
        public string Gio { get; set; }
    }
    public class DeXuatViewModel : BaseViewModel
    {
        public ICommand LoadedDanhChoBanCommand { get; set; }
        public ICommand ClickTimKiemCommand { get; set; }
        public ICommand SelectedCellsChangedRECommand { get; set; }


        private string _tinhthanhpho;
        public string TinhThanhPho { get => _tinhthanhpho; set { _tinhthanhpho = value; OnPropertyChanged(); } }

        private string _sonha;
        public string SoNha { get => _sonha; set { _sonha = value; OnPropertyChanged(); } }

        private string _xaphuong;
        public string XaPhuong { get => _xaphuong; set { _xaphuong = value; OnPropertyChanged(); } }

        private string _quanhuyen;
        public string QuanHuyen { get => _quanhuyen; set { _quanhuyen = value; OnPropertyChanged(); } }

        // text đề xuất
        private string _TextLoaiNha;
        public string TextLoaiNha { get => _TextLoaiNha; set { _TextLoaiNha = value; OnPropertyChanged(); } }
        private string _TextHuongNha;
        public string TextHuongNha { get => _TextHuongNha; set { _TextHuongNha = value; OnPropertyChanged(); } }
        private string _TextDienTich;
        public string  TextDienTich { get => _TextDienTich; set { _TextDienTich = value; OnPropertyChanged(); } }
        private string _TextDiaChi;
        public string TextDiaChi { get => _TextDiaChi; set { _TextDiaChi = value; OnPropertyChanged(); } }
        private string _TextGiaNha;
        public string TextGiaNha { get => _TextGiaNha; set { _TextGiaNha = value; OnPropertyChanged(); } }

        private ObservableCollection<TinhKhoangCach> _listprojects;
        public ObservableCollection<TinhKhoangCach> ListProjects { get => _listprojects; set { _listprojects = value; OnPropertyChanged(); } }

        // list data
        private TinhKhoangCach _DisplayRE;
        public TinhKhoangCach DisplayRE { get => _DisplayRE; set { _DisplayRE = value; OnPropertyChanged(); } }

        public DeXuatViewModel()
        {
            LoadedDanhChoBanCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // load dữ liệu backup
                if (passDataDetailRE.ListDetailDeXuat != null)
                {
                    ListProjects = new ObservableCollection<TinhKhoangCach>(passDataDetailRE.ListDetailDeXuat);
                    passDataDetailRE.ListDetailDeXuat = null;
                }
                else
                {
                    // chung 
                    List<RealEstateInfo> newListInforRE = DataProvider.Ins.DB.RealEstateInfoes.ToList();
                    TextLoaiNha = "--";
                    TextHuongNha = "--";
                    TextDienTich = "--";
                    TextGiaNha = "--";
                    TextDiaChi = "--";

                    // loại bất sản
                    if (string.IsNullOrEmpty(passData.LoaiHinh) == false)
                    {
                        if (passData.LoaiHinh == "Mua Nhà")
                        {
                            newListInforRE = newListInforRE.Where(x => x.type.Contains("Bán ") == true).ToList();
                            TextLoaiNha = "- Loại hình bất động sản: Mua nhà";
                        }
                        else
                        {
                            newListInforRE = newListInforRE.Where(x => x.type.Contains("Cho thuê ") == true).ToList();
                            TextLoaiNha = "- Loại hình bất động sản: Thuê nhà";
                        }
                    }

                    // phong thuỷ
                    if (string.IsNullOrEmpty(passData.GioiTinh) == false && passData.NamSinh != 0)
                    {
                        var temp = new List<RealEstateInfo>();
                        var _menhSinh = GetMenhSinh(passData.NamSinh, passData.GioiTinh);
                        var _huongNha = GetHuongNha(_menhSinh);
                        foreach (var infor in newListInforRE)
                        {
                            if (string.IsNullOrEmpty(infor.orientation) == false)
                            {
                                if (CheckHuongNha(infor.orientation.Replace("-", " ").ToLower(), _huongNha))
                                {
                                    temp.Add(infor);
                                }
                            }

                        }
                        TextHuongNha = "- Hướng nhà: ";
                        foreach (var hn in _huongNha)
                        {
                            TextHuongNha += " " + hn;
                        }
                        newListInforRE = temp;
                    }

                    // diện tích
                    if (string.IsNullOrEmpty(passData.SoNguoi) == false)
                    {
                        var temp = new List<RealEstateInfo>();
                        if (passData.SoNguoi == "1")
                        {
                            temp = Filter.FilterArea(0, newListInforRE);
                            temp = temp.Where(x => x.bedrooms >= 1).ToList();
                            TextDienTich = "- Diện tích từ 0 đến 30 m2 có 1 phòng ngủ trở lên";
                        }
                        else if (passData.SoNguoi == "2")
                        {
                            temp = Filter.FilterArea(1, newListInforRE);
                            temp = temp.Where(x => x.bedrooms >= 1).ToList();
                            TextDienTich = "- Diện tích từ 30 đến 50 m2 có 1 phòng ngủ trở lên";
                        }
                        else if (passData.SoNguoi == "3")
                        {
                            temp = Filter.FilterArea(2, newListInforRE);
                            temp = temp.Where(x => x.bedrooms >= 2).ToList();
                            TextDienTich = "- Diện tích từ 50 đến 80 m2 có 2 phòng ngủ trở lên";
                        }
                        else if (passData.SoNguoi == "4")
                        {
                            temp = Filter.FilterArea(3, newListInforRE);
                            temp = temp.Where(x => x.bedrooms >= 2).ToList();
                            TextDienTich = "- Diện tích từ 80 đến 100 m2 có 2 phòng ngủ trở lên";
                        }
                        else
                        {
                            temp = newListInforRE.Where(x => x.area >= 100).ToList();
                            temp = temp.Where(x => x.bedrooms >= 3).ToList();
                            TextDienTich = "- Diện tích từ 100 m2 trở lên có 3 phòng ngủ trở lên";
                        }
                        newListInforRE = temp;
                    }

                    // mức lương
                    if (passData.MucLuong > 0)
                    {
                        var temp = new List<RealEstateInfo>();
                        double MaxTien = passData.MucLuong * 0.6 / (0.074 / 12 + 1 / 180);
                        foreach (var infor in newListInforRE)
                        {
                            if (infor.price.ToLower().Contains("thuận"))
                            {
                                temp.Add(infor);
                            }
                            else
                            {
                                double price = 0;
                                if (infor.price.ToLower().Contains("triệu"))
                                {
                                    price = double.Parse(infor.price.Split(' ')[0]) * 1000000;
                                }
                                else
                                {
                                    price = double.Parse(infor.price.Split(' ')[0]) * 1000000000;
                                }

                                if (price < MaxTien)
                                {
                                    temp.Add(infor);
                                }
                            }
                        }
                        TextGiaNha = "- Giá nhà thoả thuận hoặc từ " + Math.Round(MaxTien) + " VND trở xuống";
                        newListInforRE = temp;
                    }

                    // địa chỉ
                    if ((string.IsNullOrEmpty(passData.passSoNha) == false) ||
                        (string.IsNullOrEmpty(passData.passXaHuyen) == false) ||
                        (string.IsNullOrEmpty(passData.passQuanHuyen) == false) ||
                        (string.IsNullOrEmpty(passData.passTinhThanhPho) == false))
                    {
                        ListProjects = new ObservableCollection<TinhKhoangCach>();
                        TinhThanhPho = passData.passTinhThanhPho;
                        QuanHuyen = passData.passQuanHuyen;
                        XaPhuong = passData.passXaHuyen;
                        SoNha = passData.passSoNha;

                        string address_from = "";
                        if (string.IsNullOrEmpty(SoNha) == false)
                        {
                            address_from += SoNha;
                        }

                        if (string.IsNullOrEmpty(XaPhuong) == false)
                        {
                            address_from += "," + XaPhuong;
                        }

                        if (string.IsNullOrEmpty(QuanHuyen) == false)
                        {
                            address_from += "," + QuanHuyen;
                        }

                        if (string.IsNullOrEmpty(TinhThanhPho) == false)
                        {
                            address_from += "," + TinhThanhPho;
                        }

                        var RealEstateInfo = newListInforRE;
                        foreach (var data in RealEstateInfo)
                        {
                            string loca = data.location.Trim().ToLower();
                            if (loca.Contains(TinhThanhPho.Trim().ToLower()) && loca.Contains(QuanHuyen.Trim().ToLower()))
                            {
                                var khoangcach_thoigian = locationbingmap(data.address, data.type, address_from).Split('+');
                                var anh = new BitmapImage(new Uri(String.Format(data.img)));
                                ListProjects.Add(new TinhKhoangCach()
                                {
                                    Anh = new BitmapImage(new Uri(String.Format(data.img))),
                                    realestateinfo = data,
                                    KhoangCach = float.Parse(khoangcach_thoigian[0]),
                                    Gio = khoangcach_thoigian[1]
                                });
                            }
                        }

                        TextDienTich = "- Địa chỉ ở gần " + address_from;
                        ListProjects = new ObservableCollection<TinhKhoangCach>(ListProjects.OrderByDescending(pa => pa.KhoangCach).Reverse());
                    }

                    // nếu lisproject chưa tạo => không có chạy địa chỉ
                    if (ListProjects == null)
                    {
                        ListProjects = new ObservableCollection<TinhKhoangCach>();
                        foreach (var infor in newListInforRE)
                        {
                            var newInfor = new TinhKhoangCach()
                            {
                                realestateinfo = infor,
                                Gio = "--",
                                Anh = infor.imageSource
                            };
                            ListProjects.Add(newInfor);
                        }
                    }
                }

                
            });

            ClickTimKiemCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListProjects = new ObservableCollection<TinhKhoangCach>();
                string address_from = SoNha + ", " + XaPhuong + ", " + QuanHuyen + ", " + TinhThanhPho;
                int c = 0;

                var RealEstateInfo = DataProvider.Ins.DB.RealEstateInfoes;
                foreach (var data in RealEstateInfo)
                {
                    string loca = data.location.Trim().ToLower();
                    if (loca.Contains(TinhThanhPho.Trim().ToLower()) && loca.Contains(QuanHuyen.Trim().ToLower()))
                    {
                        var khoangcach_thoigian = locationbingmap(data.address, data.type, address_from).Split('+');
                        ListProjects.Add(new TinhKhoangCach()
                        {
                            Anh = new BitmapImage(new Uri(data.img)),
                            realestateinfo = data,
                            KhoangCach = float.Parse(khoangcach_thoigian[0]),
                            Gio = khoangcach_thoigian[1]
                        });
                        c++;
                        Console.WriteLine(c);
                    }
                }

                ListProjects = new ObservableCollection<TinhKhoangCach>(ListProjects.OrderByDescending(pa => pa.KhoangCach).Reverse());

                (p as DataGrid).Items.Refresh();
            });

            SelectedCellsChangedRECommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DisplayRE != null)
                {
                    passDataDetailRE.cellRealEstateInfo = DisplayRE.realestateinfo;
                    passDataDetailRE.cellRealEstateInfo.imageSource = new BitmapImage(new Uri(DisplayRE.realestateinfo.img));
                    passDataDetailRE.ListDetailDeXuat = ListProjects.ToList();
                    var child = new ChiTietDeXuat();
                    child.DataContext = new ChiTietDeXuatViewModel();
                    OpenUC.OpenChildUC(child);

                    passData.passTinhThanhPho = TinhThanhPho;
                    passData.passQuanHuyen = QuanHuyen;
                    passData.passXaHuyen = XaPhuong;
                    passData.passSoNha = SoNha;
                }
            });
        }

        // lấy mệnh sinh theo năm sinh và giới tính
        private string GetMenhSinh(int _namSinh, string _gioiTinh)
        {
            int sum = 0;
            while (_namSinh != 0)
            {
                sum += _namSinh % 10;
                _namSinh /= 10;
            }
            _namSinh = sum;
            if (_namSinh % 9 == 0)
            {
                if (_gioiTinh == "Nam")
                {
                    return "khôn";
                }
                else
                {
                    return "tốn";
                }

            }
            else if (_namSinh % 9 == 1) 
            {
                if (_gioiTinh == "Nam")
                {
                    return "khảm";
                }
                else
                {
                    return "cấn";
                }
            }
            else if (_namSinh % 9 == 2)
            {
                if (_gioiTinh == "Nam")
                {
                    return "ly";
                }
                else
                {
                    return "càn";
                }
            }
            else if (_namSinh % 9 == 3)
            {
                if (_gioiTinh == "Nam")
                {
                    return "cấn";
                }
                else
                {
                    return "đoàn";
                }
            }
            else if (_namSinh % 9 == 4)
            {
                if (_gioiTinh == "Nam")
                {
                    return "đoài";
                }
                else
                {
                    return "cấn";
                }
            }
            else if (_namSinh % 9 == 5)
            {
                if (_gioiTinh == "Nam")
                {
                    return "càn";
                }
                else
                {
                    return "ly";
                }
            }
            else if (_namSinh % 9 == 6)
            {
                if (_gioiTinh == "Nam")
                {
                    return "khôn";
                }
                else
                {
                    return "khảm";
                }
            }
            else if (_namSinh % 9 == 7)
            {
                if (_gioiTinh == "Nam")
                {
                    return "tốn";
                }
                else
                {
                    return "tốn";
                }
            }
            else
            {
                if (_gioiTinh == "Nam")
                {
                    return "chấn";
                }
                else
                {
                    return "chấn";
                }
            }
        }

        // lấy hướng nhà theo mệnh sinh
        private List<string> GetHuongNha(string _menhSinh)
        {
            if (_menhSinh == "càn" || _menhSinh == "đoài")
            {
                return new List<string>()
                {
                    "tây",
                    "tây bắc"
                };
            }
            else if (_menhSinh == "cấn" || _menhSinh == "khôn")
            {
                return new List<string>()
                {
                    "trung tâm",
                    "tây nam",
                    "đông bắc"
                };
            }
            else if (_menhSinh == "chấn" || _menhSinh == "tốn")
            {
                return new List<string>()
                {
                    "đông",
                    "đông nam"
                };
            }
            else if (_menhSinh == "khảm")
            {
                return new List<string>()
                {
                    "bắc"
                };
            }
            else
            {
                return new List<string>()
                {
                    "nam"
                };
            }
        }

        // hàm kiểm tra có tồn tại hướng nhà không
        private bool CheckHuongNha(string _rootHuong, List<string> _huongnha)
        {
            foreach(var huongNha in _huongnha)
            {
                if (_rootHuong == huongNha)
                {
                    return true;
                }
            }
            return false;
        }

        // tính khoảng cách bằng API microsoft
        public string locationbingmap(string name_address, string name, string adđress_form)
        {
            string address = name_address.Replace(name + " ", "");
            string url_to = "http://dev.virtualearth.net/REST/v1/Locations?countryRegion=VN&addressLine=" + address + "&key=lqbcjNWAPkfRtGh7glC7~P9qGQveunpaVmctkVSYc6A~AoJlXq2XcPrU796Hq5u_T4k2rkue1EcU3Slda34kMQ1Kv3EDMmf99XfD5i1rOSKI";
            WebClient wb = new WebClient();
            var html = wb.DownloadString(url_to);
            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(html);
            json = json["resourceSets"][0]["resources"][0]["point"]["coordinates"];
            string lat_to = json[0];
            string lon_to = json[1];
            string lat_lon_to = lat_to + "," + lon_to;

            string url_from = "http://dev.virtualearth.net/REST/v1/Locations?countryRegion=VN&addressLine=" + adđress_form + "&key=AmkL9XLX6aVJuyey23Ys-qBF5NFvTvk2nbGexWNFexjfdAS74Dxh_uzKuobY6sgV";
            html = wb.DownloadString(url_from);
            json = Newtonsoft.Json.JsonConvert.DeserializeObject(html);
            json = json["resourceSets"][0]["resources"][0]["point"]["coordinates"];
            string lat_from = json[0];
            string lon_from = json[1];
            string lat_lon_from = lat_from + "," + lon_from;

            string url_dis = "https://dev.virtualearth.net/REST/v1/Routes/DistanceMatrix?origins=" + lat_lon_from + "&destinations=" + lat_lon_to + "&travelMode=driving&key=lqbcjNWAPkfRtGh7glC7~P9qGQveunpaVmctkVSYc6A~AoJlXq2XcPrU796Hq5u_T4k2rkue1EcU3Slda34kMQ1Kv3EDMmf99XfD5i1rOSKI";
            html = wb.DownloadString(url_dis);
            json = Newtonsoft.Json.JsonConvert.DeserializeObject(html);
            json = json["resourceSets"][0]["resources"][0]["results"][0];
            string distance = json["travelDistance"];
            string durantion = json["travelDuration"];

            return distance + "+" + durantion;
        }
    }
}
