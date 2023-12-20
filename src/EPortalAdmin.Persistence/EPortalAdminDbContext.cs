using EPortalAdmin.Core.Domain;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Utilities.Extensions.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace EPortalAdmin.Persistence
{
    public class EPortalAdminDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : DbContext(dbContextOptions)
    {
        protected IConfiguration Configuration { get; } = configuration;

        public DbSet<Core.FileStorage.File> Files { get; set; }
        public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
        public DbSet<Core.Domain.Entities.Endpoint> Endpoints { get; set; }
        public DbSet<EndpointOperationClaim> EndpointOperationClaims { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemOperationClaim> MenuItemOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                base.OnConfiguring(
                    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("EPortalAdminConnectionString")));
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var httpContextAccessor = this.GetService<IHttpContextAccessor>();
            var userId = httpContextAccessor.HttpContext!.User.GetUserId();

            var datas = ChangeTracker
                .Entries<BaseEntity>();
            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.UtcNow;
                        data.Entity.CreatedBy = userId;
                        break;

                    case EntityState.Modified:
                        data.Entity.UpdatedDate = DateTime.UtcNow;
                        data.Entity.UpdatedBy = userId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
