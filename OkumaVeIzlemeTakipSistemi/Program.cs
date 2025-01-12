using Microsoft.EntityFrameworkCore;
using OkumaVeIzlemeTakipSistemi.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VeritabaniContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Kullanici}/{action=GirisYap}/{id?}");

app.Run();
