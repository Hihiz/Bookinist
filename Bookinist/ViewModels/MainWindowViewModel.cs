using Bookinist.DAL.Entityes;
using Bookinist.Infrastructure.Commands;
using Bookinist.Repositories;
using Bookinist.Services.Interfaces;
using Bookinist.ViewModels.Base;
using System.Windows.Input;

namespace Bookinist.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Book> _books;
        private readonly IRepository<Seller> _sellers;
        private readonly IRepository<Buyer> _buyers;
        private readonly ISalesService _salesService;

        #region Title
        private string _title = "Главное окно программы";
        public string Title { get => _title; set => Set(ref _title, value); }
        #endregion


        #region CurrentModel : ViewModel - Текущая дочерняя модель-представления

        /// <summary>
        /// Текущая дочерняя модель-представления
        /// </summary>
        private ViewModel _currentModel;
        /// <summary>
        /// Текущая дочерняя модель-представления
        /// </summary>
        public ViewModel CurrentModel { get => _currentModel; private set => Set(ref _currentModel, value); }

        #endregion

        #region Command ShowBooksViewCommand - Отобразить представление книг

        /// <summary>
        /// Отобразить представление книг
        /// </summary>
        private ICommand _showBooksViewCommand;

        /// <summary>
        /// Отобразить представление книг
        /// </summary>
        public ICommand ShowBooksViewCommand => _showBooksViewCommand
            ??= new LambdaCommand(OnShowBooksViewCommandExecuted, CanShowBooksViewCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Отобразить представление книг
        /// </summary>
        /// <returns></returns>
        private bool CanShowBooksViewCommandExecute() => true;


        /// <summary>
        /// Проверка возможности выполнения - Отобразить представление книг
        /// </summary>
        /// <returns></returns>
        private void OnShowBooksViewCommandExecuted()
        {
            CurrentModel = new BooksViewModel(_books);
        }

        #endregion

        #region Command ShowBuyersViewCommand - Отобразить представление покупателей

        /// <summary>
        /// Отобразить представление покупателей
        /// </summary>
        private ICommand _showBuyersViewCommand;


        /// <summary>
        /// Отобразить представление покупателей
        /// </summary>
        public ICommand ShowBuyersViewCommand => _showBuyersViewCommand
            ??= new LambdaCommand(OnShowBuyersViewCommandExecuted, CanShowBuyersViewCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Отобразить представление покупателей
        /// </summary>
        /// <returns></returns>
        private bool CanShowBuyersViewCommandExecute() => true;

        /// <summary>
        /// Логика выполнения - Отобразить представление покупателей
        /// </summary>
        private void OnShowBuyersViewCommandExecuted()
        {
            CurrentModel = new BuyersViewModel(_buyers);
        }

        #endregion

        #region Command ShowStatisticViewCommand - Отобразить представление статистики

        /// <summary>
        /// Отобразить представление статистики
        /// </summary>
        private ICommand _showStatisticViewCommand;


        /// <summary>
        /// Отобразить представление статистики
        /// </summary>
        public ICommand ShowStatisticViewCommand => _showStatisticViewCommand
            ??= new LambdaCommand(OnShowStatisticViewCommandExecuted, CanShowStatisticViewCommandExecute);
        /// <summary>
        /// Проверка возможности выполнения - Отобразить представление статистики
        /// </summary>
        /// <returns></returns>
        private bool CanShowStatisticViewCommandExecute() => true;

        /// <summary>
        /// Логика выполнения - Отобразить представление статистики
        /// </summary>
        private void OnShowStatisticViewCommandExecuted()
        {
            CurrentModel = new StatisticViewModel(
                _books, _buyers, _sellers
                );
        }

        #endregion

        public MainWindowViewModel(IRepository<Book> Books, IRepository<Seller> Sellers, IRepository<Buyer> Buyers, ISalesService SalesService)
        {
            _books = Books;
            _sellers = Sellers;
            _buyers = Buyers;
            _salesService = SalesService;
        }
    }
}
