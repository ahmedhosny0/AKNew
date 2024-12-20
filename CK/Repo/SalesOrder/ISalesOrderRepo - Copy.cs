//using CK.DTO;
//using CK.Models;
//using CK.Models.TopSoft;
//using CK.ViewModel;

//namespace CK.Repo.SalesOrder
//{
//    public interface ISalesOrderRepo
//    {
//        Task<int> GetMaxCode();
//        Task<List<CustomerCode>> GetCustomerList();
//        Task<List<BranchDTO>> GetBranchesList(int Customer);
//        Task<List<ItemsData>> GetItems(string Branch, string CategoryId);
//        Task<ItemsData> GetPriceofItem(string ItemId, string Branch);
//        Task<List<BranchDatum>> GetBranchesList(string username, string Isuser);
//        Task<List<string>> GetCategoryList();
//        Task<IEnumerable<HsalesCode>> GetAllSalesOrdersAsync();
//        Task<HsalesCode> GetSalesOrderByCodeAsync(int salesCode);
//        Task AddSalesOrderAsync(HsalesCode header, List<DsalesCode> details, string username);
//        Task UpdateSalesOrderAsync(HsalesCode header, List<DsalesCode> details);
//        Task DeleteSalesOrderAsync(int salesCode);
//        Task<List<SalesOrderDTO>> GetSalesOrderAsync(int salesCode);
//        Task<List<HsalesCode>> GetAllSalesOrder();
//    }
//}
