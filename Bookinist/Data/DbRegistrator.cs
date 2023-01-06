using Bookinist.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bookinist.Data
{
    static class DbRegistrator
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<BookinistContext>(options =>
            {
                var type = configuration["Type"];

                switch (type)
                {
                    case "MSSQL":
                        options.UseSqlServer(configuration.GetConnectionString(type));
                        break;

                    case "SQLite":
                        options.UseSqlite(configuration.GetConnectionString(type));
                        break;

                    case "InMemory":
                        options.UseInMemoryDatabase("Bookinist.db");
                        break;

                    case null:
                        throw new InvalidOperationException("Не определён тип БД");

                    default:
                        throw new InvalidOperationException($"Тип подключения {type} не поддерживается");
                }
            })
            .AddTransient<DbInitializer>();

    }
}
