using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.NET_Core_MVC_03.PL.Extensions
{
    public static class ApplicationServicesExtensiond
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            // services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            // services.AddSingleton<IDepartmentRepository, DepartmentRepository>();


            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        }
    }
}
