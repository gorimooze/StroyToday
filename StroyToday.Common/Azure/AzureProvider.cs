using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using StroyToday.Application.Helpers;
using StroyToday.Application.Interfaces;

namespace StroyToday.Common.Azure
{
    public class AzureProvider : IAzureProvider
    {
        private readonly AzureOptions _options;
        private readonly BlobServiceClient _serviceClient;
        private readonly BlobContainerClient _containerClient;

        public AzureProvider(IOptions<AzureOptions> options)
        {
            _options = options.Value;
            _serviceClient = new BlobServiceClient(_options.ConnectionString);
            _containerClient = _serviceClient.GetBlobContainerClient(_options.ContainerName);
        }

        public string GetFullPathOnAzureStorage(string fileName)
        {
            return "https://stroytoday.blob.core.windows.net/stroytodayimages/" + fileName;
        }

        public async Task<GenericResponse> UploadFileAsync(Stream fileStream, string fileType, string fileName)
        {
            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(fileName);
                var blobHttpHeaders = new BlobHttpHeaders { ContentType = fileType };
                await blobClient.UploadAsync(fileStream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                
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
