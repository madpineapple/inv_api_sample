using InvDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvDataAccess.Data
{
  public interface IProductData
  {
    Task<IEnumerable<ProductModel>> GetAllProductData();
    Task DeleteProduct(int item_int);
    Task InsertProduct(ProductModel product);
    Task UpdateProduct(ProductModel product);
    Task<ProductModel?> GetProductById(int item_int);
    Task<List<ItemTotalDTO>> GetProductQuantity(string product_name);
    Task<List<ProductModel?>> GetProductInfo(string product_name);
    Task<List<ProductModel?>> GetProductExpiry(string product_name, string expiry_info, string ? p_cutoff);
    Task<List<ProductModel?>> GetProductByLot(string product_name, string product_lot_number);

    Task<List<ProductModel?>> GetProductByLocation(string product_name, string product_location);

  }
}
