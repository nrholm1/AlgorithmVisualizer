using AlgoVisualizer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoVisualizer.Algorithms
{
    public static class Pathfinder // Dijkstra's shortest path algorithm - Greedy algorithm
    {

        private static Color MARKED_COLOR = Form1.MARKED_COLOR;
        private static Color PATH_COLOR = Form1.PATH_COLOR;
        private static Color END_COLOR = Form1.END_COLOR;
        private static Color WALL_COLOR = Form1.WALL_COLOR;


        public static void CalculateShortestPathLengths(XYButton[,] InputGraph, Point start, Point end)
        {
            // Classic Dijkstra
            List<XYButton> SptSet = new List<XYButton>() { InputGraph[start.X, start.Y] };

            // Bi-directional Dijkstra - Not implemented
            //List<XYButton> SptSet = new List<XYButton>() { InputGraph[start.X, start.Y] };


            Console.WriteLine($"CalculateShortestPathLengths called with start:({start.X},{start.Y}) end:({end.X},{end.Y})");


            while (SptSet.Count < InputGraph.Length && InputGraph[end.X, end.Y].shortestPathLength == int.MaxValue)
            {
                Point tempIndex = new Point();
                int shortestDistance = int.MaxValue;

                foreach (XYButton u in SptSet)
                {

                    foreach (Point v in u.adjacentFields)
                    {
                        if(!InputGraph[v.X, v.Y].isWall)
                            if (InputGraph[v.X, v.Y].shortestPathLength > u.shortestPathLength + 1) 
                                InputGraph[v.X, v.Y].shortestPathLength = u.shortestPathLength + 1;
                        if (!SptSet.Contains(InputGraph[v.X, v.Y])
                            && InputGraph[v.X, v.Y].shortestPathLength < shortestDistance)
                        {
                            shortestDistance = InputGraph[v.X, v.Y].shortestPathLength;
                            tempIndex = v;
                        }
                    }

                    RenderField(u, (int)Form1.Fields.MARKED);
                }
                SptSet.Add(InputGraph[tempIndex.X, tempIndex.Y]);
            }
            RetracePath(InputGraph, start, end);
            SptSet.Clear();
        }

        // Traverse path in reverse order from end to start - finds one of the possibilities for shortest path between start and end
        public static void RetracePath(XYButton[,] ButtonGrid, Point start, Point end)
        {
            // Reverse list of vertices in shortest path from start to end
            List<Point> SptStartEnd = new List<Point>() { end };
            Point currentVertex = end;
            XYButton temp = ButtonGrid[currentVertex.X, currentVertex.Y];

            while(currentVertex != start)
            {
                currentVertex = temp.adjacentFields.Find(f => ButtonGrid[f.X, f.Y].shortestPathLength == temp.shortestPathLength - 1);
                temp = ButtonGrid[currentVertex.X, currentVertex.Y];
                RenderField(temp, (int)Form1.Fields.MARKED); // make sure last vertex is marked visually

                if (currentVertex != start)
                    SptStartEnd.Add(currentVertex);
            }
            SptStartEnd.RemoveAt(0);
            SptStartEnd.Reverse();
            DrawPath(SptStartEnd, ButtonGrid);
        }

        private static void DrawPath(List<Point> SptStartEnd, XYButton[,] ButtonGrid)
        {
            foreach (Point p in SptStartEnd)
            {
                RenderField(ButtonGrid[p.X,p.Y], (int)Form1.Fields.PATH); // p, p added because i cannot have default parameters for control button???
                Thread.Sleep(15); // Show path as 'animation' instead of instantly
            }
        }

        // Change so this is more modularized and encompasses Pathtracing coloring/rendering as well
        private static void RenderField(XYButton b, int FieldType)
        {
            Form1.RenderField(b, FieldType);
        }

    }
}
