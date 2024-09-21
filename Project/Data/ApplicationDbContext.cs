

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSets for your models
        public DbSet<TruckGoodsModel> TruckGoods { get; set; }
        public DbSet<Document> Documents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship between TruckGoodsModel and Document
            modelBuilder.Entity<TruckGoodsModel>()
                .HasMany(t => t.Documents)
                .WithOne(d => d.TruckGoods)
                .HasForeignKey(d => d.TruckGoodsModelId);
        }
    }
}
