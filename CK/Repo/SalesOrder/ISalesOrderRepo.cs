using CK.DTO;
using CK.Models.TopSoft;
using CK.Repo.Base;
using CK.ViewModel;

namespace CK.Repo.SalesOrder
{
    public interface ISalesOrderRepo :IBaseRepo
    {
        Task<bool> CheckTransactions(int Branch);
        Task<int> GetMaxCode();
        Task<List<CustomerCode>> GetCustomerList();
        Task<List<BranchDTO>> GetBranchesList(int Customer);
        Task<List<ItemsData>> GetItems(string Branch, string CategoryId);
        Task<ItemsData> GetPriceofItem(string ItemId, string Branch);
        Task<List<BranchDatum>> GetBranchesList(string username, string Isuser);
        Task<List<string>> GetCategoryList();
        Task<HsalesCode> GetSalesOrderByCodeAsync(int salesCode);
        Task AddSalesOrderAsync(HsalesCode header, List<DsalesCode> details, string username);
        Task UpdateSalesOrderAsync(HsalesCode header, List<DsalesCode> details);
        Task DeleteSalesOrderAsync(int salesCode);
        Task<SalesOrderRequest> GetSalesOrderAsync(int salesCode);
        Task<List<SalesOrderDTO>> GetAllSalesOrder(string StoreId, int salesCode,SalesOrderData sales);
        Task<List<SalesOrderDTO>> GetAllTransactions(string StoreId);
        Task<bool> BranchReply(HsalesCode header, string username);
    }
}
