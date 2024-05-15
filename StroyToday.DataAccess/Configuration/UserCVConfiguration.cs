using Microsoft.EntityFrameworkCore;
using StroyToday.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StroyToday.DataAccess.Configuration
{
    public class UserCVConfiguration : IEntityTypeConfiguration<UserCV>
    {
        public void Configure(EntityTypeBuilder<UserCV> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
