using FluentAssertions;
using Restaurant.Payment.Application.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Payment.Domain.Test;

public class OrderItemTest : TestBase
{

    [Fact]
    public void OrderItem_Create_Ok()
    {
        // Arrange
        var item = new
        {
            Nome = Faker.Commerce.ProductName(),
            Tipo = Faker.Commerce.Categories(1)[0],
            Preco = Faker.Random.Decimal(10, 100)
        };

        // Act
        var model = new OrderItem(
            item.Nome,
            item.Tipo,
            item.Preco
        );

        // Assert
        model.Should().BeEquivalentTo(item);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void OrderItem_Invalid_Name(string? value)
    {
        // Act
        var act = () => new OrderItem(
            value!,
            Faker.Commerce.Categories(1)[0],
            Faker.Random.Decimal(10, 100)
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void OrderItem_Invalid_Type(string? value)
    {
        // Act
        var act = () => new OrderItem(
            Faker.Commerce.ProductName(),
            value!,
            Faker.Random.Decimal(10, 100)
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void OrderItem_Invalid_Price(decimal value)
    {
        // Act
        var act = () => new OrderItem(
            Faker.Commerce.ProductName(),
            Faker.Commerce.Categories(1)[0],
            value
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

}
