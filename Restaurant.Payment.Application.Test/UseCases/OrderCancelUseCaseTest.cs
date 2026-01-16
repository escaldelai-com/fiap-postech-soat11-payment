using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.ExternalServices;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Application.UseCases;
using Restaurant.Payment.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Payment.Application.Test;

public class OrderCancelUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderCancelUseCase_Ok()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var orderService = new Mock<IOrderService>();
        var payRepo = new Mock<IPaymentInfoRepository>();
        var usecase = new OrderCancelUseCase(
            repo.Object,
            orderService.Object,
            payRepo.Object
        );
        var data = GetPaidPayment();
        var orderId = data["reference_id"]!.ToString();
        var order = GetOrder();
        order.Id = orderId;
        repo.Setup(x => x.GetById(orderId)).ReturnsAsync(order);

        // Act
        await usecase.Cancel(data);

        // Assert
        repo.Verify(x => x.Update(It.IsAny<OrderDto>()), Times.Once);
        payRepo.Verify(x => x.Create(It.IsAny<PaymentInfoDto>()), Times.Once);
        orderService.Verify(x => x.Cancel(It.IsAny<OrderDto>()), Times.Once);
        order.Status.Should().Be(OrderStatus.Canceled);
    }

    [Fact]
    public async Task OrderCancelUseCase_Null_Data()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var orderService = new Mock<IOrderService>();
        var payRepo = new Mock<IPaymentInfoRepository>();
        var usecase = new OrderCancelUseCase(
            repo.Object,
            orderService.Object,
            payRepo.Object
        );

        // Act
        var act = () => usecase.Cancel(null!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task OrderCancelUseCase_Invalid_Data()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var orderService = new Mock<IOrderService>();
        var payRepo = new Mock<IPaymentInfoRepository>();
        var usecase = new OrderCancelUseCase(
            repo.Object,
            orderService.Object,
            payRepo.Object
        );
        var data = JObject.Parse("{}");

        // Act
        var act = () => usecase.Cancel(data);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task OrderCancelUseCase_NotFound_Data()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var orderService = new Mock<IOrderService>();
        var payRepo = new Mock<IPaymentInfoRepository>();
        var usecase = new OrderCancelUseCase(
            repo.Object,
            orderService.Object,
            payRepo.Object
        );
        var data = GetPaidPayment();
        repo.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(() => null!);

        // Act
        var act = () => usecase.Cancel(data);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

}
