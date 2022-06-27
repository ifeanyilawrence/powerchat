namespace powerchat.Application
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ChatService;
    using Shared.Enum;
    using Shared.Result;
    
    public class ChatHistoryService : IChatHistoryService
    {
        private readonly IChatHistoryGranularityFactory _granularityFactory;
        
        public ChatHistoryService(IChatHistoryGranularityFactory granularityFactory)
        {
            _granularityFactory = granularityFactory;
        }
        
        public async Task<IEnumerable<GetChatHistoryResult>> GetChatHistoryAsync(GranularityLevel level)
        {
            var service = _granularityFactory.GetChatHistoryService(level);

            return await service.GetChatHistory(level);
        }
    }
}