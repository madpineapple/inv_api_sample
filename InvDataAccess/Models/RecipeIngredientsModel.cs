using System;
using System.Collections.Generic;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvDataAccess.Models
{
public class RecipeIngredientsModel
{
    public int ProductID { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; }
    public string ProductName { get; set; }
}
}