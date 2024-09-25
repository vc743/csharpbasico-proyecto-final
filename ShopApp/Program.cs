using Microsoft.EntityFrameworkCore;
using ShopApp.Data.Context;
using ShopApp.Data.Daos;
using ShopApp.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShopDbContext>(options => 
                                    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDb")));

builder.Services.AddTransient<ICategory, DaoCategory>();
builder.Services.AddTransient<ICustomer, DaoCustomer>();
builder.Services.AddTransient<ISupplier, DaoSupplier>();
builder.Services.AddTransient<IProduct, DaoProduct>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
