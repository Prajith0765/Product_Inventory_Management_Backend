using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.DTO
{
    public class ProductResponseDTO
    {
        [Column("PRODUCT_ID")]
        public long ProductId { get; set; }
        [Column("PRODUCT_NAME")]
        public string ProductName { get; set; }
        [Column("PRODUCT_CODE")]
        public string ProductCode { get; set; }
        [Column("PRODUCT_CATEGORY")]
        public string ProductCategory { get; set; }
        [Column("PRODUCT_QUANTITY")]
        public int ProductQuantity { get; set; }
        [Column("PRODUCT_EXPIRED_DATE")]
        public DateTime ProductExpiredDate { get; set; }
       
    }
}
