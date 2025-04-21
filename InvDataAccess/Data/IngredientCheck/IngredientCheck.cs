using System.Text.Json;
using InvDataAccess.DBAccess;
using InvDataAccess.Models;

namespace InvDataAccess.Data
{
  public class IngredientCheck : IIngredientCheck
  {
    public readonly IMySQLDataAccess _db;

    public IngredientCheck(IMySQLDataAccess db)
    {
      _db = db;
    }
   
  public async Task<IEnumerable<IngredientCheckModel>> CheckRecipe(List<IngredientInputModel> ingredients){
        var jsonString = JsonSerializer.Serialize(ingredients);

     var results = await _db.LoadData<IngredientCheckModel, dynamic>(
        "checkIngredientLevel",
        new { p_productName = jsonString }
    );

    return results;
    
  }

  }
}