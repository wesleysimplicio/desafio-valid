using System;
using ValidKitchenControl.Domain.Entities;
using ValidKitchenControl.Domain.Repositories;

namespace ValidKitchenControl.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new()
        {
             new Order(1,"Alface",1,"salada"),
        };

        public Task AddAsync(Order order)
        {
            order.Id = _orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;
            _orders.Add(order);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Order updatedOrder)
        {
            var existingOrder = _orders.FirstOrDefault(p => p.Id == updatedOrder.Id);
            if (existingOrder != null)
            {
                // Atualiza os detalhes do pedido existente
                existingOrder.Item = updatedOrder.Item;
                existingOrder.Quantity = updatedOrder.Quantity;
                existingOrder.Area = updatedOrder.Area;
            }
            return Task.CompletedTask;
        }

        public Task<Order> GetByIdAsync(int id) =>
            Task.FromResult(_orders.FirstOrDefault(p => p.Id == id));

        public Task<IEnumerable<Order>> GetAllAsync() =>
            Task.FromResult<IEnumerable<Order>>(_orders);

        // Exclui um pedido pelo ID

        public Task DeleteAsync(int id)
        {
            var orderToRemove = _orders.FirstOrDefault(p => p.Id == id);
            if (orderToRemove != null)
            {
                _orders.Remove(orderToRemove);
            }
            return Task.CompletedTask;
        }
    }
}
