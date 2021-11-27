using Data.Models.DB._BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.DB.Project
{
    /// <summary>
    /// Поднаправление
    /// </summary>
    public class Subfield : BaseEntity
    {
        public string Name { get; set; }

        public Guid FieldId { get; set; }

        public virtual Field Field { get; set; }
    }
}
