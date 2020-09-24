using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using RRTestTask.Abstraction.Services;
using RRTestTask.Domain;

namespace RRTestTask.Services
{
    public class CsvPreparer : ICsvPreparer
    {
        public List<PriceItem> Prepare(List<PriceItem> priceItems)
        {
            var expression = new Regex("[^a-zA-Z0-9]");
            var countExpression = new Regex(@"((\d+-)|>|<)");

            foreach (var priceItem in priceItems)
            {
                priceItem.SearchVendor = expression.Replace(priceItem.Vendor, string.Empty).ToUpper();
                priceItem.SearchNumber = expression.Replace(priceItem.Number, string.Empty).ToUpper();
                priceItem.Count = countExpression.Replace(priceItem.Count, string.Empty).ToUpper();
                priceItem.Description = priceItem.Description.Remove(511);
            }

            return priceItems;
        }
    }
}
