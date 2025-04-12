using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Queries.GetTags
{
    public record GetTagsQuery() : IRequest<GetTagsResponseProto>, IBaseRequest
    {
    }
}
