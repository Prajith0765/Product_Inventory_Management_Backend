using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management.Application.DTO;
namespace Inventory_Management.Application.Interface
{
    public interface IProductService
    {
        IList<ProductResponseDTO> GetProducts();
        Task<ProductResponseDTO> GetProductByIdAsync(int id);
        Task SaveProductAsync(ProductSaveDTO product, string loggedInUser);

        Task DeleteProductAsync(int id, string loggedInUser);

        IList<ProductResponseDTO> SearchProducts(string searchText);
    }
}
