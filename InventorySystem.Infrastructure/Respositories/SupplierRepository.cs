using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastructure.Respositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly InventorySystemDb _context;
        public SupplierRepository(InventorySystemDb context)
        {
            _context = context;
        }
        public async Task<Supplier> CreateSupplierAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier?> GetByIdAsync(Guid supplierId)
        {
            return await _context.Suppliers.FindAsync(supplierId);
        }

        public async Task<Supplier?> GetByName(string name)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<bool> UpdateAsync(Guid supplierId, Supplier supplierUpdate)
        {
            var supplier = await GetByIdAsync(supplierId);

            if (supplier == null)
            {
                return false;
            }

            try
            {
                _context.Entry(supplier).CurrentValues.SetValues(supplierUpdate);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;    
            }
        }
    }
}
