using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management.Infrastructure.Extension;
using Inventory_Management.Application.DTO;

namespace Inventory_Management.Infrastructure.SQLHelper
{
    public static class DataTableMapper
    {
        public static IList<T> ToList<T>(IDbCommand command) where T : new()
        {
            DataTable dt = new DataTable();

            using var adapter = new SqlDataAdapter((SqlCommand)command);
            adapter.Fill(dt);

            return dt.ToList<T>();
        }
    }
}
