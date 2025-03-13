using InvDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvDataAccess.Data
{
  public interface IMaterialProductData
  {
    Task<IEnumerable<MaterialProductModel>> GetMaterialProductByCustomerID(int m_customerID);
  }
}