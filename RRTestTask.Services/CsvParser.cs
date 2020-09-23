using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using CsvHelper;
using RRTestTask.Abstraction.Services;
using RRTestTask.Domain;

namespace RRTestTask.Services
{
    public class CsvParser : ICsvParser
    {
        public List<PriceItem> Parse()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv");

            foreach (var file in files)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    using (CsvReader reader = new CsvReader(sr, CultureInfo.InvariantCulture))
                    {
                        reader.Configuration.Delimiter = ";";
                        IEnumerable priceItems = reader.GetRecords<PriceItem>();
                    }
                }
            }
        }
    }
}
