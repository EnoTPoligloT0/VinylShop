using VinylShop.API.Contracts.Orders;

namespace VinylShop.API.Contracts.Pagination;

public record GetOrdersResponsePagination(IEnumerable<GetOrdersResponse> Orders, PaginationInfo Pagination);