using API.Models;
using API.Repository;
using Microsoft.EntityFrameworkCore;

[TestFixture]
public class UserRepositoryTests
{
    [Test]
    public void GetUserByUsername_ValidUsername_ReturnsUser()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<YourDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemory_UserRepositoryTests_GetUserByUsername")
            .Options;

        using (var dbContext = new YourDbContext(options))
        {
            var userRepository = new UserRepository(dbContext);

            var userToAdd = new User { UserId = 1, Username = "testuser" };
            dbContext.Users.Add(userToAdd);
            dbContext.SaveChanges();

            // Act
            var result = userRepository.GetUserByUsername("testuser");

            // Assert
            result.Equals(userToAdd);
            result.Username.Equals("testuser");
        }
    }

    [Test]
    public void GetUserByUsername_InvalidUsername_ReturnsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<YourDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemory_UserRepositoryTests_GetUserByUsername_Invalid")
            .Options;

        using (var dbContext = new YourDbContext(options))
        {
            var userRepository = new UserRepository(dbContext);

            // Act
            var result = userRepository.GetUserByUsername("nonexistentuser");

            // Assert
            result.Equals(null);
        }
    }

    [Test]
    public void AddUser_ValidUser_UserAddedSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<YourDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemory_UserRepositoryTests_AddUser")
            .Options;

        using (var dbContext = new YourDbContext(options))
        {
            var userRepository = new UserRepository(dbContext);

            var userToAdd = new User { UserId = 1, Username = "newuser" };

            // Act
            userRepository.AddUser(userToAdd);

            // Assert
            var result = dbContext.Users.FirstOrDefault(u => u.Username == "newuser");
            Assert.Equals(userToAdd, result);
        }
    }
}
