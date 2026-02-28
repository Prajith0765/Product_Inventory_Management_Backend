using Inventory_Management.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management.Application.DTO;
namespace Inventory_Management.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IList<ProductResponseDTO> GetProducts()
        {
            return productRepository.GetProducts();
        }

        public async Task<ProductResponseDTO> GetProductByIdAsync(int id)
        {
            return await productRepository.GetProductByIdAsync(id);
        }
        public async Task SaveProductAsync(ProductSaveDTO product, string loggedInUser)
        {
            await productRepository.SaveProductAsync(product, loggedInUser);
        }

        public async Task DeleteProductAsync(int id, string loggedInUser)
        {
            await productRepository.DeleteProductAsync(id, loggedInUser);
        }


        public IList<ProductResponseDTO> SearchProducts(string searchText)
        {
            return productRepository.SearchProducts(searchText);
        }
    }
}
