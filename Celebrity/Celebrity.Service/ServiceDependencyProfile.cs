using Celebrity.Service.Interface.Services;
using Celebrity.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrity.Service
{
    public class ServiceDependencyProfile
    {
        public static void Register(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<IDbService, DbService>();
            services.AddScoped<ICelebrityService, CelebrityService>();
        }
    }
}
