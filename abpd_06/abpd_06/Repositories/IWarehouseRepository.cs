using Microsoft.AspNetCore.Mvc;

namespace abpd_06.Repositories;
using abpd_06.Models.DTO_s;

public interface IWarehouseRepository
{
    Task<bool> DoesWarehouseExist(int id);
    Task<bool> DoesProductExist(int id);
    Task<bool> CheckIfOrderInProductWareHouse(int orderId);
    Task<IActionResult> FulfillOrder(int id);
    Task<bool> HasTheOrderBeenCompleted(int orderId);
    Task<int> CheckIfOrderMatchesTheRequest(int productId, int amount);
    Task InsertRecordIntoProductWarehouse(int productId, int warehouseId, int orderId, int amount, double price);
    Task<int> GetProductPrice(int productId);
}