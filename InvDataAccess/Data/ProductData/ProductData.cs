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
      _db.SaveData("InsertProduct", new
      {
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

    public async Task<List<ItemTotalDTO?>> GetProductQuantity(string product_name)
    {
      var result = await _db.LoadData<ItemTotalDTO?, dynamic>("get_product_quantity_by_name", new { p_product_name = product_name });
      return result.ToList();
    }
    public async Task<List<ProductModel?>> GetProductInfo(string product_name)
    {
      var result = await _db.LoadData<ProductModel?, dynamic>("get_product_info_by_name", new { p_product_name = product_name });
      return result.ToList();
    }
    public async Task<List<ProductModel?>> GetProductExpiry(string product_name, string expiry_info, string? p_cutoff)
    {
      var result = await _db.LoadData<ProductModel?, dynamic>("get_product_info_by_expiry", new
      {
        p_product_name = product_name,
        p_expiry_mode = expiry_info,
        p_cutoff
      });
      return result.ToList();
    }
    public async Task<List<ProductModel?>> GetProductByLot(string product_name, string product_lot_number)
    {
      var result = await _db.LoadData<ProductModel?, dynamic>("get_product_info_by_lot", new
      {
        p_product_name = product_name,
        p_lot_number = product_lot_number
      });
      return result.ToList();
    }
    public async Task<List<ProductModel?>> GetProductByLocation(string product_name, string product_location)
     {
       var result = await _db.LoadData<ProductModel?, dynamic>("get_product_info_by_location", new { p_product_name = product_name ,
         p_location = product_location});
        return result.ToList();
      }
  }
}
