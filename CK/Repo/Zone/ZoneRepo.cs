using CK.Models.TopSoft;
using CK.Models;
using CK.Repo.Base;
using Microsoft.EntityFrameworkCore;
using CK.Models.CKPro;
using CK.DTO;
using Microsoft.Data.SqlClient;

namespace CK.Repo.Zone
{
    public class ZoneRepo : BaseRepo, IZoneRepo
    {
        public ZoneRepo(
   CkproUsersContext ckproUsersContext,
   TopSoftContext topSoftContext,
   IHttpContextAccessor httpContextAccessor
  ) : base(ckproUsersContext, topSoftContext, httpContextAccessor)
        {
        }
        public async Task<bool> CheckTransactions(int Zone)
        {
            var data = await _TopSoftContext.CustomerCodes.Where(x => x.ZoneSerial == Zone).ToListAsync();
            var data2 = await _TopSoftContext.StreetCodes.Where(x => x.ZoneSerial == Zone).ToListAsync();
            var data3 = await _TopSoftContext.AreaCodes.Where(x => x.ZoneSerial == Zone).ToListAsync();
            if (data.Count > 0 || data2.Count > 0 || data3.Count > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<int> GetMaxCode()
        {
            int maxSalesCode = await _TopSoftContext.ZoneCodes
            .Where(h => h.Code.HasValue) // Select only valid SalesCode values
            .OrderByDescending(h => h.Code.Value) // Order descending to find max value
            .Select(h => h.Code.Value) // Select the actual value
            .FirstOrDefaultAsync(); // Fetch the first value
            return maxSalesCode + 1;
        }
        public async Task AddZoneCodeAsync(ZoneCode Zone)
        {
            _TopSoftContext.ZoneCodes.Add(Zone);
            await _TopSoftContext.SaveChangesAsync();
        }
        public async Task<ZoneCode?> GetZoneCodeByIdAsync(int id)
        {
            return await _TopSoftContext.ZoneCodes.FindAsync(id);
        }
        public List<SalesOrderDTO> GetAllZoneCodes()
        {
            SalesOrderDTO Conn = new SalesOrderDTO();
            using (SqlConnection connection = new SqlConnection(Conn.TopSoftConnection))
            {
                string Query = "SELECT * from RptZone ";
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    connection.Open(); // Open the connection
                    var vi = new List<SalesOrderDTO>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        SalesOrderDTO si = new SalesOrderDTO()
                        {
                            ZoneCode = test["ZoneCode"] == DBNull.Value ? "" : test["ZoneCode"].ToString(),
                            ZoneCode2 = test["ZoneCode2"] == DBNull.Value ? "" : test["ZoneCode2"].ToString(),
                            ZoneName = test["ZoneName"] == DBNull.Value ? string.Empty : test["ZoneName"].ToString(),
                        };

                        vi.Add(si);
                    }
                    return vi;
                }
            }
        }
        public async Task UpdateZoneCodeAsync(ZoneCode updatedZone)
        {
            var existingZone = await _TopSoftContext.ZoneCodes.FindAsync(updatedZone.Serial);
            if (existingZone == null) throw new Exception("Zone not found");

            existingZone.Name = updatedZone.Name;
            await _TopSoftContext.SaveChangesAsync();
        }
        // Delete
        public async Task DeleteZoneCodeAsync(int id)
        {
            var Zone = await _TopSoftContext.ZoneCodes.FindAsync(id);
            if (Zone == null) throw new Exception("Zone not found");

            _TopSoftContext.ZoneCodes.Remove(Zone);
            await _TopSoftContext.SaveChangesAsync();
        }
    }
}
