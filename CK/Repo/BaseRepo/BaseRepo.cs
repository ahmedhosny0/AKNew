using CK.DTO;
using CK.Models;
using CK.Models.CKPro;
using CK.Models.TopSoft;
using CK.ViewModel;

namespace CK.Repo.Base
{
    public class BaseRepo :IBaseRepo
    {
        public readonly CkproUsersContext _CkproUsersContext;
        public readonly TopSoftContext _TopSoftContext;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public BaseRepo(
CkproUsersContext ckproUsersContext, TopSoftContext topSoftContext, IHttpContextAccessor httpContextAccessor
            )
        {
            _CkproUsersContext = ckproUsersContext;
            this._TopSoftContext = topSoftContext;
            _httpContextAccessor = httpContextAccessor;
        }
        //Customer 
        public async Task<List<ZoneCode>> GetZone()
        {
            List<ZoneCode> Data = (from
                                     Zone in _TopSoftContext.ZoneCodes
                                   select new ZoneCode
                                   {
                                       Serial = Zone.Serial,
                                       Name = Zone.Name
                                   })
                                     .Distinct().OrderByDescending(s => s.Name).ToList();
            return Data;
        }
        public async Task<List<AreaDTO>> GetArea(int Zone)
        {

            var AreaData = (from Area in _TopSoftContext.AreaCodes
                            where Area.ZoneSerial == Zone
                            select new AreaDTO
                            {
                                Serial = Area.Serial, // Map Serial to Id
                                Name = Area.Name
                            })
                   .Distinct()
                   .OrderByDescending(s => s.Name)
                   .ToList();
            return AreaData;
        }

        public async Task<List<StreetCode>> GetStreet(int Zone, int Area)
        {
            List<StreetCode> Data = (from
                                     Street in _TopSoftContext.StreetCodes
                                     where(Street.ZoneSerial == Zone & Street.AreaSerial == Area)
                                     select new StreetCode
                                     {
                                         Serial = Street.Serial,
                                         Name = Street.Name
                                     })
                                     .Distinct().OrderByDescending(s => s.Name).ToList();
            return Data;
        }
        public async Task<List<BranchDatum>> GetBranch()
        {
            List<BranchDatum> Data = (from
                                     Branch in _TopSoftContext.BranchData
                                      select new BranchDatum
                                      {
                                          Serial = Branch.Serial,
                                          BranchName = Branch.BranchName
                                      })
                                     .Distinct().OrderByDescending(s => s.BranchName).ToList();
            return Data;
        }
    }
}
