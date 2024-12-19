using InvDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvDataAccess.Data
{
  public interface IOrderData
  {
    Task<IEnumerable<OrderModel>> GetAllOrders();
    Task CreateNewOrder(OrderModel order);
    // Task DeleteOrder(int p_OrderId);
  }
}