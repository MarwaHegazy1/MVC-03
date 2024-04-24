using ASP.NET_Core_MVC.DAL.Modules;
using ASP.NET_Core_MVC_03.PL.ViewModels;
using AutoMapper;

namespace ASP.NET_Core_MVC_03.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();

            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
