namespace Domain.Common;

public sealed class Name()
{
    public Name(string? value) : this()
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        Value = value;
    }
    
    public string? Value { get; }
}