using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RealEstateApplication.Model
{
    public class passDataDetailPJ
    {
        public static ProjectInfo cellProject { get; set; }
        public static ImageSource AnhTongQuan { get; set; }
        public static ImageSource AnhViTri { get; set; }
        public static ImageSource AnhMatBang { get; set; }
        public static ImageSource AnhTienTich { get; set; }

        public static Visibility isAnhTongQuan { get; set; }
        public static Visibility isAnhViTri { get; set; }
        public static Visibility isAnhMatBang { get; set; }
        public static Visibility isAnhTienTich { get; set; }

        public static ProjectInfo cellProjectInfo { get; set; }
    }
}
