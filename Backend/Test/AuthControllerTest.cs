// Tests/AuthControllerTests.cs
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

[TestFixture]
public class AuthControllerTests
{
    private AuthController _authController;

    [SetUp]
    public void Setup()
    {
        // Assuming you have a mock or in-memory user service
        var mockUserService = new Mock<IAuthService>();
        _authController = new AuthController(mockUserService.Object);
    }

    [Test]
    public void Login_ValidUser_ReturnsOk()
    {
        // Arrange
        var loginRequest = new LoginRequest { Username = "suman", Password = "12345" };

        var mockUserService = new Mock<IAuthService>();
        mockUserService.Setup(x => x.ValidateCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        var authController = new AuthController(mockUserService.Object);

        // Act
        var result = authController.Validate(loginRequest);

        // Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
    }

    [Test]
    public void Login_InvalidUser_ReturnsBadRequest()
    {
        // Arrange
        var loginRequest = new LoginRequest { Username = "asdfggg", Password = "sferw3" };

        var mockUserService = new Mock<IAuthService>();
        mockUserService.Setup(x => x.ValidateCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        var authController = new AuthController(mockUserService.Object);

        // Act
        var result = authController.Validate(loginRequest);

        // Assert
        Assert.That(result, Is.TypeOf<UnauthorizedObjectResult>());
    }
}
