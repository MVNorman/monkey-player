﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MVNormanNativeKit.Infrastructure.Core.Queries
{
    public sealed class QueryBus : IQueryBus
    {
        private readonly IMediator _mediator;

        public QueryBus(IMediator mediator)
        {
            _mediator = mediator ?? throw new Exception($"Missing dependency '{nameof(IMediator)}'");   
        }

        public async Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return result;
        }
    }
}
