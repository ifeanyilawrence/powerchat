namespace powerchat.Application.Tests
{
    using AutoFixture;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ChatService;
    using Infrastructure;
    using Shared.Enum;


    [TestClass]
    public class ChatHistoryGranularityFactoryTests
    {
        private Mock<IChatHistoryRepository> _chatHistoryRepositoryMock;
        private static Fixture _fixture;

        private ChatHistoryGranularityFactory _sut;
            
        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();
            _chatHistoryRepositoryMock = new Mock<IChatHistoryRepository>();
            
            _sut = new ChatHistoryGranularityFactory(_chatHistoryRepositoryMock.Object);
        }
        
        [TestMethod]
        public void GetChatHistoryService_ShouldReturnCorrectService_WhenGranularityLevelIsPassed()
        {
            // Arrange
            var level = GranularityLevel.Hourly;
            
            // Act
            var result = _sut.GetChatHistoryService(level);
            
            // Assert
            result
                .Should()
                .NotBeNull();

            result
                .Should()
                .BeOfType(typeof(GetChatHistoryByHour));
        }
    }
}