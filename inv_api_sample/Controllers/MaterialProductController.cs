using Microsoft.AspNetCore.Mvc;
using InvDataAccess.Models;
namespace inv_api_sample.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MaterialProductController: Controller
  {
    private readonly IMaterialProductData _materialProductData;

     public MaterialProductController(IMaterialProductData materialProductData)
    {
       _materialProductData = materialProductData;;
    }

    [HttpGet]
    public async Task<IResult> GetMaterialProductByCustomerID(int m_customerID)
    {
      try{
        return Results.Ok(await _materialProductData.GetMaterialProductByCustomerID(m_customerID));
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }

  }
}