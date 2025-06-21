using DAL.Entities;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;


namespace DAL.Data
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }
        public DbSet<SystemAccount> SystemAccounts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thêm data khi khởi tạo cơ sở dữ liệu
            modelBuilder.Seed();

            // Cấu hình các quan hệ giữa các thực thể
            modelBuilder.Entity<NewsArticle>()
                .HasOne(n => n.Category)
                .WithMany()
                .HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ nhiều-nhiều giữa NewsArticle và Tag thông qua NewsTag
            modelBuilder.Entity<NewsTag>()
                .HasKey(nt => new { nt.NewsArticleId, nt.TagId });

            modelBuilder.Entity<NewsTag>()
                .HasOne(nt => nt.NewsArticle)
                .WithMany(na => na.NewsTags)
                .HasForeignKey(nt => nt.NewsArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NewsTag>()
                .HasOne(nt => nt.Tag)
                .WithMany(t => t.NewsTags)
                .HasForeignKey(nt => nt.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ giữa Category và ParentCategory
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany()
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ giữa NewsArticl
            modelBuilder.Entity<NewsArticle>()
                .HasOne(n => n.CreatedBy)
                .WithMany(a => a.CreatedArticles)
                .HasForeignKey(n => n.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NewsArticle>()
                .HasOne(n => n.UpdatedBy)
                .WithMany(a => a.UpdatedArticles)
                .HasForeignKey(n => n.UpdatedById)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
