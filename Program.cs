using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BloodProject3.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BloodProject3DbContextConnection") ?? throw new InvalidOperationException("Connection string 'BloodProject3DbContextConnection' not found."); ;

builder.Services.AddDbContext<BloodProject3DbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BloodProject3DbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

DbInitialiser.AddData(app); //calls adddata method to seed the database

using (var scope = app.Services.CreateScope()) //creates admin role if it doesn't exist
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));
}

/*
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string adminID = "00000000000";
    string AdminPassword = "BloodDonation@123";



    /*
    if (await userManager.FindByEmailAsync("BDstaff@org.nz") == null)
    {
        var adminUser = new User
        {
            Id = adminID,
            UserName = "BDstaff@org.nz",
            Email = "BDstaff@org.nz";

            await userManager.CreateAsync(adminUser, AdminPassword);
            await userManager.AddToRoleAsync(adminUser, "Admin");
        };
    }
    

    if (await userManager.FindByEmailAsync("BDstaff@org.nz") == null)
    {
        var user = new IdentityUser();
        user.Id = adminID;
        user.UserName = "BDstaff@org.nz";
        user.Email = "BDstaff@org.nz";


        await userManager.CreateAsync(user, AdminPassword);
        await userManager.AddToRoleAsync(user, "Admin");

    }
}
 */

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string adminID = "00000000000";
    string adminEmail = "BDstaff@org.nz";
    string adminPassword = "BloodDonation@123";

    // searches by id first
    var existingUser = await userManager.FindByIdAsync(adminID);

    if (existingUser == null)
    {
        // Only create a new ID if it doesnt exist yet
        var user = new IdentityUser
        {
            Id = adminID,
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
    else
    {
        // If user exists, ensures details are correct
        //  Uses the 'existingUser' object because EF is already tracking it
        if (!await userManager.IsInRoleAsync(existingUser, "Admin"))
        {
            await userManager.AddToRoleAsync(existingUser, "Admin");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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