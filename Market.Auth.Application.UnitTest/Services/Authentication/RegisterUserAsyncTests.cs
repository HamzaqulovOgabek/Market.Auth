using Market.Auth.Application.Auth;
using Market.Auth.Application.Extensions;
using Market.Auth.Application.Services.AuthenticationService;
using Market.Auth.DataAccess.Repositories.UserRepo;
using Market.Auth.DataAccess.Repositories.UserTokenRepository;
using Market.Auth.Domain.Models;
using Microsoft.Extensions.Options;
using Moq;

namespace Market.Auth.Application.UnitTest.Services.Authentication
{
    [TestFixture]
    public class RegisterUserAsyncTests
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
        public async Task WhenEmailDoesNotExist_ReturnsSuccessTrue()
        {

            var userRegistrationDto = new UserRegistrationDto
            {
                EmailOrUsername = "non_existent_email@example.com",
                Password = "da"
            };
            _mockUserRepository
                .Setup(repo => repo.GetUserByEmailOrUsernameAsync(userRegistrationDto.EmailOrUsername))
                .ReturnsAsync((User)null);

            //Act
            var result = await _authenticationService.RegisterUserAsync(userRegistrationDto);

            //Assert
            Assert.That(result.Success, Is.True);
        }

        [Test]
        public async Task WhenEmailExist_ReturnSuccessFalse()
        {
            //Arrange
            var existingUser = new User
            {
                Id = 1,
                Email = "existing@email.com",
                PasswordHash = "hashed password"
            };
            _mockUserRepository
                .Setup(x => x.GetUserByEmailOrUsernameAsync("existing@email.com"))
                .ReturnsAsync(existingUser);

            var userRegistrationDto = new UserRegistrationDto
            {
                EmailOrUsername = "existing@email.com",
                Password = "hash"
            };

            //Act
            var result = await _authenticationService.RegisterUserAsync(userRegistrationDto);

            //Assert
            Assert.False(result.Success);
            Assert.That(result.Errors, Does.Contain("Username already exist").IgnoreCase);
        }

        [Test]
        public async Task WhenCalled_ShouldHashPasswordBeforeStoringInDatabase()
        {
            var userRegistrationDto = new UserRegistrationDto
            {
                EmailOrUsername = "new_user@example.com",
                Password = "plain_password"
            };

            //Act
            await _authenticationService.RegisterUserAsync(userRegistrationDto);

            //Assert
            _mockUserRepository.Verify(repo => repo.CreateAsync(
                It.Is<User>(u => u.PasswordHash != userRegistrationDto.Password)), Times.Once);
        }

        [Test]
        public async Task WhenConcurrentRegistrationForTheSameEmal_DoesNotCauseDataInconsistency()
        {
            // Arrange
            var userRegistrationDto = new UserRegistrationDto
            {
                EmailOrUsername = "concurrent_email@example.com",
                Password = "da"
            };
            _mockUserRepository
                .Setup(repo => repo.GetUserByEmailOrUsernameAsync(userRegistrationDto.EmailOrUsername))
                .ReturnsAsync((User)null);

            var tasks = new List<Task<OperationResult>>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(_authenticationService.RegisterUserAsync(userRegistrationDto));
            }

            //Act
            var results = await Task.WhenAll(tasks);

            //Assert 
            Assert.That(results.Count(r => r.Success), Is.EqualTo(1));
            Assert.That(results.Count(r => !r.Success), Is.EqualTo(9));
            _mockUserRepository.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Exactly(1));

        }
    }
}
