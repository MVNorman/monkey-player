using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MP.Data;
using MP.Domain.User;
using MVNormanNativeKit.Infrastructure.Core.Queries;
using MVNormanNativeKit.Infrastructure.Data.EFCore.Core;

namespace MP.Application.User.Queries
{
    public class GetUsersQuery
    {
        public class Query : IQuery<Result>
        {
        }

        public class User
        {
            public Guid Id { get; set; } 
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }
        public class Result
        {
            public List<User> Users { get; set; }
        }

        public class Validator: AbstractValidator<Query>
        {
            public Validator()
            {
            }
        }

        public class Handler : IQueryHandler<Query, Result>
        {
            private readonly IEfUnitOfWork<UserDataContext> _context;

            public Handler(IEfUnitOfWork<UserDataContext> context)
            {
                _context = context;
            }

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                var users = await _context.QueryRepository<UserEntity, Guid>()
                    .Queryable()
                    .Select(x => new User()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    }).ToListAsync(cancellationToken: cancellationToken);

                var result = new Result()
                {
                    Users = users
                };
                
                return result;
            }
        }
    }
}