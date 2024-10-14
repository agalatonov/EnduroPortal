using EnduroPortal.GrpcServer.Utils;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EnduroPortal.GrpcServer.Services;

public class EventsService : Events.EventsBase
{
    private readonly ILogger<EventsService> _logger;
    private readonly EnduroPortalDBContext _dbContext;
    public EventsService(ILogger<EventsService> logger, EnduroPortalDBContext eventDBContext)
    {
        _logger = logger;
        _dbContext = eventDBContext;
    }

    public override async Task<GetEventsResponse> GetEvents(GetEventsRequest request, ServerCallContext context)
    {
        //var response = new GetEventsResponse();
        //response.Events.Add(new EventSummaryBrief
        //{
        //    Name = "test",
        //    Date = DateTime.UtcNow.ToTimestamp(),
        //    Slug = "test",
        //    Description = "desc",
        //    Location = "NN"

        //});


        var events = await _dbContext.Events.ToListAsync();

        var response = new GetEventsResponse();
        foreach (var e in events)
        {
            response.Events.Add(GrpcConversions.Convert(e));
        }

        return (response);
    }

    public override async Task<GetEventResponse> GetEvent(GetEventRequest request, ServerCallContext context)
    {
        var response = new GetEventResponse();

        var result = await _dbContext.Events.FirstOrDefaultAsync(x => x.Slug == request.Slug);
        if (result != null)
        {
            GrpcConversions.GetEventResponse(result, ref response);
        }
        else
        {
            response.Result = "Event by slug was't found";
        }

        return response;
    }

    public override async Task<AddEventResponse> AddEvent(AddEventRequest request, ServerCallContext context)
    {
        var response = new AddEventResponse();

        var result = await _dbContext.Events.FirstOrDefaultAsync(x => x.Slug == request.Slug);
        if (result != null)
        {
            response.Result = "Event by slug was't found";
        }
        else
        {
            _dbContext.Events.Add(GrpcConversions.Convert(request));
        }

        return response;
    }

    public override async Task<RemoveEventResponse> RemoveEvent(RemoveEventRequest request, ServerCallContext context)
    {
        var response = new RemoveEventResponse();

        var result = await _dbContext.Events.FirstOrDefaultAsync(x => x.Slug == request.Slug);
        if (result != null)
        {
            _dbContext.Events.Remove(result);
        }
        else
        {
            response.Result = "Event by slug was't found";
        }

        return response;
    }
}

