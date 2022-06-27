namespace powerchat.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public IEnumerable<GetGranularityLevelResult> GetGranularityLevels()
        {
            return Enum.GetValues(typeof(GranularityLevel))
                .Cast<GranularityLevel>()
                .Select(x =>
                {
                    var text = x.ToString();

                    if (x == GranularityLevel.MinuteByMinute)
                        text = "Minute by minute";

                    return new GetGranularityLevelResult(
                        id: x,
                        name: text);
                })
                .ToList();
        }
    }
}