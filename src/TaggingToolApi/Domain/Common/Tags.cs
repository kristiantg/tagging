namespace Domain.Common;

public sealed class Tags
{
    public IEnumerable<string> Value { get; }

    public Tags(IEnumerable<string> value)
    {
        Value = value;
    }
}