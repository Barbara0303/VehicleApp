using Project.Service.Models;
using Project.Service.Repositories;

namespace Project.Service
{
    public class VehicleService: IVehicleService
    {
        private readonly IVehicleMakeRepository _makeRepository;

        public VehicleService(IVehicleMakeRepository makeRepository)
        {
            _makeRepository = makeRepository;
        }

        public async Task<IEnumerable<VehicleMake>> GetAllMakesAsync(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 10, int pageNumber = 1)
        {
            return await _makeRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageSize, pageNumber);
        }

        public async Task<int> CountAsync()
        {
            return await _makeRepository.CountAsync();
        }

        public async Task<VehicleMake?> GetMakeByIdAsync(int id)
        {
            return await _makeRepository.GetByIdAsync(id);
        }

        public async Task<VehicleMake> CreateMakeAsync(VehicleMake make)
        {
            return await _makeRepository.AddAsync(make);
        }
        public async Task<VehicleMake?> UpdateMakeAsync(VehicleMake make)
        {
            return await _makeRepository.UpdateAsync(make);
        }

        public async Task<VehicleMake?> DeleteMakeAsync(int id)
        {
            return await _makeRepository.DeleteAsync(id);
        }
    }
}
