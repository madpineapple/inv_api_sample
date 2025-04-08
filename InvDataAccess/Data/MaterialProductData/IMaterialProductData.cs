using InvDataAccess.Models;


namespace InvDataAccess.Data
{
  public interface IMaterialProductData
  {
    Task<IEnumerable<MaterialProductModel>> GetMaterialProductByCustomerID(int m_customerID);
  }
}