using Microsoft.AspNetCore.Mvc;
using InvDataAccess.Models;
namespace inv_api_sample.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CustomerController : Controller
  {
    private readonly ICustomerData _customerData;

    public CustomerController(ICustomerData customerData)
    {
      _customerData = customerData;
    }

    [HttpGet]
    public async Task<IResult> GetCustomers()
    {
      try
      {
        return Results.Ok(await _customerData.GetAllCustomerData());
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }

    [HttpPost]
    public async Task<IResult> InsertCustomer( CustomerModel customer)
    {
      try
      {
        await _customerData.InsertCustomer(customer);
        return Results.Ok(customer);
      }
      catch(Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }

    [HttpPut]
    public async Task<IResult> UpdateCustomer(CustomerModel customer)
    {
      try
      {
        await _customerData.UpdateCustomer(customer);
        return Results.Ok(customer);
      }
       catch(Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
    
    [HttpDelete("{id}")]
    public async Task<IResult> DeleteCustomer(int id)
    {
      try
      {
        await _customerData.DeleteCustomer(id);
        return Results.Ok();
      }
       catch(Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
    }
}