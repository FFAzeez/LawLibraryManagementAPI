using System.Net;
using LibraryManagementCore.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LibraryManagementAPI.Business.Services.Interface;
using LibraryManagementAPI.Domain.DTOs.Requests;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices) : ControllerBase
    {
        /// <summary>
        /// Register a new User.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Register")]
        public async Task<IActionResult>  Registration(RegisterDto request)
        {
            var result = await authServices.RegisterUser(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Log in Existing User.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        /// [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto client)
        {
            var result = await authServices.Login(client);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
