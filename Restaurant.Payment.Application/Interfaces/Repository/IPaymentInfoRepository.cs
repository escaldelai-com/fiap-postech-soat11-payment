using Restaurant.Payment.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Payment.Application.Interfaces.Repository;

public interface IPaymentInfoRepository
{

    Task Create(PaymentInfoDto paymentInfo);

}
