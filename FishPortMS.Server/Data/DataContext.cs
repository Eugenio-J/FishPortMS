using Microsoft.EntityFrameworkCore;

namespace FishPortMS.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    }

}
