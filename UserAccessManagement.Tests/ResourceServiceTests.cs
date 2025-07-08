using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccessManagement.Application.Interfaces;
using UserAccessManagement.Domain.Entities;

namespace UserAccessManagement.Tests
{
    public class ResourceServiceTests
    {
        [Fact]
        public async Task GetAllResourcesAsync_ReturnsListOfResources()
        {
            // Arrange
            var mockRepo = new Mock<IResourceService>();
            mockRepo.Setup(repo => repo.GetAllResourcesAsync())
                .ReturnsAsync(new List<Resource> {
                new Resource { Id = Guid.NewGuid(), Name = "Resource1" }
                });

            var service = mockRepo.Object;

            // Act
            var resources = await service.GetAllResourcesAsync();

            // Assert
            Assert.NotEmpty(resources);
            Assert.Equal("Resource1", resources[0].Name);
        }

        [Fact]
        public async Task GetResourceByIdAsync_ExistingId_ReturnsResource()
        {
            // Arrange
            var resourceId = Guid.NewGuid();
            var expectedResource = new Resource { Id = resourceId, Name = "ResourceA" };

            var mockRepo = new Mock<IResourceService>();
            mockRepo.Setup(repo => repo.GetResourceByIdAsync(resourceId))
                .ReturnsAsync(expectedResource);

            var service = mockRepo.Object;

            // Act
            var resource = await service.GetResourceByIdAsync(resourceId);

            // Assert
            Assert.NotNull(resource);
            Assert.Equal("ResourceA", resource.Name);
        }

        [Fact]
        public async Task CreateResourceAsync_ValidResource_ReturnsCreatedResource()
        {
            // Arrange
            var newResource = new Resource { Id = Guid.NewGuid(), Name = "NewResource" };

            var mockRepo = new Mock<IResourceService>();
            mockRepo.Setup(repo => repo.CreateResourceAsync(It.IsAny<Resource>()))
                .ReturnsAsync(newResource);

            var service = mockRepo.Object;

            // Act
            var createdResource = await service.CreateResourceAsync(newResource);

            // Assert
            Assert.NotNull(createdResource);
            Assert.Equal("NewResource", createdResource.Name);
        }

        [Fact]
        public async Task DeleteResourceAsync_ExistingId_ReturnsTrue()
        {
            // Arrange
            var resourceId = Guid.NewGuid();

            var mockRepo = new Mock<IResourceService>();
            mockRepo.Setup(repo => repo.DeleteResourceAsync(resourceId))
                .ReturnsAsync(true);

            var service = mockRepo.Object;

            // Act
            var result = await service.DeleteResourceAsync(resourceId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteResourceAsync_NonExistingId_ReturnsFalse()
        {
            // Arrange
            var resourceId = Guid.NewGuid();

            var mockRepo = new Mock<IResourceService>();
            mockRepo.Setup(repo => repo.DeleteResourceAsync(resourceId))
                .ReturnsAsync(false);

            var service = mockRepo.Object;

            // Act
            var result = await service.DeleteResourceAsync(resourceId);

            // Assert
            Assert.False(result);
        }
    }
}
