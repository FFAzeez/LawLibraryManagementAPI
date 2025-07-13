namespace LibraryManagementAPI.Domain.DTOs.Requests;


public class PagingModel
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "CreatedDate";
    public string SortOrder { get; set; } = "desc";
}