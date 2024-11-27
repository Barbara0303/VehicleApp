using Project.Service.Models;

namespace Project.Service
{
    public interface IVehicleService
    {
        // CRUD za VehicleMake
        Task<IEnumerable<VehicleMake>> GetAllMakesAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageSize = 5, int pageNumber = 1);
        Task<VehicleMake?> GetMakeByIdAsync(int id);
        Task<VehicleMake> CreateMakeAsync(VehicleMake make);
        Task<VehicleMake?> UpdateMakeAsync(VehicleMake make);
        Task<VehicleMake?> DeleteMakeAsync(int id);
        Task<int> CountAsync();
    }
}
