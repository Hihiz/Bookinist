using Bookinist.DAL.Entityes;
using Bookinist.Repositories;
using Bookinist.ViewModels.Base;
using System.Linq;

namespace Bookinist.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;

        #region Title
        private string _title = "Главное окно программы";
        public string Title { get => _title; set => Set(ref _title, value); }
        #endregion

        public MainWindowViewModel(IRepository<Book> BooksRepository)
        {
            _booksRepository = BooksRepository;

            var books = BooksRepository.Items.Take(10).ToArray();
        }
    }
}
