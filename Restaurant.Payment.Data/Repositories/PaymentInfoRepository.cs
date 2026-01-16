using AutoMapper;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.Presenter;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Payment.Data.Repositories;

public class PaymentInfoRepository(
    IMapper mapper,
    IDatePresenter presenter,
    PaymentContext context) : IPaymentInfoRepository
{

    public async Task Create(PaymentInfoDto paymentInfo)
    {
        paymentInfo.UpdateDate = presenter.ToUtc(paymentInfo.UpdateDate);

        var data = mapper.Map<PaymentInfoData>(paymentInfo);

        context.Attach(data.Order!);
        await context.AddAsync(data);
        await context.SaveChangesAsync();
    }

}
