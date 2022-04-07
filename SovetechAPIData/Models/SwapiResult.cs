using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovetechAPIData.Models
{
    public class SwapiResult
    {
        public int count { get; set; }

        public string origin => "Star Wars";
        public string next { get; set; }

        public string previous { get; set; }

        public List<People> results { get; set; }
    }
}
