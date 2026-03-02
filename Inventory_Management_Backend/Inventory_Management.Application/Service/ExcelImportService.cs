using ClosedXML.Excel;
using Inventory_Management.Application.DTO;
using Inventory_Management.Application.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.Service
{
    public class ExcelImportService : IExcelImportService
    {
        private readonly IProductRepository _productRepository;

        public ExcelImportService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<(List<ProductSaveDTO> products, List<string> errors)>
    ImportProductsAsync(IFormFile file)
        {
            var errors = new List<string>();

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);

            var rows = worksheet.RowsUsed().Skip(1);

            var products = rows.Select(row =>
            {
                try
                {
                    var product = new ProductSaveDTO
                    {
                        ProductName = row.Cell(1).GetString()?.Trim(),
                        ProductCode = row.Cell(2).GetString()?.Trim(),
                        ProductCategory = row.Cell(3).GetString()?.Trim()
                    };

                    //  SAFE Quantity Handling
                    if (!row.Cell(4).TryGetValue<int>(out var qty))
                    {
                        var qtyText = row.Cell(4).GetString();
                        if (!int.TryParse(qtyText, out qty))
                            throw new Exception("Invalid Quantity");
                    }

                    //  SAFE Date Handling
                    if (!row.Cell(5).TryGetValue<DateTime>(out var expiryDate))
                    {
                        var dateText = row.Cell(5).GetString();
                        if (!DateTime.TryParse(dateText, out expiryDate))
                            throw new Exception("Invalid Expiry Date");
                    }

                    product.ProductQuantity = qty;
                    product.ProductExpiredDate = expiryDate;

                    if (string.IsNullOrWhiteSpace(product.ProductName))
                        throw new Exception("Product Name is required");

                    if (product.ProductQuantity < 0)
                        throw new Exception("Quantity cannot be negative");

                    return product;
                }
                catch (Exception ex)
                {
                    errors.Add($"Row {row.RowNumber()}: {ex.Message}");
                    return null;
                }
            })
            .Where(p => p != null)
            .ToList();

            return (products, errors);
        }
    }
}
