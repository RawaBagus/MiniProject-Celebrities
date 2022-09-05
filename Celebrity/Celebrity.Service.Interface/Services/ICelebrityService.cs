using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Service.Interface.Services
{
    public interface ICelebrityService
    {
        Task<bool> CreateNewCelebrity(string name, string date, string address, string[] movies);
    }
}
