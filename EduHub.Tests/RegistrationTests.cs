using EduHub.Domain.Entities;
using Xunit;

namespace EduHub.Tests;

public class RegistrationTests
{
    [Fact]
    public void Create_ValidIds_StatusIsPending()
    {
        var r = Registration.Create(
            Guid.NewGuid(), Guid.NewGuid());

        Assert.Equal(RegistrationStatus.Pending, r.Status);
    }

    [Fact]
    public void Confirm_Pending_StatusIsConfirmed()
    {
        var r = Registration.Create(
            Guid.NewGuid(), Guid.NewGuid());

        r.Confirm();

        Assert.Equal(RegistrationStatus.Confirmed, r.Status);
    }

    [Fact]
    public void Confirm_Cancelled_ThrowsAndRemainsCancelled()
    {
        var r = Registration.Create(
            Guid.NewGuid(), Guid.NewGuid());

        r.Cancel();

        Assert.Throws<InvalidOperationException>(
            () => r.Confirm());

        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }
}