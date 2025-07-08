using Microsoft.AspNetCore.Mvc;
using Moq;
using UserAccessManagement.API.Controllers;
using UserAccessManagement.Application.DTOs;
using UserAccessManagement.Application.Interfaces;
using UserAccessManagement.Domain.Entities;
using Xunit;

namespace UserAccessManagement.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetAllUsers_ReturnsListOfUserDtos()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.GetAllUsersAsync())
                .ReturnsAsync(new List<User> {
            new User { Id = Guid.NewGuid(), FirstName = "David", LastName = "Vac", Email = "david.vacko@gmail.com" }
                });

            var controller = new UsersController(mockService.Object);
            var result = await controller.GetUsers();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var userDtos = Assert.IsAssignableFrom<List<UserDto>>(okResult.Value);

            Assert.Single(userDtos);
            Assert.Equal("David", userDtos[0].FirstName);
        }

        [Fact]
        public async Task GetUserByIdAsync_ExistingId_ReturnsUser()
        {
            var userId = Guid.NewGuid();
            var expectedUser = new User
            {
                Id = userId,
                FirstName = "Klara",
                LastName = "Vac",
                Email = "klara.vac@gmail.com"
            };

            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(expectedUser);

            var userService = mockRepo.Object;

            var user = await userService.GetUserByIdAsync(userId);

            Assert.NotNull(user);
            Assert.Equal("Klara", user.FirstName);
        }

        [Fact]
        public async Task CreateUserAsync_ValidUser_ReturnsCreatedUser()
        {
            // Arrange
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "David",
                LastName = "Vac",
                Email = "david.vacko@gmail.com"
            };

            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(repo => repo.CreateUserAsync(It.IsAny<User>()))
                .ReturnsAsync(newUser);

            var userService = mockRepo.Object;

            // Act
            var createdUser = await userService.CreateUserAsync(newUser);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal("David", createdUser.FirstName);
        }

        [Fact]
        public async Task DeleteUserAsync_ExistingId_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(repo => repo.DeleteUserAsync(userId))
                .ReturnsAsync(true);

            var userService = mockRepo.Object;

            // Act
            var result = await userService.DeleteUserAsync(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserAsync_NonExistingId_ReturnsFalse()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var mockRepo = new Mock<IUserService>();
            mockRepo.Setup(repo => repo.DeleteUserAsync(userId))
                .ReturnsAsync(false);

            var userService = mockRepo.Object;

            // Act
            var result = await userService.DeleteUserAsync(userId);

            // Assert
            Assert.False(result);
        }
    }
}
