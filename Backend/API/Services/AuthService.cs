using API.Models;
using API.Repository;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

public interface IAuthService
{
    bool ValidateCredentials(string username, string password);
}

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        // SeedData(); // Uncomment this line to seed initial data (if needed)
    }

    // Seeds initial user data for testing purposes
    public void SeedData()
    {
        User user = new User();
        user.Username = "Suman1";
        string password = "12345";

        // Generate password hash and salt
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        // Add the user to the repository
        _userRepository.AddUser(user);
    }

    // Creates a password hash and salt using HMACSHA512
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    // Validates user credentials by comparing the provided password with the stored hash and salt
    public bool ValidateCredentials(string username, string password)
    {
        User user = _userRepository.GetUserByUsername(username);

        if (user != null)
        {
            bool isValidPassword = false;

            // Verify password hash
            if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                isValidPassword = true;
            }

            return isValidPassword;
        }

        // If the user is not found, credentials are invalid
        return false;
    }

    // Verifies the provided password against the stored hash and salt
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
