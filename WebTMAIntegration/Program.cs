using WebTMAIntegration.Client;
using WebTMAIntegration.Data;
using WebTMAIntegration.Data.Helpers;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models;
using WebTMAIntegration.Services;
using WebTMAIntegration.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.Configure<UIHealthCareSettings>(
    builder.Configuration.GetSection("UIHealthCare"));

builder.Services.AddHttpClient<ITokenService, TokenService>();

// UihealthcareClient is wrapped in HttpClient factory for connection pooling
builder.Services.AddHttpClient<UIHealthCareClient>();
builder.Services.AddScoped<SqlClientHelper>();

builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<IFloorService, FloorService>();
builder.Services.AddScoped<IFloorRepository, FloorRepository>();
builder.Services.AddScoped<IWingService, WingService>();
builder.Services.AddScoped<IWingRepository, WingRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
