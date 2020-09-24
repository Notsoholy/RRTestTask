using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using RRTestTask.Abstraction.Services;
using RRTestTask.Domain;
using RRTestTask.Infrastructure.Mappers;

namespace RRTestTask.Services
{
    public class CsvParser : ICsvParser
    {
        public List<PriceItem> Parse()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv");

            var goodPriceItems = new List<PriceItem>();
            var badPriceItems = new List<string>();

            if (files.Count() == 0)
                throw new ArgumentException("No files found");

            foreach (var file in files)
            {
                using (var sr = new StreamReader(file))
                {
                    using (var reader = new CsvReader(sr, CultureInfo.InvariantCulture))
                    {
                        reader.Configuration.Delimiter = ";";
                        reader.Configuration.RegisterClassMap<PriceItemMapper>();
                        reader.Configuration.BadDataFound = context =>
                        {
                            badPriceItems.Add(context.RawRecord);
                        };

                        while (reader.Read() && !reader.Context.IsFieldBad)
                        {
                            goodPriceItems.Add(reader.GetRecord<PriceItem>());
                        }
                    }
                }
            }

            return goodPriceItems;
        }
    }
}
