using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Infrastructure.Extension
{
    public static class DataTableExtension
    {
        //Get the converted List and map the values with the dto convert the database object to C# object
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var list = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                T obj = new T();

                foreach (var prop in properties)
                {
                    // Check for [Column] attribute
                    var columnAttr = prop.GetCustomAttribute<ColumnAttribute>();
                    string columnName = columnAttr != null ? columnAttr.Name : prop.Name;

                    if (!table.Columns.Contains(columnName))
                        continue;

                    var value = row[columnName];

                    if (value == DBNull.Value)
                        continue;

                    try
                    {
                        // Handle Nullable types
                        Type targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        object safeValue = Convert.ChangeType(value, targetType);

                        prop.SetValue(obj, safeValue);
                    }
                    catch
                    {
                        // Optional: Log mapping error
                        continue;
                    }
                }

                list.Add(obj);
            }

            return list;
        }
    }
}
