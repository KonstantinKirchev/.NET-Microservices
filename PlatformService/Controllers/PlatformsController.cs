using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.UnitOfWork;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : BaseController
    {

        public PlatformsController(IPlatformData data, IMapper mapper)
            : base(data, mapper)
        {
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
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = _data.Platforms.Add(_mapper.Map<Platform>(platformCreateDto));

            if (platform == null) 
                return BadRequest();

            _data.SaveChanges();

            var result = _mapper.Map<PlatformReadDto>(platform);
            
            return CreatedAtRoute(nameof(GetPlatform), new { Id = result.Id }, result);
        }
    }
}