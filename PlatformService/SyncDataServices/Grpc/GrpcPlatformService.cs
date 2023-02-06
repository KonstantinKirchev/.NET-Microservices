using AutoMapper;
using Grpc.Core;
using PlatformService.Data.UnitOfWork;

namespace PlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformData _data;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IPlatformData data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = _data.Platforms.All().ToList();

            foreach(var plat in platforms)
            {
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(plat));
            }

            return Task.FromResult(response);
        }
    }
}