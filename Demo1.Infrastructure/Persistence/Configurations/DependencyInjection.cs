using Demo1.Application.Interfaces.IRepository;
using Demo1.Application.Interfaces.IUnitOfWork;
using Demo1.Application.Mappings;
using Demo1.Domain.Entity;
using Demo1.Infrastructure.Implementation.Repositories;
using Demo1.Infrastructure.Implementation.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo1.Application.Mappings; 
namespace Demo1.Infrastructure.Persistence.Configurations

{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure ( this IServiceCollection Services,string ConnectionString)
        {

            // Ef handling
            Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(ConnectionString,m=>m.MigrationsAssembly("Demo1.Infrastructure"))
                );
            //UnitOfwork Services
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Repositories Services
            Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            
            




            return Services;

        }
    }
}
