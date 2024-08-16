namespace VinylShop.Infrastructure;

public class JwtOptions
{
    public String SecretKey { get; set; } = string.Empty;
    
    public int ExpiresHours { get; set; }
}