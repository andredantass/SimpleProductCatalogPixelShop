using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleProductCatalog.Abstraction.DTO;
using SimpleProductCatalog.Abstraction.Interface;
using SimpleProductCatalog.Domain.Entities;
using SimpleProductCatalog.Infra.Data.Repository.Interface;


namespace SimpleProductCatalog.Application.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryServices>? _logger;
        public CategoryServices(ICategoryRepository categoryRepository,
                                IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<string> CreateCategory(CategoryDTO obj)
        {
            Category category = new Category
            {
                Name = obj.Name,
                
            };

            try
            {
                await _categoryRepository.AddAsync(category);
                await _categoryRepository.SaveChangesAsync();

                return category.Id;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteCategory(string id)
        {
            try
            {
                var deleteCategory = await _categoryRepository.GetByIdAsync(id);

                if(deleteCategory! == null)
                    return false;

                _categoryRepository.Delete(deleteCategory);
                await _categoryRepository.SaveChangesAsync();
                return true;


            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
            return categoryDTOs;
        }

        public async Task<bool> UpdateCategory(CategoryDTO category)
        {
            try
            {
                var updateCategory = await _categoryRepository.GetByIdAsync(category.Id.ToString());

                if (updateCategory! == null)
                    return false;

                updateCategory.Name = category.Name;
                _categoryRepository.Update(updateCategory);
                await _categoryRepository.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.Message);
                throw;
            }
            
        }
    }
}
