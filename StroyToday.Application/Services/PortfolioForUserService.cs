using StroyToday.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using StroyToday.Application.Interfaces;
using StroyToday.Application.Helpers;
using StroyToday.Core.Dto;
using StroyToday.Core.IRepositories;

namespace StroyToday.Application.Services
{
    public class PortfolioForUserService : IPortfolioForUserService
    {
        private readonly IAzureProvider _azureProvider;
        private readonly IPortfolioForUserRepository _portfolioForUserRepository;

        public PortfolioForUserService(IAzureProvider azureProvider, IPortfolioForUserRepository portfolioForUserRepository)
        {
            _azureProvider = azureProvider;
            _portfolioForUserRepository = portfolioForUserRepository;
        }

        public async Task<GenericResponse> Add(string base64, string imageType, int userId)
        {
            try
            {
                // Преобразование base64 в байты
                var base64Byte = Convert.FromBase64String(base64);

                // Создание потока данных из байтов
                using var imageStream = new MemoryStream(base64Byte);

                var filetype = imageType.Split('/')[1];

                // Генерация уникального имени файла
                var fileName = Guid.NewGuid() + $".{filetype}"; // Вы можете изменить расширение файла в зависимости от типа изображения

                var result = await _azureProvider.UploadFileAsync(imageStream, imageType, fileName);

                if (!result.IsSuccess)
                {
                    return result;
                }

                // Создание DTO и сохранение в репозитории
                var portfolioDto = new PortfolioForUserDto()
                {
                    ImageName = fileName,
                    UserId = userId
                };

                await _portfolioForUserRepository.Add(portfolioDto);
            }
            catch (Exception ex)
            {
                return new GenericResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

            return new GenericResponse()
            {
                IsSuccess = true
            };
        }

        private string GetBase64FileType(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                return string.Empty;
            }

            try
            {
                var stringSplit = base64String.Split(new string[] { ";base64" }, StringSplitOptions.None);
                return stringSplit[0].Replace("data:image/", "");
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}
