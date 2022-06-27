using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using powerchat.Application;
using powerchat.Shared.Enum;

namespace powerchat.Api.Controllers
{
    [ApiController]
    [Route("api/chat-history")]
    public class ChatHistoryController : Controller
    {
        private readonly IChatHistoryService _chatHistoryService;

        public ChatHistoryController(IChatHistoryService chatHistoryService)
        {
            _chatHistoryService = chatHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetChatHistoryByLevel([FromQuery] GranularityLevel level)
        {
            var result = await _chatHistoryService.GetChatHistoryAsync(level);

            return Ok(result);
        }
    }
}