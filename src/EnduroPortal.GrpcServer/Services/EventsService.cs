using EnduroPortal.GrpcServer.Utils;
using Grpc.Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EnduroPortal.GrpcServer.Services;

public class EventsService : Events.EventsBase
{
    private readonly ILogger<EventsService> _logger;
    private readonly EnduroPortalDBContext _dbContext;
    private readonly IGrpcConversions _grpcConversions;

    public EventsService(ILogger<EventsService> logger, EnduroPortalDBContext eventDBContext, IGrpcConversions grpcConversions)
    {
        _logger = logger;
        _dbContext = eventDBContext;
        _grpcConversions = grpcConversions;
    }

    public override async Task<GetEventsResponse> GetEvents(GetEventsRequest request, ServerCallContext context)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation($"EnduroPortal.GrpcServer.EventsService.GetEvents(): Get events of '{request.Year}' year");
        }

        var events = await _dbContext.Events.Where(e =>
            e.Date > new DateTime(request.Year, 1, 1).ToUniversalTime() &&
            e.Date < new DateTime(request.Year + 1, 1, 1).ToUniversalTime())
            .ToListAsync();
        var response = _grpcConversions.GetEventsResponse(events);

        return response;
    }

    public override async Task<GetEventResponse> GetEvent(GetEventRequest request, ServerCallContext context)
    {
        var response = new GetEventResponse();

        var result = await _dbContext.Events.FirstOrDefaultAsync(e => e.Slug.ToLower() == request.Slug.ToLower());
        if (result != null)
        {
            _grpcConversions.GetEventResponse(result, ref response);
        }
        else
        {
            response.Result = $"Event with slug '{request.Slug}' wasn't found";
        }

        return response;
    }

    public override async Task<AddEventResponse> AddEvent(AddEventRequest request, ServerCallContext context)
    {
        var response = new AddEventResponse();

        if (!_dbContext.Events.Any(e => e.Slug.ToLower() == request.Slug.ToLower()))
        {
            var dbEvent = _grpcConversions.GetEvent(request);

            await _dbContext.Events.AddAsync(dbEvent);
            _dbContext.SaveChanges();

            _grpcConversions.GetAddEventResponse(dbEvent, ref response);

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation($"EnduroPortal.GrpcServer.EventsService.AddEvent(): Event with sulg '{request.Slug}' was added to db");
            }
        }
        else
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError($"GrpcServer.Services.AddEvent: Event with sulg '{request.Slug}' is already exist. Event isn't added to db");
            }
            response.Result = $"Event with sulg '{request.Slug}' is already exist. Event isn't added to db";
        }

        return response;
    }

    public override async Task<DeleteEventResponse> DeleteEvent(DeleteEventRequest request, ServerCallContext context)
    {
        var response = new DeleteEventResponse();

        var rowAffected = await _dbContext.Events
                      .Where(e => e.Slug.ToLower() == request.Slug.ToLower())
                      .ExecuteDeleteAsync();

        if (rowAffected == 0)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning($"EnduroPortal.GrpcServer.EventsService.RemoveEvent(): Event with slug '{request.Slug}' was't found.");
            }

            response.Result = $"Event with slug '{request.Slug}' wasn't found";
        }

        return response;
    }

    public override async Task<UpdateEventResponse> UpdateEvent(UpdateEventRequest request, ServerCallContext context)
    {
        var response = new UpdateEventResponse();

        var @event = await _dbContext.Events.FirstOrDefaultAsync(e => e.Slug.ToLower() == request.Slug.ToLower());

        if (@event is null)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError($"EnduroPortal.GrpcServer.EventsService.UpdateEvent(): Event with sulg '{request.Slug}' is already exist. Event isn't added to db");
            }

            response.Result = $"Event with slug '{request.Slug}' wasn't found";
        }
        else
        {
            _grpcConversions.UpdateEvent(ref @event, request);

            _dbContext.SaveChanges();

            _grpcConversions.UpdateEventResponse(@event, ref response);

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation($"EnduroPortal.GrpcServer.EventsService.UpdateEvent(): Event with sulg '{request.Slug}' updated successfully");
            }
        }

        return response;
    }
}

