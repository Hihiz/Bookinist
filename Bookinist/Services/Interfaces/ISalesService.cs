using Bookinist.DAL.Entityes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookinist.Services.Interfaces
{
    interface ISalesService
    {
        IEnumerable<Deal> Deals { get; }

        Task<Deal> MakeADeal(string BookName, Seller Seller, Buyer Buyer, decimal Price);
    }
}
