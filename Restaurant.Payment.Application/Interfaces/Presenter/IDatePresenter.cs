using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Payment.Application.Interfaces.Presenter;

public interface IDatePresenter
{

    DateTime? ToUtc(DateTime? value);

    DateTime? ToTimeZone(DateTime? value);

}
