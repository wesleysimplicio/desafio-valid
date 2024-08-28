﻿using ValidKitchenControl.Domain.Entities;

namespace ValidKitchenControl.Domain.Services
{
    public interface IOrderService
    {
        Task CreateAsync(Order order);
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
    }
}
