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

namespace Application.Features.Properties.Queries
{
    public class GetPropertyByIdRequest : IRequest<PropertyDto>,ICacheable
    {
        public TimeSpan? SlidingExpiration { get; set; }
        public string CacheKey { get; set; }
        public bool ByPassCache { get; set; }
        public int PropertyId { get; set; }

        public GetPropertyByIdRequest(int propertyId)
        {
            CacheKey = $"GetPropertyById:{propertyId}";
            PropertyId = propertyId;
        }

    }
    public class GetPropertyByIdRequestHandler : IRequestHandler<GetPropertyByIdRequest, PropertyDto>
    {
        private readonly IPropertyRepo _repo;
        private readonly IMapper _mapper;
        public GetPropertyByIdRequestHandler(IPropertyRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PropertyDto> Handle(GetPropertyByIdRequest request, CancellationToken cancellationToken)
        {
            var property = await _repo.GetByIdAsync(request.PropertyId);

            if (property != null)
            {
                PropertyDto propertyDto = _mapper.Map<PropertyDto>(property);

                return propertyDto;
            }
            return null;
        }
    }
}
