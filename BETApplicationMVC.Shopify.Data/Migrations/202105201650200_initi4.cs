namespace BETApplicationMVC.Shopify.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initi4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockOrder_Item", "item_id", "dbo.Items");
            DropForeignKey("dbo.StockOrder_Item", "Order_id", "dbo.StockOrders");
            DropIndex("dbo.StockOrder_Item", new[] { "Order_id" });
            DropIndex("dbo.StockOrder_Item", new[] { "item_id" });
            DropTable("dbo.StockOrder_Item");
            DropTable("dbo.StockOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StockOrders",
                c => new
                    {
                        Order_ID = c.String(nullable: false, maxLength: 128),
                        date_created = c.DateTime(nullable: false),
                        shipped = c.Boolean(nullable: false),
                        status = c.String(),
                        packed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Order_ID);
            
            CreateTable(
                "dbo.StockOrder_Item",
                c => new
                    {
                        Order_Item_id = c.Int(nullable: false, identity: true),
                        Order_id = c.String(maxLength: 128),
                        item_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        replied = c.Boolean(nullable: false),
                        date_replied = c.DateTime(),
                        accepted = c.Boolean(nullable: false),
                        date_accepted = c.DateTime(),
                        shipped = c.Boolean(nullable: false),
                        status = c.String(),
                        date_shipped = c.DateTime(),
                    })
                .PrimaryKey(t => t.Order_Item_id);
            
            CreateIndex("dbo.StockOrder_Item", "item_id");
            CreateIndex("dbo.StockOrder_Item", "Order_id");
            AddForeignKey("dbo.StockOrder_Item", "Order_id", "dbo.StockOrders", "Order_ID");
            AddForeignKey("dbo.StockOrder_Item", "item_id", "dbo.Items", "ItemCode", cascadeDelete: true);
        }
    }
}
