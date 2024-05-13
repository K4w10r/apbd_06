using Microsoft.Data.SqlClient;
using TestFromOtherGroup.Models.DTOs;

namespace TestFromOtherGroup.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly IConfiguration _configuration;
    public async Task<bool> DoesCustomerExist(int id)
    {
        var query = "SELECT 1 FROM Customer WHERE ID = @ID";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);
        
        await connection.OpenAsync();
        
        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<List<OrderDto>> FindCustomerOrdersById(int id)
    {
        var query = "SELECT * FROM Order WHERE CustomerID = @ID";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();
    }
}