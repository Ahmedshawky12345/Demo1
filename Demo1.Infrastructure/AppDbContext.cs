using Demo1.Domain.Common;
using Demo1.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Demo1.Infrastructure
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Product> products {  get; set; }
        public DbSet<Category> categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entery in ChangeTracker.Entries<BaseEntity>()) 
                {
                switch (entery.State)
                {
                    case EntityState.Added:
                        entery.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entery.Entity.UpdatedAt= DateTime.UtcNow; 
                        break;
                    case EntityState.Deleted:
                        entery.State = EntityState.Modified;
                        entery.Entity.DeletedAt = DateTime.UtcNow;
                        entery.Entity.IsDeleted = true;
                        break; 



                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
