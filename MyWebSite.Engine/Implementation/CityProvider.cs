using MyWebSite.Model.Interfaces;
using Microsoft.Data.SqlClient;
using MyWebSite.Model.DataModel;
using Dapper;

namespace MyWebSite.Model.Implementation
{
    public class CityProvider: ICityProvider
    {
        IDbConnection connection;
        public CityProvider(IDbConnection connection)
        {
            this.connection = connection;
        }

        /*
        public async Task<List<City>> GetCities()
        {
            List<City> result = new List<City>();
            using (var connection = this.connection.CreateConnection())
            { 
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select CityId, CityName from city";
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new City()
                        {
                            CityId = reader.GetInt32(0),
                            CityName = reader.GetString(1)
                        }); ;
                    }
                }
            }
            return result;
        }
        */

        public async Task<List<City>> GetCities()
        {
            using (var connection = this.connection.CreateConnection())
            {
                var result = await connection.QueryAsync<City>(
                    "select CutyId, CityName from City");
                return result.ToList(); //полчучаем IEnumerable<City>->список
            }
        }

        /*
        public async Task<City> GetCity(int id)
        {
            using (var connection = this.connection.CreateConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from city where cityid = @id";
                SqlParameter parameter = new SqlParameter()
                {
                    ParameterName = "id",
                    Value = id,
                    SqlDbType = System.Data.SqlDbType.Int
                };
                command.Parameters.Add(parameter);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows && await reader.ReadAsync())
                    {
                        return new City()
                        {
                            CityId = reader.GetInt32(0),
                            CityName = reader.GetString(1)
                        };
                    }
                }
            }
            return new City();
        }
        */
        public async Task<City> GetCity(int id)
        {
            using (var connection = this.connection.CreateConnection())
            {
                return await connection.QueryFirstAsync<City>(
                    "select CutyId, CityName from City where CityId = @idparam",
                    new {idparam = id});
            }
        }

        public async Task<bool> UpdateCity(City city)
        {
            using (var connection = this.connection.CreateConnection())
            {
                using var transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText =
                    "update city set CityName = @cityname where cityid = @id";
                command.Parameters.AddWithValue("@id", city.CityId);
                command.Parameters.AddWithValue("@cityname", city.CityName);
                int rows = await command.ExecuteNonQueryAsync();
                if (rows != 1)
                {
                    transaction.Rollback();
                    return false;
                }

                transaction.Commit();
                return true;
            }
        }

        public async Task<bool> AddCity(City city)
        {
            using (var connection = this.connection.CreateConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "insert into city (CityName) values (@cityname)";
                command.Parameters.AddWithValue("@cityname", city.CityName);
                int rows = await command.ExecuteNonQueryAsync();
                return rows == 1;
            }
        }

        public async Task<bool> DeleteCity(int id)
        {
            using (var connection = this.connection.CreateConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "delete from City where cityid = @id";
                command.Parameters.AddWithValue("@id", id);
                int rows = await command.ExecuteNonQueryAsync();
                return rows == 1;
            }
        }
        public async Task<bool> SaveCity(City city)
        {
            if (city.CityId == null)
                return await AddCity(city);
            else
                await UpdateCity(city);
            return true;
        }

    }   
}
