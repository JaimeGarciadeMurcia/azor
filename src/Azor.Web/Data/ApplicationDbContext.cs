using Azor.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azor.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<BattleReplay> BattleReplays { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<PlayerBattle> Players { get; set; }
    public DbSet<ServerResults> ServerResults { get; set; }
    public DbSet<ObservedResults> ObservedResults { get; set; }
    public DbSet<Metadata> Metadata { get; set; }
    public DbSet<Version> Versions { get; set; }
    public DbSet<BattleResult> BattleResults { get; set; }
    public DbSet<Ribbon> Ribbons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de BattleReplay
        modelBuilder.Entity<BattleReplay>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Metadata)
                  .WithOne()
                  .HasForeignKey<BattleReplay>(e => e.Metadata.Id);
            entity.HasMany(e => e.Vehicles)
                  .WithOne(e => e.BattleReplay)
                  .HasForeignKey(e => e.BattleReplayId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración de Vehicle
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.PlayerBattle)
                  .WithMany()
                  .HasForeignKey(e => e.PlayerId);
            entity.HasOne(e => e.ServerResults)
                  .WithOne()
                  .HasForeignKey<Vehicle>(e => e.ServerResultsId);
            entity.HasOne(e => e.ObservedResults)
                  .WithOne()
                  .HasForeignKey<Vehicle>(e => e.ObservedResultsId);
            entity.HasMany(e => e.Ribbons)
                  .WithOne(e => e.Vehicle)
                  .HasForeignKey(e => e.VehicleId);
            entity.Property(e => e.Achievements)
                  .HasConversion(
                      v => string.Join(',', v),
                      v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                  );
        });

        // Configuración de Metadata
        modelBuilder.Entity<Metadata>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Version)
                  .WithOne()
                  .HasForeignKey<Metadata>(e => e.VersionId);
            entity.HasOne(e => e.BattleResult)
                  .WithOne()
                  .HasForeignKey<Metadata>(e => e.BattleResultId);
        });
    }
}