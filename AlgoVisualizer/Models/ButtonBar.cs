using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoVisualizer.Models
{
    public class ButtonBar
    {
        public Point Index { get; set; }
        public string Direction { get; set; }
        public int Length { get; set; }

        public ButtonBar(int x, int y, string direction, int length = 0)
        {
            Index = new Point(x, y);
            Direction = direction;
            Length = length;
        }

        public ButtonBar()
        {
            Index = new Point(-1, -1);
            Direction = "";
            Length = 0;
        }
    }
}
