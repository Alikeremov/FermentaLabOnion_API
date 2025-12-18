using FermentaLabOnion.Application.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Utilites.Exceptions.Autentication
{
    public class UserNotFoundException:Exception, IBaseException
    {
        public UserNotFoundException(string message="User not found..."):base(message)
        {
        }
    }
}
