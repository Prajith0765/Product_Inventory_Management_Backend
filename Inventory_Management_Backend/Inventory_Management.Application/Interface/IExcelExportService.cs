using Inventory_Management.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.Interface
{
    public interface IExcelExportService
    {
        byte[] ExportProductsToExcel(IList<ProductResponseDTO> products);
    }
}
