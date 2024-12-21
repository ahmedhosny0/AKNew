using CK.Models;
using CK.Models.CKPro;
using CK.Models.TopSoft;
using CK.Repo.Base;
using Microsoft.EntityFrameworkCore;

namespace CK.Repo.Area
{
    public class AreaRepo : BaseRepo, IAreaRepo
    {
        public AreaRepo(
 CkproUsersContext ckproUsersContext,
 TopSoftContext topSoftContext,
 IHttpContextAccessor httpContextAccessor
) : base(ckproUsersContext, topSoftContext, httpContextAccessor)
        {
        }
        public async Task<int> GetMaxCode()
        {
            int maxSalesCode = await _TopSoftContext.AreaCodes
            .Where(h => h.Code.HasValue) // Select only valid SalesCode values
            .OrderByDescending(h => h.Code.Value) // Order descending to find max value
            .Select(h => h.Code.Value) // Select the actual value
            .FirstOrDefaultAsync(); // Fetch the first value
            return maxSalesCode + 1;
        }
        public async Task<bool> CheckTransactions(int Area)
        {
            var data = await _TopSoftContext.CustomerCodes.Where(x => x.AreaSerial == Area).ToListAsync();
            var data2 = await _TopSoftContext.StreetCodes.Where(x => x.AreaSerial == Area).ToListAsync();
            if (data.Count > 0 || data2.Count > 0)
            {
                return true;
            }
            return false;
        }
        public async Task AddAreaCodeAsync(AreaCode Area)
        {
            _TopSoftContext.AreaCodes.Add(Area);
            await _TopSoftContext.SaveChangesAsync();
        }
        public async Task<AreaCode?> GetAreaCodeByIdAsync(int id)
        {
            return await _TopSoftContext.AreaCodes.FindAsync(id);
        }
        public List<AreaCode> GetAllAreaCodes()
        {
            var data = _TopSoftContext.AreaCodes.OrderByDescending(x => x.Name).ToList();
            return data;
        }
        public async Task UpdateAreaCodeAsync(AreaCode updatedArea)
        {
            var existingArea = await _TopSoftContext.AreaCodes.FindAsync(updatedArea.Serial);
            if (existingArea == null) throw new Exception("Area not found");

            existingArea.Name = updatedArea.Name;
            existingArea.ZoneSerial = updatedArea.ZoneSerial;
            await _TopSoftContext.SaveChangesAsync();
        }
        // Delete
        public async Task DeleteAreaCodeAsync(int id)
        {
            var Area = await _TopSoftContext.AreaCodes.FindAsync(id);
            if (Area == null) throw new Exception("Area not found");

            _TopSoftContext.AreaCodes.Remove(Area);
            await _TopSoftContext.SaveChangesAsync();
        }
    }
}
