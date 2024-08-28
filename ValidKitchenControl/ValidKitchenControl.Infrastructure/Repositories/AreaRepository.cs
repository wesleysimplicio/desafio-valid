using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidKitchenControl.Domain.Entities;
using ValidKitchenControl.Domain.Repositories;

namespace ValidKitchenControl.Infrastructure.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly List<Area> _orders = new()
        {
            new Area(1,"fritos"),
            new Area(2,"grelhados"),
            new Area(3,"saladas"),
            new Area(4,"bebidas"),
            new Area(5,"sobremesa")
        };

        public Task AddAsync(Area order)
        {
            _orders.Add(order);
            return Task.CompletedTask;
        }

        public Task<Area> GetByIdAsync(int id) =>
            Task.FromResult(_orders.FirstOrDefault(p => p.Id == id));

        public Task<Area> GetByNameAsync(string name) =>
               Task.FromResult(_orders.FirstOrDefault(p => p.Name == name.ToLower()));

        public Task<IEnumerable<Area>> GetAllAsync() =>
            Task.FromResult<IEnumerable<Area>>(_orders);
    }
}
