using InvDataAccess.DBAccess;
using InvDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InvDataAccess.Data
{
  public class ProductData : IProductData
  {
    private readonly IMySQLDataAccess _db;

    public ProductData(IMySQLDataAccess db)
    {
      _db = db;
    }
    public Task<IEnumerable<ProductModel>> GetAllProductData()
    {
      
      return _db.LoadData<ProductModel, dynamic>("GetAllProducts", new { });
    }
    public async Task<ProductModel?> GetProductById(int id)
    {
      var results = await _db.LoadData<ProductModel, dynamic>("GetProductByID", new { ProdItemID = id });
      return results.FirstOrDefault();
    }
    public Task InsertProduct(ProductModel product) =>
      _db.SaveData("InsertProduct", new { 
        product.ProdItemID,
        product.ProdItemName,
        product.ProdItemLoc,
        product.ProdItemNum,
        product.ProdItemLotNum,
        product.ProdVendorLotNum,
        product.ProdExpDate,
        product.ProdQuantity,
        product.ProdWeight
      });
    public Task UpdateProduct(ProductModel product) =>
      _db.SaveData("UpdateProductByID", product);

    public Task DeleteProduct(int id) => _db.SaveData("DeleteProduct", new { ProdItemID = id });
      
  }
}
