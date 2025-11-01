using BuildingBlocks.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Modules.IAM.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();
    builder.Services.AddHttpContextAccessor();
    builder.Services.RegisterInsfrastructureBuildingBlocks(builder.Configuration);
    builder.Services.RegisterIAMModule();

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(o =>
        {
            o.ExpireTimeSpan = TimeSpan.FromDays(1);
            o.SlidingExpiration = true;
        });
}

var app = builder.Build();
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseAuthentication();

    app.UseRouting();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
