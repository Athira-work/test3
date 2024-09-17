using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TechStack.Domain.Shared
{
    public static class Helper
    {
        /// <summary>
        /// Helper method for casting List to DataTable.
        /// </summary>
        /// <typeparam name="T">Entity Type.</typeparam>
        /// <param name="items">List of entities.</param>
        /// <returns>Data Table.</returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            // Get all the properties by using reflection
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                // Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    // For solving Sql Exception c# datetime to sql datetime
                    if (props[i].PropertyType == typeof(DateTime))
                    {
                        values[i] = ((DateTime)props[i].GetValue(item, null)).ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }
                    else
                    {
                        values[i] = props[i].GetValue(item, null);
                    }
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
