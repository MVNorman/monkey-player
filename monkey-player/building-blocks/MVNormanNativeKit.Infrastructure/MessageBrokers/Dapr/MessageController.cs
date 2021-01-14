using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MVNormanNativeKit.Infrastructure.MessageBrokers.Dapr
{
    [Route("dapr/messages")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MessageController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public MessageController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> ReceiveMessage([FromBody] Message message)
        {
            await _eventBus.PublishLocalAsync(message.Content);

            return Ok();
        }
    }
}
