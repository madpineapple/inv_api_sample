using System;
using System.Collections.Generic;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvDataAccess.Models
{
  public class OrderDTO
  {
    public int p_orderID { get; set; }
    public string p_customerName { get; set; }
    public DateTime p_orderDate { get; set; }
    public string p_orderStatus { get; set; }
    public List<OrderDetailDTO> OrderDetails { get; set; }
}
public class OrderDetailDTO
{
    public int p_orderDetailID { get; set; }
    public int p_OrderID { get; set; }
    public int p_product_id { get; set; }
    public string p_product_name { get; set; }
    public int p_quantity { get; set; }
    public decimal p_price { get; set; }
}
}