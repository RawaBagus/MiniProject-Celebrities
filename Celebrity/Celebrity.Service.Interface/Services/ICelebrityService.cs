using Celebrity.Model.Entities.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Service.Interface.Services
{
    public interface ICelebrityService
    {
        public Task<bool> CreateNewCelebrity(string name, string date, string address, string[] movies);
        public Task<bool> DeleteByCelebrityId(int id);
        public Task<List<CelebrityDataShow>> GetAllData(int num);
        public Task<List<CelebrityDataShow>> GetDataByMovie(string Movie,int num);

        public Task<bool> UpdateByCelebrityId(CelebrityDataUpdate data, int id);
    }
}
