using Celebrity.Model.Entities.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Data.Interface.Repositories
{
    public interface ICelebrityRepository
    {
        public Task<bool> CreateNewCelebrity(string name, string date, int TownId);
        public Task<List<CelebrityData>> GetAllData(int page);
        public Task<bool> DeleteByCelebrityId(int id);
        public Task<List<CelebrityData>> GetDataByMovie(string Movie);
        public Task<bool> UpdateCelebrityById(int id,string name, string date,int TownId);
        public Task<bool> CreateNewMovie(string name);
        public Task<bool> CreateNewAddress(string name);
        public Task<bool> IsMovieThere(string name);
        public Task<bool> IsAddressThere(string name);
        public Task<bool> IsCelebrityThere(string name);
        public Task<bool> IsRelationThere(int IdCelebrity, int IdMovie);
        public Task<int> GetIdByName(string variableName, string nama);
        public Task<bool> RelateMovieWithCelebrity(int CelebrityId, int MovieId);
        public Task<List<string>> GetAllMovies(int id);
    }
}
