using abpd_06.Models.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace abpd_06.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public async Task<bool> DoesProductExist(int id)
    {
        string query = "SELECT 1 FROM PRODUCT WHERE ID=@ID";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnectionString"));
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);
        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<bool> DoesWarehouseExist(int id)
    {
        string query = "SELECT 1 FROM WAREHOUSE WHERE ID=@ID";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnectionString"));
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);
        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public Task<bool> HasTheOrderBeenFulfilled(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckIfOrderInProductWareHouse(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> FulfillOrder(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> InsertRecordIntoProductWarehouse(int productId)
    {
        throw new NotImplementedException();
    }
}