using FinalProjectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Target> Targets { get; set; }
        public DbSet<Mission> Missions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mission>()
                .HasOne(e => e.Agent)
                .WithMany()
                .HasForeignKey(e => e.AgentId);

            modelBuilder.Entity<Mission>()
                .HasOne(e => e.Target)
                .WithMany()
                .HasForeignKey(e => e.TargetId);

            modelBuilder.Entity<Mission>()
                .HasKey(m => new { m.AgentId, m.TargetId });
        }
    }
}
