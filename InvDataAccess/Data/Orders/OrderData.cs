using InvDataAccess.DBAccess;
using InvDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace InvDataAccess.Data
{
  public class OrderData :IOrderData
  {
    public readonly IMySQLDataAccess _db;

    public OrderData(IMySQLDataAccess db)
    {
      _db = db;
    }

  public async Task<IEnumerable<OrderModel>> GetAllOrders()
  {
    var orders = await _db.LoadData<OrderModel, dynamic>("GetAllOrders", new { });
    var orderDetails = await _db.LoadData<OrderDetailsModel, dynamic>("GetAllOrderDetails", new { });

    foreach (var order in orders)
    {
      order.OrderWithDetails = orderDetails.Where(x => x.p_OrderID ==order.p_OrderID).ToList();
    }
    return orders;
  }

  public Task CreateNewOrder(OrderModel order) =>
    _db.SaveData("create_new_order", new{
      order.p_OrderID,
      order.p_CustomerId,
      order.p_OrderDate,
      order.p_OrderStatus,
      order.p_Price,
      p_OrderDetailsJson = order.p_OrderDetailsJson,
    });
  

}
}