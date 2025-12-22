using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface ICloudinaryService
    {
        Task<string> FileCreateAsync(IFormFile file);
        Task<bool> FileDeleteAsync(string filename);
    }
}
