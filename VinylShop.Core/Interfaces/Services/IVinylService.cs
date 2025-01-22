using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Services;

public interface IVinylService
{
    Task CreateVinyl(Vinyl vinyl);
    Task DeleteVinyl(Guid id);
    Task<List<Vinyl>> GetVinyls();
    Task<Vinyl> GetVinylById(Guid id);
    Task<List<Vinyl>> GetFilteredVinyls(string? genre, int? decade, string? sortOption, int page, int pageSize);
    Task<int> GetTotalVinylCount();
    Task UpdateVinyl(Guid id, string title, string artist, string genre, int releaseYear, decimal price, int stock,
        string description, bool isAvailable);
}