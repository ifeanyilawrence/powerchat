namespace powerchat.Application.ChatService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure;
    using Shared.Enum;
    using Shared.Result;

    public class GetChatHistoryByHour : IGetChatHistoryService
    {
        private readonly IChatHistoryRepository _chatHistoryRepository;
        
        public GetChatHistoryByHour(IChatHistoryRepository chatHistoryRepository)
        {
            _chatHistoryRepository = chatHistoryRepository;
        }
        
        public async Task<IEnumerable<GetChatHistoryResult>> GetChatHistory(GranularityLevel level)
        {
            var history = await _chatHistoryRepository.GetChatEventsAsync();

            var sortedResult = history
                .GroupBy(x => GetHourString(x.Time))
                .OrderByDescending(s => s.Key)
                .Select(x =>
                {
                    var chatEvents = new List<string>();
                    var enteredChatCount = x.Count(s => s.EventTypeId == ChatEventType.EnterTheRoom);
                    if (enteredChatCount > 0)
                    {
                        chatEvents.Add(enteredChatCount > 1
                            ? $"{enteredChatCount} people entered"
                            : $"{enteredChatCount} person entered");
                    }

                    var highFiveEvents = x.Count(s => s.EventTypeId == ChatEventType.HighFiveAnotherUser);
                    var distinctHighFiverUser = x.Where(s => s.EventTypeId == ChatEventType.HighFiveAnotherUser)
                        .Select(s => s.User.Id)
                        .Distinct()
                        .Count();

                    if (highFiveEvents > 0)
                    {
                        if (distinctHighFiverUser > 1)
                        {
                            chatEvents.Add(highFiveEvents > 1
                                ? $"{distinctHighFiverUser} people high-fived {highFiveEvents} other people"
                                : $"{distinctHighFiverUser} people high-fived {highFiveEvents} other person");
                        }
                        else
                        {
                            chatEvents.Add(highFiveEvents > 1
                                ? $"{distinctHighFiverUser} person high-fived {highFiveEvents} other people"
                                : $"{distinctHighFiverUser} person high-fived {highFiveEvents} other person");
                        }
                    }

                    var commentCount = x.Count(s => s.EventTypeId == ChatEventType.Comment);
                    if (commentCount > 0)
                        chatEvents.Add($"{commentCount} comments");
                    
                    var leftChatCount = x.Count(s => s.EventTypeId == ChatEventType.LeaveTheRoom);
                    if (leftChatCount > 0)
                        chatEvents.Add($"{leftChatCount} left");

                    return new
                    {
                        Time = x.Key.ToString(),
                        Events = chatEvents
                    };
                })
                .ToList();

            return sortedResult
                .Select(x => new GetChatHistoryResult(
                    time: x.Time,
                    events: x.Events))
                .ToList();
        }

        private string GetHourString(DateTime date) =>
            date.ToString("h:mm tt").Split(':')
                .First() +
            date.ToString("hh:mm tt").Split(' ')
                .Last()
                .ToLower();
    }
}