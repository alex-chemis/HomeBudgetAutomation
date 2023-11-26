using Duende.IdentityServer.EntityFramework.Options;
using HomeBudgetAutomation.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HomeBudgetAutomation.Data
{
    public partial class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {

        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<Balance> Balances { get; set; } = null!;
        public virtual DbSet<Operation> Operations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("articles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Balance>(entity =>
            {
                entity.ToTable("balance");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2)
                    .HasColumnName("amount");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("create_date");

                entity.Property(e => e.Credit)
                    .HasPrecision(18, 2)
                    .HasColumnName("credit");

                entity.Property(e => e.Debit)
                    .HasPrecision(18, 2)
                    .HasColumnName("debit");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.ToTable("operations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArticleId).HasColumnName("article_id");

                entity.Property(e => e.BalanceId).HasColumnName("balance_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("create_date");

                entity.Property(e => e.Credit)
                    .HasPrecision(18, 2)
                    .HasColumnName("credit");

                entity.Property(e => e.Debit)
                    .HasPrecision(18, 2)
                    .HasColumnName("debit");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Operations)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_operations_articles");

                entity.HasOne(d => d.Balance)
                    .WithMany(p => p.Operations)
                    .HasForeignKey(d => d.BalanceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_operations_balance");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}