using FermentaLabOnion.Persistence.Utilites.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Utilites.Helpers
{
    public static class FileValidator
    {
        public static bool ValidateType(this IFormFile file, string type = "image")
        {
            if (!file.ContentType.Contains(type)) return false;

            return true;
        }
        public static bool ValidateSize(this IFormFile file, int mb)
        {
            if (file.Length > mb * 1024 * 1024) return false;

            return true;
        }
        public static void ValidateImage(this IFormFile file, int mb = 4)
        {
            if (!file.ValidateType())
            {
                throw new ImageNotValidateException("Please enter a image");
            };
            if (!file.ValidateSize(mb))
            {
                throw new ImageNotValidateException($"The maximum size of the Image should be {mb} MB");
            }
        }
    }
}
