using System;
using System.Collections.Generic;
using System.Text;
using RRTestTask.Abstraction.Services;
using RRTestTask.Domain;
using RRTestTask.Domain.Entities;

namespace RRTestTask.Infrastructure.Mappers
{
    public class PriceItemMapper : IPriceItemMapper
    {
        public List<PriceItem> Map(List<SimplePriceItem> simplePriceItems)
        {
            var priceItems = new List<PriceItem>();

            foreach (var simplePriceItem in simplePriceItems)
            {

                var priceItem = new PriceItem();

                var priceSuccess = double.TryParse(simplePriceItem.Price, out double price);
                var countSuccess = int.TryParse(simplePriceItem.Count, out int count);

                if (priceSuccess)
                {
                    priceItem.Price = price;
                }
                else
                {
                    priceItem.Price = 0;
                }

                if (countSuccess)
                {
                    priceItem.Count = count;
                }
                else
                {
                    priceItem.Count = 0;
                }

                priceItem.Vendor = simplePriceItem.Vendor;
                priceItem.Number = simplePriceItem.Number;
                priceItem.SearchVendor = simplePriceItem.SearchVendor;
                priceItem.SearchNumber = simplePriceItem.SearchNumber;
                priceItem.Description = simplePriceItem.Description;

                priceItems.Add(priceItem);
            }

            return priceItems;
        }
    }
}
