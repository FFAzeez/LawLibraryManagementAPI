using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Domain.Model
{
    public class User:BaseModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
