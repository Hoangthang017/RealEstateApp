using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApplication.Model
{
    public class passData
    {
        public static string passTinhThanhPho { get; set; }
        public static string passQuanHuyen { get; set; }
        public static string passXaHuyen { get; set; }
        public static string passSoNha { get; set; }

        public static string GioiTinh { get; set; }
        public static int NamSinh { get; set; }
        public static string LoaiHinh { get; set; }
        public static string SoNguoi { get; set; }
        public static double MucLuong { get; set; }

        public static void Clear()
        {
            passQuanHuyen = null;
            passTinhThanhPho = null;
            passXaHuyen = null;
            passSoNha = null;
            GioiTinh = null;
            NamSinh = 0;
            LoaiHinh = null;
            SoNguoi = null;
            MucLuong = 0;
        }
    }
}