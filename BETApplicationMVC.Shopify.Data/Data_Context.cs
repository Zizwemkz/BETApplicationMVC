using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BETApplicationMVC.Shopify.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string Full_Name { get; set; }
    }
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string roleName)
            : base(roleName: roleName)
        {
        }
    }
    public class Data_Context : IdentityDbContext<ApplicationUser>
    {
        public Data_Context()
            : base("Shoppify_DB", throwIfV1Schema: false)
        {
        }

        public static Data_Context Create()
        {
            return new Data_Context();
        }
        //Roles
        public DbSet<IdentityUserRole> UserInRole { get; set; }
        public DbSet<ApplicationRole> appRoles { get; set; }
        //Stock
        public DbSet<Department> Departments { get; set; }
        public DbSet<Affiliate> Affiliates { get; set; }
        public DbSet<Affiliate_Joiner> Affiliate_Joiners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        //Temporal Shopping
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cart_Item> Cart_Items { get; set; }
        //Stock Control
      
        public DbSet<StockCart> StockCarts { get; set; }
        public DbSet<StockCart_Item> StockCart_Items { get; set; }

        //Shopping
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Item> Order_Items { get; set; }        
        public DbSet<Order_Tracking> Order_Trackings { get; set; }

        //Address Book
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Shipping_Address> Shipping_Addresses { get; set; }
        //
        public DbSet<Payment> Payments { get; set; }


        public DbSet<Notification> Notifications { get; set; }
    }
}
