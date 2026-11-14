using EduHub.Application.Services;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;
using Moq;
using Xunit;

namespace EduHub.Tests;

public class EventServiceTests
{
    [Fact]
    public async Task CreateEvent_ValidData_CallsAddAndSendOnce()
    {
        // Arrange
        var mockRepo = new Mock<IEventRepository>();
        var mockNotification = new Mock<INotificationService>();

        var service = new EventService(
            mockRepo.Object,
            mockNotification.Object);

        var clubId = Guid.NewGuid();

        // Act
        var result = await service.CreateEventAsync(
            clubId,
            "Tech Workshop",
            DateTime.UtcNow.AddDays(7),
            50);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Tech Workshop", result.Title);

        mockRepo.Verify(
            r => r.AddAsync(It.IsAny<Event>()),
            Times.Once);

        mockNotification.Verify(
            n => n.Send(It.IsAny<string>()),
            Times.Once);
    }
}