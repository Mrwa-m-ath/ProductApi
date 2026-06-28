using Microsoft.AspNetCore.Mvc;
using ProductApi.Dto.DtoProduct;
using ProductApi.Servies.SProduct;

namespace ProductApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly ISProduct product;


        public ProductController(ISProduct product)
        {

            this.product = product;
        }
        [HttpGet("GetAllProduct")]

        public async Task<ActionResult> GetAllProduct(
    [FromQuery] string? name,

     [FromQuery] int page = 1,
      [FromQuery] int pageSize = 1)

        {
            return Ok(await product.GetAllProduct(name, page, pageSize));
        }


        [HttpPost("AddNewProduct")]
        public async Task<ActionResult> AddNewProduct([FromBody] AddNewProductDto addNew)
        {
            return Ok(await (product.AddNewProduct(addNew)));
        }
        [HttpPut("UpdateProduct/{id}")]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductDto update, [FromRoute] int id)
        {
            return Ok(await (product.UpdateProduct(update, id)));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemovedProduct([FromRoute] int id)
        {
            return Ok(await (product.RemovedProduct(id)));
        }
    }
}
