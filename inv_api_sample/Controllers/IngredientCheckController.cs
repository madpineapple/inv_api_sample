using Microsoft.AspNetCore.Mvc;
using InvDataAccess.Models;
using System.Text.Json;
namespace inv_api_sample.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class IngredientCheckController : Controller
  {
    private readonly IIngredientCheck _ingredientData;

    public IngredientCheckController(IIngredientCheck ingredientData)
    {
      _ingredientData = ingredientData;
    }

  
    [HttpPost]
    public async Task<IResult> CheckForIngredients([FromBody] List<IngredientInputModel> ingredients)
    {
      try
      {
         var results = await _ingredientData.CheckRecipe(ingredients);
        return Results.Ok(results);
      }
       catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
  
  }
}