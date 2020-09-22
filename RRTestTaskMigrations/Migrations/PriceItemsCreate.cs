using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace RRTestTask.Migrations.Migrations
{
    [Migration(2020092201)]
    public class PriceItemsCreate : Migration
    {
        private readonly string _tableName = "PriceItems";

        public override void Up()
        {
            if(!Schema.Table(_tableName).Exists())
            {
                Create.Table(_tableName)
                    .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                    .WithColumn("Vendor").AsAnsiString(64).NotNullable()
                    .WithColumn("Number").AsAnsiString(64).NotNullable()
                    .WithColumn("SearchVendor").AsAnsiString(64).NotNullable()
                    .WithColumn("SearchNumber").AsAnsiString(64).NotNullable()
                    .WithColumn("Description").AsAnsiString(512).NotNullable()
                    .WithColumn("Price").AsDecimal(18, 2).NotNullable()
                    .WithColumn("Count").AsInt32().NotNullable();
            }
        }

        public override void Down()
        {
            if(Schema.Table(_tableName).Exists())
            {
                Delete.Table(_tableName);
            }
        }

    }
}
