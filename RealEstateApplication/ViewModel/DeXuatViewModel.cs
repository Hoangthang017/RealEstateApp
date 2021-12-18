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

        private ObservableCollection<TinhKhoangCach> _listprojects;
        public ObservableCollection<TinhKhoangCach> ListProjects { get => _listprojects; set { _listprojects = value; OnPropertyChanged(); } }

        // list data
        private TinhKhoangCach _DisplayRE;
        public TinhKhoangCach DisplayRE { get => _DisplayRE; set { _DisplayRE = value; OnPropertyChanged(); } }

        public DeXuatViewModel()
        {

            

            LoadedDanhChoBanCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListProjects = new ObservableCollection<TinhKhoangCach>();
                TinhThanhPho = passData.passTinhThanhPho;
                QuanHuyen = passData.passQuanHuyen;
                XaPhuong = passData.passXaHuyen;
                SoNha = passData.passSoNha;
                string address_from = SoNha + ", " + XaPhuong + ", " + QuanHuyen + ", " + TinhThanhPho;
                int c = 0;

                var RealEstateInfo = DataProvider.Ins.DB.RealEstateInfoes;
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
                        c++;
                        Console.WriteLine(c);
                    }
                }

                ListProjects = new ObservableCollection<TinhKhoangCach>(ListProjects.OrderByDescending(pa => pa.KhoangCach).Reverse());
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
                    OpenUC.OpenChildUC(new ChiTietDeXuat());
                    passData.passTinhThanhPho = TinhThanhPho;
                    passData.passQuanHuyen = QuanHuyen;
                    passData.passXaHuyen = XaPhuong;
                    passData.passSoNha = SoNha;
                }
            });
        }

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

        public string location(string name_address, string name, string addres_from)
        {
            string address_to = name_address.Replace(name + " ", "");
            string url = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + addres_from + "&destinations=" + address_to + "&mode=walking&language=vi-VN&key=" + "AIzaSyCB29qbY6OvWnzOzNoIG29t-Ton8bLrqR8";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(datastream);
            string responsereader = sreader.ReadToEnd();
            response.Close();

            WebClient wb = new WebClient();
            var html_from = wb.DownloadString(url);

            DataSet ds = new DataSet();
            ds.ReadXml(new XmlTextReader(new StringReader(responsereader)));
            string distance_duration = "";
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables["element"].Rows[0]["status"].ToString() == "OK")
                {
                    string durtion = Convert.ToString(ds.Tables["duration"].Rows[0]["text"].ToString().Trim());
                    string distance = Convert.ToString(ds.Tables["distance"].Rows[0]["text"].ToString().Trim());
                    distance_duration = distance + "+" + durtion;
                }
            }
            return distance_duration.Replace(",", ".");
        }
    }
}
