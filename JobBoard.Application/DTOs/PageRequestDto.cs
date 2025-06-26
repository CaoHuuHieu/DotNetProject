namespace JobBoard.Application.DTOs
{
    public class PageRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchBy { get; set; } = string.Empty;
        public string SearchValue { get; set; } = string.Empty;
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = "asc";

    }
}