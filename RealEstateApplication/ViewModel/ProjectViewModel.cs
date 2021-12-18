using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RealEstateApplication.Model;
using RealEstateApplication.UserControls;

namespace RealEstateApplication.ViewModel
{
    public class Projet
    {
        public ImageSource ImageViewer { get; set; }
        public ProjectInfo projectinfo { get; set; }
    }
    public class ProjectViewModel : BaseViewModel
    {
        // command mẫu để copy
        public ICommand LoadedProjectInfoCommand { get; set; }
        public ICommand ClickDuAnCommand { get; set; }
        public ICommand ClickDongDuAnCommand { get; set; }
        public ICommand ClickTimKiemCommand { get; set; }
        public ICommand SelectedCellsChangedPJCommand { get; set; }

        private ObservableCollection<Projet> _listprojects;
        public ObservableCollection<Projet> ListProjects { get => _listprojects; set { _listprojects = value; OnPropertyChanged(); } }

        private List<Projet> _backuplistprojects;
        public List<Projet> BackUPListProjects { get => _backuplistprojects; set { _backuplistprojects = value; OnPropertyChanged(); } }

        private ProjectInfo _cellprojectinfo;
        public ProjectInfo cellProjectInfo { get => _cellprojectinfo; set { _cellprojectinfo = value; OnPropertyChanged(); } }

        // Tìm kiếm

        private string _timkiemten;
        public string TimKiemTen { get => _timkiemten; set { _timkiemten = value; OnPropertyChanged(); } }

        private string _timkiemquanhuyen;
        public string TimKiemQuanHuyen { get => _timkiemquanhuyen; set { _timkiemquanhuyen = value; OnPropertyChanged(); } }

        private string _timkiemtinhthanhpho;
        public string TimKiemTinhThanhPho { get => _timkiemtinhthanhpho; set { _timkiemtinhthanhpho = value; OnPropertyChanged(); } }

        private Projet _DisplayPJ;
        public Projet DisplayPJ { get => _DisplayPJ; set { _DisplayPJ = value; OnPropertyChanged(); } }

        public ProjectViewModel()
        {

            LoadedProjectInfoCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                BackUPListProjects = new List<Projet>();
                var pro = DataProvider.Ins.DB.ProjectInfoes;
                foreach (var data in pro)
                {
                    BackUPListProjects.Add(new Projet()
                    {
                        ImageViewer = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images\\" + data.overviewImage)),
                        projectinfo = data

                    });
                }
                ListProjects = new ObservableCollection<Projet>(BackUPListProjects);
                TimKiemTen = "";
                TimKiemQuanHuyen = "";
                TimKiemTinhThanhPho = "";
            });

            SelectedCellsChangedPJCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DisplayPJ != null)
                {
                    passDataDetailPJ.cellProjectInfo = DisplayPJ.projectinfo;
                    cellProjectInfo = DisplayPJ.projectinfo;
                    if (cellProjectInfo.overviewImage != null)
                    {
                        passDataDetailPJ.AnhTongQuan = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images\\" + cellProjectInfo.overviewImage));
                        passDataDetailPJ.isAnhTongQuan = Visibility.Visible;
                    }
                    else
                    {
                        passDataDetailPJ.isAnhTongQuan = Visibility.Collapsed;
                    }

                    if (cellProjectInfo.locationImage != null)
                    {
                        passDataDetailPJ.AnhViTri = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images\\" + cellProjectInfo.locationImage));
                        passDataDetailPJ.isAnhViTri = Visibility.Visible;
                    }
                    else
                    {
                        passDataDetailPJ.isAnhViTri = Visibility.Collapsed;
                    }

                    if (cellProjectInfo.masterPlanImage != null)
                    {
                        passDataDetailPJ.AnhMatBang = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images\\" + cellProjectInfo.masterPlanImage));
                        passDataDetailPJ.isAnhMatBang = Visibility.Visible;
                    }
                    else
                    {
                        passDataDetailPJ.isAnhMatBang = Visibility.Collapsed;
                    }

                    if (cellProjectInfo.utilitiesImage != null)
                    {
                        passDataDetailPJ.AnhTienTich = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images\\" + cellProjectInfo.utilitiesImage));
                        passDataDetailPJ.isAnhTienTich = Visibility.Visible;
                    }
                    else
                    {
                        passDataDetailPJ.isAnhTienTich = Visibility.Collapsed;
                    }

                    OpenUC.OpenChildUC(new ChiTietDuAn());
                }

            });

            ClickTimKiemCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    if (TimKiemTen == "" && TimKiemQuanHuyen == "" && TimKiemTinhThanhPho == "")
                    {
                        ListProjects = new ObservableCollection<Projet>(BackUPListProjects);
                    }
                    else
                    {
                        List<Projet> ListofProject = new List<Projet>();
                        if (TimKiemTen != null && TimKiemTen != "")
                        {
                            List<Projet> ListofProduct = new List<Projet>();
                            foreach (var cellItems in BackUPListProjects)
                            {

                                string nameItem = cellItems.projectinfo.name.Trim().ToLower();
                                if (nameItem.Contains(TimKiemTen.Trim().ToLower()))
                                {
                                    ListofProject.Add(cellItems);
                                }
                            }
                        }

                        if (TimKiemQuanHuyen != null && TimKiemQuanHuyen != "")
                        {
                            List<Projet> backup = new List<Projet>(ListofProject);
                            if (ListofProject.Count() != 0)
                            {
                                ListofProject = new List<Projet>();
                                foreach (var cellItems in backup)
                                {

                                    string nameItem = cellItems.projectinfo.address.Trim().ToLower();
                                    if (nameItem.Contains(TimKiemQuanHuyen.Trim().ToLower()))
                                    {
                                        ListofProject.Add(cellItems);
                                    }
                                }
                            }
                            else
                            {
                                ListofProject = new List<Projet>();

                                foreach (var cellItems in BackUPListProjects)
                                {

                                    string nameItem = cellItems.projectinfo.address.Trim().ToLower();
                                    if (nameItem.Contains(TimKiemQuanHuyen.Trim().ToLower()))
                                    {
                                        ListofProject.Add(cellItems);
                                    }
                                }
                            }


                        }

                        if (TimKiemTinhThanhPho != null && TimKiemTinhThanhPho != "")
                        {
                            List<Projet> backup = new List<Projet>(ListofProject);

                            if (ListofProject.Count() != 0)
                            {
                                ListofProject = new List<Projet>();

                                foreach (var cellItems in backup)
                                {

                                    string nameItem = cellItems.projectinfo.address.Trim().ToLower();
                                    if (nameItem.Contains(TimKiemTinhThanhPho.Trim().ToLower()))
                                    {
                                        ListofProject.Add(cellItems);
                                    }
                                }
                            }
                            else
                            {
                                ListofProject = new List<Projet>();

                                foreach (var cellItems in BackUPListProjects)
                                {

                                    string nameItem = cellItems.projectinfo.address.Trim().ToLower();
                                    if (nameItem.Contains(TimKiemTinhThanhPho.Trim().ToLower()))
                                    {
                                        ListofProject.Add(cellItems);
                                    }
                                }
                            }
                        }

                        ListProjects = new ObservableCollection<Projet>(ListofProject);

                        (p as DataGrid).Items.Refresh();
                    }
                }
            });
        }
    }
}
