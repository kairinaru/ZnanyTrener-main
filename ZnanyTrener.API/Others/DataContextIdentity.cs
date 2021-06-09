using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZnanyTrener.API.Entities;

namespace ZnanyTrener.API.Others
{
    public class DataContextIdentity : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {   
        public DbSet<Certificate> Certificate { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DataContextIdentity(DbContextOptions<DataContextIdentity> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();

            builder.Entity<Certificate>()
                .HasOne(x => x.User)
                .WithMany(x => x.Certifiactes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);


             builder.Entity<Training>()
                .HasOne(x => x.User)
                .WithMany(x => x.TrainingsForUser)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Training>()
                .HasOne(x => x.Coach)
                .WithMany(x => x.TrainingsForCoach)
                .HasForeignKey(x => x.CoachId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}