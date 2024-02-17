using System.Security.Cryptography;

namespace Cards.Infrastructure.CryptoGraphy;

public static class PasswordHelper
{
    public static byte[] GenerateSalt()
    {
        byte[] salt = new byte[16]; 
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }
    
    public static string HashPassword(string password, byte[] salt)
    {
        int iterations = 1000; 
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
        {
            byte[] hash = pbkdf2.GetBytes(20); 
            return Convert.ToBase64String(hash);
        }
    }
}