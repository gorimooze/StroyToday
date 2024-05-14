using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using StroyToday.Application.Helpers;

namespace StroyToday.Common.Azure
{
    public class AzureHelper
    {
        private readonly AzureOptions _options;
        private BlobServiceClient _serviceClient;
        private BlobContainerClient _containerClient;

        public AzureHelper(IOptions<AzureOptions> options)
        {
            _options = options.Value;
            _serviceClient = new BlobServiceClient(_options.ConnectionString);
            _containerClient = _serviceClient.GetBlobContainerClient(_options.ContainerName);
        }

        public static string GetFullPathOnAzureStorage(string fileName)
        {
            return "https://stroytoday.blob.core.windows.net/stroytodayimages/" + fileName;
        }

        public async Task<GenericResponse> UploadFileAsync(Stream fileStream, string fileName)
        {
            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(fileStream, overwrite: true);
                
                return new GenericResponse()
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse()
                {
                    IsSuccess = true,
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse> DeleteFileAsync(string fileName)
        {
            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(fileName);
                await blobClient.DeleteIfExistsAsync();

                return new GenericResponse()
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse()
                {
                    IsSuccess = true,
                    Message = ex.Message
                };
            }
        }
    }
}
