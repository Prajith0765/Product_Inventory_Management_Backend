using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.DTO
{
    public class ProductCreateDTO
    {
        public string PRODUCT_NAME { get; set; }
        public string PROUDUCT_CODE { get; set; }
        public string PRODUCT_CATERGORY { get; set; }
        public int PRODUCT_QUANTITY { get; set; }
        public DateTime PRODUCT_EXPIRED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public string CREATED_AT { get; set; }
        public string MODIFIED_BY { get; set; }
        public string MODIFIED_AT { get; set; }
        public bool PRODUCT_ISDELETED { get; set; }

    }
}
