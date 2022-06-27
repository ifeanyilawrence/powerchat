namespace powerchat.Application.ChatService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared.Enum;
    using Shared.Result;
    
    public interface IGetChatHistoryService
    {
        Task<IEnumerable<GetChatHistoryResult>> GetChatHistory(GranularityLevel level);
    }
}