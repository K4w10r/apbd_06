using Microsoft.AspNetCore.Mvc;

namespace abpd_06.Repositories;
using abpd_06.Models.DTO_s;

public interface IWarehouseRepository
{
    Task<bool> DoesWarehouseExist(int id);
    Task<bool> DoesProductExist(int id);
    Task<bool> HasTheOrderBeenFulfilled(int id);
    Task<bool> CheckIfOrderInProductWareHouse(int orderId);
    Task<IActionResult> FulfillOrder(int id);
    Task<int> InsertRecordIntoProductWarehouse(int productId);

}