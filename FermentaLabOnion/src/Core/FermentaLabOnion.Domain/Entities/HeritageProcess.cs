using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class HeritageProcess:BaseEntity
    {
        public string Title { get; set; } = null!;
        public string BeforeImageUrl { get; set; } = null!;
        public string AfterImageUrl { get; set; } = null!;

        public string BeforeLabel { get; set; } = "Before";
        public string AfterLabel { get; set; } = "After";

        public int Order { get; set; }
        //Relational properties

        public ICollection<HeritageProcessTranslate>? Translates { get; set; }
    }
}
