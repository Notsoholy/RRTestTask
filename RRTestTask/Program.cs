using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RRTestTask.Abstraction.Repositories;
using RRTestTask.Abstraction.Services;
using RRTestTask.Domain.Entities;
using RRTestTask.Infrastructure;
using RRTestTask.Infrastructure.Mappers;
using RRTestTask.Infrastructure.Repositories;
using RRTestTask.Services;

namespace RRTestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var mailConfig = new MailConfig();

            configuration.GetSection("MailConfig").Bind(mailConfig);

            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var db = "PriceItems.db";

            var dbPath = Path.Combine(desktopPath, db);

            var serviceProvider = new ServiceCollection()
                .AddDbContext<RRTestTaskDbContext>(options =>
                {
                    options.UseSqlite($"Data Source = {dbPath}");
                })
                .AddTransient<ICsvDownloader, CsvDownloader>()
                .AddTransient<ICsvParser, CsvParser>()
                .AddTransient<ICsvPreparer, CsvPreparer>()
                .AddTransient<IPriceItemRepository, PriceItemRepository>()
                .AddTransient<IPriceItemMapper, PriceItemMapper>()
                .BuildServiceProvider();

            var downloader = serviceProvider.GetService<ICsvDownloader>();
            var parser = serviceProvider.GetService<ICsvParser>();
            var preparer = serviceProvider.GetService<ICsvPreparer>();
            var repository = serviceProvider.GetService<IPriceItemRepository>();
            var mapper = serviceProvider.GetService<IPriceItemMapper>();

            Console.WriteLine("Starting...");
            Console.WriteLine("Downloading attachments...");

            downloader.DownloadAttachments(mailConfig.Host, mailConfig.Login, mailConfig.Password);

            Console.WriteLine("Download complete.");
            Console.WriteLine("Parsing attachments...");

            var simplePriceItems = parser.Parse();

            if (simplePriceItems.Count == 0)
            {
                Console.WriteLine("No attachments to parse.");
                return;
            }
            else
            {
                Console.WriteLine("Preparing attachments...");

                var preparedItems = preparer.Prepare(simplePriceItems);

                Console.WriteLine("Mapping attachments...");

                var mappedItems = mapper.Map(preparedItems);

                Console.WriteLine("Inserting records into DB...");

                repository.Insert(mappedItems);

                Console.WriteLine("Clearing up...");

                Clear();

                Console.WriteLine("Task complete.");
            }
        }

        private static void Clear()
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory()).Where(f => f.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase)).ToArray();

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
