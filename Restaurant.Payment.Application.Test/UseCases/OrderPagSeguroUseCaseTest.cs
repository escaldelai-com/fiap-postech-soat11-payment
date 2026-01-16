using Bogus.Bson;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.ExternalServices;
using Restaurant.Payment.Application.Interfaces.Presenter;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Application.UseCases;
using Restaurant.Payment.Domain;

namespace Restaurant.Payment.Application.Test;

public class OrderPagSeguroUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderPagSeguroUseCase_Ok()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var addressRepo = new Mock<IDefaultAddressRepository>();
        var infoRepo = new Mock<IPaymentInfoRepository>();
        var pixInfoPresenter = new Mock<IPixInfoPresenter>();
        var service = new Mock<IPagSeguroService>();
        var idService = new Mock<IIdentificationService>();
        var usecase = new OrderPagSeguroUseCase(
            repo.Object,
            addressRepo.Object,
            infoRepo.Object,
            pixInfoPresenter.Object,
            idService.Object,
            service.Object
        );
        var order = GetOrder();
        var address = GetAddress();
        var json = GetJson();
        var pixInfo = GetPixInfo(order);
        repo.Setup(x => x.GetById(order.Id!)).ReturnsAsync(order);
        idService.Setup(x => x.GetById(order.Cliente!.Id)).ReturnsAsync(order.Cliente);
        addressRepo.Setup(x => x.GetById(1)).ReturnsAsync(address);
        service.Setup(x => x.SendPayment(order, address)).ReturnsAsync(json);
        pixInfoPresenter.Setup(x => x.GetPixInfo(json)).Returns(pixInfo);

        // Act
        var result = await usecase.SendPayment(order.Id!);

        // Assert
        result.Should().Be(pixInfo);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task OrderPagSeguroUseCase_Invalid_OrderId(string? value)
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var addressRepo = new Mock<IDefaultAddressRepository>();
        var infoRepo = new Mock<IPaymentInfoRepository>();
        var pixInfoPresenter = new Mock<IPixInfoPresenter>();
        var service = new Mock<IPagSeguroService>();
        var idService = new Mock<IIdentificationService>();
        var usecase = new OrderPagSeguroUseCase(
            repo.Object,
            addressRepo.Object,
            infoRepo.Object,
            pixInfoPresenter.Object,
            idService.Object,
            service.Object
        );

        // Act
        var act = () => usecase.SendPayment(value!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task OrderPagSeguroUseCase_Null_Data()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var addressRepo = new Mock<IDefaultAddressRepository>();
        var infoRepo = new Mock<IPaymentInfoRepository>();
        var pixInfoPresenter = new Mock<IPixInfoPresenter>();
        var service = new Mock<IPagSeguroService>();
        var orderId = GetGuid();
        var idService = new Mock<IIdentificationService>();
        var usecase = new OrderPagSeguroUseCase(
            repo.Object,
            addressRepo.Object,
            infoRepo.Object,
            pixInfoPresenter.Object,
            idService.Object,
            service.Object
        );
        repo.Setup(x => x.GetById(orderId)).ReturnsAsync(() => null!);

        // Act
        var act = () => usecase.SendPayment(orderId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task OrderPagSeguroUseCase_Invalid_Status()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var addressRepo = new Mock<IDefaultAddressRepository>();
        var infoRepo = new Mock<IPaymentInfoRepository>();
        var pixInfoPresenter = new Mock<IPixInfoPresenter>();
        var service = new Mock<IPagSeguroService>();
        var orderId = GetGuid();
        var order = new OrderDto { Status = OrderStatus.Canceled };
        var idService = new Mock<IIdentificationService>();
        var usecase = new OrderPagSeguroUseCase(
            repo.Object,
            addressRepo.Object,
            infoRepo.Object,
            pixInfoPresenter.Object,
            idService.Object,
            service.Object
        );
        repo.Setup(x => x.GetById(orderId)).ReturnsAsync(order);

        // Act
        var act = () => usecase.SendPayment(orderId);

        // Assert
        await act.Should().ThrowAsync<OrderStatusException>();
    }

    [Fact]
    public async Task OrderPagSeguroUseCase_Null_Client()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var addressRepo = new Mock<IDefaultAddressRepository>();
        var infoRepo = new Mock<IPaymentInfoRepository>();
        var pixInfoPresenter = new Mock<IPixInfoPresenter>();
        var service = new Mock<IPagSeguroService>();
        var orderId = GetGuid();
        var order = new OrderDto { Id = orderId, Status = OrderStatus.WaitingPayment };
        var idService = new Mock<IIdentificationService>();
        var usecase = new OrderPagSeguroUseCase(
            repo.Object,
            addressRepo.Object,
            infoRepo.Object,
            pixInfoPresenter.Object,
            idService.Object,
            service.Object
        );
        repo.Setup(x => x.GetById(orderId)).ReturnsAsync(() => order);

        // Act
        var act = () => usecase.SendPayment(orderId);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task OrderPagSeguroUseCase_Client_Not_Found()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var addressRepo = new Mock<IDefaultAddressRepository>();
        var infoRepo = new Mock<IPaymentInfoRepository>();
        var pixInfoPresenter = new Mock<IPixInfoPresenter>();
        var service = new Mock<IPagSeguroService>();
        var orderId = GetGuid();
        var order = GetOrder();
        var idService = new Mock<IIdentificationService>();
        var usecase = new OrderPagSeguroUseCase(
            repo.Object,
            addressRepo.Object,
            infoRepo.Object,
            pixInfoPresenter.Object,
            idService.Object,
            service.Object
        );
        repo.Setup(x => x.GetById(orderId)).ReturnsAsync(() => order);
        idService.Setup(x => x.GetById(order.Cliente!.Id)).ReturnsAsync(() => null!);

        // Act
        var act = () => usecase.SendPayment(orderId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task OrderPagSeguroUseCase_Null_Address()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var addressRepo = new Mock<IDefaultAddressRepository>();
        var infoRepo = new Mock<IPaymentInfoRepository>();
        var pixInfoPresenter = new Mock<IPixInfoPresenter>();
        var service = new Mock<IPagSeguroService>();
        var orderId = GetGuid();
        var order = GetOrder();
        var idService = new Mock<IIdentificationService>();
        var usecase = new OrderPagSeguroUseCase(
            repo.Object,
            addressRepo.Object,
            infoRepo.Object,
            pixInfoPresenter.Object,
            idService.Object,
            service.Object
        );
        repo.Setup(x => x.GetById(orderId)).ReturnsAsync(order);
        idService.Setup(x => x.GetById(order.Cliente!.Id)).ReturnsAsync(order.Cliente);
        addressRepo.Setup(x => x.GetById(1)).ReturnsAsync(() => null!);

        // Act
        var act = () => usecase.SendPayment(orderId);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task OrderPagSeguroUseCase_Null_Payment()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var addressRepo = new Mock<IDefaultAddressRepository>();
        var infoRepo = new Mock<IPaymentInfoRepository>();
        var pixInfoPresenter = new Mock<IPixInfoPresenter>();
        var service = new Mock<IPagSeguroService>();
        var orderId = GetGuid();
        var order = GetOrder();
        var address = GetAddress();
        var idService = new Mock<IIdentificationService>();
        var usecase = new OrderPagSeguroUseCase(
            repo.Object,
            addressRepo.Object,
            infoRepo.Object,
            pixInfoPresenter.Object,
            idService.Object,
            service.Object
        );
        repo.Setup(x => x.GetById(orderId)).ReturnsAsync(order);
        idService.Setup(x => x.GetById(order.Cliente!.Id)).ReturnsAsync(order.Cliente);
        addressRepo.Setup(x => x.GetById(1)).ReturnsAsync(address);
        service.Setup(x => x.SendPayment(order, address)).ReturnsAsync(() => null);

        // Act
        var act = () => usecase.SendPayment(orderId);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }




    private DefaultAddressDto GetAddress()
    {
        return new DefaultAddressDto
        {
            Id = 1,
            Street = Faker.Address.StreetName(),
            Number = Faker.Address.BuildingNumber(),
            Complement = Faker.Address.SecondaryAddress(),
            Locality = Faker.Random.Word(),
            City = Faker.Address.City(),
            RegionCode = Faker.Address.StateAbbr(),
            Country = Faker.Address.CountryCode(),
            ZipCode = Faker.Address.ZipCode()
        };
    }

    private JObject GetJson()
    {
        var jsonString = JsonConvert.SerializeObject(GetOrder());

        return JsonConvert.DeserializeObject<JObject>(jsonString)!;
    }

    private PixInfoDto GetPixInfo(OrderDto order)
    {
        return new PixInfoDto
        {
            Code = GetGuid(),
            Image = Faker.Internet.Url(),
            Base64 = Faker.Internet.Url(),
            ExpirationDate = Faker.Date.Future(),
            OrderId = order.Id!
        };
    }

}
