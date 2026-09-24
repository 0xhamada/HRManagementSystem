using HR.BLL.Service.Abstraction;
using HR.BLL.Service.Impelementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Common
{
    public static class ModularBuissinesLogicLayer
    {
        public static IServiceCollection AddBuissinesInBLL(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            //services.AddScoped<IAuthService, AuthService>();
            //services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
