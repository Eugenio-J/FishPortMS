using FishPortMS.Server.Data;

namespace FishPortMS.Server.Services.DashboardService
{
    public class DashboardService : IDashboardService
    {
        private readonly DataContext _dbContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DashboardService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
