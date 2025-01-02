using System;
using System.Collections.Generic;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvDataAccess.Models
{
  public class ProductModel
  {
   
    public int ProdItemID { get; set; }
    public string ProdItemName { get; set; } = string.Empty;
    public string ProdItemNum {  get; set; } = string.Empty;
    public string ProdItemLotNum {  get; set; } = string.Empty;
    public string ProdVendorLotNum { get; set; } = string.Empty;
    public string ProdItemLoc { get; set; } = string.Empty;
    public DateTime ProdExpDate { get; set; }
    public float ProdQuantity { get; set; }
    public float ProdWeight { get; set; }

  }
}
