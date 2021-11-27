using Data.Attributes;
using Data.Extensions;
using Data.Interfaces;
using Data.Models.DB._BaseEntities;
using Data.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.DB.Project
{
    public class FullProject : BaseEntity, ISearchable
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Searchable]
        public string ApplicantName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Searchable]
        public string ApplicantLastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Searchable]
        public string ApplicantEmail { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string ApplicantRole { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Сфера деятельности
        /// </summary>
        public string CompanyField { get; set; }

        /// <summary>
        /// Стадия компании
        /// </summary>
        public string CompanyStage { get; set; }

        /// <summary>
        /// Команда человек
        /// </summary>
        public int CompanyPeople { get; set; }

        /// <summary>
        /// Компетенции
        /// </summary>
        public string CompanyCompetence { get; set; }

        /// <summary>
        /// Университет
        /// </summary>
        public string CompanyUniversity { get; set; }

        public string CompanyInn { get; set; }

        public string CompanyCountry { get; set; }

        public string CompanyCity { get; set; }

        public string CompanyWebsite { get; set; }

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
        /// Направление
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Поднаправление
        /// </summary>
        public string Subfield { get; set; }

        /// <summary>
        /// Технологии
        /// </summary>
        public List<string> Technology { get; set; }

    }
}
