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
  }
}
