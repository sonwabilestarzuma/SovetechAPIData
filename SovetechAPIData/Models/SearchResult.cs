using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovetechAPIData.Models
{
    public class SearchResult
    {
        public SwapiResult swapiResult { get; set; }

        public ChuckResult chuckResult { get; set; }
    }
}
