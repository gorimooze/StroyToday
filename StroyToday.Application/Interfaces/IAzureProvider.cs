using StroyToday.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroyToday.Application.Interfaces
{
    public interface IAzureProvider
    {
        string GetFullPathOnAzureStorage(string fileName);
        Task<GenericResponse> UploadFileAsync(Stream fileStream, string fileType, string fileName);
        Task<GenericResponse> DeleteFileAsync(string fileName);
    }
}
