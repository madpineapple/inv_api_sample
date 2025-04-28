namespace InvDataAccess.Models
{public class BuildOrderItemsModel
{
  public int build_order_item_id { get; set; } 
  public int build_order_id { get;set; }
  public int product_id { get;set; }
  public int required_qty { get;set; }
}

}