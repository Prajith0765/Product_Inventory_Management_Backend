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
        //Excel Export Service
        public byte[] ExportProductsToExcel(IList<ProductResponseDTO> products)
        {
            using (var workbook = new XLWorkbook())
            {
                //Create a excel Work Sheet for the Report
                var worksheet = workbook.Worksheets.Add("Products");

                // Header on the excel
                worksheet.Cell(1, 1).Value = "Product ID";
                worksheet.Cell(1, 2).Value = "Product Name";
                worksheet.Cell(1, 3).Value = "Product Code";
                worksheet.Cell(1, 4).Value = "Category";
                worksheet.Cell(1, 5).Value = "Quantity";
                worksheet.Cell(1, 6).Value = "Expired Date";

                worksheet.Row(1).Style.Font.Bold = true;

                int row = 2;
                //Write Each product Data into the Excel 
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
                //Adjust the width of the columns based on the content
                worksheet.Columns().AdjustToContents();
                //Creating a Memory Stream for the excel to send data in a byte form
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
