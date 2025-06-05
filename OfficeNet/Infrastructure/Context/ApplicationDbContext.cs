using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfficeNet.Domain.Contracts;
using OfficeNet.Domain.Entities;
using System.Reflection.Emit;

namespace OfficeNet.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>
        (options)
    {
        public DbSet<SurveyDetails> SurveyDetail { get; set; }
        public DbSet<Plant> Plants { get;set; }
        public DbSet<UsersDepartment> UsersDepartments { get; set; }
        public DbSet<SurveyAuthenticateUser> SurveyAuthenticateUsers { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyOption> SurveyOptions { get; set; }
        public DbSet<SurveyList> SurveyListData { get; set; }
        public DbSet<SmtpDetail> SmtpDetails { get; set; }

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
            builder.Entity<SurveyList>().HasNoKey().ToView(null);
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






