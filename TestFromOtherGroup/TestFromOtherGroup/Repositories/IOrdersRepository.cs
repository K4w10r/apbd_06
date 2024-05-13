using TestFromOtherGroup.Models.DTOs;

namespace TestFromOtherGroup.Repositories;

public interface IOrdersRepository
{
    Task<bool> DoesCustomerExist(int id);
    Task<List<OrderDto>> FindCustomerOrdersById(int id);
    
}