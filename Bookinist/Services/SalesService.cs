using Bookinist.DAL.Entityes;
using Bookinist.Repositories;
using Bookinist.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookinist.Services
{
    class SalesService : ISalesService
    {
        private readonly IRepository<Book> _books;
        private readonly IRepository<Deal> _deals;

        public IEnumerable<Deal> Deals => _deals.Items;

        public SalesService(
            IRepository<Book> Books,
            IRepository<Deal> Deals)
        {
            _books = Books;
            _deals = Deals;
        }

        public async Task<Deal> MakeADeal(string BookName, Seller Seller, Buyer Buyer, decimal Price)
        {
            var book = await _books.Items.FirstOrDefaultAsync(b => b.Name == BookName).ConfigureAwait(false);
            if (book is null) return null;

            var deal = new Deal
            {
                Book = book,
                Seller = Seller,
                Buyer = Buyer,
                Price = Price
            };

            return await _deals.AddAsync(deal);
        }
    }
}
