namespace Models.Common;

public sealed class EntityName<TEntity>
{
    public string Value { get; }

    public EntityName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("");
        }

        Value = name;
    }
}
