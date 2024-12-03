using Microsoft.EntityFrameworkCore;
using Project.Service.Parameters;
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

        public async Task<int> CountAsync(FilteringParameters filteringParams)
        {
            var query = _appDbContext.VehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(filteringParams.SearchQuery))
            {
                query = query.Where(x => x.Name.Contains(filteringParams.SearchQuery) || x.Abrv.Contains(filteringParams.SearchQuery));
            }
            return await query.CountAsync();
        }

        public async Task<IEnumerable<VehicleMake>> GetAllAsync(FilteringParameters filteringParams,
            SortingParameters sortingParams,
            PagingParameters pagingParams)
        {
            var query = _appDbContext.VehicleMakes.AsQueryable();

            if (string.IsNullOrWhiteSpace(filteringParams.SearchQuery) == false)
            {
                query = query.Where(x => x.Name.Contains(filteringParams.SearchQuery) || x.Abrv.Contains(filteringParams.SearchQuery));
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

        public async Task<IEnumerable<VehicleMake>> GetAllForDropdownAsync()
        {
            return await _appDbContext.VehicleMakes.ToListAsync();
        }
    }
}
