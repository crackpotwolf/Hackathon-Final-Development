using Data.Attributes;
using Data.Interfaces;
using Search_Data.Models;
using Search_Data.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{

    public static class SearchableObjectExtension
    {
        /// <summary>
        /// Получение строкового представления объекта для поиска
        /// </summary>
        public static string GetSearchablePropertiesStringifiedValue(this ISearchable data)
        {
            var res = new List<string>();

            foreach (PropertyInfo property in data.GetType().GetProperties()
                .Where(p => p.CustomAttributes.Select(m => m.AttributeType).Contains(typeof(SearchableAttribute))))
            {
                res.Add(property.GetValue(data).ToString());
            }
            return String.Join(", ", res);
        }

        /// <summary>
        /// Индексирование объекта
        /// </summary>
        /// <param name="data">объект</param>
        /// <param name="key">ключ объекта</param>
        /// <param name="search">объект поиска</param>
        public static void AddSearchableObjectToIndex(this ISearchable data, Guid key, WordSearch search)
        {
            TextDocument document = new TextDocument()
            {
                Guid = key,
                Text = data.GetSearchablePropertiesStringifiedValue()
            };

            search.AddIndex(document);
        }
    }
}
