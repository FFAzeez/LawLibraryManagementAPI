namespace LibraryManagementAPI.Domain.DTOs.Responses
{
    public class BookResponseDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; } 
    }
}
