using Microsoft.AspNetCore.Mvc;

namespace inv_api_sample.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class RecipeController : Controller
  {
    private readonly IRecipeData _recipeData;

    public RecipeController(IRecipeData recipeData)
    {
      _recipeData = recipeData;
    }

   [HttpPost]
    public async Task<IActionResult> CreateRecipe(RecipeModel recipe)
    {
         if (!ModelState.IsValid)
    {
        return BadRequest(new { message = "Invalid data", errors = ModelState });
    }

        try
        {
            await _recipeData.CreateNewRecipeAsync(recipe);
            return Ok(new { message = "Recipe created successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
      [HttpGet("{id}")]
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
        [HttpPut]
    public async Task<IResult> UpdateRecipe(RecipeModel recipe)
    {
      try
      {
        await _recipeData.UpdateRecipe(recipe);
        return Results.Ok(recipe);
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    }
  
  }
}