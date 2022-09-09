using Celebrity.Data.Interface.Repositories;
using Celebrity.Model.Entities.SubEntities;
using Celebrity.Service.Interface.Services;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Cmp;
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
        public async Task<List<CelebrityDataShow>> GetAllData(int num)
        {
            int Page = (num-1) * 10;
            if (Page < 0)
            {
                Page = 0;
            }
            var result = await celebrityRepository.GetAllData(Page);
            foreach(var x in result)
            {
                x.Movies = await celebrityRepository.GetAllMovies(await celebrityRepository.GetIdByName("Celebrities", x.Name));
            }
            return result;
        }
        public async Task<List<CelebrityDataShow>> GetDataByMovie(string Movie,int num)
        {
            int page = (num-1) * 10;
            Movie = "%" + Movie + "%";
            var result = await celebrityRepository.GetDataByMovie(Movie, page);
            foreach (var x in result)
            {
                x.Movies = await celebrityRepository.GetAllMovies(await celebrityRepository.GetIdByName("Celebrities", x.Name));
            }
            return result;
        }
        public async Task<bool> UpdateByCelebrityId(CelebrityDataUpdate data, int id)
        {

            if (await celebrityRepository.IsAddressThere(data.Town) == false)
            {
                await celebrityRepository.CreateNewAddress(data.Town);
            }
            int TownId = await celebrityRepository.GetIdByName("Towns", data.Town);
            var result = await celebrityRepository.UpdateCelebrityById(id, data.Name, data.Date_Of_Birth, TownId);
            foreach(string x in data.MovieAdd)
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
            foreach(string x in data.MovieDelete)
            {
                if(await celebrityRepository.IsMovieThere(x))
                {
                    int MovieId = await celebrityRepository.GetIdByName("Movies", x);
                    await celebrityRepository.DeleteRelationById(id,MovieId);
                }
            }
            return true;
        }
        public async Task<bool> DeleteByCelebrityId(int id)
        {
            await celebrityRepository.DeleteRelationById(id);
            var result = await celebrityRepository.DeleteByCelebrityId(id);
            return result;
        }

        
    }
}
