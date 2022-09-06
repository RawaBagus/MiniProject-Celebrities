using Celebrity.Data.Interface.Repositories;
using Celebrity.Service.Interface.Services;
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
            var result = await celebrityRepository.CreateNewCelebrity(name, date, address, movies);
            return result;
        }

        public async Task<bool> DeleteByCelebrityId(int id)
        {
            var result = await celebrityRepository.DeleteByCelebrityId(id);
            return result;
        }
    }
}
