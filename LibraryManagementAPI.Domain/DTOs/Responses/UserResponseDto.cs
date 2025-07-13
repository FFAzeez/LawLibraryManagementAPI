﻿namespace LibraryManagementAPI.Domain.DTOs.Responses
{
    public class UserResponseDto
    {
        public string Email { get; set; }
        public string UserToken { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
