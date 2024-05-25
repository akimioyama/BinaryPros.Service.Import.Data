namespace Models.Common;

public sealed class Importance<TEntity>
{
    public decimal Value { get; }

    public Importance(decimal value)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, "value");

        Value = value;
    }
}
