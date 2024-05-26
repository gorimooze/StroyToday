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
using System.Buffers.Text;

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

        public async Task<GenericResponse> Add(IList<ImageDto> imageDtoList, int userId)
        {
            try
            {
                foreach (var imgDto in imageDtoList)
                {
                    var base64Byte = Convert.FromBase64String(imgDto.Base64);

                    // Создание потока данных из байтов
                    using var imageStream = new MemoryStream(base64Byte);

                    var filetype = imgDto.ImageType.Split('/')[1];

                    // Генерация уникального имени файла
                    var fileName = Guid.NewGuid() + $".{filetype}"; // Вы можете изменить расширение файла в зависимости от типа изображения

                    var result = await _azureProvider.UploadFileAsync(imageStream, imgDto.ImageType, fileName);

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

        public async Task<GenericResult<IList<string>>> GetImageUrlsByUserId(int userId)
        {
            try
            {
                var listNameImages = await _portfolioForUserRepository.GetImagesByUserId(userId);

                var listToReturnUrls = new List<string>();

                foreach (var nameImg in listNameImages)
                {
                    var url = _azureProvider.GetFullPathOnAzureStorage(nameImg);
                    listToReturnUrls.Add(url);
                }

                return new GenericResult<IList<string>>()
                {
                    Result = listToReturnUrls,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new GenericResult<IList<string>>()
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
            }

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
