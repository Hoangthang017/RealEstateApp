using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApplication.Model
{
    public static class Filter
    {
        public static List<string> ListTypeSellRE { get; set; }
        public static List<string> ListTypeRentRE { get; set; }
        public static List<string> ListAddressRE { get; set; }
        public static List<string> ListPriceRE { get; set; }
        public static List<string> ListAreaRE { get; set; }

        public static void CreateListData()
        {
            var infors = DataProvider.Ins.DB.RealEstateInfoes.ToList();
            ListTypeSellRE = new List<string>();
            ListTypeRentRE = new List<string>();
            ListAddressRE = new List<string>();
            ListAddressRE.Add("Toàn Quốc");
            foreach (var infor in infors)
            {
                var typeRE = infor.typeRealEstate;
                if (typeRE.Split(' ')[0].ToLower() == "bán")
                {
                    typeRE = infor.typeRealEstate.Replace("Bán ", "");
                    if (ListTypeSellRE.Contains(typeRE) == false)
                    {
                        ListTypeSellRE.Add(typeRE);
                    }
                }
                else
                {
                    typeRE = infor.typeRealEstate.Replace("Cho thuê", "");
                    if (ListTypeRentRE.Contains(typeRE) == false)
                    {
                        ListTypeRentRE.Add(typeRE);
                    }
                }

                var location = infor.locationRealEstate.Split(',')[1];
                if (ListAddressRE.Contains(location) == false)
                {
                    ListAddressRE.Add(location);
                }
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
    }
}
