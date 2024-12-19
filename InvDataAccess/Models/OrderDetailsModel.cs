namespace InvDataAccess.Models
{
  public class OrderDetailsModel
  {
    public int p_OrderDetailID { get; set; }
    public int p_OrderID { get; set; }
    public int ProdItemID { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
  }
}