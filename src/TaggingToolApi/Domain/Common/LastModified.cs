namespace Domain.Common;

public sealed class LastModified()
{
    public DateTime Value { get; } = DateTime.Now;

    public LastModified(DateTime value) : this()
    {
        Value = value;
    }
}