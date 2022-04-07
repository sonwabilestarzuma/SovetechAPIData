using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovetechAPIData.Models
{
    public class Joke
    {
  
        public string Created_at { get; set; }
        public string Icon_url { get; set; }
        public string Updated_at { get; set; }
        public string Url { get; set; }
        public string Value { get; set; }
        public string[] Categories { get; set; }
    }
}
