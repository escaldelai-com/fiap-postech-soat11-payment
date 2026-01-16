namespace Restaurant.Payment.Domain;

public static class DateTimeValidation
{

    public static Validator IsInThePast(this Validator validator, DateTime value)
    {
        return validator.Test(value < DateTime.Now,
            $"should be in the past.");
    }

    public static Validator IsInThePastOrPresent(this Validator validator, DateTime value)
    {
        return validator.Test(value <= DateTime.Now,
            $"should be in the past or present.");
    }

    public static Validator IsNotInThePast(this Validator validator, DateTime value)
    {
        return validator.Test(value >= DateTime.Now,
            $"should not be in the past.");
    }

    public static Validator IsAfter(this Validator validator, DateTime value, DateTime from)
    {
        return validator.Test(value > from,
            $"should be after {from}.");
    }

    public static Validator IsAfterOrEqual(this Validator validator, DateTime value, DateTime from)
    {
        return validator.Test(value >= from,
            $"should be after or equal to {from}.");
    }

    public static Validator IsBefore(this Validator validator, DateTime value, DateTime to)
    {
        return validator.Test(value < to,
            $"should be before {to}.");
    }

    public static Validator IsBeforeOrEqual(this Validator validator, DateTime value, DateTime to)
    {
        return validator.Test(value <= to,
            $"should be before or equal to {to}.");
    }

    public static Validator IsBetween(this Validator validator, DateTime value, DateTime start, DateTime end)
    {
        return validator.Test(value >= start && value <= end,
            $"should be between {start} and {end}.");
    }

}
