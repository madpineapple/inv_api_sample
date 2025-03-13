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
  public class OrderModel
  {
    public int p_OrderID { get; set; }
    public int p_CustomerId { get; set; }
    public DateTime p_OrderDate { get; set; }
    public string p_OrderStatus { get; set; }
    public decimal p_Price { get; set; }
    public int p_m_productID {get; set; }
    public int p_quantity{get; set; }
  }
}