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
    public class UserToSkillCategoryConfiguration : IEntityTypeConfiguration<UserToSkillCategory>
    {
        public void Configure(EntityTypeBuilder<UserToSkillCategory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserToSkillCategories)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.SkillCategory)
                .WithMany(x => x.UserToSkillCategories)
                .HasForeignKey(x => x.SkillCategoryId);
        }
    }

}
