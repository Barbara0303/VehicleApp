using Project.Service.Parameters;
using Project.Service.Models;

namespace Project.Service.Repositories
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetAllAsync(
            FilteringParameters filteringParams,
            SortingParameters sortingParams,
            PagingParameters pagingParams);
        Task<VehicleModel?> GetByIdAsync(int id);
        Task<VehicleModel> AddAsync(VehicleModel vehicleModel);
        Task<VehicleModel?> UpdateAsync(VehicleModel vehicleModel);
        Task<VehicleModel?> DeleteAsync(int id);
        Task<int> CountAsync(FilteringParameters filteringParams);
    }
}
