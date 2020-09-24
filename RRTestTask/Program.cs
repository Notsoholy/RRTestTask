using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RRTestTask.Abstraction.Repositories;
using RRTestTask.Abstraction.Services;
using RRTestTask.Infrastructure;
using RRTestTask.Infrastructure.Repositories;
using RRTestTask.Services;

namespace RRTestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<RRTestTaskDbContext>(options =>
                {
                    options.UseSqlite("Data Source = PriceItems.db");
                })
                .AddTransient<ICsvDownloader, CsvDownloader>()
                .AddTransient<ICsvParser, CsvParser>()
                .AddTransient<ICsvPreparer, CsvPreparer>()
                .AddTransient<IPriceItemRepository, PriceItemRepository>()
                .BuildServiceProvider();

            var downloader = serviceProvider.GetService<ICsvDownloader>();
            var parser = serviceProvider.GetService<ICsvParser>();
            var preparer = serviceProvider.GetService<ICsvPreparer>();
            var repository = serviceProvider.GetService<IPriceItemRepository>();

            Console.WriteLine("Starting.");

            downloader.DownloadAttachments();
            var priceItems = parser.Parse();
            var preparedItems = preparer.Prepare(priceItems);
            repository.Insert(preparedItems);

            Console.WriteLine("Completed.");
        }
    }
}
