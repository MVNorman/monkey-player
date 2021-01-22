using System;
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
    public class GetUserQuery
    {
        public class Query : IQuery<Result>
        {
            public Guid Id { get; set; }
        }

        public class Result
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(query => query.Id).NotEmpty();
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
                var user = await _context
                    .QueryRepository<UserEntity, Guid>()
                    .Queryable()
                    .Where(x => x.Id == query.Id)
                    .Select(x => new Result()
                    {
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    })
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (user is null)
                {
                    throw new ArgumentNullException($"{nameof(user)} was not found by id '{query.Id}'");
                }

                return user;
            }
        }
    }
}