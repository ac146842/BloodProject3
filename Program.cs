using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BloodProject3.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BloodProject3DbContextConnection") ?? throw new InvalidOperationException("Connection string 'BloodProject3DbContextConnection' not found.");;

builder.Services.AddDbContext<BloodProject3DbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<BloodProject3DbContext>();

//call adddata method to seed the database with initial data

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

DbInitialiser.AddData(app);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
