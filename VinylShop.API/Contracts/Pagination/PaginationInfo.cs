namespace VinylShop.API.Contracts.Pagination;

public record PaginationInfo(int CurrentPage, int PageSize, int TotalCount, int TotalPages);