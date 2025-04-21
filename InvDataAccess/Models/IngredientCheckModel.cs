public class IngredientCheckModel
{public string? pmProductName { get; set; }
    public decimal qtyRequired { get; set; }
    public decimal qtyAvailable { get; set; }
    public bool isEnough { get; set; }
}