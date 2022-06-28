namespace powerchat.Api.Tests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using AutoFixture;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Application;
    using Controllers;
    using Shared.Enum;
    using Shared.Result;


    [TestClass]
    public class ChatHistoryControllerTests
    {
        private Mock<IChatHistoryService> _chatHistoryServiceMock;
        private ChatHistoryController _controllerUnderTest;
        private static Fixture _fixture;
        
        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();

            _chatHistoryServiceMock = new Mock<IChatHistoryService>();

            _controllerUnderTest = new ChatHistoryController(_chatHistoryServiceMock.Object);
        }
        
        [TestMethod]
        public async Task GetChatHistoryByLevel_ShouldReturnOk_WhenResultIsNotNull()
        {
            // Arrange
            var parameter = _fixture.Create<GranularityLevel>();
            var chatHistory = _fixture.CreateMany<GetChatHistoryResult>(10);
            
            _chatHistoryServiceMock
                .Setup(x => x.GetChatHistoryAsync(parameter))
                .ReturnsAsync(chatHistory);

            // Act
            var response = await _controllerUnderTest
                .GetChatHistoryByLevel(parameter) as ObjectResult;

            // Assert
            response
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.OK);
            
            response
                .Value
                .Should()
                .NotBeNull();

            _chatHistoryServiceMock.Verify(x => x.GetChatHistoryAsync(parameter), Times.Once);
        }
        
        [TestMethod]
        public async Task GetChatHistoryByLevel_ShouldReturnOk_WhenResultIsNull()
        {
            // Arrange
            var chatHistory = _fixture.CreateMany<GetChatHistoryResult>(10);

            _chatHistoryServiceMock
                .Setup(x => x.GetChatHistoryAsync(It.IsAny<GranularityLevel>()))
                .ReturnsAsync((IEnumerable<GetChatHistoryResult>) null);

            // Act
            var response = await _controllerUnderTest
                .GetChatHistoryByLevel(It.IsAny<GranularityLevel>()) as ObjectResult;

            // Assert
            response
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.OK);
            
            response
                .Value
                .Should()
                .BeNull();

            _chatHistoryServiceMock.Verify(x => x.GetChatHistoryAsync(It.IsAny<GranularityLevel>()), Times.Once);
        }
        
        [TestMethod]
        public void GetGranularity_ShouldReturnGranularityLevels()
        {
            // Arrange
            var granularityLevels = _fixture.CreateMany<GetGranularityLevelResult>();
            
            _chatHistoryServiceMock
                .Setup(x => x.GetGranularityLevels())
                .Returns(granularityLevels);

            // Act
            var response = _controllerUnderTest
                .GetGranularity() as ObjectResult;

            // Assert
            response
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.OK);
            
            response
                .Value
                .Should()
                .NotBeNull();

            _chatHistoryServiceMock.Verify(x => x.GetGranularityLevels(), Times.Once);
        }
    }
}