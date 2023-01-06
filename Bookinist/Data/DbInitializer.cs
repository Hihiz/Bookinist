using Bookinist.DAL.Context;
using Bookinist.DAL.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bookinist.Data
{
    internal class DbInitializer
    {
        private readonly BookinistContext _db;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(BookinistContext db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация БД...");

            _logger.LogInformation("Удаление существующей БД...");
            await _db.Database.EnsureDeletedAsync()/*.ConfigureAwait(false)*/;
            _logger.LogInformation($"Удаление существующей БД выполнено за {timer.ElapsedMilliseconds} мс");

            //_db.Database.EnsureCreated();

            _logger.LogInformation("Миграция БД...");
            /// создает бд (если ее нет) и накатывает на нее все миграции которые есть на текущий момент
            await _db.Database.MigrateAsync();
            _logger.LogInformation($"Миграция БД выполнена за {timer.ElapsedMilliseconds}");

            if (await _db.Books.AnyAsync()) return;

            await InitializeCategories();
            await InitializeBooks();
            await InitializeSellers();
            await InitializeBayers();
            await InitializeDeals();

            _logger.LogInformation($"Инициализация БД выполнена за {timer.Elapsed.TotalSeconds}");
        }

        private const int _categoriesCount = 10;
        private Category[] _categories;

        private async Task InitializeCategories()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация категорий...");

            _categories = new Category[_categoriesCount];
            for (var i = 0; i < _categoriesCount; i++)
                _categories[i] = new Category { Name = $"Категория {i + 1}" };

            await _db.Categorys.AddRangeAsync(_categories);
            await _db.SaveChangesAsync();

            _logger.LogInformation($"Инициализация категорий выполнена за {timer.ElapsedMilliseconds}");
        }

        private const int _booksCount = 10;
        private Book[] _books;

        private async Task InitializeBooks()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация книг...");

            var rnd = new Random();

            _books = Enumerable.Range(1, _booksCount)
                .Select(i => new Book
                {
                    Name = $"Книга {i}",
                    Category = rnd.NextItemRnd(_categories)
                })
               .ToArray();

            await _db.Books.AddRangeAsync(_books);
            await _db.SaveChangesAsync();

            _logger.LogInformation($"Инициализация книг выполнена за {timer.ElapsedMilliseconds}");
        }

        private const int _sellersCount = 10;
        private Seller[] _sellers;

        private async Task InitializeSellers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация продавцов...");

            var rnd = new Random();

            _sellers = Enumerable.Range(1, _sellersCount)
                .Select(i => new Seller
                {
                    Name = $"Продавец-Имя {i}",
                    Surname = $"Продавец-Фамилия {i}",
                    Patronymic = $"Продавец-Отчество {i}",
                })
                .ToArray();

            await _db.Sellers.AddRangeAsync(_sellers);
            await _db.SaveChangesAsync();

            _logger.LogInformation($"Инициализация продавцов выполнена за {timer.ElapsedMilliseconds}");
        }


        private const int _buyerCount = 10;
        private Buyer[] _buyer;

        private async Task InitializeBayers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация покупателей...");

            var rnd = new Random();

            _buyer = Enumerable.Range(1, _buyerCount)
                .Select(i => new Buyer
                {
                    Name = $"Покупатель-Имя {i}",
                    Surname = $"Покупатель-Фамилия {i}",
                    Patronymic = $"Покупатель-Отчество {i}",
                })
                .ToArray();

            await _db.Buyers.AddRangeAsync(_buyer);
            await _db.SaveChangesAsync();

            _logger.LogInformation($"Инициализация покупателей выполнена за {timer.ElapsedMilliseconds}");
        }

        private const int _dealsCount = 1000;

        private async Task InitializeDeals()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация сделок...");

            var rnd = new Random();
            var deals = Enumerable.Range(1, _dealsCount).Select(i => new Deal
            {
                Books = rnd.NextItemRnd(_books),
                Seller = rnd.NextItemRnd(_sellers),
                Buyer = rnd.NextItemRnd(_buyer),
                Price = (decimal)(rnd.NextDouble() * 4000 + 700)
            });

            await _db.Deals.AddRangeAsync(deals);
            await _db.SaveChangesAsync();

            _logger.LogInformation($"Инициализация сделок выполнена за {timer.ElapsedMilliseconds}");
        }
    }
}