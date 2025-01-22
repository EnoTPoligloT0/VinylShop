namespace VinylShop.Infrastructure.Authentication;

public class JwtOptions
{
    public String SecretKey { get; set; } = string.Empty;
    
    public int ExpiresHours { get; set; }
}