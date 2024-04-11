using Manatee.Trello;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobson.Models;

namespace Jobson.Services
{
    public interface ITrelloService
    {
        Task AddJobToBoard(Job job, string boardId);
        Task<IBoard> CreateOrGetBoardAsync(string boardName);
        Task<IList> AddListToBoardAsync(string boardId, string listName, int position);
        Task<Board> GetBoardByIdAsync(string boardId);
        Task<ICard> AddCard(string boardId, string listId, string cardName, string cardDescription);
        Task<List<ICard>> SearchCardsByNameAsync(string boardId, string cardName);
    }

    public class TrelloService : ITrelloService
    {
        public const string NewJoblistName = "New Jobs";

        public TrelloService(IOptions<AppSettings> appSettings)
        {
            TrelloAuthorization.Default.AppKey = appSettings.Value.Trello_ApiKey;
            TrelloAuthorization.Default.UserToken = appSettings.Value.Trello_Token;
        }

        //create a new trello board
        public async Task AddJobToBoard(Job job, string boardId)
        {
            var board = await GetBoardByIdAsync(boardId);
            //Get the NewJobs list
            var list = board.Lists.FirstOrDefault(l => l.Name == NewJoblistName);
            if (list == null)
            {
                //Create the list if it doesn't exist
                list = await AddListToBoardAsync(boardId, NewJoblistName, 1);
            }

            //Add the job to the list
            var card = await AddCard(boardId, list.Id, job.Title, job.Description);
            job.TrelloCardId = card.Id;
            job.TrelloBoardName = board.Name;
            job.TrelloCardShortUrl = card.ShortUrl;
            job.TrelloCardUrl = card.Url;
            job.TrelloListId = list.Id;
            job.TrelloListName = list.Name;
        }

        public async Task<IBoard> CreateOrGetBoardAsync(string boardName)
        {
            // Get the current user's boards to check if the board already exists
            var member = new Member("me");
            await member.Boards.Refresh(); // Load the boards of the current user

            // Try to find an existing board with the specified name
            var existingBoard = member.Boards.FirstOrDefault(b => b.Name == boardName);

            if (existingBoard != null)
            {
                // Return the existing board if found
                return existingBoard;
            }
            else
            {
                // Create a new board if it doesn't exist
                //member.Boards.
                var newBoard = new Board(boardName);
                await newBoard.Refresh(); // Save the new board to Trello
                await AddListToBoardAsync(newBoard.Id, NewJoblistName, 1);

                return newBoard;
            }
        }

        public async Task<IList> AddListToBoardAsync(string boardId, string listName, int position)
        {
            var board = new Board(boardId);
            await board.Refresh(); // Ensure the board's data is up-to-date

            // Create a new list on the board with the specified name
            var newList = await board.Lists.Add(listName, position);

            return newList;
        }


        public async Task<Board> GetBoardByIdAsync(string boardId)
        {
            try
            {
                var board = new Board(boardId);
                await board.Refresh(); // Load the board's data
                return board;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        //Add card to board
        public async Task<ICard> AddCard(string boardId, string listId, string cardName, string cardDescription)
        {
            try
            {
                var board = new Board(boardId);
                await board.Refresh(); // Load the board's data

                //Get List
                var list = board.Lists.FirstOrDefault(l => l.Id == listId);

                if (list == null)
                {
                    throw new Exception("List not found");
                }

                list?.Refresh();

                // Create a new card
                var card = new Card(cardName)
                {
                    Description = cardDescription
                };

                // Add the card to the board
                return await list.Cards.Add(card);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //Get board by id
        public async Task<List<ICard>> SearchCardsByNameAsync(string boardId, string cardName)
        {
            try
            {
                var board = new Board(boardId);
                await board.Refresh(); // Load the board's data
                await board.Cards.Refresh();

                var a = board.Cards;

                // Search for cards that match the specified name
                var matchingCards = board.Cards.Where(card => card.Name == cardName).ToList();

                return matchingCards;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
