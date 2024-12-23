using CK.Models.TopSoft;
using CK.Models;
using CK.Repo.Base;
using Microsoft.EntityFrameworkCore;
using CK.Models.CKPro;
using CK.DTO;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using System.Globalization;

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
        public async Task<bool> CheckTransactions(int Street)
        {
            var data = await _TopSoftContext.CustomerCodes.Where(x => x.StreetSerial == Street).ToListAsync();
            if (data.Count > 0)
            {
                return true;
            }
            return false;
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
        public List<SalesOrderDTO> GetAllStreetCodes()
        {
            SalesOrderDTO Conn = new SalesOrderDTO();
            using (SqlConnection connection = new SqlConnection(Conn.TopSoftConnection))
            {
                string Query = "SELECT * from RptStreet where  BranchCode is not null   ";
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
                            AreaCode = test["AreaCode"] == DBNull.Value ? "" : test["AreaCode"].ToString(),
                            AreaCode2 = test["AreaCode2"] == DBNull.Value ? "" : test["AreaCode2"].ToString(),
                            AreaName = test["AreaName"] == DBNull.Value ? string.Empty : test["AreaName"].ToString(),
                            StreetCode = test["StreetCode"] == DBNull.Value ? "" : test["StreetCode"].ToString(),
                            StreetCode2 = test["StreetCode2"] == DBNull.Value ? "" : test["StreetCode2"].ToString(),
                            StreetName = test["StreetName"] == DBNull.Value ? string.Empty : test["StreetName"].ToString(),
                            DeliveryTime = test["DeliveryTime"] == DBNull.Value ? "" : test["DeliveryTime"].ToString(),
                            ServiceCost = test["ServiceCost"] == DBNull.Value ? "" : Convert.ToDouble(test["ServiceCost"]).ToString("#,##0.00"),
                            BranchCode = test["BranchCode"] == DBNull.Value ? "" : test["BranchCode"].ToString(),
                            BranchIdR = test["BranchIdR"] == DBNull.Value ? "" : test["BranchIdR"].ToString(),
                            BranchIdD = test["BranchIdD"] == DBNull.Value ? "" : test["BranchIdD"].ToString(),
                            BranchName = test["BranchName"] == DBNull.Value ? string.Empty : test["BranchName"].ToString(),
                        };

                        vi.Add(si);
                    }
                    return vi;
                }
            }
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
