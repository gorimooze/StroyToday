using Microsoft.EntityFrameworkCore;
using StroyToday.DataAccess.Models;

namespace StroyToday.DataAccess
{
    public class StroyTodayDbContext : DbContext
    {
        public StroyTodayDbContext(DbContextOptions<StroyTodayDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
