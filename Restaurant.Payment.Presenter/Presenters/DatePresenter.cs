using Microsoft.Extensions.Configuration;
using Restaurant.Payment.Application.Interfaces.Presenter;
using Restaurant.Payment.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Payment.Presenter;

public class DatePresenter(
    IConfiguration configuration) : IDatePresenter
{

    private readonly TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(configuration["TimeZone"]
        ?? throw new ArgumentNullException("TimeZone configuration is missing"));

    public DateTime? ToUtc(DateTime? value)
    {
        return value != null
            ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
            : null;
    }

    public DateTime? ToTimeZone(DateTime? value)
    {
        return value != null
            ? TimeZoneInfo.ConvertTimeFromUtc(value.Value, timeZone)
            : null;
    }

}
