using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IPropertyRepo
    {
        Task AddNewAsync(Property property);
        Task DeleteAsync(int id);
        Task<IEnumerable<Property>> GetAllAsync();
        Task UpdateAsync(Property property);
        Task<Property> GetByIdAsync(int id);
    }
}
