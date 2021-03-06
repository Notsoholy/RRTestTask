﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using RRTestTask.Abstraction.Services;
using RRTestTask.Domain;
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

            if (files.Count() == 0)
                throw new ArgumentException("No files found");

            foreach (var file in files)
            {
                using (var sr = new StreamReader(file))
                {
                    using (var reader = new CsvReader(sr, CultureInfo.InvariantCulture))
                    {
                        reader.Configuration.Delimiter = ";";
                        reader.Configuration.RegisterClassMap<SimplePriceItemMapper>();

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
