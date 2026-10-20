using a_job_finder.Interfaces;
using Dapper;
using MySqlConnector;
using System.Data;

namespace a_job_finder.Services;

public class DbService : IDbService
{
    private readonly IConfiguration _configuration;

    public DbService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private IDbConnection GetConnection()
    {
        return new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<IEnumerable<T>> Query<T>(string spname, object parameter)
    {
        using var connection = GetConnection();
        return await connection.QueryAsync<T>(spname, parameter, commandType: CommandType.StoredProcedure);
    }

    public async Task<T> QueryFirstOrDefault<T>(string spname, object parameter)
    {
        using var connection = GetConnection();
        return await connection.QueryFirstOrDefaultAsync<T>(spname, parameter, commandType: CommandType.StoredProcedure);
    }

    public async Task<List<T>> QueryMultiple<T>(string spname, object parameter)
    {
        using var connection = GetConnection();
        var result = await connection.QueryAsync<T>(spname, parameter, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }

    public async Task<bool> VerifyConnection()
    {
        try
        {
            using var connection = GetConnection();
            var result = await connection.QueryFirstOrDefaultAsync<int>("SELECT 1;", commandType: CommandType.Text);
            return result == 1;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
