using System;
using System.Collections.Generic;
using System.Text;

namespace MoM.Data
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}
