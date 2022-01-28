using Microsoft.EntityFrameworkCore;
using WebServiceASP.NET_MVC.Models;

var builder = WebApplication.CreateBuilder(args);

//Get string from config file (.json)
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(option => option.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

//app.Map("/", (ApplicationContext db) => db.Product.ToList());
/// <summary>
/// Добавление MapEndPoin
/// 
/// </summary>



app.Run();
