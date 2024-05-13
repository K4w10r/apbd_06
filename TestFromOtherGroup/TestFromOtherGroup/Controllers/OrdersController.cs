using Microsoft.AspNetCore.Mvc;
using TestFromOtherGroup.Repositories;

namespace TestFromOtherGroup.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrdersRepository _ordersRepository;

    public OrdersController(IOrdersRepository ordersRepository)
    {
        this._ordersRepository = ordersRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(int customerId)
    {
        if (!await _ordersRepository.DoesCustomerExist(customerId))
        {
            return NotFound($"Customer with given Id: {customerId} does not exist");
        }

        var orders = _ordersRepository.FindCustomerOrdersById(customerId);
        return Ok(orders);
    }
    
}