using LibraryManagementAPI.Domain.DTOs.Requests;
using LibraryManagementAPI.Domain.DTOs.Responses;

namespace LibraryManagementAPI.Business.Services.Interface
{
    public interface IAuthServices
    {
        Task<Response<string>> RegisterUser(RegisterDto register);
        Task<Response<UserResponseDto>> Login(LoginRequestDto userRequest);

    }
}