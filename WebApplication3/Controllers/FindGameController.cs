using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class FindGameController : Controller
    {
        public IActionResult Index(string[] tags)
        {
            List<Game> gamesL = new List<Game>(GameHub.games);
            List<string> t = new List<string>();

            for (int i = 0; i < gamesL.Count; i++)
            {
                foreach (Tag tag in gamesL[i].tags)
                {
                    if (!t.Contains(tag.value))
                        t.Add(tag.value);
                }
                if (tags.Length > 0)
                {
                    foreach(string tag in tags)
                    {
                        if(!gamesL[i].stringTags.Contains(tag))
                        {
                            gamesL.Remove(gamesL[i]);
                            i--;
                        }
                    }                    
                }

            }

            
            return View(new FindGameModel() { games = gamesL.ToArray(),selectTags = tags,tags=t.ToArray(), });
        }
    }
}
