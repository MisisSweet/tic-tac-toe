using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3
{
    public class GameHub : Hub
    {
        public static List<Game> games = new List<Game>() {
            new Game()
            {
                id=0,
                name="test_game",
                tags=new Tag[]{new Tag(){value="1" },new Tag() {value="2" } },
                players =1
            },
             new Game()
            {
                 id=1,
                name="test_game",
                tags=new Tag[]{new Tag(){value="1" },new Tag() {value="6" } },
                players =1

            },
              new Game()
            {
                  id=2,
                name="test_game",
                tags=new Tag[]{new Tag(){value="2" },new Tag() {value="3" } },
                players =1
            }
        };
        public override async Task OnDisconnectedAsync(Exception e)
        {
            string id = Context.ConnectionId;
            foreach (Game game in games)
            {
                if (game.playersId.Contains(id))
                {
                    await Groups.RemoveFromGroupAsync(id, game.id.ToString());
                    game.playersId.Remove(id);
                    game.players--;
                    if (game.players == 0)
                    {
                        games.Remove(game);
                    }
                    break;
                }
            }
        }
        public async Task CreateGame(Game game)
        {
            game.id = games.Count;
            games.Add(game);
            await Clients.Client(Context.ConnectionId).SendAsync("onCreateGame", game.id);
        }

        public async Task JoinGame(string playerid, string gameId)
        {
            int id = int.Parse(gameId);
            await Groups.AddToGroupAsync(playerid, gameId);
            foreach (Game game in games)
            {
                if (game.id == id)
                {
                    game.players++;
                    game.playersId.Add(playerid);
                    break;
                }
            }
        }
    }
}
