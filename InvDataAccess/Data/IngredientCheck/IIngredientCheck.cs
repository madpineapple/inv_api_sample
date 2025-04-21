using InvDataAccess.Models;


namespace InvDataAccess.Data
{
  public interface IIngredientCheck
  {
     Task <IEnumerable<IngredientCheckModel>> CheckRecipe(List<IngredientInputModel>  recipe);
  }
}