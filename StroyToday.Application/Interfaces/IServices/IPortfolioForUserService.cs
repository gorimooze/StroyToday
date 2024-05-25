using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StroyToday.Application.Helpers;

namespace StroyToday.Application.Interfaces.IServices
{
    public interface IPortfolioForUserService
    {
        Task<GenericResponse> Add(string base64, string imageType, int userId);
    }
}
