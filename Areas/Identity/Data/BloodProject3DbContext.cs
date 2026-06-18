using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BloodProject3.Models;
using BloodProject3.Models;

namespace BloodProject3.Areas.Identity.Data;

public class BloodProject3DbContext : IdentityDbContext<User>
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

        // disables cascade delete between BloodType and DonatedBlood to stop fk constraints (multiple cascade paths)
        builder.Entity<DonatedBlood>()
            .HasOne(d => d.BloodType)
            .WithMany()
            .HasForeignKey(d => d.BloodTypeID)
            .OnDelete(DeleteBehavior.NoAction);

        // disables cascade delete between Appointment and DonatedBlood to stop fk constraints (multiple cascade paths)
        builder.Entity<DonatedBlood>()
            .HasOne(d => d.Appointment)
            .WithMany()
            .HasForeignKey(d => d.AppointmentID)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<BloodProject3.Models.Answers> Answers { get; set; } = default!;

    public DbSet<BloodProject3.Models.Appointment> Appointment { get; set; } = default!;

    public DbSet<BloodProject3.Models.BloodType> BloodType { get; set; } = default!;

    public DbSet<BloodProject3.Models.DonatedBlood> DonatedBlood { get; set; } = default!;

    public DbSet<BloodProject3.Models.Donor> Donor { get; set; } = default!;

    public DbSet<BloodProject3.Models.Inventory> Inventory { get; set; } = default!;

    public DbSet<BloodProject3.Models.MedicalForm> MedicalForm { get; set; } = default!;

    public DbSet<BloodProject3.Models.Nurse> Nurse { get; set; } = default!;

    public DbSet<BloodProject3.Models.Questions> Questions { get; set; } = default!;
}