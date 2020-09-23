using System.Collections.Generic;
using RRTestTask.Domain;

namespace RRTestTask.Abstraction.Services
{
    public interface ICsvParser
    {
        List<PriceItem> Parse();
    }
}
