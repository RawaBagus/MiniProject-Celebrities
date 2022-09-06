using Celebrity.Data.Interface.Repositories;
using Celebrity.Model.Entities.SubEntities;
using Celebrity.Service.Interface.Services;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Service.Services
{
    public class CelebrityService:ICelebrityService
    {
        private readonly ICelebrityRepository celebrityRepository;
        public CelebrityService(ICelebrityRepository celebrityRepository)
        {
            this.celebrityRepository = celebrityRepository;
        }

        public async Task<bool> CreateNewCelebrity(string name, string date, string address, string[] movies)
        {
            if (await celebrityRepository.IsAddressThere(address) == false)
            {
                await celebrityRepository.CreateNewAddress(address);
            }
            if (await celebrityRepository.IsCelebrityThere(name) == true)
            {
                return false;
            }
            int TownId = await celebrityRepository.GetIdByName("Towns", address);
            var result = await celebrityRepository.CreateNewCelebrity(name, date, TownId);
            int CelebrityId = await celebrityRepository.GetIdByName("Celebrities", name);
            foreach(string x in movies)
            {
                if (await celebrityRepository.IsMovieThere(x) == false)
                {
                    await celebrityRepository.CreateNewMovie(x);
                }
                int MovieId = await celebrityRepository.GetIdByName("Movies",x);
                await celebrityRepository.RelateMovieWithCelebrity(CelebrityId,MovieId);
            }
            return result;
        }
        public async Task<List<CelebrityData>> GetAllData()
        {
            var result = await celebrityRepository.GetAllData();
            return result;
        }
        public async Task<bool> UpdateByCelebrityId(CelebrityData data, int id)
        {
            int TownId = await celebrityRepository.GetIdByName("Towns", data.Town);
            var result = await celebrityRepository.UpdateCelebrityById(id, data.Name, data.Date_Of_Birth, TownId);
            foreach(string x in data.Movies)
            {
                if(await celebrityRepository.IsMovieThere(x) == false)
                {
                    await celebrityRepository.CreateNewMovie(x);
                }
                int MovieId = await celebrityRepository.GetIdByName("Movies", x);
                if(await celebrityRepository.IsRelationThere(id, MovieId))
                {
                    continue;
                }
                else
                {
                    await celebrityRepository.RelateMovieWithCelebrity(id, MovieId);
                }
            }
            return true;
        }
        public async Task<bool> DeleteByCelebrityId(int id)
        {
            var result = await celebrityRepository.DeleteByCelebrityId(id);
            return result;
        }

        
    }
}
