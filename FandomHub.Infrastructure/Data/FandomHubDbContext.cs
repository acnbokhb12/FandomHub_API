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
	public class FandomHubDbContext : IdentityDbContext<ApplicationUser>
	{
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Character> Characters { get; set; }
		public virtual DbSet<CharacterAttribute> CharacterAttributes { get; set; }
		public virtual DbSet<CharacterAttributeGroup> CharacterAttributesGroup { get; set; }
		public virtual DbSet<Community> Communities { get; set; }
		public virtual DbSet<CommunityCategory> CommunityCategories { get; set; } 
		public virtual DbSet<EditHistory> EditHistories { get; set; }
		public virtual DbSet<AuditLog> AuditLogs { get; set; }
		private readonly string _currentUser;

		public FandomHubDbContext(DbContextOptions<FandomHubDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
		{
			_currentUser = httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "System";
		}
		 

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CommunityCategory>(entity =>
			{
				entity.HasKey(cc => new { cc.CommunityId, cc.CategoryID }); // Composite key for many-to-many
				entity.HasOne(cc => cc.Community)
					  .WithMany(c => c.CommunityCategories)
					  .HasForeignKey(cc => cc.CommunityId)
					  .OnDelete(DeleteBehavior.Cascade);
				entity.HasOne(cc => cc.Category)
					  .WithMany(c => c.CommunityCategories)
					  .HasForeignKey(cc => cc.CategoryID)
					  .OnDelete(DeleteBehavior.Cascade);

			});

			modelBuilder.Entity<Category>(entity =>
			{
				entity.HasKey(c => c.CategoryID);

				entity.Property(c => c.Name)
					.IsRequired()
					.HasMaxLength(100);
				entity.Property(c => c.Slug)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(c=>c.isActive)
					.HasDefaultValue(true);
				entity.HasIndex(c => c.Slug)
					.IsUnique();
			});

			// Configuration for Character
			modelBuilder.Entity<Character>(entity =>
			{
				entity.HasKey(c => c.CharacterId);

				entity.Property(c => c.Name)
					.IsRequired()
					.HasMaxLength(100);
				 
				entity.Property(c => c.Avatar)
					.HasMaxLength(255);

				entity.HasOne(c => c.Community)
					.WithMany(c => c.Characters)
					.HasForeignKey(c => c.CommunityId)
					.OnDelete(DeleteBehavior.SetNull); // Optional: Define delete behavior

				// Add navigation property for CharacterAttributeGroups
				entity.HasMany(c => c.CharacterAttributeGroups)
					.WithOne(cag => cag.Character)
					.HasForeignKey(cag => cag.CharacterId);
			});

			// Configuration for CharacterAttribute
			modelBuilder.Entity<CharacterAttribute>(entity =>
			{
				entity.HasKey(ca => ca.CharacterAttributeId);

				entity.Property(ca => ca.Key)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(ca => ca.Value)
					.HasMaxLength(255);

				entity.HasOne(ca => ca.CharacterAttributeGroup)
					.WithMany(cag => cag.CharacterAttributes)
					.HasForeignKey(ca => ca.CharacterAttributeGroupId)
					.OnDelete(DeleteBehavior.Cascade);  
			});

			// Configuration for CharacterAttributeGroup
			modelBuilder.Entity<CharacterAttributeGroup>(entity =>
			{
				entity.HasKey(cag => cag.CharacterAttributeGroupId);

				entity.Property(cag => cag.Name)
					.IsRequired()
					.HasMaxLength(100);

				entity.HasOne(cag => cag.Character)
					.WithMany(c => c.CharacterAttributeGroups)
					.HasForeignKey(cag => cag.CharacterId)
					.OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior

				entity.HasMany(cag => cag.CharacterAttributes)
					.WithOne(ca => ca.CharacterAttributeGroup)
					.HasForeignKey(ca => ca.CharacterAttributeGroupId);
			});

			modelBuilder.Entity<Community>(entity =>
			{
				entity.HasKey(c => c.CommunityId);

				entity.Property(c => c.Title)
					.IsRequired()
					.HasMaxLength(255);

				entity.Property(c => c.LogoImage)
					.HasMaxLength(255);

				entity.Property(c => c.CoverImage)
					.HasMaxLength(255);

				entity.Property(c => c.Slug)
					.HasMaxLength(50);

				entity.Property(c => c.ContentText)
					.HasMaxLength(1000);

				entity.Property(c => c.Summary)
					.HasMaxLength(500);

				entity.Property(c => c.isActive)
					.HasDefaultValue(true); 

				entity.HasMany(c => c.Characters)
					.WithOne(c => c.Community)
					.HasForeignKey(c => c.CommunityId)
					.OnDelete(DeleteBehavior.SetNull); // Optional: Define delete behavior
			});

			// Configuration for EditHistory
			modelBuilder.Entity<EditHistory>(entity =>
			{
				entity.HasKey(eh => eh.Id);

				entity.Property(eh => eh.TargetEntityType)
					.HasMaxLength(100); 

				entity.Property(eh => eh.ChangeSummary)
					.HasMaxLength(1000);

				entity.Property(eh => eh.isActive)
					.HasDefaultValue(true);

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
