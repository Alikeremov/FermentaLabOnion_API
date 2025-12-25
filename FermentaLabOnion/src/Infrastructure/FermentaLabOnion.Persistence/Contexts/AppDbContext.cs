using FermentaLabOnion.Domain.Entities;
using FermentaLabOnion.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private readonly IHttpContextAccessor _accessor;
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslate> ProductTranslates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslate> CategoryTranslates { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagTranslate> TagTranslates { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<AppTranslate> AppTranslates { get; set; }
        public DbSet<ChineseWisdom> ChineseWisdoms { get; set; }
        public DbSet<ChineseWisdomTranslate> ChineseWisdomTranslates { get; set; }
        public DbSet<HeritageProcess> HeritageProcesses { get; set; }
        public DbSet<HeritageProcessTranslate> HeritageProcessTranslates { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<InformationTranslate> InformationTranslates { get; set; }
        public DbSet<Question> Questions { get; set; } 
        public DbSet<QuestionTranslate> QuestionTranslates { get; set; }
        public DbSet<ShareSpecial> ShareSpecials { get; set; }
        public DbSet<ShareSpecialTranslate> ShareSpecialTranslates { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = _accessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userId ?? "System";
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = userId ?? "System";
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
