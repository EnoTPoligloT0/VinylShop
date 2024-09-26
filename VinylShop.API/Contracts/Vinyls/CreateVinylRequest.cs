using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;


namespace VinylShop.API.Contracts.Vinyls;

public record CreateVinylRequest(
    [Required] string Title,
    [Required] string Artist,
    [Required] string Genre,
    [Required] int ReleaseYear,
    [Required] decimal Price,
    [Required] int Stock,
    [Required] string Description,
    [Required] bool IsAvailable,
    IFormFile ImageFile
); 