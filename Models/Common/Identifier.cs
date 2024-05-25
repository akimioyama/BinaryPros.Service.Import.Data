using Models.Entity;

namespace Models.Common;

public sealed record Identifier<TEntity>
    where TEntity : FeedEntity
{
    public string Value { get; }

    public Identifier(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Value string can not be null, empty, " +
                "or consists only of white-space characters.",
                nameof(value));
        }

        Value = value;
    }
}
