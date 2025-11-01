using BuildingBlocks.Application.Common.Behaviors;
using BuildingBlocks.Infrastructure;
using Modules.IAM.Application;
using Modules.IAM.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();

    var mediatRKey = builder.Configuration.GetValue<string>("MediatR:Key");
    builder.Services.AddMediatR(c =>
    {
        c.RegisterServicesFromAssemblies(typeof(IAMApplicationMarker).Assembly);
        c.LicenseKey = mediatRKey;
        c.AddOpenBehavior(typeof(ValidationBehavior<,>));
    });
    builder.Services.RegisterInsfrastructureBuildingBlocks(builder.Configuration);
    builder.Services.RegisterIAMModule();
    builder.Services.AddHttpContextAccessor();
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

    app.UseRouting();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
