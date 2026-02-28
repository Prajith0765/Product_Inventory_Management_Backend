using Inventory_Management.Application.DTO;
using Inventory_Management.Application.Interface;
using Inventory_Management.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Inventory_Management_Controller.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IExcelExportService _excelExportService;
        public ProductController(
            IProductService productService,
            IExcelExportService excelExportService
            )
        {
            this._service = productService;
            this._excelExportService = excelExportService;
        }
        
        //Controller that calls the Service for GetProducts
        [HttpGet("getAll")]
        public async Task<IActionResult> GetProducts()
        {
            var products =  _service.GetProducts();
            return Ok(products);
        }
        //Controller that calls the Service for GetProducts By ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [Authorize(Roles = "Admin,Manager")]
        //Controller that calls the Service for Adding New Products
        [HttpPost("save")]
        public async Task<IActionResult> SaveProduct([FromBody] ProductSaveDTO dto)
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
                return Unauthorized("User not authenticated");

            await _service.SaveProductAsync(dto, username);

            return Ok(new
            {
                message = dto.ProductId == null
                    ? "Product created successfully"
                    : "Product updated successfully"
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task DeleteProduct(int id)
        {
            string loggedInUser = User.Identity.Name;
            await _service.DeleteProductAsync(id, loggedInUser);
        }


        [HttpGet("SearchProduct")]
        public IActionResult SearchProduct(string searchText)
        {
            var result = _service.SearchProducts(searchText);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("debug")]
        public IActionResult Debug()
        {
            return Ok(new
            {
                Name = User.Identity?.Name,
                AllClaims = User.Claims.Select(c => new { c.Type, c.Value }),
                Roles = User.Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value)
            });
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportProducts()
        {
            var products =  _service.GetProducts()
                           ?? new List<ProductResponseDTO>();

            var fileBytes = _excelExportService.ExportProductsToExcel(products);

            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Products_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
            );
        }
    }
}
