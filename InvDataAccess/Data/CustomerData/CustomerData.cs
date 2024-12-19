using InvDataAccess.DBAccess;
using InvDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvDataAccess.Data
{
  public class CustomerData : ICustomerData
  {
    public readonly IMySQLDataAccess _db;

    public CustomerData(IMySQLDataAccess db)
    {
      _db = db;
    }
    public Task<IEnumerable<CustomerModel>> GetAllCustomerData()
    {
      return _db.LoadData<CustomerModel, dynamic>("get_all_customers", new { });
    }
    // public async Task<CustomerModel?> GetCustomerById(int id)
    // {
    //   var results = await _db.LoadData<ustomerModel, dynamic>("GetProductByID", new { ProdItemID = id });
    //   return results.FirstOrDefault();
    // }
    public Task InsertCustomer(CustomerModel customer) =>
      _db.SaveData("create_new_customer", new { 
        customer.p_CustomerId,
        customer.p_CustomerName,
        customer.p_ContactInfo
      });

    public Task UpdateCustomer(CustomerModel customer) =>
      _db.SaveData("UpdateCustomerInfo", customer);

    public Task DeleteCustomer(int id) => _db.SaveData("delete_customer", new { p_CustomerId = id });
  }
}