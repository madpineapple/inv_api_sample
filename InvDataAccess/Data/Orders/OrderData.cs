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

  public async Task<IEnumerable<OrderDTO>> GetAllOrders()
  {
    var ordersAndDetails  = await _db.LoadData<dynamic, dynamic>("GetAllOrdersAndDetails", new { });

   var orders = ordersAndDetails
    .GroupBy(x => x.p_orderID) // Group by orderID to organize the details
    .Select(group => new OrderDTO
    {
        p_orderID = group.Key,
        p_customerName = group.FirstOrDefault()?.p_customerName ?? "", // Handle null customerName
        p_orderDate = group.FirstOrDefault()?.p_orderDate ?? DateTime.MinValue, // Handle null orderDate
        p_orderStatus = group.FirstOrDefault()?.p_orderStatus ?? "", // Handle null orderStatus
        OrderDetails = group.Select(details => new OrderDetailDTO
        {
            p_orderDetailID = details.p_orderDetailID ?? 0, // Handle null orderDetailID
            p_OrderID = details.p_OrderID ?? 0, // Handle null p_OrderID
            p_product_id = details.p_product_id ?? 0, // Handle null product_id
            p_product_name = details.p_product_name ?? "", // Handle null product_name
            p_quantity = details.p_quantity ?? 0, // Handle null quantity
            p_price = details.p_price ?? 0m // Handle null price
        }).ToList()
    }).ToList();

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