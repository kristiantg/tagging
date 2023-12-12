namespace Domain.Common;

public sealed class Created()
{
    public DateTime Value { get; }

    public Created(DateTime value) : this()
    {
        Value = value;
    }
}