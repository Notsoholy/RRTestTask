using System;
using System.Collections.Generic;
using System.Text;
using RRTestTask.Abstraction.Repositories;
using RRTestTask.Domain;

namespace RRTestTask.Infrastructure.Repositories
{
    public class PriceItemRepository : IPriceItemRepository
    {
        private readonly RRTestTaskDbContext _context;

        public PriceItemRepository(RRTestTaskDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Insert(List<PriceItem> priceItems)
        {
            if (priceItems.Count == 0)
                throw new ArgumentException("No price items found");

            _context.PriceItems.AddRange(priceItems);
            _context.SaveChanges();
        }
    }
}
