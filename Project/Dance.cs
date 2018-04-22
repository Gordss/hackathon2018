using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Dance
    {
        public String name { get; set; }
        public String stepString { get; set; }
        public String difficulty { get; set; }
        public Step[] steps { get; set; }
    }

    public class Step
    {
        public string step { get; set; }
        public string time { get; set; }
        public int duration { get; set; }
    }
}
