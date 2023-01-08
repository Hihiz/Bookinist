using Bookinist.DAL.Entityes;
using Bookinist.Repositories;
using Bookinist.ViewModels.Base;
using MathCore.WPF.Commands;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bookinist.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private readonly IRepository<Book> _books;
        private readonly IRepository<Buyer> _buyers;
        private readonly IRepository<Seller> _sellers;
        private readonly IRepository<Deal> _deals;

        #region BooksCount Количество книг
        private int _booksCount;
        public int BooksCount { get => _booksCount; private set => Set(ref _booksCount, value); }
        #endregion

        #region Command ComputeStatisticCommand Вычислить статистические данные
        private ICommand _computeStatisticCommand;
        public ICommand ComputeStatisticCommand => _computeStatisticCommand ??= new LambdaCommandAsync(OnComputeStatisticCommandExecuted, CanComputeStatisticCommandExecute);
        private bool CanComputeStatisticCommandExecute() => true;
        private async Task OnComputeStatisticCommandExecuted()
        {
            BooksCount = await _books.Items.CountAsync();

            var deals = _deals.Items;

            var books = await deals.GroupBy(deal => deal.Books).Take(5).ToArrayAsync();

            var bestsellers = await deals.GroupBy(deal => deal.Books)
               .ToArrayAsync();
        }

        #endregion

        public StatisticViewModel(IRepository<Book> Books, IRepository<Buyer> Buyers, IRepository<Seller> Sellers, IRepository<Deal> Deals)
        {
            _books = Books;
            _buyers = Buyers;
            _sellers = Sellers;
            _deals = Deals;
        }
    }
}
