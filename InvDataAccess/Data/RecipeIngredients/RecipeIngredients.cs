using System.Text.Json;
using InvDataAccess.DBAccess;
using InvDataAccess.Models;

namespace InvDataAccess.Data
{
  public class RecipeIngredients : IRecipeIngredients 
  {
    public readonly IMySQLDataAccess _db;

    public RecipeIngredients (IMySQLDataAccess db)
    {
      _db = db;
    }
   
   
  public async Task<IEnumerable<RecipeIngredientsModel>>  GetRecipeDetailsByID(int id)
  {
      var results = await _db.LoadData<RecipeIngredientsModel, dynamic>("GetIngredientListByRecipeID", new { p_m_productID = id });
      return results;
  }

  }
}