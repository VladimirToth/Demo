﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{

    public class Rootobject
    {
        public List<Station> stations { get; set; }
    }

    public class Station
    {
        public string id { get; set; }
        public string name { get; set; }
        public string arrives { get; set; }
        public string departs { get; set; }
        public int distance { get; set; }
        public int restTime { get; set; }
        public int duration { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public int tempDistance { get; set; }
        public TimeSpan tempDuration { get; set; }
    
    }

}


