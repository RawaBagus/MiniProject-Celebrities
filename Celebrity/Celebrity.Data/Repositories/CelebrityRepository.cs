using Celebrity.Data.Interface.Repositories;
using Celebrity.Service.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Data.Repositories
{
    public class CelebrityRepository : ICelebrityRepository
    {
        private readonly IDbService _dbService;
        public CelebrityRepository(IDbService dbService)
        {
            _dbService = dbService;
        }
        public async Task<bool> CreateNewCelebrity(string name, string date, string address, string[] movies)
        {
            if (await IsAddressThere(address)==false)
            {
                await CreateNewAddress(address);
            }
            if(await IsCelebrityThere(name) == false)
            {
                return false;
            }
            int TownId = await _dbService.Get<int>("select Id from Towns where Name=@Name", new {Name = address});
            await _dbService.ModifyData("insert into Celebrities(Name, Date_Of_Birth, IdTown" +
                ") values(@Name, @Date_Of_Birth, @IdTown)", new {Name=name,Date_Of_Birth=date,IdTown=TownId});
            int CelebrityId = await _dbService.Get<int>("select Id from Celebrities where Name=@Name", new { Name = name });
            foreach(string x in movies)
            {
                if (await IsMovieThere(x)==false)
                {
                    await CreateNewMovie(x);
                }
                int MovieId = await _dbService.Get<int>("select Id from Movies where Name=@Name", new { Name = x });
                await _dbService.ModifyData("insert into MoviesAndCelebrities values(@IdCelebrity,@IdMovie)", new { IdCelebrity = CelebrityId, IdMovie = MovieId });
            }
            return true;
        }
        public async Task<bool> DeleteByCelebrityId(int id)
        {
            await _dbService.ModifyData("Delete from MoviesAndCelebrities where IdCelebrity=@Id", new {Id=id});
            await _dbService.ModifyData("Delete from Celebrities where Id=@Id;", new { Id = id });
            return true;
        }

        public async Task<bool> CreateNewMovie(string name)
        {
            await _dbService.ModifyData("insert into Movies(Name) values(@Name)", new { Name = name });
            return true;
        }
        public async Task<bool> CreateNewAddress(string name)
        {
            await _dbService.ModifyData("insert into Towns(Name) values(@Name)", new { Name = name });
            return true;
        }
        public async Task<bool> IsMovieThere(string nama)
        {
            var exists = await _dbService.Check("select count(1) from Movies where Name=@Name", new { name = nama });
            return exists;
        }
        public async Task<bool> IsAddressThere(string nama)
        {
            var exists = await _dbService.Check("select count(1) from Towns where Name=@name", new { name = nama });
            return exists;
        }
        public async Task<bool> IsCelebrityThere(string nama)
        {
            var exists = await _dbService.Check("select count(1) from Celebrities where Name=@name", new { name = nama });
            return exists;
        }

        
    }
}

