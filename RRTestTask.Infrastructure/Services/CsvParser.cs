using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using RRTestTask.Abstraction.Services;
using RRTestTask.Domain.Entities;
using RRTestTask.Infrastructure.Mappers;

namespace RRTestTask.Services
{
    public class CsvParser : ICsvParser
    {
        public List<SimplePriceItem> Parse()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv");

            var priceItems = new List<SimplePriceItem>();

            foreach (var file in files)
            {
                using (var sr = new StreamReader(file))
                {
                    using (var reader = new CsvReader(sr, CultureInfo.InvariantCulture))
                    {
                        reader.Configuration.Delimiter = ";";
                        reader.Configuration.RegisterClassMap<SimplePriceItemMapper>();
                        reader.Configuration.BadDataFound = null;

                        while (reader.Read() && !reader.Context.IsFieldBad)
                        {
                            priceItems.Add(reader.GetRecord<SimplePriceItem>());
                        }
                    }
                }
            }

            return priceItems;
        }
    }
}
