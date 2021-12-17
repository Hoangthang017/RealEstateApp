using System.Windows.Controls;

namespace RealEstateApplication.Model
{
    static class OpenUC
    {
        public static Grid BackupGridOpenUC { get; set; }

        public static void OpenChildUC(UserControl child)
        {
            BackupGridOpenUC.Children.Clear();
            BackupGridOpenUC.Children.Add(child);
        }
    }
}
