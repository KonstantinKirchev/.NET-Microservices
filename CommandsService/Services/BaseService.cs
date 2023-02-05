using AutoMapper;
using CommandsService.Data.UnitOfWork;

namespace CommandsService.Services
{
    public abstract class BaseService
    {
        protected readonly ICommandsData _data;
        protected readonly IMapper _mapper;

        public BaseService(ICommandsData data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
    }
}