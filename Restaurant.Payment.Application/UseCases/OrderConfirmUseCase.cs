using System;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.Repository;
using Restaurant.Payment.Application.Interfaces.UseCases;
using Restaurant.Payment.Domain;

namespace Restaurant.Payment.Application.UseCases;

public class OrderConfirmUseCase(
    IPaymentInfoRepository repo,
    IOrderFinalizeUseCase finalizeUseCase,
    IOrderCancelUseCase cancelUseCase) : IOrderConfirmUseCase
{

    public async Task Confirm(JObject data)
    {
        var charge = Validate(data);
        var status = charge!.Value<string>("status")?.ToUpper(CultureInfo.CurrentCulture);
        var paymentCode = charge["payment_response"]?.Value<string>("code");

        switch ($"{status}_{paymentCode}")
        {
            case "PAID_20000":
                await finalizeUseCase.Finalize(data!);
                break;
            case "CANCELED_20000":
                await cancelUseCase.Cancel(data!);
                break;
            default:
                break;
        }

        await repo.Create(new PaymentInfoDto
        {
            OrderId = data!.Value<string>("reference_id"),
            Status = status,
            UpdateDate = DateTime.Now,
            PaymentData = data
        });
    }


    private static JObject Validate(JObject data)
    {
        var validator = Validator.Create();

        validator.IsNotNull(data).Validate();
        validator.Test(data.ContainsKey("reference_id"), "Invalid Payment - No reference_id").Validate();

        JArray? charges = data?["charges"] as JArray;

        validator.Test(charges != null, "Invalid Payment - No charges found").Validate();
        validator.Test(charges!.Any(), "Invalid Payment - No charges found").Validate();
        validator.Test(charges!.Count == 1, "Invalid Payment - Multiple charges found").Validate();

        JObject? charge = charges?[0] as JObject;

        validator.Test(charge != null, "Invalid Payment - Invalid charge").Validate();
        validator
            .Test(charge!.ContainsKey("status"), "Invalid Payment - Invalid charge - No status")
            .Test(charge!.ContainsKey("payment_response"), "Invalid Payment - Invalid charge - No payment_response")
            .Validate();

        return charge!;
    }

}




/*
{
    "id": "ORDE_D7449312-9F5C-42AE-8FF1-BA3639707242",
    "reference_id": "6967092abd5e8adcd09d9196",
    "created_at": "2026-01-15T12:13:04.017-03:00",
    "customer": {
        "name": "José da Silva",
        "email": "jose.silva@fakemail.com",
        "tax_id": "07632310029"
    },
    "items": [
        {
            "name": "Cheeseburger Clássico",
            "quantity": 1,
            "unit_amount": 2490
        },
        {
            "name": "Pão de Alho",
            "quantity": 1,
            "unit_amount": 990
        },
        {
            "name": "Chá Gelado",
            "quantity": 1,
            "unit_amount": 950
        },
        {
            "name": "Torta de Maçã Individual",
            "quantity": 1,
            "unit_amount": 1350
        }
    ],
    "shipping": {
        "address": {
            "street": "Av. Lins de Vasconcelos",
            "number": "1264",
            "locality": "Cambuci",
            "city": "São Paulo",
            "region_code": "SP",
            "country": "BRA",
            "postal_code": "01538001"
        }
    },
    "qr_codes": [
        {
            "id": "QRCO_E5D959D7-67DE-4AFD-B261-F06E62F54D0E",
            "expiration_date": "2026-01-15T13:13:05.000-03:00",
            "amount": {
                "value": 5780
            },
            "text": "00020101021226850014br.gov.bcb.pix2563api-h.pagseguro.com/pix/v2/E5D959D7-67DE-4AFD-B261-F06E62F54D0E27600016BR.COM.PAGSEGURO0136E5D959D7-67DE-4AFD-B261-F06E62F54D0E520457335303986540557.805802BR5918Leandro Escaldelai6009SAO PAULO62070503***6304DDFE",
            "arrangements": [
                "PIX"
            ],
            "links": [
                {
                    "rel": "QRCODE.PNG",
                    "href": "https://sandbox.api.pagseguro.com/qrcode/QRCO_E5D959D7-67DE-4AFD-B261-F06E62F54D0E/png",
                    "media": "image/png",
                    "type": "GET"
                },
                {
                    "rel": "QRCODE.BASE64",
                    "href": "https://sandbox.api.pagseguro.com/qrcode/QRCO_E5D959D7-67DE-4AFD-B261-F06E62F54D0E/base64",
                    "media": "text/plain",
                    "type": "GET"
                }
            ]
        }
    ],
    "charges": [
        {
            "id": "CHAR_E5D959D7-67DE-4AFD-B261-F06E62F54D0E",
            "reference_id": "6967092abd5e8adcd09d9196",
            "status": "PAID",
            "created_at": "2026-01-15T12:13:19.913-03:00",
            "paid_at": "2026-01-15T12:13:21.885-03:00",
            "amount": {
                "value": 5780,
                "currency": "BRL",
                "summary": {
                    "total": 5780,
                    "paid": 5780,
                    "refunded": 0,
                    "incremented": 0
                }
            },
            "payment_response": {
                "code": "20000",
                "message": "SUCESSO"
            },
            "payment_method": {
                "type": "PIX",
                "pix": {
                    "notification_id": "NTF_1C48D504-C07E-4B0C-AE34-4DE91B80273F",
                    "end_to_end_id": "1e42c1a12e9b466da34eff4ecc6a64c0",
                    "holder": {
                        "name": "API-PIX Payer Mock",
                        "tax_id": "***931180**"
                    }
                }
            },
            "links": [
                {
                    "rel": "SELF",
                    "href": "https://internal.sandbox.api.pagseguro.com/charges/CHAR_E5D959D7-67DE-4AFD-B261-F06E62F54D0E",
                    "media": "application/json",
                    "type": "GET"
                },
                {
                    "rel": "CHARGE.CANCEL",
                    "href": "https://internal.sandbox.api.pagseguro.com/charges/CHAR_E5D959D7-67DE-4AFD-B261-F06E62F54D0E/cancel",
                    "media": "application/json",
                    "type": "POST"
                }
            ],
            "metadata": {
                "ps_order_id": "ORDE_D7449312-9F5C-42AE-8FF1-BA3639707242"
            }
        }
    ],
    "notification_urls": [
        "https://restaurant-pagseguro-hook.sa-east-1.elasticbeanstalk.com/webhook/pagseguro"
    ],
    "links": [
        {
            "rel": "SELF",
            "href": "https://sandbox.api.pagseguro.com/orders/ORDE_D7449312-9F5C-42AE-8FF1-BA3639707242",
            "media": "application/json",
            "type": "GET"
        },
        {
            "rel": "PAY",
            "href": "https://sandbox.api.pagseguro.com/orders/ORDE_D7449312-9F5C-42AE-8FF1-BA3639707242/pay",
            "media": "application/json",
            "type": "POST"
        }
    ]
}
*/