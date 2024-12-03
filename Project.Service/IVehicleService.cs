using Project.Service.Parameters;
using Project.Service.Models;

namespace Project.Service
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleMake>> GetAllVehicleMakesAsync(
            FilteringParameters filteringParams,
            SortingParameters sortingParams,
            PagingParameters pagingParams);
        Task<VehicleMake?> GetVehicleMakeByIdAsync(int id);
        Task<VehicleMake> CreateVehicleMakeAsync(VehicleMake make);
        Task<bool> UpdateVehicleMakeAsync(VehicleMake make);
        Task<bool> DeleteVehicleMakeAsync(int id);
        Task<int> CountVehicleMakeAsync(FilteringParameters filteringParams);
        Task<IEnumerable<VehicleMake>> GetAllVehicleMakesForDropdownAsync();

        Task<IEnumerable<VehicleModel>> GetAllVehicleModelsAsync(
            FilteringParameters filteringParams,
            SortingParameters sortingParams,
            PagingParameters pagingParams);
        Task<VehicleModel?> GetVehicleModelByIdAsync(int id);
        Task<VehicleModel> AddVehicleModelAsync(VehicleModel vehicleModel);
        Task<bool> UpdateVehicleModelAsync(VehicleModel vehicleModel);
        Task<bool> DeleteVehicleModelAsync(int id);
        Task<int> GetVehicleModelCountAsync(FilteringParameters filteringParams);
    }
}
