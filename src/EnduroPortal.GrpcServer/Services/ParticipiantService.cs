using Grpc.Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EnduroPortal.GrpcServer.Services
{
    public class ParticipiantService: Participiant.ParticipiantBase
    {
        private readonly EnduroPortalDBContext _dbContext;

        public ParticipiantService(EnduroPortalDBContext dBContext) 
        {
            _dbContext = dBContext;
        }

        public override Task<AddParticipiantResponse> AddParticipiant(AddParticipiantRequest request, ServerCallContext context)
        {
            var response = new AddParticipiantResponse();

            if(!_dbContext.Participiants.Any(p => p.Email == request.Email)
            {
                var 

                _dbContext.Participiants.Add();
            }
            else
            {

            }

            return Task.FromResult(response);
        }

        public override Task<RemovePatricipianResponse> RemoveParticipiant(RemovePatricipianRequest request, ServerCallContext context)
        {
            return base.RemoveParticipiant(request, context);
        }

        public override Task<GetParticipiantsResponse> GetParticipiants(GetParticipiantsRequest request, ServerCallContext context)
        {
            return base.GetParticipiants(request, context);
        }
    }
}
