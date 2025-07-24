
using Microsoft.AspNetCore.Mvc;
using SimpleProductCatalog.Abstraction.DTO;
using SimpleProductCatalog.Abstraction.Interface;

namespace SimpleProductCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController()
        {
            
        }
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> Delete([FromServices] ICategoryServices service,
          string id)
        {
            try
            {
                return Ok(await service.DeleteCategory(id));
            }
            catch (Exception e)
            {
                return BadRequest("Error:  not possible update Category");
            }
        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> Put([FromServices] ICategoryServices service,
           [FromBody] CategoryDTO request)
        {
            try
            {
                return Ok(await service.UpdateCategory(request));
            }
            catch (Exception e)
            {
                return BadRequest("Error:  not possible update Category");
            }
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> Post([FromServices] ICategoryServices service,
            [FromBody] CategoryDTO request)
        {
            try
            {
                return Ok(await service.CreateCategory(request));
            }
            catch (Exception e)
            {
                return BadRequest("Error:  not possible creating a new Category");
            }
        }
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> Get([FromServices] ICategoryServices service)
        {
            try
            {
                return Ok(await service.GetAllCategory());
            }
            catch (Exception e)
            {
                return BadRequest("Error:  not possible creating a new Category");
            }
        }
    }
}
