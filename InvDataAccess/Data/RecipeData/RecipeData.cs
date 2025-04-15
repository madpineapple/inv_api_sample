using InvDataAccess.DBAccess;
using InvDataAccess.Models;

namespace InvDataAccess.Data
{
  public class RecipeData : IRecipeData
  {
    public readonly IMySQLDataAccess _db;

    public RecipeData(IMySQLDataAccess db)
    {
      _db = db;
    }
   
    public async Task CreateNewRecipeAsync(RecipeModel recipe)
    {
        await _db.SaveData("create_new_recipe", new
        {  
        recipe.p_m_productName,
        recipe.p_customerID,
        recipe.p_quantity,
        recipe.p_unit,
        p_productName = recipe?.p_productName?.ToString()
        });
  }

  public async Task<IEnumerable<RecipeModel>>  GetRecipeDetailsByID(int id)
  {
      var results = await _db.LoadData<RecipeModel, dynamic>("GetIngredientListByMProductID", new { m_customerID = id });
      return results;
  }

  public Task UpdateRecipe(RecipeModel recipe) => _db.SaveData("UpdateMProductAndRecipe", recipe);
  
  }
}