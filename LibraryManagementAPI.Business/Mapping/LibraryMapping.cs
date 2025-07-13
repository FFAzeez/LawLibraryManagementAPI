using AutoMapper;
using LibraryManagementAPI.Domain.DTOs.Requests;
using LibraryManagementAPI.Domain.DTOs.Responses;
using LibraryManagementAPI.Domain.Model;

namespace LibraryManagementAPI.Business.Utility.Mapping
{
    public class LibraryMapping:Profile
    {
        public LibraryMapping()
        {
            CreateMap<Book,BookDTO>().ReverseMap();
            CreateMap<Book,BookResponseDTO>();
            CreateMap<RegisterDto, User>().ReverseMap();
        }
    }
}
