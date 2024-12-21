using CK.Models.TopSoft;
using CK.Repo.Base;

namespace CK.Repo.Area
{
    public interface IAreaRepo :IBaseRepo
    {
        Task<bool> CheckTransactions(int Area);
        Task<int> GetMaxCode();
        Task AddAreaCodeAsync(AreaCode Area);
        List<AreaCode> GetAllAreaCodes();
        Task<AreaCode?> GetAreaCodeByIdAsync(int id);
        Task DeleteAreaCodeAsync(int id);
        Task UpdateAreaCodeAsync(AreaCode updatedArea);
    }
}
