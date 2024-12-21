using CK.Models.TopSoft;
using CK.Repo.Base;

namespace CK.Repo.Zone
{
    public interface IZoneRepo :IBaseRepo
    {
        Task<bool> CheckTransactions(int Zone);
        Task<int> GetMaxCode();
        Task AddZoneCodeAsync(ZoneCode Zone);
        List<ZoneCode> GetAllZoneCodes();
        Task<ZoneCode?> GetZoneCodeByIdAsync(int id);
        Task DeleteZoneCodeAsync(int id);
        Task UpdateZoneCodeAsync(ZoneCode updatedZone);
    }
}
