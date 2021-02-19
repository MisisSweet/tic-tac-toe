using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApplication3.Models
{
    public class FindGameModel
    {
        public Game[] games { get; set; }
        public string[] tags
        {
            get;set;
        }
        public string[] selectTags { get; set; }

        public string getJsonListTag
        {
            get
            {
                return JsonConvert.SerializeObject(tags);
            }
        }
        public string getJsonListSelTag
        {
            get
            {
                return JsonConvert.SerializeObject(selectTags);
            }
        }
    }
}
