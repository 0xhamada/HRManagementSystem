using HR.DAL.Repo.Abstraction;
using HR.DAL.Repo.Impelementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Common
{
    public static class ModularDataAccessLayer
    {
        public static IServiceCollection AddBuissinesInDall(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();

            return services;
        }
    }
}
