using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Base_API.Services;

public class UserService
{
    public static string HashPassword(string password)
    {
        byte[] salt = Convert.FromBase64String("XwQzJ+8QX0KZ1z3Z1z3Z1w==");

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8
        ));
        
        return hashed;
    }
}