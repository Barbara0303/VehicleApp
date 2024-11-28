using Project.Service.Models;

namespace Project.Service.Repositories
{
    public interface IVehicleMakeRepository
    {
        Task<IEnumerable<VehicleMake>> GetAllAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageSize = 0, int pageNumber = 0);
        Task<VehicleMake?> GetByIdAsync(int id);
        Task<VehicleMake> AddAsync(VehicleMake vehicleMake);
        Task<VehicleMake?> UpdateAsync(VehicleMake vehicleMake);
        Task<VehicleMake?> DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
