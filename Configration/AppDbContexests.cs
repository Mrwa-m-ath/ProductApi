using Microsoft.EntityFrameworkCore;
using ProductApi.Model;

namespace ProductApi.Configration
{
    public class AppDbContexests : DbContext
    {
        public AppDbContexests(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Categorise> Categories { get; set; }
        public DbSet<Product> products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasOne(s => s.categories).
                WithMany(s => s.Products).HasForeignKey(s => s.IdCategores).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
