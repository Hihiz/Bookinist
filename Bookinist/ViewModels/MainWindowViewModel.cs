using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Title

        private string _title = "Главное окно программы";
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion
    }
}
