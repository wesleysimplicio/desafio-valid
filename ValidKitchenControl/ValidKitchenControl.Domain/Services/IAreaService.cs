using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidKitchenControl.Domain.Entities;

namespace ValidKitchenControl.Domain.Services
{
    public interface IAreaService
    {
        Task CreateAsync(Area order);
        Task<Area> GetByIdAsync(int id);
        Task<Area> GetByNameAsync(string name);
        Task<IEnumerable<Area>> GetAllAsync();
    }
}
