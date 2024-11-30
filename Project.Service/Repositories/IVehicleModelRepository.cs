using Project.Service.Models;

namespace Project.Service.Repositories
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection, string? makeFilter, int pageSize = 0, int pageNumber = 0);
        Task<VehicleModel?> GetByIdAsync(int id);
        Task<VehicleModel> AddAsync(VehicleModel vehicleModel);
        Task<VehicleModel?> UpdateAsync(VehicleModel vehicleModel);
        Task<VehicleModel?> DeleteAsync(int id);
        Task<int> CountAsync(string? searchQuery, string? makeFilter);
    }
}
