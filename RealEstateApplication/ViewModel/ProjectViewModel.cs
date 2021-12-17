using System.Windows.Input;

namespace RealEstateApplication.ViewModel
{
    public class ProjectViewModel : BaseViewModel
    {
        // command mẫu để copy
        public ICommand LoadedMainWindowCommand { get; set; }
        public ProjectViewModel()
        {
            LoadedMainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
            });
        }
    }
}
