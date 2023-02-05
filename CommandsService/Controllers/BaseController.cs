using CommandsService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly ICommandService _commandService;

        public BaseController(ICommandService commandService)
        {
            _commandService = commandService;
        }
    } 
}