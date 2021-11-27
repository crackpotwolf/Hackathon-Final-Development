using Data.Models.DB._BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.DB.Project
{
    /// <summary>
    /// Направление
    /// </summary>
    public class Field : BaseEntity
    {
        public string Name { get; set; }
    }
}
