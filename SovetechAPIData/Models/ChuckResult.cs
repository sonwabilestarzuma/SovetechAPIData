using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovetechAPIData.Models
{
    public class ChuckResult
    {
        public string Total { get; set; }
        public string origin => "Chuck Norris Jokes";

        public List<Joke> result { get; set; }
    }
}
