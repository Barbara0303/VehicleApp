using AutoMapper;
using Project.MVC.Models.ViewModels;
using Project.Service.Models;

namespace Project.MVC.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelViewModel>()
            .ForMember(dest => dest.MakeName, opt => opt.MapFrom(src => src.VehicleMake.Name)).ReverseMap();
   
        }
    }
}
