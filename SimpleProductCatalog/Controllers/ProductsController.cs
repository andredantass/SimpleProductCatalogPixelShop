
using Microsoft.AspNetCore.Mvc;
using SimpleProductCatalog.Abstraction.DTO;
using SimpleProductCatalog.Abstraction.Interface;

namespace SimpleProductCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
            
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> Delete([FromServices] IProductServices service,
          string id)
        {
            try
            {
                return Ok(await service.DeleteProduct(id));
            }
            catch (Exception e)
            {
                return BadRequest("Error: not possible delete Product");
            }
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> Put([FromServices] IProductServices service,
           [FromBody] ProductDTO request)
        {
            try
            {
                var (success, error) = await service.UpdateProduct(request);

                if (!success)
                {
                    return BadRequest(new { Message = error });
                }

                return Ok(new { Message = "Product updated successfully." });
            }
            catch (Exception e)
            {
                // Optionally log exception here
                return StatusCode(500, new { Message = "Error:  not possible updating Product." });
            }
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Post([FromServices] IProductServices service,
            [FromBody] ProductDTO request)
        {
            try
            {
                return Ok(await service.CreateProduct(request));
            }
            catch (Exception e)
            {
                return BadRequest("Error:  not possible creating a new Product");
            }
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> Get([FromServices] IProductServices service)
        {
            try
            {
                return Ok(await service.GetAllProduct());
            }
            catch (Exception e)
            {
                return BadRequest("Error:  not possible get all products");
            }
        }
    }
}
