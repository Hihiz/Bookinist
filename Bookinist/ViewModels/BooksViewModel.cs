using Bookinist.DAL.Entityes;
using Bookinist.Infrastructure.DebugServices;
using Bookinist.Repositories;
using Bookinist.ViewModels.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;

        #region BooksFilter Искомое слово   
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

        private readonly CollectionViewSource _booksViewSource;

        public ICollectionView BooksView => _booksViewSource.View;

        public IEnumerable<Book> Books => _booksRepository.Items;

        public BooksViewModel()
            : this(new DebugBooksRepository())
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }

        public BooksViewModel(IRepository<Book> BooksRepository)
        {
            _booksRepository = BooksRepository;

            _booksViewSource = new CollectionViewSource
            {
                Source = _booksRepository.Items.ToArray(),
                SortDescriptions =
                {
                    new SortDescription(nameof(Book.Name), ListSortDirection.Ascending)
                }
            };

            _booksViewSource.Filter += OnBooksFilter;
        }

        private void OnBooksFilter(object Sender, FilterEventArgs E)
        {
            if (!(E.Item is Book book) || string.IsNullOrEmpty(BooksFilter)) return;

            if (!book.Name.Contains(BooksFilter))
                E.Accepted = false;
        }
    }
}
