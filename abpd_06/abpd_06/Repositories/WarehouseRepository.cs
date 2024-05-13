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
        string query = "SELECT 1 FROM PRODUCT WHERE IdProduct=@ID";
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
        string query = "SELECT 1 FROM WAREHOUSE WHERE IdWarehouse=@ID";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnectionString"));
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);
        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<bool> HasTheOrderBeenCompleted(int orderId)
    {
        string query = "SELECT 1 FROM ORDER WHERE IdOrder=@ID AND FulfilledAt IS NOT NULL ";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnectionString"));
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", orderId);
        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public Task<bool> CheckIfOrderInProductWareHouse(int orderId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CheckIfOrderMatchesTheRequest(int productId, int amount)
    {
        string query = "SELECT IdOrder AS ID FROM ORDER WHERE IdProduct=@ID AND Amount=@AM";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnectionString"));
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", productId);
        command.Parameters.AddWithValue("@AM", amount);
        await connection.OpenAsync();

        var id = await command.ExecuteScalarAsync();

        if (id is null) throw new Exception();
	    
        return Convert.ToInt32(id);
    }
    

    public Task<IActionResult> FulfillOrder(int id)
    {
        throw new NotImplementedException();
    }

    public async Task InsertRecordIntoProductWarehouse(int productId, int warehouseId, int orderId, int amount, double price)
    {
        string date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string query = @"INSERT INTO Product_Warehouse (
                 IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
                VALUES (@WID, @PID, @OID, @AM, @PR, @DA)";
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@WID", warehouseId);
        command.Parameters.AddWithValue("@PID", productId);
        command.Parameters.AddWithValue("@OID", orderId);
        command.Parameters.AddWithValue("@AM", amount);
        command.Parameters.AddWithValue("@PR", price);
        command.Parameters.AddWithValue("@DA", date);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }

    public async Task<int> GetProductPrice(int productId)
    {
        string query = "SELECT Price FROM Product WHERE IdProduct=@ID";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnectionString"));
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", productId);
        await connection.OpenAsync();

        var price = await command.ExecuteScalarAsync();

        if (price is null) throw new Exception();
	    
        return Convert.ToInt32(price);
    }
}