using ClosedXML.Excel;
using Inventory_Management.Application.DTO;
using Inventory_Management.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.Service
{
    public class ExcelExportService : IExcelExportService
    {
        public byte[] ExportProductsToExcel(IList<ProductResponseDTO> products)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");

                // Header
                worksheet.Cell(1, 1).Value = "Product ID";
                worksheet.Cell(1, 2).Value = "Product Name";
                worksheet.Cell(1, 3).Value = "Product Code";
                worksheet.Cell(1, 4).Value = "Category";
                worksheet.Cell(1, 5).Value = "Quantity";

                worksheet.Row(1).Style.Font.Bold = true;

                int row = 2;

                foreach (var product in products)
                {
                    worksheet.Cell(row, 1).Value = product.ProductId;
                    worksheet.Cell(row, 2).Value = product.ProductName;
                    worksheet.Cell(row, 3).Value = product.ProductCode;
                    worksheet.Cell(row, 4).Value = product.ProductCategory;
                    worksheet.Cell(row, 5).Value = product.ProductQuantity;
                    worksheet.Cell(row, 6).Value = product.ProductExpiredDate;
                    row++;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
