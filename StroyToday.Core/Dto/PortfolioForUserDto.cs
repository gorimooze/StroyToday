using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroyToday.Core.Dto
{
    public class PortfolioForUserDto
    { public int Id { get; set; }
        public int UserId { get; set; }
        public string ImageName { get; set; }
    }
}
