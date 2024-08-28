using ValidKitchenControl.Domain.Entities;
using ValidKitchenControl.Domain.Repositories;
using ValidKitchenControl.Domain.Services;

namespace ValidKitchenControl.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAreaService _areaService;

        public OrderService(IOrderRepository orderRepository, IAreaService areaService)
        {
            _orderRepository = orderRepository;
            _areaService = areaService;
        }

        public async Task CreateAsync(Order order)
        {
            var area = await _areaService.GetByNameAsync(order.Area);
            if(area == null) new Exception("Área inválida.");

            // Adiciona lógica de validação ou regras de negócio
            await _orderRepository.AddAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);

        }
    }
}
