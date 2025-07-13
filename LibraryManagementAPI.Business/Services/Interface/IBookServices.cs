using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementAPI.Domain.DTOs.Requests;
using LibraryManagementAPI.Domain.DTOs.Responses;

namespace LibraryManagementCore.Services.Interface
{
    public interface IBookServices
    {
        Task<Response<string>> CreateBook(BookDTO bookDto);
        Task<Response<string>> DeleteBook(int Id);
        Task<Response<IEnumerable<BookResponseDTO>>> GetBooks(GetAllBooksRequest request);
        Task<Response<string>> UpdateBook(int Id,BookUpdateDto update);
    }
}