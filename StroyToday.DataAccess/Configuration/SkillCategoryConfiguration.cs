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
    public class SkillCategoryConfiguration : IEntityTypeConfiguration<SkillCategory>
    {
        public void Configure(EntityTypeBuilder<SkillCategory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.UserToSkillCategories)
                .WithOne(x => x.SkillCategory)
                .HasForeignKey(x => x.SkillCategoryId);
        }
    }
}
