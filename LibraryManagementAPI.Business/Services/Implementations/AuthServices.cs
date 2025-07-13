using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using LibraryManagementAPI.Business.Common;
using LibraryManagementAPI.Business.Exceptions;
using LibraryManagementAPI.Business.Services.Interface;
using LibraryManagementAPI.Business.Utility;
using LibraryManagementAPI.Domain.DTOs.Requests;
using LibraryManagementAPI.Domain.DTOs.Responses;
using LibraryManagementAPI.Domain.Model;
using LibraryManagementAPI.Infrastructure.Persistence.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementAPI.Business.Services.Implementations
{
    public class AuthServices(IMapper mapper, IAsyncRepository<User> userRepository,IConfiguration _config) : IAuthServices
    {

        /// <summary>
        ///  Register a new user
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public async Task<Response<string>> RegisterUser(RegisterDto register)
        {
            var response = new Response<string>();
            try
            {
                var user = mapper.Map<User>(register);
                user.UserName = register.Email;
                user.Password = register.Password.GetSHA512();
                var result = await userRepository.AddAsync(user);
                if (result == null)
                    throw new CustomException(Constants.notCreated);
                response.IsSuccessful = true;
                response.Message = Constants.success;
                response.StatusCode = HttpStatusCode.Created;
            }
            catch (CustomException ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = Constants.notCompleted;
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
        
        /// <summary>
        /// Login method and also generate jwt token
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public async Task<Response<UserResponseDto>> Login(LoginRequestDto userRequest)
        {
            var response = new Response<UserResponseDto>();
            try
            {

                var user = await userRepository.GetSingleAsync(_ => _.Email == userRequest.Email);
                if (user == null)
                    throw new CustomException(Constants.inValid); 
                if(user.Password != userRequest.Password.GetSHA512())
                    throw new CustomException(Constants.inValid);
                var token = await  CreateToken(user);
                response.IsSuccessful = true;
                response.Message = Constants.login;
                response.StatusCode = HttpStatusCode.OK;
                response.Data = new UserResponseDto()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FullName = user.FirstName +" "+user.LastName,
                     UserToken = token,
                };
            }
            catch (CustomException ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = Constants.notCompleted;
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return response;
        }

        private async Task<string> CreateToken(User request)
        {
            try
            {


                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecurityKey"]));
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, $"{request.Email}"),
                    new Claim(ClaimTypes.GivenName, $"{request.FirstName} {request.LastName}"),
                    new Claim(ClaimTypes.NameIdentifier, $"{request.UserName}")
                };
                var token = new JwtSecurityToken(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(double.Parse(_config["JWT:TimeOutSeconds"])),
                    signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                );

                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return await Task.FromResult(jwtToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
