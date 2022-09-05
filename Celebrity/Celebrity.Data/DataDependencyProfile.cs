using Celebrity.Data.Interface.Repositories;
using Celebrity.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Data
{
    public class DataDependencyProfile
    {
        public static void Register(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<ICelebrityRepository, CelebrityRepository>();
        }
    }
}
