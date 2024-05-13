using abpd_06.Repositories;
using Microsoft.AspNetCore.Mvc;
using abpd_06.Models.DTO_s;

namespace abpd_06.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private IWarehouseRepository _warehouseRepository;

    public WarehouseController(IWarehouseRepository repository)
    {
        _warehouseRepository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductToWarehouse(AddNewProduct newProduct)
    {
        if (!await _warehouseRepository.DoesProductExist(newProduct.IdProduct))
        {
            return NotFound();
        }

        int orderId = -1;
        try
        {
            orderId = await _warehouseRepository.CheckIfOrderMatchesTheRequest(newProduct.IdProduct, newProduct.Amount);
        }
        catch (Exception e)
        {
            return NotFound();}

        if (orderId != -1 && !await _warehouseRepository.HasTheOrderBeenCompleted(orderId))
        {
            return BadRequest();
        }

        double price = newProduct.Amount * await _warehouseRepository.GetProductPrice(newProduct.IdProduct);
        await _warehouseRepository.InsertRecordIntoProductWarehouse(
            newProduct.IdProduct, newProduct.IdWarehouse, orderId, newProduct.Amount, price);

        return Ok();
    }
}