using Bogus.DataSets;
using FluentAssertions;
using Restaurant.Payment.Application.Test;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Restaurant.Payment.Domain.Test;

public class OrderTest : TestBase
{

    [Fact]
    public void Order_Create_Ok()
    {
        // Arrange
        var order = new
        {
            Data = Faker.Date.Past(),
            Cliente = GetGuid(),
            Status = Faker.Random.Word(),
            Items = Faker.Make(3, () => new
            {
                Nome = Faker.Commerce.ProductName(),
                Tipo = Faker.Commerce.Categories(1)[0],
                Preco = Faker.Random.Decimal(10, 100)

            }).ToArray()
        };

        // Act
        var model = new Order(
            order.Data,
            order.Cliente,
            order.Status,
            [.. order.Items.Select(i => new OrderItem(i.Nome, i.Tipo, i.Preco))]
        );

        // Assert
        model.Should().BeEquivalentTo(order);
    }

    [Fact]
    public void Order_Create_Invalid_Data()
    {
        // Act
        var act = () => new Order(
            Faker.Date.Future(),
            GetGuid(),
            Faker.Random.Word(),
            [.. Faker.Make(3, () => new OrderItem(
                Faker.Commerce.Product(),
                Faker.Commerce.Categories(1)[0],
                Faker.Random.Decimal(10, 100)))]
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Order_Create_Invalid_Cliente(string? value)
    {
        // Act
        var act = () => new Order(
            Faker.Date.Past(),
            value!,
            Faker.Random.Word(),
            [.. Faker.Make(3, () => new OrderItem(
                Faker.Commerce.Product(),
                Faker.Commerce.Categories(1)[0],
                Faker.Random.Decimal(10, 100)))]
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Order_Create_Invalid_Status(string? value)
    {
        // Act
        var act = () => new Order(
            Faker.Date.Past(),
            GetGuid(),
            value!,
            [.. Faker.Make(3, () => new OrderItem(
                Faker.Commerce.Product(),
                Faker.Commerce.Categories(1)[0],
                Faker.Random.Decimal(10, 100)))]
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void Order_Create_Null_Items()
    {
        // Act
        var act = () => new Order(
            Faker.Date.Past(),
            GetGuid(),
            Faker.Random.Word(),
            null!
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void Order_Create_No_Items()
    {
        // Act
        var act = () => new Order(
            Faker.Date.Past(),
            GetGuid(),
            Faker.Random.Word(),
            []
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void Order_ConfirmPay_Ok()
    {
        // Arrange
        var model = new Order(
            Faker.Date.Past(),
            GetGuid(),
            OrderStatus.WaitingPayment,
            [.. Faker.Make(3, () => new OrderItem(
                Faker.Commerce.Product(),
                Faker.Commerce.Categories(1)[0],
                Faker.Random.Decimal(10, 100)))]
        );

        // Act
        model.ConfirmPay();

        // Assert
        model.Status.Should().Be(OrderStatus.Paid);
        model.DataPagamento.Should().NotBeNull();
    }

    [Fact]
    public void Order_ConfirmPay_Invalid_Status()
    {
        // Arrange
        var model = new Order(
            Faker.Date.Past(),
            GetGuid(),
            OrderStatus.Canceled,
            [.. Faker.Make(3, () => new OrderItem(
                Faker.Commerce.Product(),
                Faker.Commerce.Categories(1)[0],
                Faker.Random.Decimal(10, 100)))]
        );

        // Act
        var act = model.ConfirmPay;

        // Assert
        act.Should().Throw<OrderStatusException>();
    }

}
