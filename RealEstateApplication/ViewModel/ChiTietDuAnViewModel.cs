using RealEstateApplication.Model;
using RealEstateApplication.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace RealEstateApplication.ViewModel
{
    public class ChiTietDuAnViewModel : BaseViewModel
    {
        public ICommand ClickComebackCommand { get; set; }
        public ICommand LoadedUserControlsCommand { get; set; }

        private ImageSource _anhtongquan;
        public ImageSource AnhTongQuan { get => _anhtongquan; set { _anhtongquan = value; OnPropertyChanged(); } }

        private ImageSource _anhvitri;
        public ImageSource AnhViTri { get => _anhvitri; set { _anhvitri = value; OnPropertyChanged(); } }

        private ImageSource _anhmatbang;
        public ImageSource AnhMatBang { get => _anhmatbang; set { _anhmatbang = value; OnPropertyChanged(); } }

        private ImageSource _anhdientich;
        public ImageSource AnhTienTich { get => _anhdientich; set { _anhdientich = value; OnPropertyChanged(); } }

        // Ẩn hiện ảnh
        private Visibility _isanhvitri;
        public Visibility isAnhViTri { get => _isanhvitri; set { _isanhvitri = value; OnPropertyChanged(); } }

        private Visibility _isanhtongquan;
        public Visibility isAnhTongQuan { get => _isanhtongquan; set { _isanhtongquan = value; OnPropertyChanged(); } }

        private Visibility _isanhmatbang;
        public Visibility isAnhMatBang { get => _isanhmatbang; set { _isanhmatbang = value; OnPropertyChanged(); } }

        private Visibility _isanhtientich;
        public Visibility isAnhTienTich { get => _isanhtientich; set { _isanhtientich = value; OnPropertyChanged(); } }

        private ProjectInfo _cellprojectinfo;
        public ProjectInfo cellProjectInfo { get => _cellprojectinfo; set { _cellprojectinfo = value; OnPropertyChanged(); } }

        public ChiTietDuAnViewModel()
        {

            LoadedUserControlsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AnhTongQuan = passDataDetailPJ.AnhTongQuan;
                AnhViTri = passDataDetailPJ.AnhViTri;
                AnhMatBang = passDataDetailPJ.AnhMatBang;
                AnhTienTich = passDataDetailPJ.AnhTienTich;

                isAnhViTri = passDataDetailPJ.isAnhViTri;
                isAnhTongQuan = passDataDetailPJ.isAnhTongQuan;
                isAnhMatBang = passDataDetailPJ.isAnhMatBang;
                isAnhTienTich = passDataDetailPJ.isAnhTienTich;

                cellProjectInfo = passDataDetailPJ.cellProjectInfo;

            });

            ClickComebackCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenUC.OpenChildUC(new ProjectUC());

            });
        }
    }
}
