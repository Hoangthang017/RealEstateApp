using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RealEstateApplication.Model
{
    static class OpenUC
    {
        public static Grid GridOpenUC { get; set; }

        public static UserControl CurrentUC { get; set; }

        public static void OpenChildUC(UserControl _currentUC)
        {
            CurrentUC = _currentUC;
            GridOpenUC.Children.Clear();
            GridOpenUC.Children.Add(CurrentUC);
        }
    }
}
