namespace powerchat.Application.ChatService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Shared.Enum;
    using Infrastructure;
    using Shared.Result;
    
    public class GetChatHistoryByMinute : IGetChatHistoryService
    {
        private readonly IChatHistoryRepository _chatHistoryRepository;
        
        public GetChatHistoryByMinute(IChatHistoryRepository chatHistoryRepository)
        {
            _chatHistoryRepository = chatHistoryRepository;
        }
        
        public async Task<IEnumerable<GetChatHistoryResult>> GetChatHistory(GranularityLevel level)
        {
            var history = await _chatHistoryRepository.GetChatEventsAsync();

            var sortedResult = history
                .OrderByDescending(s => s.Time)
                .Select(x =>
                {
                    var textEvent = "";
                    switch (x.EventTypeId)
                    {
                        case ChatEventType.EnterTheRoom:
                            textEvent = $"{x.User.Username} enters the room";
                            break;
                        case ChatEventType.LeaveTheRoom:
                            textEvent = $"{x.User.Username} leaves";
                            break;
                        case ChatEventType.Comment:
                            textEvent = $"{x.User.Username} comments: \"{x.Comment}\"";
                            break;
                        case ChatEventType.HighFiveAnotherUser:
                            textEvent = $"{x.User.Username} high-fives {x.EventForUser?.Username}";
                            break;
                    }

                    return new
                    {
                        Time = x.Time
                            .ToString("h:mmtt")
                            .ToLower(),
                        Events = new List<string> {textEvent}
                    };
                })
                .ToList();

            return sortedResult
                .Select(x => new GetChatHistoryResult(
                    time: x.Time,
                    events: x.Events))
                .ToList();
        }
    }
}