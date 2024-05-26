using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StroyToday.Application.Helpers;
using StroyToday.Core.Dto;

namespace StroyToday.Application.Interfaces.IServices
{
    public interface IPortfolioForUserService
    {
        Task<GenericResponse> Add(IList<ImageDto> imageDtoList, int userId);
        Task<GenericResult<IList<string>>> GetImageUrlsByUserId(int userId);
    }
}
