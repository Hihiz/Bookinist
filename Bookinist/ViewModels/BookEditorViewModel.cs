using Bookinist.DAL.Entityes;
using Bookinist.ViewModels.Base;
using System;

namespace Bookinist.ViewModels
{
    internal class BookEditorViewModel : ViewModel
    {
        #region BookId : int - Идентификатор книги

        public int BookId { get; }

        #endregion

        #region Name : string - Название книги

        /// <summary>Название книги</summary>
        private string _name;

        /// <summary>Название книги</summary>
        public string Name { get => _name; set => Set(ref _name, value); }

        #endregion

        public BookEditorViewModel()
            : this(new Book { Id = 1, Name = "Букварь!" })
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Не для рантайма");
        }

        public BookEditorViewModel(Book book)
        {
            BookId = book.Id;
            Name = book.Name;
        }
    }
}
