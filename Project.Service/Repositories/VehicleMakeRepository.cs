using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Models;

namespace Project.Service.Repositories
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private readonly AppDbContext _appDbContext;
        public VehicleMakeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;      
        }
        public async Task<VehicleMake> AddAsync(VehicleMake vehicleMake)
        {
            await _appDbContext.VehicleMakes.AddAsync(vehicleMake);
            await _appDbContext.SaveChangesAsync();
            return vehicleMake;
        }

        public async Task<VehicleMake?> DeleteAsync(int id)
        {
            var existingVehicleMake = await _appDbContext.VehicleMakes.FindAsync(id);
            if (existingVehicleMake != null)
            {
                _appDbContext.Remove(existingVehicleMake);
                await _appDbContext.SaveChangesAsync();
                return existingVehicleMake;
            }
            return null;
        }

        public async Task<int> CountAsync()
        {
            return await _appDbContext.VehicleMakes.CountAsync();
        }

        public async Task<IEnumerable<VehicleMake>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection,
             int pageSize = 5, int pageNumber = 1)
        {
            var query = _appDbContext.VehicleMakes.AsQueryable();

            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Name.Contains(searchQuery) || x.Abrv.Contains(searchQuery));
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                }

                if (string.Equals(sortBy, "Abrv", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.Abrv) : query.OrderBy(x => x.Abrv);
                }

            }

            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<VehicleMake?> GetByIdAsync(int id)
        {
            return await _appDbContext.VehicleMakes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<VehicleMake?> UpdateAsync(VehicleMake vehicleMake)
        {
            _appDbContext.VehicleMakes.Update(vehicleMake);
            await _appDbContext.SaveChangesAsync();
            return vehicleMake;
        }
    }
}
