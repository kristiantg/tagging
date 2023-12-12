namespace Domain.Common;

public sealed class LaunchDate()
{
    public LaunchDate(DateTime value) : this()
    {
        Value = value;
    }
    
    public DateTime Value { get; }
}