using CK.DTO;
using CK.Models;
using CK.Models.CKPro;
using CK.Models.TopSoft;
using CK.Repo.Base;
using CK.ViewModel;
using Microsoft.Data.SqlClient;
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
            var phone1 = await _TopSoftContext.CustomerCodes.Where(x => (x.Phone1 == Customer.Phone1 || x.Phone1 == Customer.Phone2 || x.Phone1 == Customer.Phone3)
            && x.CustomerCode1 != Customer.CustomerCode1 && !string.IsNullOrEmpty(x.Phone1)).ToListAsync();
            var phone2 = await _TopSoftContext.CustomerCodes.Where(x => (x.Phone2 == Customer.Phone1 || x.Phone2 == Customer.Phone2 || x.Phone2 == Customer.Phone3)
            && x.CustomerCode1 != Customer.CustomerCode1 && !string.IsNullOrEmpty(x.Phone2) ).ToListAsync();
            var phone3 = await _TopSoftContext.CustomerCodes.Where(x => (x.Phone3 == Customer.Phone1 || x.Phone3 == Customer.Phone2 || x.Phone3 == Customer.Phone3)
            && x.CustomerCode1 != Customer.CustomerCode1 && !string.IsNullOrEmpty(x.Phone3)).ToListAsync();
            if (phone1.Count > 0 || phone2.Count > 0 || phone3.Count > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<CustomerCode?> GetCustomerCodeByIdAsync(int id)
        {
            return await _TopSoftContext.CustomerCodes.FindAsync(id);
        }
        public List<SalesOrderDTO> GetAllCustomerCodes()
        {
            SalesOrderDTO Conn = new SalesOrderDTO();
            using (SqlConnection connection = new SqlConnection(Conn.TopSoftConnection))
            {
                string Query = "SELECT * from RptCustomer    ";
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    connection.Open(); // Open the connection
                    var vi = new List<SalesOrderDTO>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        SalesOrderDTO si = new SalesOrderDTO()
                        {
                            ZoneCode = test["ZoneCode"] == DBNull.Value ? "" : test["ZoneCode"].ToString(),
                            ZoneCode2 = test["ZoneCode2"] == DBNull.Value ? "" : test["ZoneCode2"].ToString(),
                            ZoneName = test["ZoneName"] == DBNull.Value ? string.Empty : test["ZoneName"].ToString(),
                            AreaCode = test["AreaCode"] == DBNull.Value ? "" : test["AreaCode"].ToString(),
                            AreaCode2 = test["AreaCode2"] == DBNull.Value ? "" : test["AreaCode2"].ToString(),
                            AreaName = test["AreaName"] == DBNull.Value ? string.Empty : test["AreaName"].ToString(),
                            StreetCode = test["StreetCode"] == DBNull.Value ? "" : test["StreetCode"].ToString(),
                            StreetCode2 = test["StreetCode2"] == DBNull.Value ? "" : test["StreetCode2"].ToString(),
                            StreetName = test["StreetName"] == DBNull.Value ? string.Empty : test["StreetName"].ToString(),
                            DeliveryTime = test["DeliveryTime"] == DBNull.Value ? "" : test["DeliveryTime"].ToString(),
                            ServiceCost = test["ServiceCost"] == DBNull.Value ? "" : Convert.ToDouble(test["ServiceCost"]).ToString("#,##0.00"),
                            BranchCode = test["BranchCode"] == DBNull.Value ? "" : test["BranchCode"].ToString(),
                            BranchIdR = test["BranchIdR"] == DBNull.Value ? "" : test["BranchIdR"].ToString(),
                            BranchIdD = test["BranchIdD"] == DBNull.Value ? "" : test["BranchIdD"].ToString(),
                            BranchName = test["BranchName"] == DBNull.Value ? string.Empty : test["BranchName"].ToString(),
                            CustomerCode = test["CustomerCode"] == DBNull.Value ? string.Empty : test["CustomerCode"].ToString(),
                            CustomerName = test["CustomerName"] == DBNull.Value ? string.Empty : test["CustomerName"].ToString(),
                            Phone1 = test["Phone1"] == DBNull.Value ? string.Empty : test["Phone1"].ToString(),
                            Phone2 = test["Phone2"] == DBNull.Value ? string.Empty : test["Phone2"].ToString(),
                            Address1 = test["Address1"] == DBNull.Value ? string.Empty : test["Address1"].ToString(),
                            Address2 = test["Address2"] == DBNull.Value ? string.Empty : test["Address2"].ToString(),
                        };

                        vi.Add(si);
                    }
                    return vi;
                }
            }
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
