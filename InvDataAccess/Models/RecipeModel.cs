
namespace InvDataAccess.Models
{
  public class RecipeModel
  {
    public string? p_m_productName { get; set; }
    public int p_m_productID{ get; set; }

    public int p_customerID{ get; set; }
    public int p_quantity { get; set; }
    public string? p_unit { get; set; }
    public string? p_productName{ get; set; }
  }
}