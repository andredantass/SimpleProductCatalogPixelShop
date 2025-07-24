using SimpleProductCatalog.Abstraction.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleProductCatalog.Abstraction.Interface
{
    public interface IProductServices
    {
        Task<List<ProductDTO>> GetAllProduct();
        Task<ProductDTO> GetProductById(string id);
        Task<string> CreateProduct(ProductDTO product);
        Task<bool> DeleteProduct(string id);
        Task<(bool Success, string Error)> UpdateProduct(ProductDTO obj);
    }
}
