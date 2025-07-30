using FandomHub.Domain.Entities;
using FandomHub.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Data
{
	public class FandomHubDbContext : IdentityDbContext<IdentityApplicationUser>
	{
		public virtual DbSet<Category> Categories { get; set; } 
		public virtual DbSet<Community> Communities { get; set; }
		public virtual DbSet<CommunityCategory> CommunityCategories { get; set; } 
		public virtual DbSet<EditHistory> EditHistories { get; set; }
		public virtual DbSet<AuditLog> AuditLogs { get; set; }
		public virtual DbSet<Hub> Hubs { get; set; }
		public virtual DbSet<HubCategory> HubCategories { get; set; }
		public virtual DbSet<Languages> Languages { get; set; }	
		public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
		public virtual DbSet<WikiPage> WikiPages { get; set; }
		public virtual DbSet<Notification> Notifications { get; set; }
		public virtual DbSet<NotificationType> NotificationTypes { get; set; }
		public virtual DbSet<FcmToken> FcmTokens { get; set; }

		private readonly string _currentUser;

		public FandomHubDbContext(DbContextOptions<FandomHubDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
		{
			_currentUser = httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "System";
		}
		 

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// COMMUNITY - CATEGORY (many-to-many)
			modelBuilder.Entity<CommunityCategory>(entity =>
			{
				entity.HasKey(cc => new { cc.CommunityId, cc.CategoryID });  
				entity.HasOne(cc => cc.Community)
					  .WithMany(c => c.CommunityCategories)
					  .HasForeignKey(cc => cc.CommunityId)
					  .OnDelete(DeleteBehavior.Cascade);
				entity.HasOne(cc => cc.Category)
					  .WithMany(c => c.CommunityCategories)
					  .HasForeignKey(cc => cc.CategoryID)
					  .OnDelete(DeleteBehavior.Cascade);

			});

			// HUB - CATEGORY (many-to-many)
			modelBuilder.Entity<HubCategory>(entity =>
			{
				entity.HasKey(hc => new { hc.HubId, hc.CategoryID });

				entity.HasOne(hc => hc.Hub)
					  .WithMany(h => h.HubCategories)
					  .HasForeignKey(hc => hc.HubId)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(hc => hc.Category)
					  .WithMany()
					  .HasForeignKey(hc => hc.CategoryID)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			// CATEGORY
			modelBuilder.Entity<Category>(entity =>
			{
				entity.HasKey(c => c.CategoryID);

				entity.Property(c => c.Name)
					.IsRequired()
					.HasMaxLength(100);
				entity.Property(c => c.Slug)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(c=>c.IsActive)
					.HasDefaultValue(true);
				entity.HasIndex(c => c.Slug)
					.IsUnique();
				entity.Property(c => c.IsActive)
					.HasDefaultValue(true);
			});

			// COMMUNITY
			modelBuilder.Entity<Community>(entity =>
			{
				entity.HasKey(c => c.CommunityId);

				entity.Property(c => c.Name)
					.IsRequired()
					.HasMaxLength(255);

				entity.Property(c => c.SubName)
					.HasMaxLength(255);

				entity.Property(c => c.LogoImage)
					.HasMaxLength(255);

				entity.Property(c => c.CoverImage)
					.HasMaxLength(255);

				entity.Property(c => c.Avatar)
					.HasMaxLength(255);

				entity.Property(c => c.Slug)
					.HasMaxLength(50);

				entity.Property(c => c.ContentText)
					.HasMaxLength(1000);

				entity.Property(c => c.Summary)
					.HasMaxLength(500);

				entity.Property(c => c.IsActive)
					.HasDefaultValue(true);

				entity.HasOne(c => c.Hub)
					.WithMany()
					.HasForeignKey(c => c.HubId)
					.OnDelete(DeleteBehavior.Restrict);

				entity.HasOne(c => c.Languages)
					.WithMany()
					.HasForeignKey(c => c.LanguagesId)
					.OnDelete(DeleteBehavior.Restrict);

				//entity.HasOne<IdentityApplicationUser>()
				//	.WithMany()
				//	.HasForeignKey("CreatedBy")
				//	.OnDelete(DeleteBehavior.Restrict);
				//entity.HasOne<IdentityApplicationUser>()
				//	.WithMany()
				//	.HasForeignKey("UpdatedBy")
				//	.OnDelete(DeleteBehavior.Restrict);
				//entity.HasOne<IdentityApplicationUser>()
				//	.WithMany()
				//	.HasForeignKey("DeleteBy")
				//	.OnDelete(DeleteBehavior.Restrict);
			});

			// WIKIPAGE
			modelBuilder.Entity<WikiPage>(entity =>
			{
				entity.HasKey(p => p.WikiPageId);

				entity.Property(p => p.Title)
					  .HasMaxLength(200);

				entity.Property(p => p.SubTitle)
					  .HasMaxLength(200);

				entity.Property(p => p.Avatar)
					  .HasMaxLength(255);
				entity.Property(p => p.Slug)
					  .IsRequired()
					  .HasMaxLength(100);

				entity.HasIndex(p => p.Slug)
					  .IsUnique();

				entity.HasOne(p => p.Community)
					  .WithMany(c => c.WikiPages)
					  .HasForeignKey(p => p.CommunityId)
					  .OnDelete(DeleteBehavior.Cascade);

				//entity.HasOne<IdentityApplicationUser>()
				//	.WithMany()
				//	.HasForeignKey("CreatedBy")
				//	.OnDelete(DeleteBehavior.Restrict);
				//entity.HasOne<IdentityApplicationUser>()
				//	.WithMany()
				//	.HasForeignKey("UpdatedBy")
				//	.OnDelete(DeleteBehavior.Restrict);
				//entity.HasOne<IdentityApplicationUser>()
				//	.WithMany()
				//	.HasForeignKey("DeleteBy")
				//	.OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<Notification>(entity =>
			{
				entity.HasKey(n => n.NotificationId);
				entity.Property(n => n.Message)
					  .IsRequired()
					  .HasMaxLength(200); 
				entity.Property(n => n.IsRead)
					  .HasDefaultValue(false);
				//entity.HasOne<IdentityApplicationUser>()
				//	.WithMany()
				//	.HasForeignKey("UserId")
				//	.OnDelete(DeleteBehavior.Restrict);
			});


			// LANGUAGES
			modelBuilder.Entity<Languages>(entity =>
			{
				entity.HasKey(l => l.LanguagesId);

				entity.Property(l => l.LanguageCode)
					  .IsRequired()
					  .HasMaxLength(10);

				entity.Property(l => l.LanguageName)
					  .IsRequired()
					  .HasMaxLength(100);

				entity.Property(l => l.IsActive)
					  .HasDefaultValue(true);
			});

			// HUB
			modelBuilder.Entity<Hub>(entity =>
			{
				entity.HasKey(h => h.HubId);

				entity.Property(h => h.Name)
					  .IsRequired()
					  .HasMaxLength(100);
				entity.Property(c => c.Slug)
					.HasMaxLength(50);
				entity.Property(l => l.IsActive)
					  .HasDefaultValue(true);
			});

			// Configuration for EditHistory
			modelBuilder.Entity<EditHistory>(entity =>
			{
				entity.HasKey(eh => eh.Id);

				entity.Property(eh => eh.TargetEntityType)
					.HasMaxLength(100); 

				entity.Property(eh => eh.ChangeSummary)
					.HasMaxLength(1000);

				entity.Property(eh => eh.IsActive)
					.HasDefaultValue(true);

				//entity.HasOne<IdentityApplicationUser>() 
				//	.WithMany()                   
				//	.HasForeignKey("CreatedBy")    
				//	.OnDelete(DeleteBehavior.Restrict);
				//entity.HasOne<IdentityApplicationUser>()
				//	.WithMany()
				//	.HasForeignKey("UpdatedBy")
				//	.OnDelete(DeleteBehavior.Restrict);
				//entity.HasOne<IdentityApplicationUser>()
				//	.WithMany()
				//	.HasForeignKey("DeleteBy")
				//	.OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<RefreshToken>()
				.HasOne<IdentityApplicationUser>()
				.WithMany()
				.HasForeignKey(rt => rt.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<FcmToken>(entity =>
			{
				entity.HasKey(e => e.FcmTokenId);

				entity.Property(e => e.UserId)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.Token)
					.IsRequired()
					.HasMaxLength(500);

				entity.Property(e => e.UniqueId)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.DeviceId)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.DeviceName)
					.HasMaxLength(100);

				entity.Property(e => e.DeviceType)
					.HasMaxLength(20);

				entity.Property(e => e.AppVersion)
					.HasMaxLength(20);

				entity.HasIndex(e => e.UserId);
				entity.HasIndex(e => e.DeviceId).IsUnique();
				entity.HasIndex(e => e.Token);
			});

		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			// Lấy các thay đổi đang theo dõi
			var modifiedEntries = ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
				.ToList();

			var auditLogs = new List<AuditLog>();

			foreach (var entry in modifiedEntries)
			{
				var audit = new AuditLog
				{
					EntityName = entry.Entity.GetType().Name,
					PerformedBy = _currentUser,
					PerformedAt = DateTime.UtcNow,
					Action = entry.State.ToString()
				};

				if (entry.State == EntityState.Modified)
				{
					var original = new Dictionary<string, object>();
					var current = new Dictionary<string, object>();

					foreach (var prop in entry.OriginalValues.Properties)
					{
						var originalValue = entry.OriginalValues[prop]?.ToString();
						var currentValue = entry.CurrentValues[prop]?.ToString();

						if (originalValue != currentValue)
						{
							original[prop.Name] = originalValue;
							current[prop.Name] = currentValue;
						}
					}

					audit.OriginalValue = JsonSerializer.Serialize(original);
					audit.NewValue = JsonSerializer.Serialize(current);
				}
				else if (entry.State == EntityState.Added)
				{
					audit.NewValue = JsonSerializer.Serialize(entry.CurrentValues.ToObject());
				}
				else if (entry.State == EntityState.Deleted)
				{
					audit.OriginalValue = JsonSerializer.Serialize(entry.OriginalValues.ToObject());
				}

				auditLogs.Add(audit);
			}

			// Thêm audit logs vào DbSet nếu có
			if (auditLogs.Any())
			{
				AuditLogs.AddRange(auditLogs);
			}

			return await base.SaveChangesAsync(cancellationToken);
		}

	}
}
