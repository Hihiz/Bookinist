using Bookinist.DAL.Context;
using Bookinist.DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.DAL
{
    class DealsRepository : DbRepository<Deal>
    {
        public override IQueryable<Deal> Items => base.Items
            .Include(item => item.Books)
            .Include(item => item.Seller)
            .Include(item => item.Buyer);

        public DealsRepository(BookinistContext db) : base(db) { }
    }
}
