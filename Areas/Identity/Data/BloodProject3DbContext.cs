using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BloodProject3.Models;
using BloodProject3.Models;

namespace BloodProject3.Areas.Identity.Data;

public class BloodProject3DbContext : IdentityDbContext<IdentityUser>
{
    public BloodProject3DbContext(DbContextOptions<BloodProject3DbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<BloodProject3.Models.Answers> Answers { get; set; } = default!;

public DbSet<BloodProject3.Models.Appointment> Appointment { get; set; } = default!;

public DbSet<BloodProject3.Models.BloodType> BloodType { get; set; } = default!;

public DbSet<BloodProject3.Models.DonatedBlood> DonatedBlood { get; set; } = default!;

public DbSet<BloodProject3.Models.Donor> Donor { get; set; } = default!;

public DbSet<BloodProject3.Models.Inventory> Inventory { get; set; } = default!;

public DbSet<BloodProject3.Models.MedicalForm> MedicalForm { get; set; } = default!;

public DbSet<BloodProject3.Models.Nurse> Nurse { get; set; } = default!;

public DbSet<BloodProject3.Models.Profile> Profile { get; set; } = default!;

public DbSet<BloodProject3.Models.Questions> Questions { get; set; } = default!;
}
