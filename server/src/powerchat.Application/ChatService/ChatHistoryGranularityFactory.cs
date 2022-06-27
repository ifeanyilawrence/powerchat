namespace powerchat.Application.ChatService
{
    using Infrastructure;
    using Shared.Enum;
    
    public class ChatHistoryGranularityFactory : IChatHistoryGranularityFactory
    {
        private readonly IChatHistoryRepository _chatHistoryRepository;

        public ChatHistoryGranularityFactory(IChatHistoryRepository chatHistoryRepository)
        {
            _chatHistoryRepository = chatHistoryRepository;
        }

        public IGetChatHistoryService GetChatHistoryService(GranularityLevel level)
        {
            switch (level)
            {
                case GranularityLevel.MinuteByMinute:
                    return new GetChatHistoryByMinute(_chatHistoryRepository);
                
                case GranularityLevel.Hourly:
                    return new GetChatHistoryByHour(_chatHistoryRepository);
                
                default:
                    return new GetChatHistoryByMinute(_chatHistoryRepository);
            }
        }
    }
}