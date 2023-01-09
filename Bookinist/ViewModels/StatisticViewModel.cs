using Bookinist.DAL.Entityes;
using Bookinist.Models;
using Bookinist.Repositories;
using Bookinist.Service;
using Bookinist.ViewModels.Base;
using MathCore.WPF.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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

        public ObservableCollection<BestSellerInfo> Bestsellers { get; } = new ObservableCollection<BestSellerInfo>();

        #region Command ComputeStatisticCommand Вычислить статистические данные
        private ICommand _computeStatisticCommand;
        public ICommand ComputeStatisticCommand => _computeStatisticCommand ??= new LambdaCommandAsync(OnComputeStatisticCommandExecuted);
        private bool CanComputeStatisticCommandExecute() => true;
        private async Task OnComputeStatisticCommandExecuted()
        {
            await ComputeDealsStatisticAsync();
        }
        private async Task ComputeDealsStatisticAsync()
        {
            var bestsellersQuery = _deals.Items
               .GroupBy(b => b.Book.Id)
               .Select(deals => new { BookId = deals.Key, Count = deals.Count(), Sum = deals.Sum(d => d.Price) })
               .OrderByDescending(deals => deals.Count)
               .Take(5)
               .Join(_books.Items,
                    deals => deals.BookId,
                    book => book.Id,
                     (deals, book) => new BestSellerInfo
                     {
                         Book = book,
                         SellCount = deals.Count,
                         SumCost = deals.Sum
                     });

            Bestsellers.AddClear(await bestsellersQuery.ToArrayAsync());
            //foreach (var bestseller in await bestsellers_query.ToArrayAsync())
            //    Bestsellers.Add(bestseller);
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
