using FermentaLabOnion.Application.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Utilites.Exceptions.Common
{
    public class ImageNotValidateException : Exception, IBaseException
    {
        public ImageNotValidateException(string message = "Image is not validate") : base(message)
        {

        }
    }
}
