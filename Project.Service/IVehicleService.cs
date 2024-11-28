using Project.Service.Models;

namespace Project.Service
{
    public interface IVehicleService
    {
        // CRUD za VehicleMake
        Task<IEnumerable<VehicleMake>> GetAllMakesAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageSize = 0, int pageNumber = 0);
        Task<VehicleMake?> GetMakeByIdAsync(int id);
        Task<VehicleMake> CreateMakeAsync(VehicleMake make);
        Task<VehicleMake?> UpdateMakeAsync(VehicleMake make);
        Task<VehicleMake?> DeleteMakeAsync(int id);
        Task<int> CountAsync();
        Task<IEnumerable<VehicleMake>> GetAllMakesForDropdownAsync();

        Task<IEnumerable<VehicleModel>> GetAllVehicleModelsAsync(string? searchQuery, string? sortBy, string? sortDirection, string? makeFilter, int pageSize = 0, int pageNumber = 0);
        Task<VehicleModel?> GetVehicleModelByIdAsync(int id);
        Task<VehicleModel> AddVehicleModelAsync(VehicleModel vehicleModel);
        Task<VehicleModel?> UpdateVehicleModelAsync(VehicleModel vehicleModel);
        Task<VehicleModel?> DeleteVehicleModelAsync(int id);
        Task<int> GetVehicleModelCountAsync();
    }
}
