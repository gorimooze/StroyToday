﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroyToday.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        public ICollection<UserToSkillCategory> UserToSkillCategories { get; set; }
        public UserCV UserCV { get; set; }
    }
}
