using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.DTO
{
    public class DashboardRawDTO
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime ProductExpiredDate { get; set; }
    }
}
