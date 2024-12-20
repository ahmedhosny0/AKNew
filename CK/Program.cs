using CK.Models;
using CK.Services;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using HangfireBasicAuthenticationFilter;
using CK.Models.AXDBTest;
using CK.Repo;
using CK.ViewModel;
using CK.Repo.SalesOrder;
using CK.Models.TopSoft;
using CK.Repo.Customer;
using CK.Models.AXDB;
using CK.Repo.Base;
using CK.Repo.Zone;
using CK.Repo.Area;
using CK.Repo.Street;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICompareTenderRepo, CompareTenderRepo>();
builder.Services.AddScoped<IBaseRepo, BaseRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IZoneRepo, ZoneRepo>();
builder.Services.AddScoped<IAreaRepo, AreaRepo>();
builder.Services.AddScoped<IStreetRepo, StreetRepo>();
builder.Services.AddScoped< ISalesOrderRepo ,SalesOrderRepo > ();
builder.Services.AddScoped<AxdbtestContext>();
builder.Services.AddScoped<TopSoftContext>();
builder.Services.AddScoped<AxdbContext>();
builder.Services.AddScoped<CkproUsersContext>();
builder.Services.AddScoped<SalesData>();
builder.Services.AddScoped<TenderData>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Login/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
builder.Services.AddDbContext<CkproUsersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set an appropriate timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        UsePageLocksOnDequeue = true,
        DisableGlobalLocks = true
    }));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Enable Hangfire Dashboard (optional)
//app.UseHangfireDashboard();
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    //AppPath = "" //The path for the Back To Site link. Set to null in order to hide the Back To  Site link.
    DashboardTitle = "My Website",
    Authorization = new[]
        {

                new HangfireCustomBasicAuthenticationFilter{
                    User = "AK",
                    Pass = "Aa@123123"
                    //User = System.Configuration.ConfigurationManager.GetSection("HangfireSettings:UserName").ToString(),
                    //Pass = System.Configuration.ConfigurationManager.GetSection("HangfireSettings:").ToString() 
                }
            }
}); ;
// Set up endpoint routing
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Logout}/{id?}");
});

// Example: Enqueue a job
// BackgroundJob.Enqueue(() => GenerateAndSendReport());

// Example: Schedule a recurring job
// RecurringJob.AddOrUpdate(() => GenerateAndSendReport(), Cron.Daily);
RecurringJob.AddOrUpdate(
    "daily-job-AK",
    () => JobServices.ExportExcel(), // Your method to run
    "0 8 * * *", // Cron expression for 6 AM Egypt time (4 AM UTC)
    TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time")); // Specify Egypt's time zone explicitly
RecurringJob.AddOrUpdate(
    "ItemPrice2024-job-AK",
    () => ItemPriceJob.ExportExcel(), // Your method to run
    "0 */12 * * *", // Cron expression for 6 AM Egypt time (4 AM UTC)
    TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time")); // Specify Egypt's time zone explicitly
app.Run();
