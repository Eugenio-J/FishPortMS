using FishPortMS.Shared.Models.Account;
using FishPortMS.Shared.Models.Announcements;
using FishPortMS.Shared.Models.ConPettyCash;
using FishPortMS.Shared.Models.Expenses;
using FishPortMS.Shared.Models.NotifSubscription;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Models.Receipts;
using FishPortMS.Shared.Models.Views;
using Microsoft.EntityFrameworkCore;

namespace FishPortMS.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReceiptSalesSummaryVM>()
                .HasNoKey()
                .ToView("VReceiptSalesSummary");
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
        public DbSet<Announcement> Announcements => Set<Announcement>();
        public DbSet<PettyCash> PettyCashes => Set<PettyCash>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();
        public DbSet<NotifSubscription> NotifSubscriptions => Set<NotifSubscription>();
        public DbSet<MasterInventory> MasterInventories => Set<MasterInventory>();
        public DbSet<MasterProduct> MasterProducts => Set<MasterProduct>();
        public DbSet<ProductCarousel> ProductCarousels => Set<ProductCarousel>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<Receipt> Receipts => Set<Receipt>();
        public DbSet<ReceiptItem> ReceiptItems => Set<ReceiptItem>();
        public DbSet<VendorExpense> VendorExpenses => Set<VendorExpense>();
        public DbSet<VendorExpenseCategory> VendorExpenseCategories => Set<VendorExpenseCategory>();

        //Views

        public DbSet<ReceiptSalesSummaryVM> ReceiptSalesSummaryVMs => Set<ReceiptSalesSummaryVM>();


    }

}
