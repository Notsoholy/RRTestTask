using System;
using System.Collections.Generic;
using System.Text;

namespace RRTestTask.Domain
{
    public class PriceItem
    {
        public int Id { get; set; }
        public string Vendor { get; set; }
        public string Number { get; set; }
        public string SearchVendor { get; set; }
        public string SearchNumber { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
