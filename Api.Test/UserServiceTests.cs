using Api.Models;
using Api.Repositories;
using Api.Services;
using Moq;

namespace Api.Test
{
    public class UserServiceTests
    {
        private readonly UserService _sut;
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        public UserServiceTests()
        {
            //System Under Test
            _sut = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            //Arrange
            int userId = 1;

            var user = new User
            {
                Id = 1,
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

            //Act
            var result = await _sut.GetByIdAsync(userId);

            //Assert
            Assert.Equal(userId, result.Id);
            Assert.Equal(user.Name, result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNothing_WhenUserDoesNotExists()
        {
            int userId = 1;
            //Arrange
            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(() => null);

            //Act
            var result = await _sut.GetByIdAsync(userId);

            //Assert
            Assert.Null(result);
        }
    }
}