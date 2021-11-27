using Data.Attributes;
using Data.Interfaces;
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
    }
}
