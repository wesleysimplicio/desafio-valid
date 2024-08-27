using ValidKitchenControl.Domain.Entities;
using ValidKitchenControl.Domain.Repositories;
using ValidKitchenControl.Domain.Services;

namespace ValidKitchenControl.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateOrderAsync(Order order)
        {
            // Adiciona lógica de validação ou regras de negócio
            await _orderRepository.AddAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }
    }
}
