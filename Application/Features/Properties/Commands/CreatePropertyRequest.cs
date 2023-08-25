using Application.Features.Behaviors.Contracts;
using Application.Models;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Properties.Commands
{
    public class CreatePropertyRequest : IRequest<bool> , /* only classes those inherits this can have validation */ IValidatable
    {
        public NewProperty PropertyRequest { get; set; }

        public CreatePropertyRequest(NewProperty newProperty)
        {
            PropertyRequest = newProperty;
        }
    }
    public class CreatePropertyRequestHandler : IRequestHandler<CreatePropertyRequest, bool>
    {
        private readonly IPropertyRepo _repo;
        private readonly IMapper _mapper;

        public CreatePropertyRequestHandler(IPropertyRepo propertyRepo, IMapper mapper)
        {
            _repo = propertyRepo;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreatePropertyRequest request, CancellationToken cancellationToken)
        {
            Property property = _mapper.Map<Property>(request.PropertyRequest);

            property.ListDate = DateTime.Now;
            await _repo.AddNewAsync(property);

            return true;
        }
    }
}
