using CourierManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourierManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Consignment> Consignments { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<TrackingLog> TrackingLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Consignment>()
                .HasMany(e => e.TrackingLogs)
                .WithOne(e => e.Consignment)
                .HasForeignKey(e => e.ConsignmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}