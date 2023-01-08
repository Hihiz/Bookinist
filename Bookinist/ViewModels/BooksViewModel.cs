using Bookinist.DAL.Entityes;
using Bookinist.Repositories;
using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;

        public BooksViewModel(IRepository<Book> BooksRepository)
        {
            _booksRepository = BooksRepository;
        }
    }
}
