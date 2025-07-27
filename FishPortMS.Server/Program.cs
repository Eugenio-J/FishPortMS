using FishPortMS.Server.Services.BlobStorageService;
using FishPortMS.Server.Data;
using FishPortMS.Server.Helper;
using FishPortMS.Server.Repositories.MasterProductRepository;
using FishPortMS.Server.Services.AccountService;
using FishPortMS.Server.Services.BlobStorageService;
using FishPortMS.Server.Services.DashboardService;
using FishPortMS.Server.Services.MasterProductService;
using FishPortMS.Server.Services.ProductCategoryService;
using FishPortMS.Server.Services.ReceiptService;
using FishPortMS.Server.Services.UserManagementService;
using FishPortMS.Server.Services.VendorExpenseService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FishPortSMSConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var blobStorageConnectionString = builder.Configuration.GetValue<string>("fpstoragekey") ?? throw new InvalidOperationException("Connection string 'fpstoragekey' not found.");

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton(x => new BlobServiceClient(blobStorageConnectionString));



// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetValue<string>("AccessToken")!)), 
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 3;
        options.QueueLimit = 3;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });


    rateLimiterOptions.AddPolicy("fixed-by-ip", context =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: RateLimitingHelper.GetClientIp(context),
        factory: _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
        }));


    rateLimiterOptions.AddPolicy("verify-email", context =>
           RateLimitPartition.GetTokenBucketLimiter(
        partitionKey: RateLimitingHelper.GetClientIp(context), 
         factory: _ => new TokenBucketRateLimiterOptions
         {
             TokenLimit = 3,               // Max tokens available
             TokensPerPeriod = 1,          // Regen 1 token per interval
             ReplenishmentPeriod = TimeSpan.FromSeconds(30), // Every 12s
             QueueLimit = 0,
             AutoReplenishment = true
         }));
});

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();
builder.Services.AddScoped<IMasterProductService, MasterProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IVendorExpenseService, VendorExpenseService>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();


// Repositories

builder.Services.AddScoped<IMasterProductRepository, MasterProductRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.UseAuthentication();

app.UseRateLimiter();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
