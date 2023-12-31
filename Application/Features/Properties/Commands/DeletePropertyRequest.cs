﻿using Application.Features.Behaviors.Contracts;
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
    public class DeletePropertyRequest : IRequest<bool>, IRemovalCacheable
    {
        public TimeSpan? SlidingExpiration { get; set; }
        public string CacheKey { get; set; }
        public bool ByPassCache { get; set; }
        public int PropertyId { get; set; }

        public DeletePropertyRequest(int propertyId)
        {
            CacheKey = $"GetPropertyById:{propertyId}";
            PropertyId = propertyId;
        }
    }

    public class DeletePropertyRequestHandler : IRequestHandler<DeletePropertyRequest, bool>
    {
        private readonly IPropertyRepo _repo;
        public DeletePropertyRequestHandler(IPropertyRepo repo)
        {
            _repo = repo;
        }
        async Task<bool> IRequestHandler<DeletePropertyRequest, bool>.Handle(DeletePropertyRequest request, CancellationToken cancellationToken)
        {
            var prop = await _repo.GetByIdAsync(request.PropertyId);

            if (prop == null)
            {
                return false;
            }
            else
            {
                await _repo.DeleteAsync(request.PropertyId);
                return true;
            }

        }
    }
}
