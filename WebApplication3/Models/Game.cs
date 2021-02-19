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

        public List<string> playersId = new List<string>();

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
    }
}
