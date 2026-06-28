using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Dto.DtoUser;
using ProductApi.Servies.SUser;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {

        private readonly ISUser user;

        public UserController(ISUser user)
        {
            this.user = user;
        }
        [HttpGet("GetUsers")]
        [AllowAnonymous]
        public async Task<ActionResult> GetUsers()
        {

            return Ok(await user.GetUsers());

        }
        [HttpPost("AddNewUser")]
        [AllowAnonymous]
        public async Task<ActionResult> AddNewUser([FromBody] AddNewUserDto addNew)
        {

            return Ok(await user.AddNewUserDto(addNew));

        }
        [HttpPost("SignInUser")]
        [AllowAnonymous]
        public async Task<ActionResult> SingInUser([FromBody] SingInUserDto sing)
        {

            return Ok(await user.SingInUser(sing));

        }
        [HttpDelete("RemovedUserById/{id}")]

        public async Task<ActionResult> RemovedUserById(int id)
        {

            return Ok(await user.RemovedUserById(id));

        }
        [HttpPut("UpdateUser/{id}")]

        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDto update, int id)
        {

            return Ok(await user.UpdateUser(update, id));

        }
    }
}
/*
    Name = s.Name,
                IdProduct = s.IdProduct,
                Description = s.Description,
                IdCategores = s.IdCategores,
                Price = s.Price,
                Quantity = s.Quantity,
 */