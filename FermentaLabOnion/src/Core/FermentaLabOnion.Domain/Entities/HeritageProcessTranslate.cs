using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class HeritageProcessTranslate:BaseEntityTranslate
    {
        public string BeforeLabel { get; set; } = null!;
        public string AfterLabel { get; set; } = null!;
        public string Title { get; set; } = null!;
        //Relational properties
        public int HeritageProcessId { get; set; }
        public HeritageProcess HeritageProcess { get; set; }
    }
}
