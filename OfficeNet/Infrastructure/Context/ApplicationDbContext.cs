using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfficeNet.Domain.Entities;

namespace OfficeNet.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>
        (options)
    {
        public DbSet<SurveyDetails> SurveyDetail { get; set; }
        public DbSet<Plant> Plants { get;set; }
        public DbSet<UsersDepartment> UsersDepartments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            //builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }
    }
}


////public class ApplicationDbContext: IdentityDbContext<ApplicationUser,IdentityRole<long>, long>
////{
////    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
////    : base(options)
////    {
////    }
///






