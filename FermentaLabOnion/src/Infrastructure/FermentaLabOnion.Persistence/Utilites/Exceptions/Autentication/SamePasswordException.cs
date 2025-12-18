using FermentaLabOnion.Application.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Utilites.Exceptions.Autentication
{
    public class SamePasswordException : Exception, IBaseException
    {
        public SamePasswordException(string message = "The new password can't be the same as the old one.") : base(message)
        {
        }
    }
}
