using InvDataAccess.Models;


namespace InvDataAccess.Data
{
  public interface IRecipeData
  {
     Task CreateNewRecipeAsync(RecipeModel recipe);
     Task<IEnumerable<RecipeModel>> GetRecipeDetailsByID(int id);
     Task UpdateRecipe(RecipeModel recipe);

  }
}