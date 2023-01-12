using Bookinist.DAL.Context;
using Bookinist.DAL.Entityes;
using Bookinist.Infrastructure.Commands;
using Bookinist.Repositories;
using Bookinist.Services.Interfaces;
using Bookinist.ViewModels.Base;
using System.Windows.Input;

namespace Bookinist.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IUserDialog _userDialog;
        private readonly IRepository<Book> _books;
        private readonly IRepository<Seller> _sellers;
        private readonly IRepository<Buyer> _buyers;
        private readonly IRepository<Deal> _deals;
        private readonly ISalesService _salesService;

        #region Title - Заголовок
        private string _title = "Главное окно программы";
        public string Title { get => _title; set => Set(ref _title, value); }
        #endregion

        #region CurrentModel Текущая дочерняя модель-представления
        private ViewModel _currentModel;
        public ViewModel CurrentModel { get => _currentModel; private set => Set(ref _currentModel, value); }
        #endregion

        #region Command ShowBooksViewCommand - Отобразить представление книг
        private ICommand _showBooksViewCommand;
        public ICommand ShowBooksViewCommand => _showBooksViewCommand
            ??= new LambdaCommand(OnShowBooksViewCommandExecuted, CanShowBooksViewCommandExecute);
        private bool CanShowBooksViewCommandExecute() => true;
        private void OnShowBooksViewCommandExecuted()
        {
            CurrentModel = new BooksViewModel(_books, _userDialog);
        }
        #endregion

        #region Command ShowBuyersViewCommand - Отобразить представление покупателей
        private ICommand _showBuyersViewCommand;
        public ICommand ShowBuyersViewCommand => _showBuyersViewCommand
            ??= new LambdaCommand(OnShowBuyersViewCommandExecuted, CanShowBuyersViewCommandExecute);
        private bool CanShowBuyersViewCommandExecute() => true;
        private void OnShowBuyersViewCommandExecuted()
        {
            CurrentModel = new BuyersViewModel(_buyers);
        }
        #endregion

        #region Command ShowStatisticViewCommand - Отобразить представление статистики
        private ICommand _showStatisticViewCommand;
        /// <summary>Отобразить представление статистики</summary>
        public ICommand ShowStatisticViewCommand => _showStatisticViewCommand
            ??= new LambdaCommand(OnShowStatisticViewCommandExecuted, CanShowStatisticViewCommandExecute);
        private bool CanShowStatisticViewCommandExecute() => true;
        private void OnShowStatisticViewCommandExecuted()
        {
            CurrentModel = new StatisticViewModel(
                _books, _buyers, _sellers,
                _deals
                );
        }
        #endregion

        public MainWindowViewModel(
            IUserDialog UserDialog,
            IRepository<Book> Books,
            IRepository<Seller> Sellers,
            IRepository<Buyer> Buyers,
            IRepository<Deal> Deals,
            ISalesService SalesService)
        {
            _userDialog = UserDialog;
            _books = Books;
            _sellers = Sellers;
            _buyers = Buyers;
            _deals = Deals;
            _salesService = SalesService;
        }
    }
}
