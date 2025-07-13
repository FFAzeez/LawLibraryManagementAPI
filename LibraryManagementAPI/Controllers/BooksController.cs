using LibraryManagementCore.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LibraryManagementAPI.Domain.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBookServices bookServices) : ControllerBase
    {
        /// <summary>
        /// Create New Book
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("")]
        public async Task<IActionResult>  Create(BookDTO bookDto)
        {
            var result = await bookServices.CreateBook(bookDto);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Update Existing Book
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookUpdateDto bookDto)
        {
            var result = await bookServices.UpdateBook(id,bookDto);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get all Exiting Books
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("")]
        public async Task<IActionResult> GetAll(GetAllBooksRequest request)
        {
            var result = await bookServices.GetBooks(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Delete Existing Book
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await bookServices.DeleteBook(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
