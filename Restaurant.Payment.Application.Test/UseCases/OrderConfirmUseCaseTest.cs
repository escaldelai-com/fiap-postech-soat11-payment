using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Application.Interfaces.UseCases;
using Restaurant.Payment.Application.UseCases;
using Restaurant.Payment.Domain;

namespace Restaurant.Payment.Application.Test;

public class OrderConfirmUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderConfirmUseCase_Finalize_Ok()
    {
        // Arrange
        var repo = new Mock<IPaymentInfoRepository>();
        var finalizeUseCase = new Mock<IOrderFinalizeUseCase>();
        var cancelUseCase = new Mock<IOrderCancelUseCase>();
        var useCase = new OrderConfirmUseCase(
            repo.Object,
            finalizeUseCase.Object,
            cancelUseCase.Object
        );
        var jobject = GetPaidPayment();

        // Act
        await useCase.Confirm(jobject);

        // Assert
        finalizeUseCase.Verify(x => x.Finalize(jobject), Times.Once);
        cancelUseCase.Verify(x => x.Cancel(jobject), Times.Never);
    }

    [Fact]
    public async Task OrderConfirmUseCase_Cancel_Ok()
    {
        // Arrange
        var repo = new Mock<IPaymentInfoRepository>();
        var finalizeUseCase = new Mock<IOrderFinalizeUseCase>();
        var cancelUseCase = new Mock<IOrderCancelUseCase>();
        var useCase = new OrderConfirmUseCase(
            repo.Object,
            finalizeUseCase.Object,
            cancelUseCase.Object
        );
        var jobject = GetCanceledPayment();

        // Act
        await useCase.Confirm(jobject);

        // Assert
        finalizeUseCase.Verify(x => x.Finalize(jobject), Times.Never);
        cancelUseCase.Verify(x => x.Cancel(jobject), Times.Once);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(/*lang=json,strict*/ "{}")]
    [InlineData(/*lang=json,strict*/ "{\"reference_id\":\"foo\"}")]
    [InlineData(/*lang=json,strict*/ "{\"reference_id\":\"foo\",\"charges\":[]}")]
    [InlineData(/*lang=json,strict*/ "{\"reference_id\":\"foo\",\"charges\":[{},{}]}")]
    [InlineData(/*lang=json,strict*/ "{\"reference_id\":\"foo\",\"charges\":[{}]}")]
    [InlineData(/*lang=json,strict*/ "{\"reference_id\":\"foo\",\"charges\":[{\"status\":\"foo\"}]}")]
    public async Task OrderConfirmUseCase_Invalid_Data(string? json)
    {
        // Arrange
        var repo = new Mock<IPaymentInfoRepository>();
        var finalizeUseCase = new Mock<IOrderFinalizeUseCase>();
        var cancelUseCase = new Mock<IOrderCancelUseCase>();
        var useCase = new OrderConfirmUseCase(
            repo.Object,
            finalizeUseCase.Object,
            cancelUseCase.Object
        );
        var jobject = GetPayment(json);

        // Act
        var act = () => useCase.Confirm(jobject!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

}
