using Data.Models.DB._BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.DB.Project
{
    /// <summary>
    /// Заявка
    /// </summary>
    public class Project : BaseEntity
    {
        /// <summary>
        /// Название проекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стадия проекта
        /// </summary>
        public string Stage { get; set; }

        /// <summary>
        /// Стадия продаж
        /// </summary>
        public string SaleStage { get; set; }

        /// <summary>
        /// Количество выполненных пилотов
        /// </summary>
        public int ImplementedPilotsCount { get; set; }

        /// <summary>
        /// Краткое описание
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Descripption { get; set; }

        /// <summary>
        /// Ценностное предложение
        /// </summary>
        public string ValueProposition { get; set; }

        /// <summary>
        /// Характеристики пилота
        /// </summary>
        public string PilotCharacteristics { get; set; }

        /// <summary>
        /// Бюджет
        /// </summary>
        public string Budget { get; set; }

        /// <summary>
        /// Конкуренты
        /// </summary>
        public string Competitors { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Требуются ли инвестиции
        /// </summary>
        public bool Investment { get; set; }

        /// <summary>
        /// Наличие экспертизы
        /// </summary>
        public bool Expertise { get; set; }

        /// <summary>
        /// Результат экспертизы
        /// </summary>
        public string ExpertiseReport { get; set; }

        /// <summary>
        /// Имя пути
        /// </summary>
        public string PathName { get; set; }

        public Guid ApplicantId { get; set; }

        public virtual Applicant Applicant { get; set; }

        public Guid CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public Guid SubfieldId { get; set; }

        public virtual Subfield Subfield { get; set; }

        public virtual List<ProjectTechnologies> Technologies {get;set;}
    }
}
