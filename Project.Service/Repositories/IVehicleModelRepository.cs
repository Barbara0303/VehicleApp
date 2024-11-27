using Project.Service.Models;

namespace Project.Service.Repositories
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetAllAsync(string? searchQuery);
        Task<VehicleModel?> GetByIdAsync(int id);
        Task<VehicleModel> AddAsync(VehicleModel model);
        Task<VehicleModel?> UpdateAsync(VehicleModel model);
        Task<VehicleModel?> DeleteAsync(int id);
    }
}
