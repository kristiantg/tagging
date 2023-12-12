namespace Application.Contracts.Requests;

public record CreateChannelRequest(string Title, DateTime LaunchDate, IEnumerable<string> Tags);