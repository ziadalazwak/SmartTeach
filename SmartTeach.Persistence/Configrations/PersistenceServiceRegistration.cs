using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartTeach.App.Interfaces;
using SmartTeach.App.Services;
using SmartTeach.Domain.Interfaces;
using SmartTeach.Persistence.Dbcontext;
using SmartTeach.Persistence.Reposatory;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Persistence.Configrations
{
    public static class PersistenceServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with SQL Server
            services.AddDbContext<SmartTeachDbcontext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericReposatory<>),typeof(GenericReposatory<>));
            services.AddScoped<IGruopMangmentService, GroupMangmentService>();
            services.AddScoped<ISessionReposatory, SessionReposatory>();
            services.AddScoped<ISessionMangmentService, SessionMangmentService>();  
            services.AddScoped<IAttendacesReposatory, AttendacesReposatory>();
            services.AddScoped<IReprotMangmentService, ReportMangmentService>();


        }
       
    }
}
