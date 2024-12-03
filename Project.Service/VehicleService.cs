using Project.Service.Parameters;
using Project.Service.Models;
using Project.Service.Repositories;

namespace Project.Service
{
    public class VehicleService: IVehicleService
    {
        private readonly IVehicleMakeRepository _makeRepository;
        private readonly IVehicleModelRepository _modelRepository;

        public VehicleService(IVehicleMakeRepository makeRepository, IVehicleModelRepository modelRepository)
        {
            _makeRepository = makeRepository;
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<VehicleMake>> GetAllVehicleMakesAsync(
            FilteringParameters filteringParams,
            SortingParameters sortingParams,
            PagingParameters pagingParams)
        {
            return await _makeRepository.GetAllAsync(filteringParams, sortingParams, pagingParams);
        }

        public async Task<int> CountVehicleMakeAsync(FilteringParameters filteringParams)
        {
            return await _makeRepository.CountAsync(filteringParams);
        }

        public async Task<VehicleMake?> GetVehicleMakeByIdAsync(int id)
        {
            return await _makeRepository.GetByIdAsync(id);
        }

        public async Task<VehicleMake> CreateVehicleMakeAsync(VehicleMake make)
        {
            return await _makeRepository.AddAsync(make);
        }
        public async Task<bool> UpdateVehicleMakeAsync(VehicleMake make)
        {
            var updatedMake = await _makeRepository.UpdateAsync(make);
            return updatedMake != null;
           
        }

        public async Task<bool> DeleteVehicleMakeAsync(int id)
        {
            var deletedMake = await _makeRepository.DeleteAsync(id);
            return deletedMake != null;
        }

        public async Task<IEnumerable<VehicleModel>> GetAllVehicleModelsAsync(
            FilteringParameters filteringParams,
            SortingParameters sortingParams,
            PagingParameters pagingParams)
        {
            return await _modelRepository.GetAllAsync(filteringParams, sortingParams, pagingParams);
        }

        public async Task<VehicleModel?> GetVehicleModelByIdAsync(int id)
        {
            return await _modelRepository.GetByIdAsync(id);
        }

        public async Task<VehicleModel> AddVehicleModelAsync(VehicleModel vehicleModel)
        {
            return await _modelRepository.AddAsync(vehicleModel);
        }

        public async Task<bool> UpdateVehicleModelAsync(VehicleModel vehicleModel)
        {
            var updatedModel = await _modelRepository.UpdateAsync(vehicleModel);
            return updatedModel != null;
        }

        public async Task<bool> DeleteVehicleModelAsync(int id)
        {
            var deletedModel = await _modelRepository.DeleteAsync(id);
            return deletedModel != null;
        }

        public async Task<int> GetVehicleModelCountAsync(FilteringParameters filteringParams)
        {
            return await _modelRepository.CountAsync(filteringParams);
        }

        public async Task<IEnumerable<VehicleMake>> GetAllVehicleMakesForDropdownAsync()
        {
            return await _makeRepository.GetAllForDropdownAsync();
        }
    }
}
