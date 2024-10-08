using Grpc.Core;

namespace EnduroPortal.GrpcServer.Services;

public class EventsService: Events.EventsBase
{
    private readonly ILogger<EventsService> _logger;
    public EventsService(ILogger<EventsService> logger)
    {
        _logger = logger;
    }

    public override Task<EventsResponse> GetEvents(EventsRequest request, ServerCallContext context)
    {
        return base.GetEvents(request, context);
    }

    public override Task<EventResponse> GetEvent(EventRequest request, ServerCallContext context)
    {
        return base.GetEvent(request, context);
    }


}

