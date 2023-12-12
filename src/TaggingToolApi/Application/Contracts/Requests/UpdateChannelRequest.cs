namespace Application.Contracts.Requests;

public record UpdateChannelRequest(IEnumerable<string> Tags, string Title, DateTime LaunchDate);