using Bookinist.DAL.Entityes;
using Bookinist.Repositories;
using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    class BuyersViewModel : ViewModel
    {
        private readonly IRepository<Buyer> _buyers;

        public BuyersViewModel(IRepository<Buyer> Buyers)
        {
            _buyers = Buyers;
        }
    }
}
