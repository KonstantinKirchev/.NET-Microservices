using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.UnitOfWork;

namespace PlatformService.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IPlatformData _data;
        protected readonly IMapper _mapper;

        public BaseController(IPlatformData data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
    }
}