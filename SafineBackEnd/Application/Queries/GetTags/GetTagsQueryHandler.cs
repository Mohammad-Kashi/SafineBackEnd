using Domain.IRepository;
using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Queries.GetTags
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, GetTagsResponseProto>
    {
        private readonly IQueryRepository _queryRepository;
        public GetTagsQueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<GetTagsResponseProto> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _queryRepository.GetTags();
            var result = new GetTagsResponseProto();
            result.TagName.AddRange(tags);
            return result;
        }
    }
}
