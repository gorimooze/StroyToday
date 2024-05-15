using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroyToday.DataAccess.Models
{
    public class PortfolioForUserCV
    {
        public int Id { get; set; }
        public int UserCVId { get; set; }
        public string ImageName { get; set; }

        public UserCV UserCV { get; set; }
    }
}
