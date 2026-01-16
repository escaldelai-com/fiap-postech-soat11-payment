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

public class OrderFinalizeUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderFinalizeUseCase_Ok()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var orderService = new Mock<IOrderService>();
        var payRepo = new Mock<IPaymentInfoRepository>();
        var usecase = new OrderFinalizeUseCase(
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
        await usecase.Finalize(data);

        // Assert
        repo.Verify(x => x.Update(It.IsAny<OrderDto>()), Times.Once);
        payRepo.Verify(x => x.Create(It.IsAny<PaymentInfoDto>()), Times.Once);
        orderService.Verify(x => x.ConfirmPayment(It.IsAny<OrderDto>()), Times.Once);
        order.Status.Should().Be(OrderStatus.Paid);
    }

    [Fact]
    public async Task OrderFinalizeUseCase_Null_Data()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var orderService = new Mock<IOrderService>();
        var payRepo = new Mock<IPaymentInfoRepository>();
        var usecase = new OrderFinalizeUseCase(
            repo.Object,
            orderService.Object,
            payRepo.Object
        );

        // Act
        var act = () => usecase.Finalize(null!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task OrderFinalizeUseCase_Invalid_Data()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var orderService = new Mock<IOrderService>();
        var payRepo = new Mock<IPaymentInfoRepository>();
        var usecase = new OrderFinalizeUseCase(
            repo.Object,
            orderService.Object,
            payRepo.Object
        );
        var data = JObject.Parse("{}");

        // Act
        var act = () => usecase.Finalize(data);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task OrderFinalizeUseCase_NotFound_Data()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var orderService = new Mock<IOrderService>();
        var payRepo = new Mock<IPaymentInfoRepository>();
        var usecase = new OrderFinalizeUseCase(
            repo.Object,
            orderService.Object,
            payRepo.Object
        );
        var data = GetPaidPayment();
        repo.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(() => null!);

        // Act
        var act = () => usecase.Finalize(data);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

}
