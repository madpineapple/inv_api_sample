using Microsoft.AspNetCore.Mvc;
using InvDataAccess.Models;
namespace inv_api_sample.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OrderController : Controller
  {
    private readonly IOrderData _orderData;

    public OrderController(IOrderData orderData)
    {
      _orderData = orderData;
    }

    [HttpGet]
    public async Task<IResult> GetAllOrders()
    {
      try
      {
        return Results.Ok(await _orderData.GetAllOrders());
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }

    [HttpPost]
    public async Task<IResult> CreateNewOrder(OrderModel order)
    {
      try
      {
        await _orderData.CreateNewOrder(order);
        return Results.Ok(order);
      }
       catch(Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
  }
}