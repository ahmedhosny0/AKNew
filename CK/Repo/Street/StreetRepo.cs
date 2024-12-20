using CK.Models.TopSoft;
using CK.Models;
using CK.Repo.Base;
using Microsoft.EntityFrameworkCore;

namespace CK.Repo.Street
{
    public class StreetRepo : BaseRepo, IStreetRepo
    {
        public StreetRepo(
   CkproUsersContext ckproUsersContext,
   TopSoftContext topSoftContext,
   IHttpContextAccessor httpContextAccessor
  ) : base(ckproUsersContext, topSoftContext, httpContextAccessor)
        {
        }
        public async Task<int> GetMaxCode()
        {
            int maxSalesCode = await _TopSoftContext.StreetCodes
            .Where(h => h.Code.HasValue) // Select only valid SalesCode values
            .OrderByDescending(h => h.Code.Value) // Order descending to find max value
            .Select(h => h.Code.Value) // Select the actual value
            .FirstOrDefaultAsync(); // Fetch the first value
            return maxSalesCode + 1;
        }
        public async Task AddStreetCodeAsync(StreetCode Street)
        {
            _TopSoftContext.StreetCodes.Add(Street);
            await _TopSoftContext.SaveChangesAsync();
        }
        public async Task<StreetCode?> GetStreetCodeByIdAsync(int id)
        {
            return await _TopSoftContext.StreetCodes.FindAsync(id);
        }
        public List<StreetCode> GetAllStreetCodes()
        {
            var data = _TopSoftContext.StreetCodes.OrderByDescending(x => x.Name).ToList();
            return data;
        }
        public async Task UpdateStreetCodeAsync(StreetCode updatedStreet)
        {
            var existingStreet = await _TopSoftContext.StreetCodes.FindAsync(updatedStreet.Serial);
            if (existingStreet == null) throw new Exception("Street not found");

            existingStreet.Name = updatedStreet.Name;
            existingStreet.ZoneSerial = updatedStreet.ZoneSerial;
            existingStreet.AreaSerial = updatedStreet.AreaSerial;
            existingStreet.BranchSerial = updatedStreet.BranchSerial;
            existingStreet.DeliveryTime = updatedStreet.DeliveryTime;
            existingStreet.ServiceCost = updatedStreet.ServiceCost;
            await _TopSoftContext.SaveChangesAsync();
        }
        // Delete
        public async Task DeleteStreetCodeAsync(int id)
        {
            var Street = await _TopSoftContext.StreetCodes.FindAsync(id);
            if (Street == null) throw new Exception("Street not found");

            _TopSoftContext.StreetCodes.Remove(Street);
            await _TopSoftContext.SaveChangesAsync();
        }
    }
}
