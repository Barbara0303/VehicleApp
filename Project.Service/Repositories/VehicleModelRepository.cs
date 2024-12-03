using Microsoft.EntityFrameworkCore;
using Project.Service.Parameters;
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

        public async Task<int> CountAsync(FilteringParameters filteringParams)
        {
            var query = _appDbContext.VehicleModels.Include(x => x.VehicleMake).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filteringParams.FilterQuery))
            {
                query = query.Where(x => x.VehicleMake.Name.Contains(filteringParams.FilterQuery));
            }

            if (!string.IsNullOrEmpty(filteringParams.SearchQuery))
            {
                query = query.Where(x => x.Name.Contains(filteringParams.SearchQuery) || x.Abrv.Contains(filteringParams.SearchQuery));
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

        public async Task<IEnumerable<VehicleModel>> GetAllAsync(
            FilteringParameters filteringParams,
            SortingParameters sortingParams,
            PagingParameters pagingParams)
        {
            var query = _appDbContext.VehicleModels.Include(x => x.VehicleMake).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filteringParams.FilterQuery))
            {
                query = query.Where(x => x.VehicleMake.Name.Contains(filteringParams.FilterQuery));
            }

            if (string.IsNullOrWhiteSpace(filteringParams.SearchQuery) == false)
            {
                query = query.Where(x => x.Name.Contains(filteringParams.SearchQuery) || x.Abrv.Contains(filteringParams.SearchQuery)
                || x.VehicleMake.Name.Contains(filteringParams.SearchQuery));
            }

            if (string.IsNullOrWhiteSpace(sortingParams.SortBy) == false)
            {
                var isDesc = string.Equals(sortingParams.SortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortingParams.SortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                }

                if (string.Equals(sortingParams.SortBy, "Abrv", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.Abrv) : query.OrderBy(x => x.Abrv);
                }

            }

 
                var skipResults = (pagingParams.PageNumber - 1) * pagingParams.PageSize;
                query = query.Skip(skipResults).Take(pagingParams.PageSize);
 

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
