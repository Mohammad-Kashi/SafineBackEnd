using Grpc.Core;
using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Queries.GetLocationsBySearchArg
{
    public record GetLocationsBySearchArgQuery(string SearchText, IEnumerable<string> BusinessTags, bool IsOpen) : IRequest<LocationMessages>, IBaseRequest
    {
        public GetLocationsBySearchArgQuery(SearchArg item, Metadata requestHeader) : this(item.SearchText, item.BusinessTags, item.IsOpen)
        {
            
        }
    }
}
