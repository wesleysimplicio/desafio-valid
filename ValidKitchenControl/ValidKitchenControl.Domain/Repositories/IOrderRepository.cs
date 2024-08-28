using ValidKitchenControl.Domain.Entities;

namespace ValidKitchenControl.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateAsync(Order updatedOrder);
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
    }
}
