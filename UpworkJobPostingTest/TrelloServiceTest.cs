using Manatee.Trello;
using Moq;
using NUnit;
using NUnit.Framework;
using global::Jobson.Models;
using global::Jobson.Services;
using global::Jobson;
using Microsoft.Extensions.Options;
using NUnit.Framework.Legacy;

namespace UpworkJobPostingTest
{
    namespace Jobson.Tests
    {
        [TestFixture]
        public class TrelloServiceTest
        {
            private Mock<IOptions<AppSettings>> mockOptions;
            private AppSettings appSettings;

            [SetUp]
            public void SetUp()
            {
                appSettings = new AppSettings
                {
                    Trello_ApiKey = "",
                    Trello_Token = ""
                };
                mockOptions = new Mock<IOptions<AppSettings>>();
                mockOptions.Setup(ap => ap.Value).Returns(appSettings);

                trelloService = new TrelloService(mockOptions.Object);
            }

            [Test]
            public async Task AddJobToBoard_BoardExists_ListExists()
            {
                // Arrange
                var job = new Job { Title = "Test Job", Description = "Test Description" };
                var boardId = "testBoardId";
                var board = new Mock<IBoard>();
                var mockListCollection = new Mock<IListCollection>();
                var list = new Mock<IList>();
                var card = new Mock<ICard>();
                board.Setup(b => b.Lists).Returns(mockListCollection.Object);
                list.Setup(l => l.Name).Returns(TrelloService.NewJoblistName);
                card.Setup(c => c.Id).Returns("cardId");
                mockListCollection.Setup(lc => lc.FirstOrDefault(It.IsAny<Func<IList, bool>>())).Returns(list.Object);

                var trelloServiceMock = new Mock<TrelloService>();
                trelloServiceMock.Setup(service => service.AddJobToBoard(It.IsAny<Job>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(card.Object));
                // Act
                await trelloServiceMock.Object.AddJobToBoard(job, boardId);
                // Assert
                Assert.That(job.TrelloCardId, Iz.Not.Null);
            }

            [Test]
            public async Task CreateOrGetBoardAsync_BoardExists_ReturnsExistingBoard()
            {
                // Arrange
                string boardName = "Existing Board";
                var existingBoard = new Mock<IBoard>();
                existingBoard.Setup(b => b.Name).Returns(boardName);

                // Act
                var board = await trelloService.CreateOrGetBoardAsync(boardName);

                // Assert
                Assert.That(boardName, Iz.EqualTo(board.Name));
            }

            [Test]
            public async Task CreateOrGetBoardAsync_BoardDoesNotExist_CreatesNewBoard()
            {
                // Arrange
                string boardName = "TEST - Brandon";

                // Act
                var board = await trelloService.CreateOrGetBoardAsync(boardName);

                // Assert
                Assert.That(boardName, Is.EqualTo(board.Name));
            }

            [Test]
            public async Task AddListToBoardAsync_AddsListToBoard()
            {
                // Arrange
                string boardId = "testBoardId";
                string listName = "New List";

                // Act
                var list = await trelloService.AddListToBoardAsync(boardId, listName, 1);

                // Assert
                Assert.That(listName,Is.EqualTo(list.Name));
            }

            [Test]
            public async Task GetBoardByIdAsync_ReturnsBoard()
            {
                // Arrange
                string boardId = "testBoardId";

                // Act
                var board = await trelloService.GetBoardByIdAsync(boardId);

                // Assert
                Assert.That(board, Is.Not.Null);
            }

            [Test]
            public async Task AddCard_CreatesCardInList()
            {
                // Arrange
                string boardId = "testBoardId";
                string listId = "testListId";
                string cardName = "New Card";
                string cardDescription = "Card Description";

                // Act
                var card = await trelloService.AddCard(boardId, listId, cardName, cardDescription);

                // Assert
                Assert.That(cardName, Is.EqualTo(card.Name));
            }

            [Test]
            public async Task SearchCardsByNameAsync_FindsMatchingCards()
            {
                // Arrange
                string boardId = "testBoardId";
                string cardName = "Test Card";

                // Act
                var cards = await trelloService.SearchCardsByNameAsync(boardId, cardName);

                // Assert
                Assert.That(cards.Any(c => c.Name == cardName),Is.True);
            }

        }
    }

}
