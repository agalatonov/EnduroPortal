using EnduroPortal.GrpcServer.Utils;
using Grpc.Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EnduroPortal.GrpcServer.Services
{
    public class ParticipiantService : Participiant.ParticipiantBase
    {
        private readonly EnduroPortalDBContext _dbContext;

        public ParticipiantService(EnduroPortalDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public override async Task<AddParticipiantResponse> AddParticipiant(AddParticipiantRequest request, ServerCallContext context)
        {
            var response = new AddParticipiantResponse();

            if (!_dbContext.Participiants.Any(p => string.Equals(p.Email, request.Email, StringComparison.InvariantCultureIgnoreCase)))
            {
                var participiant = GrpcConversions.GetParticipiant(request);

                await _dbContext.Participiants.AddAsync(participiant);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                response.Result = $"Email should be unique. Participant with email '{request.Email}' is already registred";
            }

            return response;
        }

        public override async Task<RemovePatricipianResponse> RemoveParticipiant(RemovePatricipianRequest request, ServerCallContext context)
        {
            var rowAffected = await _dbContext.Participiants
                .Where(p => string.Equals(p.Email, request.Email, StringComparison.InvariantCultureIgnoreCase))
                .ExecuteDeleteAsync();

            var response = new RemovePatricipianResponse();
            response.Result = rowAffected == 0 ? "" : $"participiant with email '{request.Email}' is not registred";

            return response;
        }

        public override Task<GetParticipiantsResponse> GetParticipiants(GetParticipiantsRequest request, ServerCallContext context)
        {
            return base.GetParticipiants(request, context);
        }
    }
}
