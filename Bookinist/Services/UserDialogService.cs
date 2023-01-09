using Bookinist.DAL.Entityes;
using Bookinist.Services.Interfaces;
using Bookinist.ViewModels;
using Bookinist.Views.Windows;
using System.Windows;

namespace Bookinist.Services
{
    internal class UserDialogService : IUserDialog
    {
        public bool Edit(Book book)
        {
            var bookEditorModel = new BookEditorViewModel(book);

            var bookEditorWindow = new BookEditorWindow
            {
                DataContext = bookEditorModel
            };

            if (bookEditorWindow.ShowDialog() != true) return false;

            book.Name = bookEditorModel.Name;

            return true;
        }

        public bool ConfirmInformation(string Information, string Caption) => MessageBox
           .Show(
                Information, Caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Information)
                == MessageBoxResult.Yes;

        public bool ConfirmWarning(string Warning, string Caption) => MessageBox
           .Show(
                Warning, Caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning)
                == MessageBoxResult.Yes;

        public bool ConfirmError(string Error, string Caption) => MessageBox
           .Show(
                Error, Caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Error)
                == MessageBoxResult.Yes;
    }
}
