using Celebrity.Data.Interface.Repositories;
using Celebrity.Model.Entities.SubEntities;
using Celebrity.Service.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Celebrity.Data.Repositories
{
    public class CelebrityRepository : ICelebrityRepository
    {
        private readonly IDbService _dbService;
        public CelebrityRepository(IDbService dbService)
        {
            _dbService = dbService;
        }
        public async Task<bool> CreateNewCelebrity(string name, string date, int TownId)
        {
            await _dbService.ModifyData("insert into Celebrities(Name, Date_Of_Birth, IdTown" +
                ") values(@Name, @Date_Of_Birth, @IdTown)", new {Name=name,Date_Of_Birth=date,IdTown=TownId});
            return true;
        }
        public async Task<List<CelebrityData>> GetAllData()
        {
            var result = await _dbService.GetList<CelebrityData>("select Celebrities.Name, " +
                "Celebrities.Date_Of_Birth, " +
                "(Select Towns.Name from Towns where Towns.Id=Celebrities.IdTown) as Town " +
                "from Celebrities " +
                "group by Celebrities.Id " +
                "order by Celebrities.Id asc " +
                "limit 10",new { });
            return result;
        }
        public async Task<List<CelebrityData>> GetDataByMovie(string Movie)
        {
            string x = "%";
            x = x + Movie + x;
            var result = await _dbService.GetList<CelebrityData>("select Celebrities.Name, " +
                "Celebrities.Date_Of_Birth, " +
                "(Select Towns.Name from Towns where Towns.Id=Celebrities.IdTown) as Town " +
                "from Celebrities " +
                "left outer join MoviesAndCelebrities on Celebrities.Id=MoviesAndCelebrities.IdCelebrity " +
                "left outer join Movies on Movies.Id=MoviesAndCelebrities.IdMovie " +
                "where Movies.Name like @Name " +
                "group by Celebrities.Id " +
                "order by Celebrities.Id asc " +
                "limit 10", new { Name= x});
            return result;
        }

        public async Task<bool> UpdateCelebrityById(int id, string name, string date, int TownId)
        {
            await _dbService.ModifyData("update Celebrities " +
                "set Name=@name, Date_Of_Birth=@date, IdTown=@TownId " +
                "where Id=@id", new {name=name,date=date,TownId=TownId,id=id});
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
        public async Task<bool> IsRelationThere(int IdCel,int IdMov)
        {
            var exists = await _dbService.Check("select count(1) from MoviesAndCelebrities where IdCelebrity=@IdCelebrity and IdMovie=@IdMovie", new { IdCelebrity = IdCel,IdMovie=IdMov });
            return exists;
        }
        public async Task<int> GetIdByName(string variableName, string nama)
        {
            int id = await _dbService.Get<int>("select Id from "+variableName+" where Name=@Name", new { Name = nama });
            return id;
        }
        public async Task<bool> RelateMovieWithCelebrity(int CelebrityId, int MovieId) {

            await _dbService.ModifyData("insert into MoviesAndCelebrities values(@IdCelebrity,@IdMovie)", new { IdCelebrity = CelebrityId, IdMovie = MovieId });
            return true;
        }
        public async Task<List<string>> GetAllMovies(int id)
        {
            var result = await _dbService.GetList<string>("select Movies.Name from Celebrities " +
                "join MoviesAndCelebrities on Celebrities.Id=MoviesAndCelebrities.IdCelebrity " +
                "join Movies on Movies.Id=MoviesAndCelebrities.IdMovie " +
                "where Celebrities.Id=@id", new { id = id });
            return result;
        }

        
    }
}

