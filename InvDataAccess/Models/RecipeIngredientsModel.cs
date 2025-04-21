namespace InvDataAccess.Models
{
public class RecipeIngredientsModel
{
    public int id { get; set; }
    public int m_productID { get; set; }
    public int quantity { get; set; }
    public string ?unit { get; set; }
    public string ?product_name { get; set; }
}
}