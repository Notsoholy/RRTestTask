using System;
using System.Collections.Generic;
using System.Text;
using RRTestTask.Domain;

namespace RRTestTask.Abstraction.Repositories
{
    public interface IPriceItemRepository
    {
        void Insert(List<PriceItem> priceItems);
    }
}
