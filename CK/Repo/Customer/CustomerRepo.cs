using CK.DTO;
using CK.Models;
using CK.Models.TopSoft;
using CK.Repo.Base;
using CK.ViewModel;

namespace CK.Repo.Customer
{
    public class CustomerRepo :BaseRepo, ICustomerRepo
    {
        public CustomerRepo(
        CkproUsersContext ckproUsersContext,
        TopSoftContext topSoftContext,
        IHttpContextAccessor httpContextAccessor
    ) : base(ckproUsersContext, topSoftContext, httpContextAccessor)
        {
        }
        public async Task AddCustomerCodeAsync(CustomerCode customer)
        {
            _TopSoftContext.CustomerCodes.Add(customer);
            await _TopSoftContext.SaveChangesAsync();
        }
        public async Task<CustomerCode?> GetCustomerCodeByIdAsync(int id)
        {
            return await _TopSoftContext.CustomerCodes.FindAsync(id);
        }
        public List<CustomerCode> GetAllCustomerCodes()
        {
            var data =  _TopSoftContext.CustomerCodes.OrderByDescending(x => x.CustomerCode1).ToList();
            return data;
        }
        public async Task UpdateCustomerCodeAsync(CustomerCode updatedCustomer)
        {
            var existingCustomer = await _TopSoftContext.CustomerCodes.FindAsync(updatedCustomer.CustomerCode1);
            if (existingCustomer == null) throw new Exception("Customer not found");

            existingCustomer.CustomerName = updatedCustomer.CustomerName;
            existingCustomer.Phone1 = updatedCustomer.Phone1;
            existingCustomer.Phone2 = updatedCustomer.Phone2;
            existingCustomer.Address1 = updatedCustomer.Address1;
            existingCustomer.Address2 = updatedCustomer.Address2;
            existingCustomer.ZoneSerial = updatedCustomer.ZoneSerial;
            existingCustomer.AreaSerial = updatedCustomer.AreaSerial;
            existingCustomer.StreetSerial = updatedCustomer.StreetSerial;

            await _TopSoftContext.SaveChangesAsync();
        }
        // Delete
        public async Task DeleteCustomerCodeAsync(int id)
        {
            var customer = await _TopSoftContext.CustomerCodes.FindAsync(id);
            if (customer == null) throw new Exception("Customer not found");

            _TopSoftContext.CustomerCodes.Remove(customer);
            await _TopSoftContext.SaveChangesAsync();
        }
    }
}
