using InvDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvDataAccess.Data
{
  public interface ICustomerData
  {
    Task<IEnumerable<CustomerModel>> GetAllCustomerData();
    Task DeleteCustomer(int p_CustomerId);
    Task InsertCustomer(CustomerModel customer);
    Task UpdateCustomer(CustomerModel customer);
    //Task<CustomerModel> GetCustomerById(int CustomerId);
  }
}