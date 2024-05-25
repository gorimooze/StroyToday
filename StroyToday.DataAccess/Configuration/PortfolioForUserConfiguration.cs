using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StroyToday.DataAccess.Models;

namespace StroyToday.DataAccess.Configuration
{
    public class PortfolioForUserConfiguration : IEntityTypeConfiguration<PortfolioForUser>
    {
        public void Configure(EntityTypeBuilder<PortfolioForUser> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.UserId);

            builder.Property(p => p.ImageName)
                .IsRequired();
        }
    }

}
