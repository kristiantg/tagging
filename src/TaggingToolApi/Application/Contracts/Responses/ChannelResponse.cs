namespace Application.Contracts.Responses;

public record ChannelResponse(string Title, DateTime LaunchDate, IEnumerable<string> Tags);