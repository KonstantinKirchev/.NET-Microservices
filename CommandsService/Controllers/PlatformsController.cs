using CommandsService.Dtos;
using CommandsService.Models;
using CommandsService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : BaseController
    {
        public PlatformsController(ICommandService commandService) 
            : base(commandService)
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms() 
        {
            return Ok(_commandService.GetAllPlatforms());
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");
            return Ok("Inbound test from Platforms Controller");
        }
    }
}