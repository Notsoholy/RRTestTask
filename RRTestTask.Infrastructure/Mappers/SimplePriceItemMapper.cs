using CsvHelper.Configuration;
using RRTestTask.Domain.Entities;

namespace RRTestTask.Infrastructure.Mappers
{
    public sealed class SimplePriceItemMapper : ClassMap<SimplePriceItem>
    {
        public SimplePriceItemMapper()
        {
            Map(member => member.Vendor).Name("Бренд");
            Map(member => member.Number).Name("Каталожный номер");
            Map(member => member.Description).Name("Описание");
            Map(member => member.Price).Name("Цена, руб.");
            Map(member => member.Count).Name("Наличие");
        }
    }
}
