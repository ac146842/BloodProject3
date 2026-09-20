using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BloodProject3.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BloodProject3DbContextConnection") ?? throw new InvalidOperationException("Connection string 'BloodProject3DbContextConnection' not found."); ;

builder.Services.AddDbContext<BloodProject3DbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BloodProject3DbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Calls the AddData method to seed the database
DbInitialiser.AddData(app); 


// Creates the "Admin" user role if it doesn't exist yet
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));
}

// Creates an Admin account if one doesn't exist yet, or ensures the existing account has the correct details
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    // Admin account details
    string adminID = "00000000000";
    string adminEmail = "BDstaff@org.nz";
    string adminPassword = "BloodDonation@123";

    // Checks to see if the Admin account already exists using the adminID
    var existingUser = await userManager.FindByIdAsync(adminID);

    if (existingUser == null)
    {
        // Sets up a new admin user account with it's details
        var user = new User
        {
            Id = adminID,
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            FirstName = "System",      
            LastName = "Admin"  
        };

        // Creates the user in the database and gives the user the "Admin" role
        var result = await userManager.CreateAsync(user, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
    else
    {
        // If the user exists but lost the Admin role, this gives it back to them
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