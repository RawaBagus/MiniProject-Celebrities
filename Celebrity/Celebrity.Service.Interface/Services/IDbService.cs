using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Service.Interface.Services
{
    public interface IDbService
    {
        Task<int> ModifyData(string command, object param);
        Task<List<T>> GetList<T>(string command, object param);
        Task<T> Get<T>(string command, object param);
        Task<bool> Check(string command, object param);
    }
}
