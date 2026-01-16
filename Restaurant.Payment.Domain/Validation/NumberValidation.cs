namespace Restaurant.Payment.Domain;

public static class NumberValidation
{

    public static Validator GreaterThanZero(this Validator validator, int value)
    {
        return validator.Test(value > 0,
            $"should be greater than zero.");
    }

    public static Validator GreaterThanOrEqualToZero(this Validator validator, int value)
    {
        return validator.Test(value >= 0,
            $"should be greater than or equal to zero.");
    }

    public static Validator GreaterThanZero(this Validator validator, decimal value)
    {
        return validator.Test(value > 0,
            $"should be greater than zero.");
    }

    public static Validator GreaterThanOrEqualToZero(this Validator validator, decimal value)
    {
        return validator.Test(value >= 0,
            $"should be greater than or equal to zero.");
    }


}
