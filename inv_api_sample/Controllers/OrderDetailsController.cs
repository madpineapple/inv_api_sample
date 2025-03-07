using Microsoft.AspNetCore.Mvc;
using InvDataAccess.Models;
namespace inv_api_sample.Controllers
{
  //  //[ApiController]
  // //[Route("[controller]")]
  // public class OrderDetailsController : Controller
  // {
  //   private readonly IOrderDetailsData _orderDetailData;
  //   public OrderDetailsController(IOrderDetailsData orderDetailsData){
  //     _orderDetailData = orderDetailsData;
  //   }

  //   [HttpGet]
  //   public async Task<IActionResult> GetOrderDetails()
  //   {
  //     try
  //     {
  //       return Results.Ok(await _orderDetailData.OrderDetails);
  //     }
  //     catch (Exception ex)
  //     {
  //       return Results.Problem(ex.Message);
  //     }
  //   }
  // }
}