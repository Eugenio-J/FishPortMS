using FishPortMS.Shared.Models.Account;
using FishPortMS.Shared.Models.Announcements;
using FishPortMS.Shared.Models.Attendance;
using FishPortMS.Shared.Models.ConPettyCash;
using FishPortMS.Shared.Models.Expenses;
using FishPortMS.Shared.Models.FishPort;
using FishPortMS.Shared.Models.NotifSubscription;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Models.Sales;
using Microsoft.EntityFrameworkCore;

namespace FishPortMS.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
               .HasOne(o => o.SalesOrder)
               .WithMany(s => s.OrderItems)
               .HasForeignKey(o => o.SalesOrderId)
               .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
        public DbSet<Announcement> Announcements => Set<Announcement>();
        public DbSet<ConsignacionAttendance> ConsignacionAttendances => Set<ConsignacionAttendance>();
        public DbSet<PettyCash> PettyCashes => Set<PettyCash>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();
        public DbSet<Consignacion> Consignacions => Set<Consignacion>();
        public DbSet<ConsignacionEmployee> ConsignacionEmployees => Set<ConsignacionEmployee>();
        public DbSet<ConsignacionOwner> ConsignacionOwners => Set<ConsignacionOwner>();
        public DbSet<NotifSubscription> NotifSubscriptions => Set<NotifSubscription>();
        public DbSet<ClientInventory> ClientInventories => Set<ClientInventory>();
        public DbSet<ClientProduct> ClientProducts => Set<ClientProduct>();
        public DbSet<MasterInventory> MasterInventories => Set<MasterInventory>();
        public DbSet<MasterProduct> MasterProducts => Set<MasterProduct>();
        public DbSet<ProductCarousel> ProductCarousels => Set<ProductCarousel>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<ConsignacionSession> ConsignacionSessions => Set<ConsignacionSession>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
        public DbSet<Receipt> Receipts => Set<Receipt>();
        public DbSet<ReceiptItem> ReceiptItems => Set<ReceiptItem>();

    }

}
