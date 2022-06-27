namespace powerchat.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared.Model;
    
    public interface IChatHistoryRepository
    {
        Task<IEnumerable<ChatEventHistory>> GetChatEventsAsync();
    }
}