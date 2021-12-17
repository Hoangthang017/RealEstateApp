using System.Collections.Generic;

namespace RealEstateApplication.Model
{
    public class Container
    {
        public bool Purchase { get; set; }
        public List<RealEstateInfo> BackupViewRE { get; set; }
        public List<RealEstateInfo> BackupViewQueryRE { get; set; }
        public string Query { get; set; }
        public string DisplayTypeRE { get; set; }
        public string DisplayCity { get; set; }
        public string DisplayPrice { get; set; }
        public string DisplayArea { get; set; }
        public string DisplayFilter { get; set; }
        public RealEstateInfo ViewRE { get; set; }
        public string PriceLoan { get; set; }

        public void Clear()
        {
            Purchase = false;
            BackupViewQueryRE = null;
            BackupViewRE = null;
            Query = null;
            DisplayCity = null;
            DisplayFilter = null;
            DisplayArea = null;
            DisplayPrice = null;
            DisplayTypeRE = null;
            ViewRE = null;
            PriceLoan = null;
        }

        public Container()
        {
            Purchase = false;
            BackupViewRE = new List<RealEstateInfo>();
            BackupViewQueryRE = new List<RealEstateInfo>();
            ViewRE = new RealEstateInfo();
        }
    }
}
