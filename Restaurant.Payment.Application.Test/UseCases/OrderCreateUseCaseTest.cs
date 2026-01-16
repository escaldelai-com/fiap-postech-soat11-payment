using Bogus.Extensions.Brazil;
using FluentAssertions;
using Moq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Application.UseCases;
using Restaurant.Payment.Domain;

namespace Restaurant.Payment.Application.Test;

public class OrderCreateUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderCreateUseCase_Ok()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var useCase = new OrderCreateUseCase(repo.Object);
        var order = GetOrder();
        repo.Setup(x => x.Create(order)).ReturnsAsync(order);

        // Act
        var result = await useCase.Create(order);

        // Assert
        result.Should().BeEquivalentTo(order);
    }

    [Fact]
    public async Task OrderCreateUseCase_Null_Order()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var useCase = new OrderCreateUseCase(repo.Object);

        // Act
        var act = () => useCase.Create(null!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task OrderCreateUseCase_Null_Order_Data()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var useCase = new OrderCreateUseCase(repo.Object);
        var order = new OrderDto();

        // Act
        var act = () => useCase.Create(order);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }





}
