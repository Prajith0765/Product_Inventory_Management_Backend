using Inventory_Management.Application.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.Interface
{
    public interface IExcelImportService
    {
        Task<(List<ProductSaveDTO> products, List<string> errors)> ImportProductsAsync(IFormFile file);
    }
}
