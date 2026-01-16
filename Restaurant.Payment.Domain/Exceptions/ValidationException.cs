namespace Restaurant.Payment.Domain;

public class ValidationException(IEnumerable<string> errors) : Exception
{

    public override string Message => 
        "Validation Errors: " + string.Join(Environment.NewLine, errors);

}
