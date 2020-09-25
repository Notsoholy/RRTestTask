using System.Collections.Generic;
using RRTestTask.Domain.Entities;

namespace RRTestTask.Abstraction.Services
{
    public interface ICsvParser
    {
        List<SimplePriceItem> Parse();
    }
}
