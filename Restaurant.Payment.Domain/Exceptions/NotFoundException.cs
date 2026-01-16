namespace Restaurant.Payment.Domain;

public class NotFoundException(string item) : Exception
{

    public override string Message => $"{item} Not found";

}
