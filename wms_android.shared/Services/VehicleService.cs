using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wms_android.shared.Data;
using wms_android.shared.Interfaces;
using wms_android.shared.Models;
using Microsoft.EntityFrameworkCore;

namespace wms_android.shared.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly AppDbContext _context;

        public VehicleService(AppDbContext context)
        {
            _context = context;
        }

        public List<Vehicle> GetVehicles()
        {
            return _context.Vehicles.ToList();
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(Guid id)
        {
            return await _context.Vehicles.FindAsync(id) 
                ?? throw new KeyNotFoundException($"Vehicle with ID {id} not found");
        }

        public List<Drivers> GetDrivers()
        {
            return _context.Drivers.ToList();
        }
    }
}
