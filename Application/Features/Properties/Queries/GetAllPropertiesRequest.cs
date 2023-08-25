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
    public class GetAllPropertiesRequest : IRequest<IEnumerable<PropertyDto>>, ICacheable
    {
        //implementing of ICacheable properties
        public TimeSpan? SlidingExpiration { get ; set ; }
        public string CacheKey { get; set; }
        public bool ByPassCache { get; set; }

        public GetAllPropertiesRequest()
        {
            CacheKey = "GetAllProperties";
        }
    }
    public class GetAllPropertiesRequestHandler : IRequestHandler<GetAllPropertiesRequest, IEnumerable<PropertyDto>>
    {
        private readonly IPropertyRepo _repo;
        private readonly IMapper _mapper;
        public GetAllPropertiesRequestHandler(IPropertyRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
         
        public async Task<IEnumerable<PropertyDto>> Handle(GetAllPropertiesRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<Property> res = await _repo.GetAllAsync();

            if (res != null)
            {
                var result = _mapper.Map<IEnumerable<PropertyDto>>(res);

                return result;
            }
            return null;

        }
    }
}
