using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MP.Application.User.Commands;
using MP.Application.User.Queries;
using MP.Data;
using MVNormanNativeKit.Infrastructure.Core.Commands;
using MVNormanNativeKit.Infrastructure.Core.Queries;
using MVNormanNativeKit.Infrastructure.Data.EFCore.Core;

namespace MP.Access.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IEfUnitOfWork<UserDataContext> _context;

        private readonly ILogger<UserController> _logger;
        
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public UserController(
            ILogger<UserController> logger, 
            IEfUnitOfWork<UserDataContext> context,
            ICommandBus commandBus,
            IQueryBus queryBus)
        {
            _logger = logger;
            _context = context;
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
        public async Task<ActionResult<GetUserQuery.Result>> Get([FromRoute] Guid id)
        {
            var query = new GetUserQuery.Query()
            {
                Id = id
            };

            var result = await _queryBus.Send(query);
            
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<CreateUserCommand.Result>> Create([FromBody] CreateUserCommand.Command command)
        {
            var result = await _commandBus.Send(command); 
            return Ok(result);
        }
    }
}