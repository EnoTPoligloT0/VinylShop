using VinylShop.API.Contracts.Orders;
using VinylShop.API.Contracts.Vinyls;

namespace VinylShop.API.Contracts.Pagination;

public record GetOrdersResponsePagination(IEnumerable<GetOrdersResponse> Orders, PaginationInfo Pagination);

public record GetVinylsResponsePagination(IEnumerable<GetVinylResponse> Vinyls, PaginationInfo Pagination);