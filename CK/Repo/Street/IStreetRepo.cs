using CK.Models.TopSoft;
using CK.Repo.Base;

namespace CK.Repo.Street
{
    public interface IStreetRepo : IBaseRepo
    {
        Task<bool> CheckTransactions(int Street);
        Task<int> GetMaxCode();
        Task AddStreetCodeAsync(StreetCode Street);
        List<StreetCode> GetAllStreetCodes();
        Task<StreetCode?> GetStreetCodeByIdAsync(int id);
        Task DeleteStreetCodeAsync(int id);
        Task UpdateStreetCodeAsync(StreetCode updatedStreet);
    }
}
