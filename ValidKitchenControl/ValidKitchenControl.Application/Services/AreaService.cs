using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidKitchenControl.Domain.Entities;
using ValidKitchenControl.Domain.Repositories;
using ValidKitchenControl.Domain.Services;

namespace ValidKitchenControl.Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task CreateAsync(Area area)
        {
            // Adiciona lógica de validação ou regras de negócio
            await _areaRepository.AddAsync(area);
        }

        public async Task<IEnumerable<Area>> GetAllAsync()
        {
            return await _areaRepository.GetAllAsync();
        }

        public async Task<Area> GetByIdAsync(int id)
        {
            return await _areaRepository.GetByIdAsync(id);
        }

        public async Task<Area> GetByNameAsync(string name)
        {
            return await _areaRepository.GetByNameAsync(name);

        }
    }
}
