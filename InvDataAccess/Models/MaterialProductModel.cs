namespace InvDataAccess.Models
{
    public class MaterialProductModel
  {
    public int ?m_productID{get;set;}
    public string ?m_productName{get;set;}
    public int m_customerID{get;set;}
    public string ?sku{get;set;}
    public DateTime created_at{get;set;}
  }
}