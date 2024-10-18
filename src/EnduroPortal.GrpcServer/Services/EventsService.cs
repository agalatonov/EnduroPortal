using EnduroPortal.GrpcServer.Utils;
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
        var events = await _dbContext.Events.Where(e => e.Date > new DateTime(request.Year, 1, 1)).ToListAsync();
        var response = GrpcConversions.GetEventsResponse(events);

        return response;
    }

    public override async Task<GetEventResponse> GetEvent(GetEventRequest request, ServerCallContext context)
    {
        var response = new GetEventResponse();

        var result = await _dbContext.Events.FirstOrDefaultAsync(e => e.Slug.ToLower() == request.Slug.ToLower());
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

        if (!_dbContext.Events.Any(e => e.Slug == request.Slug))
        {
            var dbEvent = GrpcConversions.GetEvent(request);

            await _dbContext.Events.AddAsync(dbEvent);
            _dbContext.SaveChanges();

            GrpcConversions.GetEventResponse(dbEvent, ref response);
        }
        else
        {
            response.Result = "Event by slug is already exist";
        }

        return response;
    }

    public override async Task<RemoveEventResponse> RemoveEvent(RemoveEventRequest request, ServerCallContext context)
    {
        var response = new RemoveEventResponse();

        var rowAffected = await _dbContext.Events
                      .Where(e => string.Equals(e.Slug, request.Slug, StringComparison.InvariantCultureIgnoreCase))
                      .ExecuteDeleteAsync();

        response.Result = rowAffected == 0 ? "" : $"Event with slug '{request.Slug}' is not exist";

        return response;
    }
}

