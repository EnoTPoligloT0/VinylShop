using VinylShop.Core.Dtos.VinylDtos;
using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Repositories;

public interface IVinylRepository
{
    Task Create(Vinyl vinyl);
    Task Delete(Guid id);
    Task<List<Vinyl>> Get();
    Task<Vinyl> GetById(Guid id);
    Task Update(Guid id, string title, string artist, string genre, int releaseYear, decimal price, int stock,
        string description, bool isAvailable);
}