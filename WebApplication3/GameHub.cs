using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3
{
    public class GameHub : Hub
    {
        public static List<Game> games = new List<Game>();
        public static int idGame = 0;
        public override async Task OnDisconnectedAsync(Exception e)
        {
            string id = Context.ConnectionId;
            foreach (Game game in games)
            {
                if (game.playersId.Contains(id))
                {
                    await Groups.RemoveFromGroupAsync(id, game.id.ToString());
                    game.playersId.Remove(id);

                    int playerNum = 1;
                    if (!game.numberPlayer[1].Equals(id))
                        playerNum=2;
                    game.numberPlayer[playerNum] = "";

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
            game.id = idGame;
            game.isFirstPlayerStep = true;
            idGame++;
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
                    if (game.players == 2)
                    {
                    await Clients.Client(Context.ConnectionId).SendAsync("onExitGame");
                            break;
                    }
                    int playerNum = 1;
                    if (!String.IsNullOrEmpty(game.numberPlayer[1]))
                    {
                        playerNum = 2;
                    }
                    game.numberPlayer[playerNum] = playerid;
                    game.players++;
                    game.playersId.Add(playerid);
                    await Clients.Client(Context.ConnectionId).SendAsync("loadGame", JsonConvert.SerializeObject(game.gameArea),playerNum,game.isFirstPlayerStep);
                    break;
                }
            }
        }

        public async Task Step(string gameId, int player,int x,int y)
        {
            int id = int.Parse(gameId);
            foreach (Game game in games)
            {
                if (game.id == id)
                {
                    if((game.isFirstPlayerStep && player==1) || (!game.isFirstPlayerStep && player == 2))
                    {
                        int result = game.Step(x, y);
                        await Clients.Group(gameId.ToString()).SendAsync("onUpdateGame", JsonConvert.SerializeObject(game.gameArea), result,game.isFirstPlayerStep);
                    }
                    else
                    {
                        await Clients.Client(Context.ConnectionId).SendAsync("error", "Ход другого игрока");
                    }
                    break;
                }
            }
        }
    }
}
