using Project.Service.Models;

namespace Project.Service
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleMake>> GetAllVehicleMakesAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageSize = 0, int pageNumber = 0);
        Task<VehicleMake?> GetVehicleMakeByIdAsync(int id);
        Task<VehicleMake> CreateVehicleMakeAsync(VehicleMake make);
        Task<VehicleMake?> UpdateVehicleMakeAsync(VehicleMake make);
        Task<VehicleMake?> DeleteVehicleMakeAsync(int id);
        Task<int> CountVehicleMakeAsync();
        Task<IEnumerable<VehicleMake>> GetAllVehicleMakesForDropdownAsync();

        Task<IEnumerable<VehicleModel>> GetAllVehicleModelsAsync(string? searchQuery, string? sortBy, string? sortDirection, string? makeFilter, int pageSize = 0, int pageNumber = 0);
        Task<VehicleModel?> GetVehicleModelByIdAsync(int id);
        Task<VehicleModel> AddVehicleModelAsync(VehicleModel vehicleModel);
        Task<VehicleModel?> UpdateVehicleModelAsync(VehicleModel vehicleModel);
        Task<VehicleModel?> DeleteVehicleModelAsync(int id);
        Task<int> GetVehicleModelCountAsync();
    }
}
