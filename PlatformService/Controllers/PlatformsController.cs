using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.UnitOfWork;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataservices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : BaseController
    {
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(IPlatformData data, IMapper mapper, ICommandDataClient commandDataClient)
            : base(data, mapper)
        {
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            var platforms = _data.Platforms.All().ToList();
            var result = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);

            return Ok(result);
        }

        [HttpGet("{id:int}", Name="GetPlatform")]
        public ActionResult<PlatformReadDto> GetPlatform(int id)
        {
            var platform = _data.Platforms.Find(id);
            
            if (platform == null)
                return NotFound();
            
            var result = _mapper.Map<PlatformReadDto>(platform);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = _data.Platforms.Add(_mapper.Map<Platform>(platformCreateDto));

            if (platform == null) 
                return BadRequest();

            _data.SaveChanges();

            var result = _mapper.Map<PlatformReadDto>(platform);

            try
            {
                await _commandDataClient.SendPlatformToCommand(result);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }
            
            return CreatedAtRoute(nameof(GetPlatform), new { Id = result.Id }, result);
        }
    }
}