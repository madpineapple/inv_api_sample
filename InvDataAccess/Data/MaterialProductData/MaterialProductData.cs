using InvDataAccess.DBAccess;
using InvDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvDataAccess.Data
{
  public class MaterialProductData : IMaterialProductData
  {
    public readonly IMySQLDataAccess _db;

    public MaterialProductData(IMySQLDataAccess db)
    {
      _db = db;
    }
    public async Task<IEnumerable<MaterialProductModel>> GetMaterialProductByCustomerID(int m_customerID)
    {
       var results = await  _db.LoadData<MaterialProductModel, dynamic>("GetMaterialProductByCustomerID", new { m_customerID = m_customerID });
       return results;
    }
 
  }
}