using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class ChineseWisdomTranslate:BaseEntityTranslate
    {
        public string Tittle { get; set; } = null!;
        public string Description { get; set; } = null!;
        //Relational properties

        public int ChineseWisdomId { get; set; }
        public ChineseWisdom ChineseWisdom { get; set; } 
    }
}
