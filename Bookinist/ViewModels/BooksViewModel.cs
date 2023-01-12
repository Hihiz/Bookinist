using Bookinist.DAL.Entityes;
using Bookinist.Infrastructure.DebugServices;
using Bookinist.Repositories;
using Bookinist.Services.Interfaces;
using Bookinist.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System;
using MathCore.WPF.Commands;
using Bookinist.Services;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly IUserDialog _userDialog;

        #region Books - Коллекция книг
        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                if (Set(ref _books, value))
                {
                    _booksViewSource = new CollectionViewSource
                    {
                        Source = value,
                        SortDescriptions =
                        {
                            new SortDescription(nameof(Book.Name), ListSortDirection.Ascending)
                        }
                    };

                    _booksViewSource.Filter += OnBooksFilter;
                    _booksViewSource.View.Refresh();

                    OnPropertyChanged(nameof(BooksView));
                }
            }
        }
        #endregion

        #region BooksFilter - Искомое слово
        private string _booksFilter;
        public string BooksFilter
        {
            get => _booksFilter;
            set
            {
                if (Set(ref _booksFilter, value))
                    _booksViewSource.View.Refresh();
            }
        }
        #endregion

        private CollectionViewSource _booksViewSource;

        public ICollectionView BooksView => _booksViewSource?.View;

        #region SelectedBook  Выбранная книга
        private Book _selectedBook;
        public Book SelectedBook { get => _selectedBook; set => Set(ref _selectedBook, value); }
        #endregion

        #region Command LoadDataCommand - Команда загрузки данных из репозитория
        private ICommand _loadDataCommand;
        public ICommand LoadDataCommand => _loadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);
        private bool CanLoadDataCommandExecute() => true;
        private async Task OnLoadDataCommandExecuted()
        {
            //Books = (await _BooksRepository.Items.ToArrayAsync()).ToObservableCollection();
            Books = new ObservableCollection<Book>(await _booksRepository.Items.ToArrayAsync());
        }
        #endregion

        #region Command AddNewBookCommand - Добавление новой книги
        private ICommand _addNewBookCommand;
        public ICommand AddNewBookCommand => _addNewBookCommand
            ??= new LambdaCommand(OnAddNewBookCommandExecuted, CanAddNewBookCommandExecute);
        private bool CanAddNewBookCommandExecute() => true;
        private void OnAddNewBookCommandExecuted()
        {
            var new_book = new Book();

            if (!_userDialog.Edit(new_book))
                return;

            _books.Add(_booksRepository.Add(new_book));

            SelectedBook = new_book;
        }

        #endregion

        #region Command RemoveBookCommand : Book - Удаление указанной книги
        private ICommand _removeBookCommand;
        public ICommand RemoveBookCommand => _removeBookCommand
            ??= new LambdaCommand<Book>(OnRemoveBookCommandExecuted, CanRemoveBookCommandExecute);
        private bool CanRemoveBookCommandExecute(Book p) => p != null || SelectedBook != null;
        private void OnRemoveBookCommandExecuted(Book p)
        {
            var bookToRemove = p ?? SelectedBook;

            if (!_userDialog.ConfirmWarning($"Вы хотите удалить книгу {bookToRemove.Name}?", "Удаление книги"))
                return;

            _booksRepository.Remove(bookToRemove.Id);

            Books.Remove(bookToRemove);
            if (ReferenceEquals(SelectedBook, bookToRemove))
                SelectedBook = null;
        }

        #endregion

        public BooksViewModel()
            : this(new DebugBooksRepository(), new UserDialogService())
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");

            _ = OnLoadDataCommandExecuted();
        }

        public BooksViewModel(IRepository<Book> BooksRepository, IUserDialog UserDialog)
        {
            _booksRepository = BooksRepository;
            _userDialog = UserDialog;
        }

        private void OnBooksFilter(object Sender, FilterEventArgs E)
        {
            if (!(E.Item is Book book) || string.IsNullOrEmpty(BooksFilter)) return;

            if (!book.Name.Contains(BooksFilter))
                E.Accepted = false;
        }
    }
}
