using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EnduroPortal.GrpcServer.Services;

public class EventsService : Events.EventsBase
{
    private readonly ILogger<EventsService> _logger;
    private readonly EventDBContext _dbContext;
    public EventsService(ILogger<EventsService> logger, EventDBContext eventDBContext)
    {
        _logger = logger;
        _dbContext = eventDBContext;
    }

    public override async Task<EventsResponse> GetEvents(EventsRequest request, ServerCallContext context)
    {
        var response = new EventsResponse();
        response.Events.Add(new EventSummaryBrief
        {
            Name = "test",
            Date = DateTime.UtcNow.ToTimestamp(),
            Slug = "test"
            Description = "desc",
            Location = "NN"

        });


        var res = _dbContext.Events.GetAsyncEnumerator();

        return (response);
    }

    public override Task<EventResponse> GetEvent(EventRequest request, ServerCallContext context)
    {
        var result = _dbContext.Events.FirstOrDefaultAsync(x => x.Slug == request.Slug);



        return base.GetEvent(request, context);
    }


}

