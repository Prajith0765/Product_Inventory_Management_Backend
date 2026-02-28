using Inventory_Management.Application.DTO;
using Inventory_Management.Application.Interface;
using Inventory_Management.Infrastructure.SQLHelper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.Service
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IConfiguration _configuration;

        public DashboardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IList<DashboardSummaryDTO> GetDashboardSummary()
        {
            using var conn = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            using var cmd = new SqlCommand("SP_GET_PRODUCTS_FOR_DASHBOARD", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            return DataTableMapper.ToList<DashboardSummaryDTO>(cmd);
        }
    }
}
