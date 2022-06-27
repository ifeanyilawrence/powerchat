using System;

namespace powerchat.Infrastructure
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Shared.Model;

    public class ChatHistoryRepository : IChatHistoryRepository
    {
        public async Task<IEnumerable<ChatEventHistory>> GetChatEventsAsync()
        {
            try
            {
                Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
                var historyFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ??
                                                   string.Empty, "chat-history.json");
            
                using var streamReader = new StreamReader(historyFilePath);
                var chatEvents = await streamReader.ReadToEndAsync();

                return JsonConvert.DeserializeObject<List<ChatEventHistory>>(chatEvents);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}