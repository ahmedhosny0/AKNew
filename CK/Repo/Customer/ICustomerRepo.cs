using CK.DTO;
using CK.Models.TopSoft;
using CK.Repo.Base;

namespace CK.Repo.Customer
{
    public interface ICustomerRepo : IBaseRepo
    {
        //Customer
        Task AddCustomerCodeAsync(CustomerCode customer);
        List<CustomerCode> GetAllCustomerCodes();
        Task<CustomerCode?> GetCustomerCodeByIdAsync(int id);
        Task DeleteCustomerCodeAsync(int id);
        Task UpdateCustomerCodeAsync(CustomerCode updatedCustomer);
    }
}
