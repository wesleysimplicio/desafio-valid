using ValidKitchenControl.Domain.Entities;

namespace ValidKitchenControl.Domain.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}
