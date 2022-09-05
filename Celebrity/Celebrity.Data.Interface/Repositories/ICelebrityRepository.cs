using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Data.Interface.Repositories
{
    public interface ICelebrityRepository
    {
        public Task<bool> CreateNewCelebrity(string name, string date, string address, string[] movies);
        public Task<bool> CreateNewMovie(string name);
        public Task<bool> CreateNewAddress(string name);
        public Task<bool> IsMovieThere(string name);
        public Task<bool> IsAddressThere(string name);
    }
}
