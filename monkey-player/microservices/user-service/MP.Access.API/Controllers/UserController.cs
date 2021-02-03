using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MP.Application.User.Commands;
using MP.Application.User.Queries;
using MVNormanNativeKit.Infrastructure.Core.Commands;
using MVNormanNativeKit.Infrastructure.Core.Queries;

namespace MP.Access.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public UserController(
            ICommandBus commandBus,
            IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet, Route("")]
        public async Task<ActionResult<GetUsersQuery.Result>> Get()
        {
            var query = new GetUsersQuery.Query()
            { };
            
            var result = await _queryBus.Send(query);
            
            return Ok(result);
        }
        
        [HttpGet, Route("{id}")]
        public async Task<ActionResult<GetUserQuery.Result>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetUserQuery.Query()
            {
                Id = id
            };

            var result = await _queryBus.Send(query, cancellationToken);
            
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<CreateUserCommand.Result>> Create([FromBody] CreateUserCommand.Command command, CancellationToken cancellationToken)
        {
            var result = await _commandBus.Send(command, cancellationToken); 
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<DeleteUserCommand.Result>> Delete([FromBody] DeleteUserCommand.Command command, CancellationToken cancellationToken)
        {
            var result =  await _commandBus.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}