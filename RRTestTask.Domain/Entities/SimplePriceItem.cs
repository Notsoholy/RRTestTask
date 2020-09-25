using System;
using System.Collections.Generic;
using System.Text;

namespace RRTestTask.Domain.Entities
{
    public class SimplePriceItem
    {
        public string Id { get; set; }
        public string Vendor { get; set; }
        public string Number { get; set; }
        public string SearchVendor { get; set; }
        public string SearchNumber { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Count { get; set; }
    }
}
