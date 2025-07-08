using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccessManagement.Application.DTOs;
using UserAccessManagement.Application.Interfaces;
using UserAccessManagement.Domain.Entities;
using UserAccessManagement.Domain.Enums;

namespace UserAccessManagement.Tests
{
    public class AccessGrantServiceTests
    {
        [Fact]
        public async Task GrantAccessAsync_ValidDto_ReturnsTrue()
        {
            // Arrange
            var dto = new GrantAccessDto
            {
                UserId = Guid.NewGuid(),
                ResourceId = Guid.NewGuid(),
                AccessLevel = AccessLevel.Read.ToString() // Assuming this enum is accessible here
            };

            var mockService = new Mock<IAccessGrantService>();
            mockService.Setup(s => s.GrantAccessAsync(dto)).ReturnsAsync(true);

            // Act
            var result = await mockService.Object.GrantAccessAsync(dto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RevokeAccessAsync_ExistingUserAndResource_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var resourceId = Guid.NewGuid();

            var mockService = new Mock<IAccessGrantService>();
            mockService.Setup(s => s.RevokeAccessAsync(userId, resourceId)).ReturnsAsync(true);

            // Act
            var result = await mockService.Object.RevokeAccessAsync(userId, resourceId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RevokeAccessAsync_NonExistingUserOrResource_ReturnsFalse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var resourceId = Guid.NewGuid();

            var mockService = new Mock<IAccessGrantService>();
            mockService.Setup(s => s.RevokeAccessAsync(userId, resourceId)).ReturnsAsync(false);

            // Act
            var result = await mockService.Object.RevokeAccessAsync(userId, resourceId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetResourcesByUserAsync_ExistingUser_ReturnsResourceList()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedResources = new List<UserResourceAccessDto>
        {
            new UserResourceAccessDto
            {
                ResourceId = Guid.NewGuid(),
                ResourceName = "Resource 1",
                AccessLevel = AccessLevel.Write.ToString()
            },
            new UserResourceAccessDto
            {
                ResourceId = Guid.NewGuid(),
                ResourceName = "Resource 2",
                AccessLevel = AccessLevel.Read.ToString()
            }
        };

            var mockService = new Mock<IAccessGrantService>();
            mockService.Setup(s => s.GetResourcesByUserAsync(userId)).ReturnsAsync(expectedResources);

            // Act
            var resources = await mockService.Object.GetResourcesByUserAsync(userId);

            // Assert
            Assert.NotNull(resources);
            Assert.Collection(resources,
                r => Assert.Equal("Resource 1", r.ResourceName),
                r => Assert.Equal("Resource 2", r.ResourceName));
        }
    }
}
