﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApplication3.Models
{
    public class Tag
    {
        [JsonProperty("value")]
        public string value { get; set; }

    }
}