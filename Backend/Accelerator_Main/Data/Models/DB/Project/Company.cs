using Data.Models.DB._BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.DB.Project
{
    /// <summary>
    /// Компания
    /// </summary>
    public class Company : BaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Сфера деятельности
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Стадия компании
        /// </summary>
        public string Stage { get; set; }

        /// <summary>
        /// Команда человек
        /// </summary>
        public int People { get; set; }

        /// <summary>
        /// Компетенции
        /// </summary>
        public string Competence { get; set; }

        /// <summary>
        /// Университет
        /// </summary>
        public string University { get; set; }

        public string Inn { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Website { get; set; }

    }
}
