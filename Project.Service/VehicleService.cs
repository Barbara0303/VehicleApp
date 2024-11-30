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

        public async Task<IEnumerable<VehicleMake>> GetAllVehicleMakesAsync(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 0, int pageNumber =0)
        {
            return await _makeRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageSize, pageNumber);
        }

        public async Task<int> CountVehicleMakeAsync()
        {
            return await _makeRepository.CountAsync();
        }

        public async Task<VehicleMake?> GetVehicleMakeByIdAsync(int id)
        {
            return await _makeRepository.GetByIdAsync(id);
        }

        public async Task<VehicleMake> CreateVehicleMakeAsync(VehicleMake make)
        {
            return await _makeRepository.AddAsync(make);
        }
        public async Task<VehicleMake?> UpdateVehicleMakeAsync(VehicleMake make)
        {
            return await _makeRepository.UpdateAsync(make);
        }

        public async Task<VehicleMake?> DeleteVehicleMakeAsync(int id)
        {
            return await _makeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<VehicleModel>> GetAllVehicleModelsAsync(string? searchQuery, string? sortBy, string? sortDirection, string? makeFilter, int pageSize = 0, int pageNumber = 0)
        {
            return await _modelRepository.GetAllAsync(searchQuery, sortBy, sortDirection, makeFilter, pageSize, pageNumber);
        }

        public async Task<VehicleModel?> GetVehicleModelByIdAsync(int id)
        {
            return await _modelRepository.GetByIdAsync(id);
        }

        public async Task<VehicleModel> AddVehicleModelAsync(VehicleModel vehicleModel)
        {
            return await _modelRepository.AddAsync(vehicleModel);
        }

        public async Task<VehicleModel?> UpdateVehicleModelAsync(VehicleModel vehicleModel)
        {
            return await _modelRepository.UpdateAsync(vehicleModel);
        }

        public async Task<VehicleModel?> DeleteVehicleModelAsync(int id)
        {
            return await _modelRepository.DeleteAsync(id);
        }

        public async Task<int> GetVehicleModelCountAsync()
        {
            return await _modelRepository.CountAsync();
        }

        public async Task<IEnumerable<VehicleMake>> GetAllVehicleMakesForDropdownAsync()
        {
            return await _makeRepository.GetAllAsync();
        }
    }
}
