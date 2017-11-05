using System.Collections.Generic;
using System.Windows.Shapes;

namespace CGLAB1
{
    public class WChain
    {
        public List<WPoint> List { get; set; }
        public Polyline Shell { get; set; }
        public Polyline QBShell { get; set; }
        public Polyline CBShell { get; set; }
        public List<Polyline> Dump { get; set; }

        public WChain()
        {
            List = new List<WPoint>();
            Shell = new Polyline();
            QBShell = new Polyline();
            CBShell = new Polyline();
            Dump = new List<Polyline>();
        }

        public WChain(WPoint wp)
        {
            List = new List<WPoint>();
            List.Add(wp);
            Shell = new Polyline();
            QBShell = new Polyline();
            CBShell = new Polyline();
            Dump = new List<Polyline>();
        }

        public string ToString(int index)
        {
            return "Chain " + (index + 1);
        }
    }
}