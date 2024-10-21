using EnduroPortal.GrpcServer.Utils;
using Grpc.Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EnduroPortal.GrpcServer.Services
{
    public class ParticipiantService : Participiants.ParticipiantsBase
    {
        private readonly EnduroPortalDBContext _dbContext;
        private readonly ILogger<ParticipiantService> _logger;
        private readonly IGrpcConversions _grpcConversions;

        public ParticipiantService(EnduroPortalDBContext dBContext, ILogger<ParticipiantService> logger, IGrpcConversions grpcConversions)
        {
            _dbContext = dBContext;
            _logger = logger;
            _grpcConversions = grpcConversions;
        }

        public override async Task<AddParticipiantResponse> AddParticipiant(AddParticipiantRequest request, ServerCallContext context)
        {
            var response = new AddParticipiantResponse();

            if (!_dbContext.Participiants
                .Any(p => p.Email.ToLower() == request.Email.ToLower()))
            {
                var participiant = _grpcConversions.GetParticipiant(request);

                await _dbContext.Participiants.AddAsync(participiant);
                _dbContext.SaveChanges();

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation($"EnduroPortal.GrpcServer.ParticipiantService.AddParticipiant(): Participant with email '{request.Email}' was added to db");
                }
            }
            else
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                {
                    _logger.LogWarning($"EnduroPortal.GrpcServer.ParticipiantService.AddParticipiant: Participiant with email '{request.Email}' is already registred.");
                }

                response.Result = $"Email should be unique. Participant with email '{request.Email}' is already registred";
            }

            return response;
        }

        public override async Task<RemovePatricipianResponse> RemoveParticipiant(RemovePatricipianRequest request, ServerCallContext context)
        {
            var rowAffected = await _dbContext.Participiants
                .Where(p => p.Email.ToLower() == request.Email.ToLower() &&
                        p.EventSlug.ToLower() == request.EventSlug.ToLower())
                .ExecuteDeleteAsync();

            var response = new RemovePatricipianResponse();
            if (rowAffected == 0)
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                {
                    _logger.LogWarning($"EnduroPortal.GrpcServer.ParticipiantService.RemoveParticipiant: Participiant with email '{request.Email}' isn't registred.");
                }

                response.Result = $"Participiant with email '{request.Email}' is not registred";
            }
            else
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation($"EnduroPortal.GrpcServer.ParticipiantService.RemoveParticipiant: Participiant with email '{request.Email}' was deleted from event");
                }
            }

            return response;
        }

        public override async Task<GetParticipiantsResponse> GetParticipiants(GetParticipiantsRequest request, ServerCallContext context)
        {
            var participiants = await _dbContext.Participiants
                .Where(p => p.EventSlug.ToLower() == request.EventSlug.ToLower())
                .ToListAsync();

            var response = _grpcConversions.GetParticipiantsResponse(participiants);

            return response;
        }
    }
}
