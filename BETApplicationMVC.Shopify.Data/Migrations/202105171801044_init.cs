namespace BETApplicationMVC.Shopify.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Address_ID = c.Int(nullable: false, identity: true),
                        street_number = c.Int(nullable: false),
                        street_name = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        Country = c.String(),
                        Building_Name = c.String(),
                        Floor = c.String(),
                        Contact_Number = c.String(),
                        Address_Type = c.String(),
                        Comments = c.String(),
                        Order_ID = c.String(maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Address_ID)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.Order_ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Order_ID = c.String(nullable: false, maxLength: 128),
                        date_created = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 128),
                        shipped = c.Boolean(nullable: false),
                        status = c.String(),
                        packed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Order_ID)
                .ForeignKey("dbo.Customers", t => t.Email)
                .Index(t => t.Email);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 35),
                        LastName = c.String(nullable: false, maxLength: 35),
                        phone = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Email);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        payment_ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        AmountPaid = c.Double(nullable: false),
                        PaymentFor = c.String(nullable: false),
                        PaymentMethod = c.String(nullable: false),
                        Order_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.payment_ID)
                .ForeignKey("dbo.Customers", t => t.Email, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.Email)
                .Index(t => t.Order_ID);
            
            CreateTable(
                "dbo.Order_Item",
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
                .PrimaryKey(t => t.Order_Item_id)
                .ForeignKey("dbo.Items", t => t.item_id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_id)
                .Index(t => t.Order_id)
                .Index(t => t.item_id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemCode = c.Int(nullable: false, identity: true),
                        Category_ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 80),
                        Description = c.String(nullable: false, maxLength: 255),
                        QuantityInStock = c.Int(nullable: false),
                        Picture = c.Binary(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ItemCode)
                .ForeignKey("dbo.Categories", t => t.Category_ID, cascadeDelete: true)
                .Index(t => t.Category_ID);
            
            CreateTable(
                "dbo.Cart_Item",
                c => new
                    {
                        cart_item_id = c.String(nullable: false, maxLength: 128),
                        cart_id = c.String(maxLength: 128),
                        item_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.cart_item_id)
                .ForeignKey("dbo.Carts", t => t.cart_id)
                .ForeignKey("dbo.Items", t => t.item_id, cascadeDelete: true)
                .Index(t => t.cart_id)
                .Index(t => t.item_id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        cart_id = c.String(nullable: false, maxLength: 128),
                        date_created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.cart_id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Category_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        Department_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Category_ID)
                .ForeignKey("dbo.Departments", t => t.Department_ID, cascadeDelete: true)
                .Index(t => t.Name, unique: true, name: "Category_Index")
                .Index(t => t.Department_ID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Department_ID = c.Int(nullable: false, identity: true),
                        Department_Name = c.String(nullable: false, maxLength: 80),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Department_ID)
                .Index(t => t.Department_Name, unique: true, name: "Department_Index");
            
            CreateTable(
                "dbo.StockCart_Item",
                c => new
                    {
                        cart_item_id = c.String(nullable: false, maxLength: 128),
                        cart_id = c.String(maxLength: 128),
                        item_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.cart_item_id)
                .ForeignKey("dbo.Items", t => t.item_id, cascadeDelete: true)
                .ForeignKey("dbo.StockCarts", t => t.cart_id)
                .Index(t => t.cart_id)
                .Index(t => t.item_id);
            
            CreateTable(
                "dbo.StockCarts",
                c => new
                    {
                        cart_id = c.String(nullable: false, maxLength: 128),
                        date_created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.cart_id);
            
            CreateTable(
                "dbo.Order_Tracking",
                c => new
                    {
                        tracking_ID = c.Int(nullable: false, identity: true),
                        order_ID = c.String(maxLength: 128),
                        date = c.DateTime(nullable: false),
                        status = c.String(),
                        Recipient = c.String(),
                    })
                .PrimaryKey(t => t.tracking_ID)
                .ForeignKey("dbo.Orders", t => t.order_ID)
                .Index(t => t.order_ID);
            
            CreateTable(
                "dbo.Affiliate_Joiner",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Affiliate_Key = c.Guid(nullable: false),
                        Email = c.String(),
                        New_Customer_Email = c.String(),
                        used = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Affiliates", t => t.Affiliate_Key, cascadeDelete: true)
                .Index(t => t.Affiliate_Key);
            
            CreateTable(
                "dbo.Affiliates",
                c => new
                    {
                        Affiliate_Key = c.Guid(nullable: false, identity: true),
                        Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Affiliate_Key)
                .ForeignKey("dbo.Customers", t => t.Email)
                .Index(t => t.Email);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        not_id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        text = c.String(),
                        date = c.DateTime(nullable: false),
                        isViewed = c.Boolean(nullable: false),
                        url = c.String(),
                        reply_email = c.String(),
                    })
                .PrimaryKey(t => t.not_id);
            
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
                .PrimaryKey(t => t.Order_Item_id)
                .ForeignKey("dbo.Items", t => t.item_id, cascadeDelete: true)
                .ForeignKey("dbo.StockOrders", t => t.Order_id)
                .Index(t => t.Order_id)
                .Index(t => t.item_id);
            
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Full_Name = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StockOrder_Item", "Order_id", "dbo.StockOrders");
            DropForeignKey("dbo.StockOrder_Item", "item_id", "dbo.Items");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Affiliate_Joiner", "Affiliate_Key", "dbo.Affiliates");
            DropForeignKey("dbo.Affiliates", "Email", "dbo.Customers");
            DropForeignKey("dbo.Addresses", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.Order_Tracking", "order_ID", "dbo.Orders");
            DropForeignKey("dbo.Order_Item", "Order_id", "dbo.Orders");
            DropForeignKey("dbo.Order_Item", "item_id", "dbo.Items");
            DropForeignKey("dbo.StockCart_Item", "cart_id", "dbo.StockCarts");
            DropForeignKey("dbo.StockCart_Item", "item_id", "dbo.Items");
            DropForeignKey("dbo.Items", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.Cart_Item", "item_id", "dbo.Items");
            DropForeignKey("dbo.Cart_Item", "cart_id", "dbo.Carts");
            DropForeignKey("dbo.Orders", "Email", "dbo.Customers");
            DropForeignKey("dbo.Payments", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.Payments", "Email", "dbo.Customers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.StockOrder_Item", new[] { "item_id" });
            DropIndex("dbo.StockOrder_Item", new[] { "Order_id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Affiliates", new[] { "Email" });
            DropIndex("dbo.Affiliate_Joiner", new[] { "Affiliate_Key" });
            DropIndex("dbo.Order_Tracking", new[] { "order_ID" });
            DropIndex("dbo.StockCart_Item", new[] { "item_id" });
            DropIndex("dbo.StockCart_Item", new[] { "cart_id" });
            DropIndex("dbo.Departments", "Department_Index");
            DropIndex("dbo.Categories", new[] { "Department_ID" });
            DropIndex("dbo.Categories", "Category_Index");
            DropIndex("dbo.Cart_Item", new[] { "item_id" });
            DropIndex("dbo.Cart_Item", new[] { "cart_id" });
            DropIndex("dbo.Items", new[] { "Category_ID" });
            DropIndex("dbo.Order_Item", new[] { "item_id" });
            DropIndex("dbo.Order_Item", new[] { "Order_id" });
            DropIndex("dbo.Payments", new[] { "Order_ID" });
            DropIndex("dbo.Payments", new[] { "Email" });
            DropIndex("dbo.Orders", new[] { "Email" });
            DropIndex("dbo.Addresses", new[] { "Order_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.StockOrders");
            DropTable("dbo.StockOrder_Item");
            DropTable("dbo.Notifications");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Affiliates");
            DropTable("dbo.Affiliate_Joiner");
            DropTable("dbo.Order_Tracking");
            DropTable("dbo.StockCarts");
            DropTable("dbo.StockCart_Item");
            DropTable("dbo.Departments");
            DropTable("dbo.Categories");
            DropTable("dbo.Carts");
            DropTable("dbo.Cart_Item");
            DropTable("dbo.Items");
            DropTable("dbo.Order_Item");
            DropTable("dbo.Payments");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.Addresses");
        }
    }
}
