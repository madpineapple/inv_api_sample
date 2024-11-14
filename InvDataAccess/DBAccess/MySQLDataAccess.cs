using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using MySqlConnector;

namespace InvDataAccess.DBAccess
{
  public class MySQLDataAccess: IMySQLDataAccess
  {
    private readonly IConfiguration _config;
    public MySQLDataAccess(IConfiguration config)
    {
      _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
      string storedProcedure,
      U parameters,
      string connectionId = "Default")
    {
      using IDbConnection connection = new MySqlConnector.MySqlConnection(_config.GetConnectionString(connectionId));
      return await connection.QueryAsync<T>(storedProcedure, parameters,
        commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(
  string storedProcedure,
  T parameters,
  string connectionId = "Default")
    {
      using IDbConnection connection = new MySqlConnector.MySqlConnection(_config.GetConnectionString(connectionId));

      await connection.ExecuteAsync(storedProcedure, parameters,
        commandType: CommandType.StoredProcedure);
    }
  }
}
