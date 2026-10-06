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
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<ILeaveTypeService, LeaveTypeService>();
            services.AddScoped<ILeaveRequestService, LeaveRequestService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IResignationService, ResignationService>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
