using ConsidWebShop.Api.Entities;
using ConsidWebShop.Api.Repositories;
using ConsidWebShop.Api.Repositories.Contracts;
using ConsidWebShop.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsidWebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepositorycs;

        public UserController(IUserRepository userRepositorycs)
        {
            _userRepositorycs = userRepositorycs;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userRepositorycs.GetUsers();
                if (users == null)
                {
                    return NotFound();
                }
                return users.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetItem(int id)
        {
            try
            {
                var user = await _userRepositorycs.GetUser(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(User);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                                "Error retrieving data from the database");
            }

        }

    }
}
