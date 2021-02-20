using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApplication3.Models
{
    public class Game
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("tags")]
        public Tag[] tags { get; set; }

        [JsonProperty("players")]
        public int players { get; set; }
        public Dictionary<int,string> numberPlayer = new Dictionary<int, string>()
        {
            { 1,"" },
            { 2 ,"" }
        };
        public List<string> playersId = new List<string>();

        public int[,] gameArea =      {
            { 0,0,0 },
            { 0,0,0 },
            { 0,0,0 }
        };
        public bool isFirstPlayerStep = true;

        public string tagsStr { get
            {
                string str = "";
                for(int i =0;i<tags.Length;i++)
                {
                    if (i == 0)
                        str += tags[i].value;
                    else
                        str += ','+tags[i].value ;
                }
                return str;
            } }
        public Game() { }

        public string[] stringTags
        {
            get
            {
                List<string> list = new List<string>();
                foreach(Tag t in tags)
                {
                    list.Add(t.value);
                }
                return list.ToArray();
            }
        }

        public int Step(int x,int y)
        {
            gameArea[x, y] = isFirstPlayerStep ? 1: 2;
            isFirstPlayerStep = !isFirstPlayerStep;
            return checkWin();
        }

        public int checkWin()
        {
            int result = 0;
            if(checkElements(0,0,1)|| checkElements(1, 0,1)|| checkElements(2, 0, 1)|| checkElements(0, 1, 1)  || checkElements(1, 1, 1)  || checkElements(2, 1, 1)  || checkElements(0, 2, 1)  || checkElements(1, 2, 1) )
            {
                result = 1;
            }
            if (checkElements(0, 0,2)  || checkElements(1, 0, 2)  || checkElements(2, 0, 2)  || checkElements(0, 1, 2)  || checkElements(1, 1, 2)  || checkElements(2, 1, 2)  || checkElements(0, 2, 2)  || checkElements(1, 2, 2) )
            {
                result = 2;
            }
            if (result == 0)
            {
                bool draw = true;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (gameArea[i, j] == 0)
                        {
                            draw = false;
                            break;
                        }
                    }
                }
                if (draw) result = 3;
            }
               
            return result;
        }

        private bool checkElements(int index,int type,int player)
        {
            bool result = false;
            switch (type)
            {
                case 0: result = gameArea[0, index] == player && gameArea[1, index] == player && gameArea[2, index] == player;break;
                case 1: result = gameArea[index, 0] == player && gameArea[index,1] == player && gameArea[ index,2] == player; break;
                case 2:
                    result = index==0? gameArea[0, 0] == player && gameArea[1, 1] == player && gameArea[2, 2] == player:
                        gameArea[2, 0] == player && gameArea[1, 1] == player && gameArea[0, 2] == player;
                    break;
            }
            return result;
        }
    }
}
