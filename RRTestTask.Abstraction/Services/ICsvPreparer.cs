using System;
using System.Collections.Generic;
using System.Text;
using RRTestTask.Domain;

namespace RRTestTask.Abstraction.Services
{
    public interface ICsvPreparer
    {
        List<PriceItem> Prepare(List<PriceItem> priceItems);
    }
}
