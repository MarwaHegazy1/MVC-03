using ASP.NET_Core_MVC.BLL;
using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.NET_Core_MVC_03.PL.Extensions
{
    public static class ApplicationServicesExtensiond
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            // services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            // services.AddSingleton<IDepartmentRepository, DepartmentRepository>();


            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
