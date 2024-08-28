﻿    using System;
    using ValidKitchenControl.Domain.Entities;
    using ValidKitchenControl.Domain.Repositories;

    namespace ValidKitchenControl.Infrastructure.Repositories
    {
        public class OrderRepository : IOrderRepository
        {
            private readonly List<Order> _orders = new();

            public Task AddAsync(Order order)
            {
                _orders.Add(order);
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
