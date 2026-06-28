using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Dto.DtoCategores;
using ProductApi.Servies.SCategores;

namespace ProductApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoresController : ControllerBase
    {
        private readonly ISCategores Categories;

        public CategoresController(ISCategores categores)
        {
            this.Categories = categores;
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult> GetAllCategores(
            [FromQuery] string Name,
             [FromQuery] int page = 1,
             [FromQuery] int pageSize = 10)
        {

            return Ok(await Categories.GetAllCategores(Name, page, pageSize));
        }
        [AllowAnonymous]
        [HttpPost("AddNewCategories")]

        public async Task<ActionResult> AddNewCategores([FromBody] AddNewCategoriseDto add)
        {
            return Ok(await Categories.AddNewCategorise(add));
        }
        [HttpPut("UpdateCategories/{id}")]
        public async Task<ActionResult> UpdateCategoriesDto([FromBody] UpdateCategoresDto update, [FromRoute] int id)

        {
            return Ok(await Categories.UpdateCategores(update, id));
        }
        [HttpDelete("RemoveCatigores/{id}")]

        public async Task<ActionResult> RemoveById([FromRoute] int id)
        {
            return Ok(await Categories.removeCatigores(id));
        }
    }
}
