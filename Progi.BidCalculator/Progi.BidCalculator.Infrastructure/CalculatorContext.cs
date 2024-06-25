using Microsoft.EntityFrameworkCore;
using Progi.BidCalculator.Core.Entities;
using Progi.BidCalculator.Infrastructure.Data.Seed;

namespace Progi.BidCalculator.Infrastructure;

public class CalculatorContext(DbContextOptions<CalculatorContext> options) : DbContext(options)
{
    public virtual DbSet<CalculatorSetting> CalculatorSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed classifiers
        modelBuilder.ApplyConfiguration(new FeeSettingsConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();
        var addedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
        var auditable = ChangeTracker.Entries<Auditable>().ToList();
        if (auditable.Count != 0)
        {
            foreach (var record in auditable)
            {
                switch (record.State)
                {
                    case EntityState.Added:
                        record.Entity.CreationDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        record.Entity.UpdateDate = DateTime.UtcNow;
                        break;
                }
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}