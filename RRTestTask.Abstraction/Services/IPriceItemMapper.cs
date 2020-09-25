using System.Collections.Generic;
using RRTestTask.Domain;
using RRTestTask.Domain.Entities;

namespace RRTestTask.Abstraction.Services
{
    public interface IPriceItemMapper
    {
        List<PriceItem> Map(List<SimplePriceItem> simplePriceItem);
    }
}
