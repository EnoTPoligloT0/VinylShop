using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Models;

namespace VinylShop.Application.Services;

public class VinylService : IVinylService
{
    public readonly IVinylRepository _vinylRepository;

    public VinylService(IVinylRepository vinylRepository)
    {
        _vinylRepository = vinylRepository;
    }

    public async Task CreateVinyl(Vinyl vinyl)
    {
        await _vinylRepository.Create(vinyl);
    }

    public async Task<List<Vinyl>> GetVinyls()
    {
        return await _vinylRepository.Get();
    }

    public async Task<Vinyl> GetVinylById(Guid id)
    {
        return await _vinylRepository.GetById(id);
    }

    public async Task UpdateVinyl(Guid id, string title, string artist, string genre, int releaseYear,
        decimal price, int stock, string description, bool isAvailable)
    {
        await _vinylRepository.Update(id, title, artist, genre, releaseYear, price, stock, description, isAvailable);
    }

    public async Task DeleteVinyl(Guid id)
    {
        await _vinylRepository.Delete(id);
    }
}