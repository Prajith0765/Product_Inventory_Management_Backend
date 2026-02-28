using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.DTO
{
    public class DashboardSummaryDTO
    {
        [Column("PRODUCT_NAME")]
        public string ProductName { get; set; }

        [Column("PRODUCT_CODE")]
        public string ProductCode { get; set; }

        public int TotalQuantity { get; set; }
        public int ExpiredQuantity { get; set; }
    }
}

