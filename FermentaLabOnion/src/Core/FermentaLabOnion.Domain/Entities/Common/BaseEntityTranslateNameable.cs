using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities.Common
{
    public abstract class BaseEntityTranslateNameable: BaseEntityTranslate
    {
        public string Name { get; set; } = null!;
    }
}
