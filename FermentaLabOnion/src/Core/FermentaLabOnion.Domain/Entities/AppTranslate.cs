using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class AppTranslate:BaseEntityTranslate
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        //Relational properties
        public int ApplicationId { get; set; }
        public App Application { get; set; }
    }
}
