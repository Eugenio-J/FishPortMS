using FishPortMS.Shared.Models.Account;
using FishPortMS.Shared.Models.Attendance;
using FishPortMS.Shared.Models.ConPettyCash;
using FishPortMS.Shared.Models.FishPort;
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

        // USER 

        public DbSet<User> Users => Set<User>();
        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();


        public DbSet<PettyCash> PettyCashes => Set<PettyCash>();

        //FISH PORT 


        public DbSet<Consignacion> Consignacions => Set<Consignacion>();
        public DbSet<ConsignacionEmployee> ConsignacionEmployees => Set<ConsignacionEmployee>();

        public DbSet<ConsignacionOwner> ConsignacionOwners => Set<ConsignacionOwner>();
        public DbSet<ConsignacionSession> ConsignacionSessions => Set<ConsignacionSession>();

        public DbSet<ConsignacionAttendance> ConsignacionAttendances => Set<ConsignacionAttendance>();
        // SALES
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();

    }

}
