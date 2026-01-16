namespace Restaurant.Payment.Domain;

public class OrderItem : IComparable<OrderItem>
{

    public string Nome { get; private set; }

    public string Tipo { get; private set; }

    public decimal Preco { get; private set; }


    public OrderItem(string nome, string tipo, decimal preco)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(nome)
            .IsNotNullOrWhiteSpace(tipo)
            .GreaterThanZero(preco)
            .Validate();

        Nome = nome;
        Tipo = tipo;
        Preco = preco;
    }


    public int CompareTo(OrderItem? other)
    {
        if (other == null) return 1;

        var result = Tipo.CompareTo(other.Tipo, StringComparison.OrdinalIgnoreCase);
        if (result != 0) return result;

        result = Nome.CompareTo(other.Nome, StringComparison.OrdinalIgnoreCase);
        if (result != 0) return result;

        return result;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is OrderItem other && Equals(other));
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Nome, Tipo);
    }

    public static bool operator ==(OrderItem? left, OrderItem? right)
    {
        return ReferenceEquals(left, right) || (left is not null && left.Equals(right));
    }

    public static bool operator !=(OrderItem? left, OrderItem? right)
    {
        return !(left == right);
    }

    public static bool operator <(OrderItem? left, OrderItem? right)
    {
        return left == null || left.CompareTo(right) < 0;
    }

    public static bool operator <=(OrderItem? left, OrderItem? right)
    {
        return left == null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(OrderItem? left, OrderItem? right)
    {
        return left == null || left.CompareTo(right) > 0;
    }

    public static bool operator >=(OrderItem? left, OrderItem? right)
    {
        return left == null || left.CompareTo(right) >= 0;
    }

}
