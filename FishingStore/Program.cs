using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using Microsoft.Extensions.Options;
using FishingStore.Model.Repos.Abstract;
using FishingStore.Model.Repos.EFs;
using FishingStore.Model;
using Microsoft.EntityFrameworkCore;
using FishingStore.Migrations;
using FishingStore.Model.Entities;
using FishingStore.Model.Parameters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddTransient<ICategoryRepo, EfCategoryRepo>();
 builder.Services.AddTransient<IItemRepo, EfItemRepo>();
builder.Services.AddTransient<IRoleRepo, EfRoleRepo>();
builder.Services.AddTransient<IUserRepo, EfUserRepo>();
builder.Services.AddTransient<ICartRepo, EfCartRepo>();
builder.Services.AddTransient<DataManager>();


var connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FishingStore;AttachDbFilename=|DataDirectory|\\Data\\FishingStore.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
builder.Services.AddDbContext<FishingStoreContext>(x => x.UseSqlServer(connection));
//builder.Services.AddIdentityCore<IdentityUser>(Options => Options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>().AddEntityFrameworkStores<FishingStoreContext>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => EfCartRepo.GetCart(sp));
builder.Services.AddControllersWithViews().
    AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).
    AddDataAnnotationsLocalization();
builder.Services.AddMemoryCache();
builder.Services.AddSession();




builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
        {
        new CultureInfo("en"),
        new CultureInfo("ru")
                };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ru");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var localizationOptions = services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
    //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    //var role = "Admin";
    app.UseRequestLocalization(localizationOptions);

    //    if (!await roleManager.RoleExistsAsync(role))
    //        await roleManager.CreateAsync(new IdentityRole(role));


}

app.Run();
