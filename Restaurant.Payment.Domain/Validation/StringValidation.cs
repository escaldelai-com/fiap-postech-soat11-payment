namespace Restaurant.Payment.Domain;

public static class StringValidation
{

    public static Validator IsNotNullOrEmpty(this Validator validator, string? value)
    {
        return validator.Test(!string.IsNullOrEmpty(value),
            $"Should not be null or empty.");
    }

    public static Validator IsNotNullOrWhiteSpace(this Validator validator, string? value)
    {
        return validator.Test(!string.IsNullOrWhiteSpace(value),
            $"should not be null, empty or white spaces only.");
    }

    public static Validator MaxLength(this Validator validator, string value, int maxLength)
    {
        return validator.Test(value != null && value.Length <= maxLength,
            $"Should not exceed maximum length of {maxLength}.");
    }


}
