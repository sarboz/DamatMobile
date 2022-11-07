using System.Linq;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DamatMobile.Core.Context
{
    public class ArmugonContext : DbContext, IArmugonContext
    {
        private readonly IDatabasePathProvider _pathProvider;

        public ArmugonContext(IDatabasePathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public DbSet<VirtualCard> VirtualCards { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"FileName={_pathProvider.GetDatabasePath()}");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(customer => customer.VirtualCards)
                .WithOne(card => card.Customer)
                .HasForeignKey(card => card.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasMany(customer => customer.PurchaseHistories)
                .WithOne(history => history.Customer)
                .HasForeignKey(history => history.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseHistory>().HasMany(customer => customer.PurchaseDetails)
                .WithOne(detail => detail.PurchaseHistory)
                .HasForeignKey(detail => detail.PurchaseHistoryId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public new void Add(object entity) => base.Add(entity);

        public new void Update(object entity) => base.Update(entity);

        public new void Remove(object entity) => base.Remove(entity);

        public void RemoveAll<T>() where T : BaseEntity
        {
            var baseEntities = base.Set<T>();
            if (baseEntities.Any())
                baseEntities.RemoveRange(baseEntities.ToList());
        }

        public IQueryable<T> GetDbSet<T>() where T : BaseEntity => base.Set<T>();

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}