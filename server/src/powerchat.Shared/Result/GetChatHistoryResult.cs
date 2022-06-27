namespace powerchat.Shared.Result
{
    using System.Collections.Generic;
    
    public class GetChatHistoryResult
    {
        public GetChatHistoryResult(
            string time,
            IEnumerable<string> events)
        {
            Time = time;
            Events = events;
        }
        
        public string Time { get; }

        public IEnumerable<string> Events { get; }
    }
}