using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroyToday.DataAccess.Models
{
    public class SkillCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserToSkillCategory> UserToSkillCategories { get; set; }
    }
}
