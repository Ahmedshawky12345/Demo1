using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Application.Interfaces.Iservices
{
    public interface IimageService
    {
        Task<string> SaveFileAsync(IFormFile file, string FolderName);
    }
}
