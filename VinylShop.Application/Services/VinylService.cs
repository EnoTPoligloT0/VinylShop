using CSharpFunctionalExtensions;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Models;

namespace VinylShop.Application.Services;

public class VinylService : IVinylService
{
    private readonly IVinylRepository _vinylRepository;

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
    
    public async Task<Result> UpdateVinylImage(Guid vinylId, string imageBase64)
    {
        var vinyl = await _vinylRepository.GetById(vinylId);
        if (vinyl == null)
        {
            return Result.Failure("Vinyl not found.");
        }

        // Convert Base64 back to byte array
        byte[] imageData = Convert.FromBase64String(imageBase64);

        // Call the repository to update the image
        await _vinylRepository.UpdateImage(vinylId, imageData);

        return Result.Success();
    }



    public async Task DeleteVinyl(Guid id)
    {
        await _vinylRepository.Delete(id);
    }
}