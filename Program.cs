using Microsoft.EntityFrameworkCore;
using VivesShop.Ui.Mvc.Core;
using VivesShop.Ui.Mvc.Models;
using VivesShop.Ui.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<VivesShopDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(VivesShopDbContext)); 
});

// Register the ShoppingCartService as a singleton
builder.Services.AddSingleton<ShoppingCartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    var scope = app.Services.CreateScope();
    var database = scope.ServiceProvider.GetRequiredService<VivesShopDbContext>();
    database.Seed();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
