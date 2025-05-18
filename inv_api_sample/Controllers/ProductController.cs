using Microsoft.AspNetCore.Mvc;
using InvDataAccess.Models;
namespace inv_api_sample.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductController : Controller
  {
    private readonly IProductData _productData;

    public ProductController(IProductData productData)
    {
      _productData = productData;
    }

    [HttpGet]
    public async Task<IResult> GetProducts()
    {
      try
      {
        return Results.Ok(await _productData.GetAllProductData());
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
    [HttpGet("{item_int}")]
    public async Task<IResult> GetProduct(int item_int)
    {
      try
      {
        var products = await _productData.GetProductById(item_int);
        if (products == null)
        {
          return Results.NotFound();
        }
        return Results.Ok(products);
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
        [HttpGet("info/{product_name}")]
    public async Task<IResult> GetProductInfo(string product_name)
    {
      try
      {
        var products = await _productData.GetProductInfo(product_name);
        return Results.Ok(products);
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
        [HttpGet("countinfo/{product_name}")]
    public async Task<IResult> GetProductQuantity(string product_name)
    {
      try
      {
        var products = await _productData.GetProductQuantity(product_name);
        return Results.Ok(products);
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
    [HttpGet("expiryInfo/{product_name}")]
    public async Task<IResult> GetProductExpiry(string product_name, string expiry_info, string? p_cutoff)
    {
      try
      {
        var products = await _productData.GetProductExpiry(product_name, expiry_info, p_cutoff);
        return Results.Ok(products);
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
        [HttpGet("lotInfo/{product_name}")]
    public async Task<IResult> GetProductByLot(string product_name, string product_lot_number)
    {
      try
      {
        var products = await _productData.GetProductByLot(product_name, product_lot_number);
        return Results.Ok(products);
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
         [HttpGet("locationInfo/{product_name}")]
    public async Task<IResult> GetProductByLocation(string product_name, string product_location)
    {
      try
      {
        var products = await _productData.GetProductByLocation(product_name, product_location);
        return Results.Ok(products);
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
    [HttpPost]
    public async Task<IResult> InsertProduct(ProductModel product)
    {
      try
      {
        await _productData.InsertProduct(product);
        return Results.Ok(product);
      }
      catch (Exception ex) 
      {
        return Results.Problem(ex.Message);
      }
    }
    [HttpPut]
    public async Task<IResult> UpdateProduct(ProductModel product)
    {
      try
      {
        await _productData.UpdateProduct(product);
        return Results.Ok(product);
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
    [HttpDelete("{id}")]
    public async Task<IResult> DeleteProduct(int id)
    {
      try
      {
        await _productData.DeleteProduct(id);
        return Results.Ok();
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
  }
}
