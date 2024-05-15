using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroyToday.DataAccess.Models
{
    public class UserToSkillCategory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SkillCategoryId { get; set; }

        public User User { get; set; }
        public SkillCategory SkillCategory { get; set; }
    }
}
