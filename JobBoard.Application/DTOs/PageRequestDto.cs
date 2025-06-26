namespace JobBoard.Application.DTOs
{
    public class PageRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string searchBy { get; set; } = string.Empty;
        public string searchValue { get; set; } = string.Empty;
        public string sortBy { get; set; } = string.Empty;
        public string sortDirection { get; set; } = "asc";

    }
}