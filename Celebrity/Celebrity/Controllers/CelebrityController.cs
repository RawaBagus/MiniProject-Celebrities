using Celebrity.Model.Entities.SubEntities;
using Celebrity.Service.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCelebrityById(int id) 
        {
            var result = await celebrityService.DeleteByCelebrityId(id);
            return Ok(result);
        }
        [HttpGet("{num:int}")]
        public async Task<List<CelebrityData>> GetAllData(int num=1)
        {
            var result = await celebrityService.GetAllData(num);
            return result;
        }
        [HttpGet("{search}")]
        public async Task<List<CelebrityData>> GetDataByMovie(string search)
        {
            var result = await celebrityService.GetDataByMovie(search);
            return result;
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody]CelebrityDataUpdate? data,int id)
        {
            var result = await celebrityService.UpdateByCelebrityId(data, id);
            return Ok(result);
        }

    }
}
