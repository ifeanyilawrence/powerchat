using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using powerchat.Application.ChatService;
using powerchat.Shared.Enum;
using powerchat.Shared.Result;

namespace powerchat.Application.Tests
{
    [TestClass]
    public class ChatHistoryServiceTests
    {
        private Mock<IChatHistoryGranularityFactory> _granularityFactoryMock;
        private Mock<IGetChatHistoryService> _getChatHistoryServiceMock;
        private static Fixture _fixture;

        private ChatHistoryService _sut;
            
        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();
            _granularityFactoryMock = new Mock<IChatHistoryGranularityFactory>();
            _getChatHistoryServiceMock = new Mock<IGetChatHistoryService>();
            
            _sut = new ChatHistoryService(_granularityFactoryMock.Object);
        }
        
        [TestMethod]
        public async Task GetChatHistoryAsync_ShouldReturnNull_WhenNoHistoryIsFound()
        {
            // Arrange
            _getChatHistoryServiceMock
                .Setup(x => x.GetChatHistory(It.IsAny<GranularityLevel>()))
                .ReturnsAsync((IEnumerable<GetChatHistoryResult>) null);

            var chatHistoryService = _getChatHistoryServiceMock.Object;

            _granularityFactoryMock
                .Setup(x => x.GetChatHistoryService(It.IsAny<GranularityLevel>()))
                .Returns(chatHistoryService);

            // Act
            var result = await _sut.GetChatHistoryAsync(It.IsAny<GranularityLevel>());
            
            // Assert
            result
                .Should()
                .BeNull();

            _getChatHistoryServiceMock.Verify(x => x.GetChatHistory(It.IsAny<GranularityLevel>()), Times.Once);
        }
        
        
        [TestMethod]
        public async Task GetChatHistoryAsync_ShouldReturnChatHistory_WhenCHatHistoryExist()
        {
            // Arrange
            var level = _fixture.Create<GranularityLevel>();
            
            var chatHistory = _fixture.CreateMany<GetChatHistoryResult>(10);
            
            _getChatHistoryServiceMock
                .Setup(x => x.GetChatHistory(level))
                .ReturnsAsync(chatHistory);

            var chatHistoryService = _getChatHistoryServiceMock.Object;

            _granularityFactoryMock
                .Setup(x => x.GetChatHistoryService(level))
                .Returns(chatHistoryService);

            // Act
            var result = await _sut.GetChatHistoryAsync(level);
            
            // Assert
            result
                .Should()
                .NotBeNull();

            result
                .Count()
                .Should()
                .Be(10);

            _getChatHistoryServiceMock.Verify(x => x.GetChatHistory(level), Times.Once);
        }
    }
}