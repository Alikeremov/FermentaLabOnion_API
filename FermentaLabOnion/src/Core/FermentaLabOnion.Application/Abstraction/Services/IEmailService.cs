using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string eamilTo, string subject, string body, bool isHtml = false);
    }
}
