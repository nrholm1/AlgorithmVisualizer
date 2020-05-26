using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoVisualizer.Models
{
    public class XYButton : System.Windows.Forms.Button
    {
        public Point index { get; set; }
        public List<Point> adjacentFields { get; set; } 
        public int shortestPathLength { get; set; }
        public bool isWall { get; set; }


        public XYButton(int x, int y) 
        {
            InitValues(x, y);
        }

        public void InitValues(int x, int y)
        {
            index = new Point(x, y);
            adjacentFields = CalculateAdjacentFields();
            shortestPathLength = int.MaxValue;
            isWall = false;
            this.BackColor = Color.Transparent;
        }

        private List<Point> CalculateAdjacentFields() // add 2 to 4 adjacent XYButtons for the XYButton
        {
            List<Point> adjFields = new List<Point>();

            // Add adjFields clockwise
            if (index.Y != 0)
                adjFields.Add(new Point(index.X, index.Y - 1));
            if (index.X != Form1.GRID_BOUNDARY_X - 1)
                adjFields.Add(new Point(index.X + 1, index.Y));
            if (index.Y != Form1.GRID_BOUNDARY_Y - 1)
                adjFields.Add(new Point(index.X, index.Y + 1));
            if (index.X != 0)
                adjFields.Add(new Point(index.X - 1, index.Y));

            return adjFields;
        }

        public void ToggleWall()
        {
            if (!isWall)
            {
                //Console.WriteLine($"Wall will be built here: ({index.X},{index.Y})");
                isWall = true;
                Form1.RenderField(this, (int)Form1.Fields.WALL);
            }
            else
            {
                //Console.WriteLine($"Wall will be torn down here: ({index.X},{index.Y})");
                isWall = false;
                Form1.RenderField(this, (int)Form1.Fields.EMPTY);
            }
        }

    }
}
