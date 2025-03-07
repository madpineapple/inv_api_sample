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
    Task<IEnumerable<OrderDTO>> GetAllOrders();
    Task CreateNewOrder(OrderModel order);
  }
}