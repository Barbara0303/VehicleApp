using AutoMapper;
using Project.MVC.Models.ViewModels;
using Project.Service.Models;

namespace Project.MVC.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
        }
    }
}
