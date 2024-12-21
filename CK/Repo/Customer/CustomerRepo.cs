using CK.DTO;
using CK.Models;
using CK.Models.CKPro;
using CK.Models.TopSoft;
using CK.Repo.Base;
using CK.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CK.Repo.Customer
{
    public class CustomerRepo : BaseRepo, ICustomerRepo
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
        public async Task<bool> CheckTransactions(int Customer)
{
            var data = await _TopSoftContext.HsalesCodes.Where(x => x.CustomerCode == Customer.ToString()).ToListAsync();
            if (data.Count > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> CheckExist(CustomerCode Customer)
        {
            var phone1 = await _TopSoftContext.CustomerCodes.Where(x => x.Phone1 == Customer.Phone1).ToListAsync();
            if (phone1.Count > 0)
            {
                return true;
            }
            return false;
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
            existingCustomer.Phone3 = updatedCustomer.Phone3;
            existingCustomer.Address1 = updatedCustomer.Address1;
            existingCustomer.Address2 = updatedCustomer.Address2;
            existingCustomer.ZoneSerial = updatedCustomer.ZoneSerial;
            existingCustomer.AreaSerial = updatedCustomer.AreaSerial;
            existingCustomer.StreetSerial = updatedCustomer.StreetSerial;
            existingCustomer.Notes = updatedCustomer.Notes;

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
