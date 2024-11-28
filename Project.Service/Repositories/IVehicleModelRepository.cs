using Project.Service.Models;

namespace Project.Service.Repositories
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetAllAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, string? makeFilter = null, int pageSize = 0, int pageNumber = 0);
        Task<VehicleModel?> GetByIdAsync(int id);
        Task<VehicleModel> AddAsync(VehicleModel vehicleModel);
        Task<VehicleModel?> UpdateAsync(VehicleModel vehicleModel);
        Task<VehicleModel?> DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
