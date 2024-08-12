namespace VinylShop.API.Contracts.Vinyls;

public record UpdateVinylRequest(
    string Title,
    string Artist,
    string Genre,
    int ReleaseYear,
    decimal Price,
    int Stock,
    string Description,
    bool IsAvailable
);