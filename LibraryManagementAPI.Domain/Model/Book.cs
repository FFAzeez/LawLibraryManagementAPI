using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Domain.Model
{
    public class Book:BaseModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [DataType(DataType.Text)]
        public string Author { get; set; }
        [DataType(DataType.Text)]
        public string ISBN { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}
