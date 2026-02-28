using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.DTO
{
    public class ProductSaveDTO
    {
        [Column("PRODUCT_ID")]
        public long? ProductId { get; set; }
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
        //[Column("CREATED_BY")]
        //public string CreatedBy { get; set; }
        //[Column("CREATED_AT")]
        //public DateTime CreatedAt { get; set; }
        //[Column("MODIFIED_BY")]
        //public string? ModifiedBy { get; set; }
        //[Column("MODIFIED_AT")]
        //public DateTime? ModifiedAt { get; set; }
        //[Column("PRODUCT_ISDELETED")]
        //public bool IsDeleted { get; set; }
    }
}
