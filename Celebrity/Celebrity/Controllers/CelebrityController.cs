using Celebrity.Model.Entities.SubEntities;
using Celebrity.Service.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Celebrity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CelebrityController : Controller
    {
        private readonly ICelebrityService celebrityService;
        public CelebrityController(ICelebrityService celebrityService)
        {
            this.celebrityService = celebrityService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CelebrityData celeb)
        {
            var result = await celebrityService.CreateNewCelebrity(celeb.Name,celeb.Date_Of_Birth,celeb.Town,celeb.Movies.ToArray());
            return Ok(result);
        }
    }
}
