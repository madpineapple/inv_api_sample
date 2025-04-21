using InvDataAccess.Models;


namespace InvDataAccess.Data
{
  public interface IRecipeIngredients
  {
     Task<IEnumerable<RecipeIngredientsModel>> GetRecipeDetailsByID(int id);

  }
}