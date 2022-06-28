namespace powerchat.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Application;
    using Shared.Enum;
    using Shared.Result;
    
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
        [ProducesResponseType(typeof(GetChatHistoryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetChatHistoryByLevel([FromQuery] GranularityLevel granularity)
        {
            var result = await _chatHistoryService.GetChatHistoryAsync(granularity);

            return Ok(result);
        }
        
        [HttpGet("granularity")]
        [ProducesResponseType(typeof(GetGranularityLevelResult), StatusCodes.Status200OK)]
        public IActionResult GetGranularity()
        {
            var result = _chatHistoryService.GetGranularityLevels();

            return Ok(result);
        }
    }
}