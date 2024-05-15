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
    public class PortfolioForUserCVConfiguration : IEntityTypeConfiguration<PortfolioForUserCV>
    {
        public void Configure(EntityTypeBuilder<PortfolioForUserCV> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserCVId)
                .IsRequired();

            builder.HasOne(p => p.UserCV)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.UserCVId);

            builder.Property(p => p.ImageName)
                .IsRequired();
        }
    }

}
