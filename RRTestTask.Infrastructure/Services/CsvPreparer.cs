using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using RRTestTask.Abstraction.Services;
using RRTestTask.Domain;
using RRTestTask.Domain.Entities;

namespace RRTestTask.Services
{
    public class CsvPreparer : ICsvPreparer
    {
        public List<SimplePriceItem> Prepare(List<SimplePriceItem> simplePriceItems)
        {
            var expression = new Regex("[^a-zA-Z0-9]");
            var countExpression = new Regex(@"((\d+-)|\D)");

            foreach (var simplePriceItem in simplePriceItems)
            {
                simplePriceItem.SearchVendor = expression.Replace(simplePriceItem.Vendor, string.Empty).ToUpper();
                simplePriceItem.SearchNumber = expression.Replace(simplePriceItem.Number, string.Empty).ToUpper();
                simplePriceItem.Count = countExpression.Replace(simplePriceItem.Count, string.Empty).ToUpper();
                if (simplePriceItem.Description.Length > 512)
                {
                    simplePriceItem.Description = simplePriceItem.Description.Remove(511);
                }
            }

            return simplePriceItems;
        }
    }
}
