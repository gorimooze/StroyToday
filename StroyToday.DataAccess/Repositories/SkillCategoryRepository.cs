﻿using StroyToday.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StroyToday.Core.Dto;
using StroyToday.DataAccess.Models;

namespace StroyToday.DataAccess.Repositories
{
    public class SkillCategoryRepository : ISkillCategoryRepository
    {
        private readonly StroyTodayDbContext _context;

        public SkillCategoryRepository(StroyTodayDbContext context)
        {
            _context = context;
        }

        public async Task Add(SkillCategoryDto skillCategoryDto)
        {
            var skillCategoryEntity = new SkillCategory()
            {
                Name = skillCategoryDto.Name
            };

            await _context.SkillCategories.AddAsync(skillCategoryEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<SkillCategoryDto>> GetAll()
        {
            var list = await _context.SkillCategories.Select(x => new SkillCategoryDto()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return list;
        }
    }
}
