using Blazored.LocalStorage;
using FishPortMS;
using FishPortMS.Services.ClientAccountService;
using FishPortMS.Utilities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using FishPortMS.Client.Response;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using FishPortMS.Services.ClientUserManagementService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");  

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped<ModifiedSnackBar>();

builder.Services.AddPWAUpdater();

builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IClientAccountService, ClientAccountService>();
builder.Services.AddScoped<IClientUserManagementService, ClientUserManagementService>();


await builder.Build().RunAsync();
