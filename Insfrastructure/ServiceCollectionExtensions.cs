using Application.Features.Behaviors.Validations;
using Application.Repository;
using Insfrastructure.Contexts;
using Insfrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insfrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            return services.AddTransient<IPropertyRepo, PropertyRepo>()
                           .AddDbContext<ApplicationDbContext>(options => options
                           .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
