using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvDataAccess.DBAccess
{
  public interface IMySQLDataAccess
  {
    Task<IEnumerable<T>> LoadData<T, U>( string storedProcedure, U parameters, string connectionId = "Default");
    Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default");

  }
}
