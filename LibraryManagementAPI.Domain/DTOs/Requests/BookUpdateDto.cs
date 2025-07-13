namespace LibraryManagementAPI.Domain.DTOs.Requests
{
    public class BookUpdateDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
    }
    
}
