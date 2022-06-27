namespace powerchat.Application
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared.Enum;
    using Shared.Result;
    
    public interface IChatHistoryService
    {
        Task<IEnumerable<GetChatHistoryResult>> GetChatHistoryAsync(GranularityLevel level);

        IEnumerable<GetGranularityLevelResult> GetGranularityLevels();
    }
}