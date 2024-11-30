using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Models;
using System;

namespace Project.Service.Repositories
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly AppDbContext _appDbContext;

        public VehicleModelRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<VehicleModel> AddAsync(VehicleModel vehicleModel)
        {
            await _appDbContext.VehicleModels.AddAsync(vehicleModel);
            await _appDbContext.SaveChangesAsync();
            return vehicleModel;
        }

        public async Task<int> CountAsync(string? searchQuery, string? makeFilter)
        {
            var query = _appDbContext.VehicleModels.Include(x => x.VehicleMake).AsQueryable();

            if (!string.IsNullOrWhiteSpace(makeFilter))
            {
                query = query.Where(x => x.VehicleMake.Name.Contains(makeFilter));
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(x => x.Name.Contains(searchQuery) || x.Abrv.Contains(searchQuery));
            }
            return await query.CountAsync();
        }

        public async Task<VehicleModel?> DeleteAsync(int id)
        {
            var existing = await _appDbContext.VehicleModels.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null)
            {
                return null;
            }

            _appDbContext.VehicleModels.Remove(existing);
            await _appDbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<IEnumerable<VehicleModel>> GetAllAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, string? makeFilter = null, int pageSize = 0, int pageNumber = 0)
        {
            var query = _appDbContext.VehicleModels.Include(x => x.VehicleMake).AsQueryable();

            if (!string.IsNullOrWhiteSpace(makeFilter))
            {
                query = query.Where(x => x.VehicleMake.Name.Contains(makeFilter));
            }

            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Name.Contains(searchQuery) || x.Abrv.Contains(searchQuery)
                || x.VehicleMake.Name.Contains(searchQuery));
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

            if (pageSize > 0 && pageNumber > 0)
            {
                var skipResults = (pageNumber - 1) * pageSize;
                query = query.Skip(skipResults).Take(pageSize);
            }

            return await query.ToListAsync();
        }

        public async Task<VehicleModel?> GetByIdAsync(int id)
        {
            return await _appDbContext.VehicleModels.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<VehicleModel?> UpdateAsync(VehicleModel vehicleModel)
        {
            var existing = await _appDbContext.VehicleModels.FirstOrDefaultAsync(x => x.Id == vehicleModel.Id);
            if (existing == null)
            {
                return null;
            }

            _appDbContext.Entry(existing).CurrentValues.SetValues(vehicleModel);
            await _appDbContext.SaveChangesAsync();
            return existing;
        }
    }
}
