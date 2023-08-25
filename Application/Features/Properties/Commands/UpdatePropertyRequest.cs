using Application.Models;
using Application.Repository;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Properties.Commands
{
    public class UpdatePropertyRequest : IRequest<bool>
    {
        public UpdateProperty UpdateProperty { get; set; }
        public UpdatePropertyRequest(UpdateProperty update)
        {
            UpdateProperty = update;

        }
    }
    public class UpdatePropertyHandler : IRequestHandler<UpdatePropertyRequest, bool>
    {
        private readonly IPropertyRepo _repo;
        public UpdatePropertyHandler(IPropertyRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdatePropertyRequest request, CancellationToken cancellationToken)
        {
            // check if exist in db
            // => update
            Property propertyInDb = await _repo.GetByIdAsync(request.UpdateProperty.Id);
            if (propertyInDb != null)
            {
                propertyInDb.Name = request.UpdateProperty.Name;
                propertyInDb.Louge = request.UpdateProperty.Louge;
                propertyInDb.Dining = request.UpdateProperty.Dining;
                propertyInDb.Rates = request.UpdateProperty.Rates;
                propertyInDb.BathroomCount = request.UpdateProperty.BathroomCount;
                propertyInDb.BedroomCount = request.UpdateProperty.BedroomCount;
                propertyInDb.Address = request.UpdateProperty.Address;
                propertyInDb.Description = request.UpdateProperty.Description;
                propertyInDb.ErfSize = request.UpdateProperty.ErfSize;
                propertyInDb.FloorSize = request.UpdateProperty.FloorSize;
                propertyInDb.KitchenCount = request.UpdateProperty.KitchenCount;
                propertyInDb.Levies = request.UpdateProperty.Levies;
                propertyInDb.PetsAllowed = request.UpdateProperty.PetsAllowed;
                propertyInDb.Price = request.UpdateProperty.Price;
                propertyInDb.Type = request.UpdateProperty.Type;

                await _repo.UpdateAsync(propertyInDb);
                return true;

            }
            return false;

        }
    }
}
