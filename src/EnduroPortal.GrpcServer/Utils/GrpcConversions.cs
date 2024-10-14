using Google.Protobuf.WellKnownTypes;
using Infrastructure.Models;

namespace EnduroPortal.GrpcServer.Utils
{
    public class GrpcConversions
    {
        internal static Event Convert(AddEventRequest addEventRequest)
        {
            var result = new Event{
                Name = addEventRequest.Name,
                Description = addEventRequest.Description,
                Slug = addEventRequest.Slug,
                Date = addEventRequest.Date.ToDateTime(),
                Location = addEventRequest.Location
            };

            return result;
        }

        internal static void GetEventResponse(Event eventdb, ref GetEventResponse response)
        {
            response.Name = eventdb.Name;
            response.Description = eventdb.Description;
            response.Slug = eventdb.Slug;
            response.Date = eventdb.Date.ToTimestamp();
            response.Location = eventdb.Location;
        }

        internal static GetEventResponse ConvertToGrpcEvent(List<Event> events)
        {
            foreach (var e in events)
            {

            }
        }
    }
}
