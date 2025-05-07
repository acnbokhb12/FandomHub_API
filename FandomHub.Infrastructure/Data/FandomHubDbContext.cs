using FandomHub.Domain.Entities;
using FandomHub.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Data
{
	public class FandomHubDbContext : IdentityDbContext<ApplicationUser>
	{
		public FandomHubDbContext(DbContextOptions<FandomHubDbContext> options) : base(options)
		{
		}

		public virtual DbSet<Content> Contents { get; set; }
		public virtual DbSet<ContentType> ContentTypes { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<ContentCategory> ContentCategories { get; set; }
		public virtual DbSet<ContentEditHistory> ContentEditHistories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ContentCategory>(entity =>
			{
				entity.HasKey(cc => new { cc.ContentID, cc.CategoryID });

				entity.HasOne(cc => cc.Content)
						.WithMany(c => c.ContentCategories)
						.HasForeignKey(cc => cc.ContentID);

				entity.HasOne(cc => cc.Category)
						.WithMany(c => c.ContentCategories)
						.HasForeignKey(cc => cc.CategoryID);
			});

			modelBuilder.Entity<Category>(entity =>
			{
				entity.HasKey(c => c.CategoryID);

				entity.Property(c => c.Name)
					.IsRequired()
					.HasMaxLength(100);
				entity.Property(c => c.Slug)
					.IsRequired()
					.HasMaxLength(100);
				entity.Property(c=>c.isActive)
					.HasDefaultValue(true);
				entity.HasIndex(c => c.Slug)
					.IsUnique();
			});

			modelBuilder.Entity<ContentType>(entity =>
			{
				entity.HasKey(ct=>ct.ContentTypeID);
				entity.Property(ct => ct.Name)
					.IsRequired()
					.HasMaxLength(100);
				entity.Property(ct => ct.Slug)
					.IsRequired()
					.HasMaxLength(100);
				entity.Property(ct => ct.Description)
					.HasMaxLength(500);
				entity.HasIndex(ct => ct.Slug)
					.IsUnique();
				entity.Property(ct=>ct.isActive)
					.HasDefaultValue(true);
			});
			modelBuilder.Entity<Content>(entity =>
			{
				entity.HasKey(c => c.ContentID);

				entity.Property(c => c.Title)
					  .IsRequired()
					  .HasMaxLength(200);

				entity.Property(c => c.Slug)
					  .IsRequired()
					  .HasMaxLength(200);

				entity.HasIndex(c => c.Slug)
					  .IsUnique();

				entity.Property(c => c.Summary)
					  .HasMaxLength(500);

				entity.Property(c => c.ContentText)
					  .IsRequired();

				entity.Property(c => c.CoverImage)
					  .HasMaxLength(250);

				entity.Property(c => c.CreatedById)
					  .HasMaxLength(450);

				entity.Property(c => c.CreatedAt)
					  .IsRequired();

				entity.Property(c => c.isActive)
					.HasDefaultValue(true);

				entity.HasOne(c => c.ContentType)
					  .WithMany(ct => ct.Contents)
					  .HasForeignKey(c => c.ContentTypeID);

				entity.HasOne<ApplicationUser>()               // Không cần navigation property
					  .WithMany()
					  .HasForeignKey(c => c.CreatedById)
					  .OnDelete(DeleteBehavior.Restrict);

			});

			modelBuilder.Entity<ContentEditHistory>(entity =>
			{
				entity.HasKey(e => e.HistoryID);

				entity.Property(e => e.ChangeSummary)
					  .HasMaxLength(250);

				entity.Property(e => e.OldContent)
					  .IsRequired();

				entity.Property(e => e.EditedById)
					  .HasMaxLength(450);
				entity.Property(e=>e.isActive)
					  .HasDefaultValue(true);

				entity.Property(e => e.EditedAt)
					  .IsRequired();

				entity.HasOne(e => e.Content)
					  .WithMany(c => c.EditHistories)
					  .HasForeignKey(e => e.ContentID);

				entity.HasOne<ApplicationUser>()
					  .WithMany()
					  .HasForeignKey(eh => eh.EditedById)
					  .OnDelete(DeleteBehavior.SetNull);
			});
		}
	}
}
