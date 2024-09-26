using Market.Auth.Application.Auth;
using Market.Auth.Application.Services.AuthenticationService;
using Market.Auth.DataAccess.Repositories.UserRepo;
using Market.Auth.DataAccess.Repositories.UserTokenRepository;
using Market.Auth.Domain.Models;
using Microsoft.Extensions.Options;
using Moq;

namespace Market.Auth.Application.UnitTest.Services.Authentication
{
    [TestFixture]
    public class LoginAsyncTests
    {
        internal Mock<IUserRepository> _mockUserRepository;
        internal Mock<IJwtHelper> _mockJwtHelper;
        internal Mock<IUserTokenRepository> _mockUserTokenRepository;
        internal Mock<IOptions<JwtSettings>> _mockJwtSettings;
        internal AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            _mockUserRepository = new Mock<IUserRepository>();
            _mockJwtHelper = new Mock<IJwtHelper>();
            _mockUserTokenRepository = new Mock<IUserTokenRepository>();
            _mockJwtSettings = new Mock<IOptions<JwtSettings>>();

            _authenticationService = new AuthenticationService(
                _mockUserRepository.Object,
                _mockJwtHelper.Object,
                _mockUserTokenRepository.Object,
                _mockJwtSettings.Object
            );
        }
        [Test]
        public async Task WhenEmailNotFound_ReturnsErrorInvalidUsernameOrPassword()
        {
            //Arrange
            var userLoginDto = new UserLoginDto
            {
                EmailOrUsername = "non_existent_email@example.com",
                Password = "plain_password"
            };
            _mockUserRepository
                .Setup(repo => repo.GetUserByEmailOrUsernameAsync(userLoginDto.EmailOrUsername))
                .ReturnsAsync((User)null);

            // Act
            var result = await _authenticationService.LoginAsync(userLoginDto);

            //Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Errors, Does.Contain("Invalid username or password").IgnoreCase);
        }

        [Test]
        public async Task WhenPasswordDoesNotMatch_ReturnsError()
        {
            //Arrange
            var userLoginDto = new UserLoginDto
            {
                EmailOrUsername = "existing_user@example.com",
                Password = "wrong_password"
            };

            var existingUser = new User
            {
                Id = 1,
                Email = "existing_user@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("correct_password")
            };
            _mockUserRepository
                .Setup(x => x.GetUserByEmailOrUsernameAsync(userLoginDto.EmailOrUsername))
                .ReturnsAsync(existingUser);

            //Act
            var result = await _authenticationService.LoginAsync(userLoginDto);

            //Assert
            Assert.False(result.Success);
            Assert.That(result.Errors, Does.Contain("Invalid username or password").IgnoreCase);
        }

        [Test]
        public async Task WhenLoginFailsFiveTimes_UsersGetsLockedout()
        {
            //Arrange
            var userLoginDto = new UserLoginDto
            {
                EmailOrUsername = "existing@email.com",
                Password = "wrong_password"
            };
            var user = new User
            {
                Email = "existing@email.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("correct_password"),
                FailedLoginAttempts = 4
            };
            _mockUserRepository
                .Setup(repo => repo.GetUserByEmailOrUsernameAsync(userLoginDto.EmailOrUsername))
                .ReturnsAsync(user);

            var result = await _authenticationService.LoginAsync(userLoginDto);

            Assert.That(user.FailedLoginAttempts, Is.EqualTo(0));
            Assert.IsNotNull(user.LockoutEndTime);
        }
    }
}
