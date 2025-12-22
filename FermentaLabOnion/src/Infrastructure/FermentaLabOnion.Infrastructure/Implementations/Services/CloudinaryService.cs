using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using FermentaLabOnion.Application.Abstraction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Infrastructure.Implementations.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration _configuration;

        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> FileCreateAsync(IFormFile file)
        {
            string fileName = string.Concat(Guid.NewGuid(), file.FileName.Substring(file.FileName.LastIndexOf('.')));
            var myAccount = new Account { ApiKey = _configuration["CloudinarySettings:APIKey"], ApiSecret = _configuration["CloudinarySettings:APISecret"], Cloud = _configuration["CloudinarySettings:CloudName"] };
            Cloudinary _cloudinary = new Cloudinary(myAccount);
            _cloudinary.Api.Secure = true;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, stream),
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            string url = uploadResult.SecureUri.ToString();
            return url;
        }

        public async Task<bool> FileDeleteAsync(string filename)
        {
            var myAccount = new Account
            {
                ApiKey = _configuration["CloudinarySettings:APIKey"],
                ApiSecret = _configuration["CloudinarySettings:APISecret"],
                Cloud = _configuration["CloudinarySettings:CloudName"]
            };

            Cloudinary _cloudinary = new Cloudinary(myAccount);
            _cloudinary.Api.Secure = true;
            string publicId = filename.Substring(filename.LastIndexOf('/') + 1);
            publicId = publicId.Substring(0, publicId.LastIndexOf('.'));
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
            if (deletionResult.StatusCode == System.Net.HttpStatusCode.OK) return true;
            return false;
        }
    }
}
