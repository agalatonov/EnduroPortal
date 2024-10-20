using EnduroPortal.GrpcServer.Utils;
using Grpc.Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EnduroPortal.GrpcServer.Services
{
    public class ParticipiantService : Participiant.ParticipiantBase
    {
        private readonly EnduroPortalDBContext _dbContext;
        private readonly ILogger<ParticipiantService> _logger;

        public ParticipiantService(EnduroPortalDBContext dBContext, ILogger<ParticipiantService> logger)
        {
            _dbContext = dBContext;
            _logger = logger;
        }

        public override async Task<AddParticipiantResponse> AddParticipiant(AddParticipiantRequest request, ServerCallContext context)
        {
            var response = new AddParticipiantResponse();

            if (!_dbContext.Participiants
                .Any(p => string.Equals(p.Email, request.Email, StringComparison.InvariantCultureIgnoreCase)))
            {
                var participiant = GrpcConversions.GetParticipiant(request);

                await _dbContext.Participiants.AddAsync(participiant);
                _dbContext.SaveChanges();

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation($"GrpcServer.Services.AddParticipiant: Participant with email '{request.Email}' was added to db");
                }
            }
            else
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                {
                    _logger.LogWarning($"GrpcServer.Services.AddParticipiant: Participiant with email '{request.Email}' is already registred.");
                }

                response.Result = $"Email should be unique. Participant with email '{request.Email}' is already registred";
            }

            return response;
        }

        public override async Task<RemovePatricipianResponse> RemoveParticipiant(RemovePatricipianRequest request, ServerCallContext context)
        {
            var rowAffected = await _dbContext.Participiants
                .Where(p => p.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase) &&
                        p.EventSlug.Equals(request.EventSlug, StringComparison.InvariantCultureIgnoreCase))
                .ExecuteDeleteAsync();

            var response = new RemovePatricipianResponse();
            if (rowAffected == 0)
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                {
                    _logger.LogWarning($"GrpcServer.Services.RemoveParticipiant: Participiant with email '{request.Email}' isn't registred.");
                }

                response.Result = $"Participiant with email '{request.Email}' is not registred";
            }
            else
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation($"GrpcServer.Services.RemoveParticipiant: Participiant with email '{request.Email}' was deleted from event");
                }
            }

            return response;
        }

        public override async Task<GetParticipiantsResponse> GetParticipiants(GetParticipiantsRequest request, ServerCallContext context)
        {
            var participiants = await _dbContext.Participiants
                .Where(p => string.Equals(p.EventSlug, request.EventSlug, StringComparison.InvariantCultureIgnoreCase))
                .ToListAsync();

            var response = GrpcConversions.GetParticipiantsResponse(participiants);

            return response;
        }
    }
}
