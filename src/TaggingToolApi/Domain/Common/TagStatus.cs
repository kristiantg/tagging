namespace Domain.Common;

public sealed class TagStatus()
{
    public TagStatus(int value) : this()
    {
        Value = value;
    }
    
    public int Value { get; }
}