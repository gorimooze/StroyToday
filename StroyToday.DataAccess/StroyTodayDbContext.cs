using Microsoft.EntityFrameworkCore;
using StroyToday.DataAccess.Configuration;
using StroyToday.DataAccess.Models;

namespace StroyToday.DataAccess
{
    public class StroyTodayDbContext : DbContext
    {
        public StroyTodayDbContext(DbContextOptions<StroyTodayDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserCVConfiguration());
            modelBuilder.ApplyConfiguration(new SkillCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserToSkillCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PortfolioForUserConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCV> UserCVs { get; set; }
        public DbSet<SkillCategory> SkillCategories { get; set; }
        public DbSet<UserToSkillCategory> UserToSkillCategories { get; set; }
        public DbSet<PortfolioForUser> Portfolios { get; set; }
    }
}
