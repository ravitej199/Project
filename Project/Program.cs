using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Repository.IRepository;
using Project.Repository;
using Project.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})



.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Logging.AddConsole();
builder.Services.AddScoped<ITruckGoodsRepository, TruckGoodsRepository>();
builder.Services.AddScoped<TruckGoodsService>();
builder.Host.UseSerilog();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");

    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
    await CreateDefaultUsers(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePagesWithReExecute("/Account/AccessDenied");

app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An unhandled exception occurred.");

        context.Response.Redirect("/Home/Error");
    }
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "SecurityOperator", "CustomsOfficer" };

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

async Task CreateDefaultUsers(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Create GateOperator user
    var gateOperatorUser = new IdentityUser { UserName = "gateoperator@example.com", Email = "gateoperator@example.com" };
    string gateOperatorPassword = "Password123!";
    var user = await userManager.FindByEmailAsync("gateoperator@example.com");
    if (user == null)
    {
        var createGateOperator = await userManager.CreateAsync(gateOperatorUser, gateOperatorPassword);
        if (createGateOperator.Succeeded)
        {
            await userManager.AddToRoleAsync(gateOperatorUser, "SecurityOperator");
        }
    }
    var customsOfficerUser = new IdentityUser { UserName = "customsofficer@example.com", Email = "customsofficer@example.com" };
    string customsOfficerPassword = "Password123!";
    user = await userManager.FindByEmailAsync("customsofficer@example.com");
    if (user == null)
    {
        var createCustomsOfficer = await userManager.CreateAsync(customsOfficerUser, customsOfficerPassword);
        if (createCustomsOfficer.Succeeded)
        {
            await userManager.AddToRoleAsync(customsOfficerUser, "CustomsOfficer");
        }
    }
}

app.Run();
