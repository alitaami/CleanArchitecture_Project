using Application.Repository;
using Domain;
using Insfrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insfrastructure.Repositories
{
    public class PropertyRepo : IPropertyRepo
    {
        private readonly ApplicationDbContext _db;

        public PropertyRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddNewAsync(Property property)
        {
            await _db.Properties.AddAsync(property);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var property = await _db.Properties
                                    .FirstOrDefaultAsync(p => p.Id == id);
            if (property != null)
            {
                _db.Properties.Remove(property);
                _db.SaveChanges();
            }
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _db.Properties
                            .ToListAsync();
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            return await _db.Properties
                            .Where(p => p.Id == id)
                            .FirstOrDefaultAsync();

        }

        public async Task UpdateAsync(Property property)
        {
            _db.Properties.Update(property);
            await _db.SaveChangesAsync();
        }
    }
}
