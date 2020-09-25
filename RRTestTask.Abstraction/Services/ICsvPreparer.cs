using System;
using System.Collections.Generic;
using System.Text;
using RRTestTask.Domain;
using RRTestTask.Domain.Entities;

namespace RRTestTask.Abstraction.Services
{
    public interface ICsvPreparer
    {
        List<SimplePriceItem> Prepare(List<SimplePriceItem> simplePriceItems);
    }
}
