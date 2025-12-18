using FermentaLabOnion.Application.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Utilites.Exceptions.Common
{
    public class UnDeleteException : Exception, IBaseException
    {
        public UnDeleteException(string message = "This item can't delete") : base(message)
        {
        }
    }
}
