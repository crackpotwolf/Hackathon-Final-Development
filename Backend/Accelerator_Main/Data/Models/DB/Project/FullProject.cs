using Data.Attributes;
using Data.Extensions;
using Data.Interfaces;
using Data.Models.DB._BaseEntities;
using Data.Models.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string? ApplicantName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Searchable]
        public string? ApplicantLastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Searchable]
        public string? ApplicantEmail { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string? ApplicantRole { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Searchable]
        public string? CompanyName { get; set; }

        /// <summary>
        /// Сфера деятельности
        /// </summary>
        [Searchable]
        public string? CompanyField { get; set; }

        /// <summary>
        /// Стадия компании
        /// </summary>
        [Searchable]
        public string? CompanyStage { get; set; }

        /// <summary>
        /// Команда человек
        /// </summary>
        public int? CompanyPeople { get; set; }

        /// <summary>
        /// Компетенции
        /// </summary>
        [Searchable]
        public string? CompanyCompetence { get; set; }

        /// <summary>
        /// Университет
        /// </summary>
        [Searchable]
        public string? CompanyUniversity { get; set; }

        public string? CompanyInn { get; set; }

        [Searchable]
        public string? CompanyCountry { get; set; }

        [Searchable]
        public string? CompanyCity { get; set; }

        [Searchable]
        public string? CompanyWebsite { get; set; }

        /// <summary>
        /// Название проекта
        /// </summary>
        [Searchable]
        public string? Name { get; set; }

        /// <summary>
        /// Стадия проекта
        /// </summary>
        [Searchable]
        public string? Stage { get; set; }

        /// <summary>
        /// Стадия продаж
        /// </summary>
        [Searchable]
        public string? SaleStage { get; set; }

        /// <summary>
        /// Количество выполненных пилотов
        /// </summary>
        public int? ImplementedPilotsCount { get; set; }

        /// <summary>
        /// Краткое описание
        /// </summary>
        [Searchable]
        public string? ShortDescription { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [Searchable]
        public string? Descripption { get; set; }

        /// <summary>
        /// Ценностное предложение
        /// </summary>
        [Searchable]
        public string? ValueProposition { get; set; }

        /// <summary>
        /// Характеристики пилота
        /// </summary>
        [Searchable]
        public string? PilotCharacteristics { get; set; }

        /// <summary>
        /// Бюджет
        /// </summary>
        public string? Budget { get; set; }

        /// <summary>
        /// Конкуренты
        /// </summary>
        [Searchable]
        public string? Competitors { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>
        [Searchable]
        public string? Comments { get; set; }

        /// <summary>
        /// Требуются ли инвестиции
        /// </summary>
        public bool? Investment { get; set; }

        /// <summary>
        /// Наличие экспертизы
        /// </summary>
        public bool? Expertise { get; set; }

        /// <summary>
        /// Результат экспертизы
        /// </summary>
        [Searchable]
        public string? ExpertiseReport { get; set; }

        /// <summary>
        /// Направление
        /// </summary>
        [Searchable]
        public string? Field { get; set; }

        /// <summary>
        /// Поднаправление
        /// </summary>
        [Searchable]
        public string? Subfield { get; set; }

        /// <summary>
        /// Технологии
        /// </summary>
        public List<string>? Technology { get; set; }

        /// <summary>
        /// Список технологий в строковом представлении
        /// </summary>
        [Searchable]
        [JsonIgnore]
        [NotMapped]
        public string TechnologyString { get => Technology!=null ? String.Join(", ", Technology) : ""; }

    }
}
