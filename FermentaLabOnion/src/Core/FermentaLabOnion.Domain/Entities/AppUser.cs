using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string ProfileImage { get; set; } = "https://res.cloudinary.com/ddxhgsscq/image/upload/v1718727326/d2g3oil5fdm7lc9zcysu.png";
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
    }
}
