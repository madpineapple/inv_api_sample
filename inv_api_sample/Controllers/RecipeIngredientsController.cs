using Microsoft.AspNetCore.Mvc;

namespace inv_api_sample.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class RecipeIngredientsController : Controller
  {
    private readonly IRecipeIngredients  _recipeData;

    public RecipeIngredientsController(IRecipeIngredients  recipeData)
    {
      _recipeData = recipeData;
    }

   
      [HttpGet]
    public async Task<IResult> GetRecipeIngredientsByID(int id)
    {
      try
      {
        var recipe = await _recipeData.GetRecipeDetailsByID(id);
        if (recipe == null)
        {
          return Results.NotFound();
        }
        return Results.Ok(recipe);
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
  
  }
}