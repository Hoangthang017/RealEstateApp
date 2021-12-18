using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace RealEstateApplication.Model
{
    public static class Filter
    {
        public static List<string> ListTypeSellRE { get; set; }
        public static List<string> ListTypeRentRE { get; set; }
        public static List<string> ListAddressRE { get; set; }
        public static List<string> ListPriceRE { get; set; }
        public static List<string> ListPriceRentRE { get; set; }
        public static List<string> ListAreaRE { get; set; }

        // load data 
        public static void CreateListData()
        {
            var infors = DataProvider.Ins.DB.RealEstateInfoes.ToList();
            ListTypeSellRE = new List<string>();
            ListTypeRentRE = new List<string>();
            ListAddressRE = new List<string>();
            foreach (var infor in infors)
            {
                var typeRE = infor.type;
                if (typeRE.Split(' ')[0].ToLower() == "bán")
                {
                    typeRE = infor.type.Replace("Bán ", "");
                    if (ListTypeSellRE.Contains(typeRE) == false)
                    {
                        ListTypeSellRE.Add(typeRE);
                    }
                }
                else
                {
                    typeRE = infor.type.Replace("Cho thuê ", "");
                    if (ListTypeRentRE.Contains(typeRE) == false)
                    {
                        ListTypeRentRE.Add(typeRE);
                    }
                }

                var location = infor.location.Split(',')[1];
                if (ListAddressRE.Contains(location) == false)
                {
                    ListAddressRE.Add(location);
                }
                infor.img = System.AppDomain.CurrentDomain.BaseDirectory + "Images\\" + infor.img;

                try
                {
                    infor.imageSource = new BitmapImage(new Uri(String.Format(infor.img)));
                }
                catch
                {
                    infor.imageSource = new BitmapImage(new Uri(String.Format(System.AppDomain.CurrentDomain.BaseDirectory + "Images\\no-image.jpg")));
                }
                infor.squareRoot = " m\u00b2";

                infor.price = infor.price.Replace(',', '.');

                infor.description = infor.description.Trim();

                infor.address = infor.address.Replace(infor.type, "").Replace("Dự án", "");
            }

            ListPriceRE = new List<string>()
            {
                "Dưới 500 Triệu",
                "500 - 1 Tỷ",
                "1 - 2 Tỷ",
                "2 - 3 Tỷ",
                "3 - 5 Tỷ",
                "5 - 7 Tỷ",
                "7 - 10 Tỷ",
                "10 - 20 Tỷ",
                "20 - 30 Tỷ",
                "Trên 30 Tỷ"
            };

            ListPriceRentRE = new List<string>()
            {
                "Dưới 1 Triệu",
                "1 - 3 Triệu",
                "3 - 5 Triệu",
                "5 - 10 Triệu",
                "10 - 40 Triệu",
                "40 - 70 Triệu",
                "70 - 100 Triệu",
                "Trên 100 Triệu"
            };

            ListAreaRE = new List<string>()
            {
                "Dưới 30 m\u00b2",
                "30 - 50 m\u00b2",
                "50  - 80 m\u00b2",
                "80 - 100 m\u00b2",
                "100 - 150 m\u00b2",
                "150 - 300 m\u00b2",
                "300 - 500 m\u00b2",
                "Trên 500 m\u00b2"
            };
        }

        // filter theo loại
        public static List<RealEstateInfo> FilterTypeRE(string typeRE, string nameRE)
        {
            if (typeRE == "Bán")
            {
                return DataProvider.Ins.DB.RealEstateInfoes.Where(x => x.type.Replace("Bán ", "") == nameRE).ToList();
            }
            return DataProvider.Ins.DB.RealEstateInfoes.Where(x => x.type.ToLower().Contains("thuê") == true &&
                                                                    x.type.ToLower().Contains(nameRE.ToLower()) == true
            ).ToList();
        }

        // filter theo thành phố
        public static List<RealEstateInfo> FilterCity(string city, List<RealEstateInfo> ListViewRE)
        {
            return ListViewRE.Where(x => x.location.Split(',')[1] == city).ToList();
        }

        // filter theo giá
        public static List<RealEstateInfo> FilterPrice(int id, List<RealEstateInfo> ListViewRE)
        {
            switch (id)
            {
                case 0:
                    return ListViewRE.Where(x =>
                                                (x.price.ToLower().Contains("triệu") == true) && (double.Parse((x.price.Split(' ')[0])) < 500d)
                    ).ToList();
                case 1:
                    return ListViewRE.Where(x =>
                                                ((x.price.ToLower().Contains("triệu") == true) && (double.Parse((x.price.Split(' ')[0])) >= 500)) ||
                                                ((x.price.ToLower().Contains("tỷ") == true) && (double.Parse((x.price.Split(' ')[0])) < 1))
                    ).ToList();
                case 2:
                    return ListViewRE.Where(x => (x.price.ToLower().Contains("tỷ") == true) &&
                                                    (double.Parse((x.price.Split(' ')[0])) >= 1) &&
                                                    (double.Parse((x.price.Split(' ')[0])) < 2)
                    ).ToList();
                case 3:
                    return ListViewRE.Where(x => (x.price.ToLower().Contains("tỷ") == true) &&
                                                    (double.Parse((x.price.Split(' ')[0])) >= 2) &&
                                                    (double.Parse((x.price.Split(' ')[0])) < 3)
                    ).ToList();
                case 4:
                    return ListViewRE.Where(x => (x.price.ToLower().Contains("tỷ") == true) &&
                                                    (double.Parse((x.price.Split(' ')[0])) >= 3) &&
                                                    (double.Parse((x.price.Split(' ')[0])) < 5)
                    ).ToList();
                case 5:
                    return ListViewRE.Where(x => (x.price.ToLower().Contains("tỷ") == true) &&
                                                    (double.Parse((x.price.Split(' ')[0])) >= 5) &&
                                                    (double.Parse((x.price.Split(' ')[0])) < 7)
                    ).ToList();
                case 6:
                    return ListViewRE.Where(x => (x.price.ToLower().Contains("tỷ") == true) &&
                                                    (double.Parse((x.price.Split(' ')[0])) >= 7) &&
                                                    (double.Parse((x.price.Split(' ')[0])) < 10)
                    ).ToList();
                case 7:
                    return ListViewRE.Where(x => (x.price.ToLower().Contains("tỷ") == true) &&
                                                    (double.Parse((x.price.Split(' ')[0])) >= 10) &&
                                                    (double.Parse((x.price.Split(' ')[0])) < 20)
                    ).ToList();
                case 8:
                    return ListViewRE.Where(x => (x.price.ToLower().Contains("tỷ") == true) &&
                                                    (double.Parse((x.price.Split(' ')[0])) >= 20) &&
                                                    (double.Parse((x.price.Split(' ')[0])) < 30)
                    ).ToList();
                case 9:
                    return ListViewRE.Where(x => (x.price.ToLower().Contains("tỷ") == true) &&
                                                    (double.Parse((x.price.Split(' ')[0])) >= 30)
                    ).ToList();
                default:
                    return new List<RealEstateInfo>();
            }
        }

        // filter theo giá cho thuê
        public static List<RealEstateInfo> FilterPriceRent(int id, List<RealEstateInfo> ListViewRE)
        {
            ListViewRE = ListViewRE.Where(x => x.price.ToLower().Contains("thỏa thuận") == false).ToList();
            switch (id)
            {
                case 0:
                    return ListViewRE.Where(x => (double.Parse((x.price.Split(' ')[0])) < 1)
                    ).ToList();
                case 1:
                    return ListViewRE.Where(x => (double.Parse((x.price.Split(' ')[0])) >= 1) &&
                                                 (double.Parse((x.price.Split(' ')[0])) < 3)

                    ).ToList();
                case 2:
                    return ListViewRE.Where(x => (double.Parse((x.price.Split(' ')[0])) >= 3) &&
                                                 (double.Parse((x.price.Split(' ')[0])) < 5)
                    ).ToList();
                case 3:
                    return ListViewRE.Where(x => (double.Parse((x.price.Split(' ')[0])) >= 5) &&
                                                 (double.Parse((x.price.Split(' ')[0])) < 10)

                    ).ToList();
                case 4:
                    return ListViewRE.Where(x => (double.Parse((x.price.Split(' ')[0])) >= 10) &&
                                                 (double.Parse((x.price.Split(' ')[0])) < 40)

                    ).ToList();
                case 5:
                    return ListViewRE.Where(x => (double.Parse((x.price.Split(' ')[0])) >= 40) &&
                                                 (double.Parse((x.price.Split(' ')[0])) < 70)
                    ).ToList();
                case 6:
                    return ListViewRE.Where(x => (double.Parse((x.price.Split(' ')[0])) >= 70) &&
                                                 (double.Parse((x.price.Split(' ')[0])) < 100)

                    ).ToList();
                case 7:
                    return ListViewRE.Where(x => (double.Parse((x.price.Split(' ')[0])) >= 100)

                     ).ToList();
                default:
                    return new List<RealEstateInfo>();
            }
        }


        // filter theo diện tích
        public static List<RealEstateInfo> FilterArea(int id, List<RealEstateInfo> ListViewRE)
        {
            switch (id)
            {
                case 0:
                    return ListViewRE.Where(x => x.area < 30).ToList();
                case 1:
                    return ListViewRE.Where(x =>
                                                x.area >= 30 &&
                                                x.area < 50
                    ).ToList();
                case 2:
                    return ListViewRE.Where(x =>
                                                x.area >= 50 &&
                                                x.area < 80
                    ).ToList();
                case 3:
                    return ListViewRE.Where(x =>
                                                x.area >= 80 &&
                                                x.area < 100
                    ).ToList();
                case 4:
                    return ListViewRE.Where(x =>
                                                x.area >= 100 &&
                                                x.area < 150
                    ).ToList();
                case 5:
                    return ListViewRE.Where(x =>
                                                x.area >= 150 &&
                                                x.area < 300
                    ).ToList();
                case 6:
                    return ListViewRE.Where(x =>
                                                x.area >= 300 &&
                                                x.area < 500
                    ).ToList();
                case 7:
                    return ListViewRE.Where(x =>
                                                x.area >= 500
                    ).ToList();
                default:
                    return new List<RealEstateInfo>();
            }
        }
    }
}
