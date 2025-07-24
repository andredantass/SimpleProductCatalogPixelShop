using SimpleProductCatalog.Abstraction.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleProductCatalog.Abstraction.Interface
{
    public interface ICategoryServices
    {
        Task<List<CategoryDTO>> GetAllCategory();
        Task<string> CreateCategory(CategoryDTO category);
        Task<bool> DeleteCategory(string id);
        Task<bool> UpdateCategory(CategoryDTO category);
    }
}
