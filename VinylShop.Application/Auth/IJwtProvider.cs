using VinylShop.Core.Models;

namespace VinylShop.Application.Auth;

public interface IJwtProvider
{
    string Generate(User user);
}