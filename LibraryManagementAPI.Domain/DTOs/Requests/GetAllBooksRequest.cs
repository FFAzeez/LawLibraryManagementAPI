namespace LibraryManagementAPI.Domain.DTOs.Requests;

public class GetAllBooksRequest:PagingModel
{
    public string? Search { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}