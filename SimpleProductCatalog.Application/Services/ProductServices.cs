using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleProductCatalog.Abstraction.DTO;
using SimpleProductCatalog.Abstraction.Interface;
using SimpleProductCatalog.Application.Util;
using SimpleProductCatalog.Domain.Entities;
using SimpleProductCatalog.Infra.Data.Repository;
using SimpleProductCatalog.Infra.Data.Repository.Interface;


namespace SimpleProductCatalog.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryServices>? _logger;
        public ProductServices(IProductRepository productRepository,
                               ICategoryRepository categoryRepository, 
                               IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateProduct(ProductDTO obj)
        {

            var productCategory = await _categoryRepository.GetByIdAsync(obj.Category!.Id);

            if(productCategory! == null) {
                return "Category was not found. ";
            }

            Product product = new Product
            {
                Name = obj.Name,
                Price = obj.Price,
                Description = obj.Description,
                CategoryId = productCategory.Id
            };

            try
            {
                await _productRepository.AddAsync(product);
                await _productRepository.SaveChangesAsync();

                return product.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return "";
            }
        }

        public async Task<bool> DeleteProduct(string id)
        {
            try
            {
                var deleteProduct = await _productRepository.GetByIdAsync(id);

                if (deleteProduct! == null)
                    return false;

                _productRepository.Delete(deleteProduct);
                await _productRepository.SaveChangesAsync();
                return true;


            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<ProductDTO>> GetAllProduct()
        {
            var products = await _productRepository.GetAllAsync();
            var productsDTO = _mapper.Map<List<ProductDTO>>(products);
            return productsDTO;
        }

        public async Task<ProductDTO> GetProductById(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var productDTO = _mapper.Map<ProductDTO>(product);
            return productDTO;
        }

        public async Task<(bool Success, string Error)> UpdateProduct(ProductDTO obj)
        {
            try
            {
                if (!ValidationSummary.ValidateProductForUpdate(obj, out var validationMessage))
                {
                    _logger?.LogWarning("Product update validation failed: {Reason}", validationMessage);
                    return (false, validationMessage);
                }

                var updateProduct = await _productRepository.GetByIdAsync(obj.Id.ToString());
                if (updateProduct! == null)
                {
                    return (false, "Product not found.");
                }

                if (updateProduct.CategoryId != obj.Category!.Id)
                {
                    var productCategory = await _categoryRepository.GetByIdAsync(obj.Category!.Id);
                    if (productCategory! == null)
                    {
                        return (false, "Target category not found.");
                    }
                    updateProduct.Category = productCategory;
                }

                updateProduct.Name = obj.Name;
                updateProduct.Description = obj.Description;
                updateProduct.Price = obj.Price;

                _productRepository.Update(updateProduct);
                await _productRepository.SaveChangesAsync();

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Unexpected error during product update.");
                return (false, "An unexpected error occurred.");
            }
        }
    }
}
