using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StroyToday.Core.Dto;

namespace StroyToday.Core.IRepositories
{
    public interface IUserToSkillCategoryRepository
    {
        Task Add(UserToSkillCategoryDto  userToSkillCategoryDto);
    }
}
