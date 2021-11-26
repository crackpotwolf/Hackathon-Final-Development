using Data.Models.DB._BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.DB.Project
{
    /// <summary>
    /// Технологии проекта
    /// </summary>
    public class ProjectTechnologies
    {
        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public Guid TechnologyId { get; set; }

        public virtual Technology Technology { get; set; }
    }
}
