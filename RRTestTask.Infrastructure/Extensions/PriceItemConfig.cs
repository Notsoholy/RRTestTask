using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RRTestTask.Domain;

namespace RRTestTask.Infrastructure.Extensions
{
    static class PriceItemConfig
    {
        internal static ModelBuilder ConfigurePriceItem(this ModelBuilder builder)
        {
            return builder.Entity<PriceItem>(entity =>
            {
                entity.ToTable("PriceItems");
                entity.HasKey(e => e.Id);
                entity.Property(item => item.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(item => item.Vendor).IsRequired().HasMaxLength(64);
                entity.Property(item => item.Number).IsRequired().HasMaxLength(64);
                entity.Property(item => item.SearchVendor).IsRequired().HasMaxLength(64);
                entity.Property(item => item.SearchNumber).IsRequired().HasMaxLength(64);
                entity.Property(item => item.Description).IsRequired().HasMaxLength(512);
                entity.Property(item => item.Price).IsRequired();
                entity.Property(item => item.Count).IsRequired();
            });
        }
    }
}
