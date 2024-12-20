using CK.DTO;
using CK.Models.TopSoft;

namespace CK.Repo.Base
{
    public interface IBaseRepo
    {
        //Customer
        Task<List<BranchDatum>> GetBranch();
        Task<List<StreetCode>> GetStreet(int Zone,int Area);
        Task<List<AreaDTO>> GetArea(int Zone);
        Task<List<ZoneCode>> GetZone();
    }
}
