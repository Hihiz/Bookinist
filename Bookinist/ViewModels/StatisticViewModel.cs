using Bookinist.DAL.Entityes;
using Bookinist.Repositories;
using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private readonly IRepository<Book> _books;
        private readonly IRepository<Buyer> _buyers;
        private readonly IRepository<Seller> _sellers;

        public StatisticViewModel(IRepository<Book> Books, IRepository<Buyer> Buyers, IRepository<Seller> Sellers)
        {
            _books = Books;
            _buyers = Buyers;
            _sellers = Sellers;
        }
    }
}
