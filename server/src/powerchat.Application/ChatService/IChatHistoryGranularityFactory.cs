namespace powerchat.Application.ChatService
{
    using Shared.Enum;
    
    public interface IChatHistoryGranularityFactory
    {
        IGetChatHistoryService GetChatHistoryService(GranularityLevel level);
    }
}