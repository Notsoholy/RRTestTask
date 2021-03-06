﻿using System;
using FluentMigrator.Runner;
using RRTestTask.Migrations.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RRTestTask.Migrations
{
    class Program
    {
        static void Main()
        {
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var db = "PriceItems.db";

            var databaseConnection = $"Data Source = {Path.Combine(desktop, db)}";

            var serviceProvider = CreateServices(databaseConnection);

            using var scope = serviceProvider.CreateScope();

            UpdateDatabase(scope.ServiceProvider);
        }

        private static IServiceProvider CreateServices(string databaseConnection)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(r => r
                .AddSQLite()
                .WithGlobalConnectionString(databaseConnection)
                .ScanIn(typeof(PriceItemsCreate).Assembly).For.Migrations())
                .AddLogging(l => l.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }
    }
}
