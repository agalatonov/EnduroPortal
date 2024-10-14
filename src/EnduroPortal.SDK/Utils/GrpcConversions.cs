using Domain.Models;
using EnduroPortal.GrpcServer;

namespace EnduroPortal.SDK.Utils
{
    public class GrpcConversions
    {
        internal static List<Event> Convert(GetEventsResponse grpcResponse)
        {
            List<Event> result = new List<Event>();

            foreach (var i in grpcResponse.Events)
            {
                result.Add(new Event
                {
                    Name = i.Name,
                    Date = i.Date.ToDateTime(),
                    Slug = i.
                    Description =
                            i.Description,
                    Location = i.Location
                });
            }

            return result;
        }
    }
}
