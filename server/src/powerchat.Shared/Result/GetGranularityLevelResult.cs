namespace powerchat.Shared.Result
{
    using Enum;
    
    public class GetGranularityLevelResult
    {
        public GetGranularityLevelResult(
            GranularityLevel id,
            string name)
        {
            Id = id;
            Name = name;
        }
        
        public GranularityLevel Id { get; }

        public string Name { get; }
    }
}