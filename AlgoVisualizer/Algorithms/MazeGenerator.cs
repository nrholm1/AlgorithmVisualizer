using AlgoVisualizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoVisualizer.Algorithms
{
    public static class RecursiveDivision
    {
        private const char HORIZONTAL = 'h';
        private const char VERTICAL = 'v';

        private static int GRID_BOUNDARY_X = Form1.GRID_BOUNDARY_X;
        private static int GRID_BOUNDARY_Y = Form1.GRID_BOUNDARY_Y;
        private static Random rand = new Random();
        private static Queue<ButtonBar> AreaQueue = new Queue<ButtonBar>();

        public static void GenerateMaze(XYButton[,] InputGraph)
        {
            ButtonBar initialBar = PickInitialBar(InputGraph);

            FillBar(initialBar, InputGraph);



            ButtonBar currentBar = PickBar(initialBar);
            //Console.WriteLine($"({AreaQueue.Peek().Index.X},{AreaQueue.Peek().Index.Y}) - Direction: {AreaQueue.Peek().Direction}");

            AreaQueue.Enqueue(currentBar);

            FillBar(AreaQueue.Peek(), InputGraph);
            //Console.WriteLine($"({AreaQueue.Peek().Index.X},{AreaQueue.Peek().Index.Y}) - Direction: {AreaQueue.Peek().Direction}");
            //Console.WriteLine($"Length of queue: {AreaQueue.Count()}");

            AreaQueue.Dequeue();
            

        }

        private static ButtonBar PickBar(ButtonBar btnBar)
        {
            int x = 0;
            int y = 0;
            string dir = "";
            int len = 0;
            ButtonBar newBar;
            if (btnBar.Direction == "left" || btnBar.Direction == "right")
            {
                x = btnBar.Direction == "left" ? _RNG(btnBar.Index.X - btnBar.Length, btnBar.Index.X) : _RNG(btnBar.Index.X, btnBar.Index.X + btnBar.Length);
                y = btnBar.Index.Y;
                dir = PickDirection(VERTICAL); 
                len = btnBar.Direction == "left" ? btnBar.Index.X : GRID_BOUNDARY_X - btnBar.Index.X;
            }
            if (btnBar.Direction == "up" || btnBar.Direction == "down")
            {
                x = btnBar.Index.X;
                y = btnBar.Direction == "up" ? _RNG(btnBar.Index.Y - btnBar.Length, btnBar.Index.Y) : _RNG(btnBar.Index.Y, btnBar.Index.Y + btnBar.Length);
                dir = PickDirection(HORIZONTAL); 
                len = btnBar.Direction == "down" ? btnBar.Index.Y : GRID_BOUNDARY_Y - btnBar.Index.Y;
            }
            newBar = new ButtonBar(x, y, dir, len);
            return newBar;
        }

        private static ButtonBar PickInitialBar(XYButton[,] InputGraph)
        {
            ButtonBar initBar;

            if (_RNG(0,100) > 50)
            {
                // Make column wall
                int x = _RNG(0, InputGraph.GetLength(0));
                string dir = _RNG(0, 100) > 50 ? "up" : "down";
                initBar = dir == "down" ? new ButtonBar(x, 0, dir, InputGraph.GetLength(1)) : new ButtonBar(x, InputGraph.GetLength(1) - 1, dir, InputGraph.GetLength(1));
            }
            else
            {
                // Make row wall
                int y = _RNG(0, InputGraph.GetLength(1));
                string dir = _RNG(0, 100) > 50 ? "left" : "right";
                initBar = dir == "right" ? new ButtonBar(0, y, dir, InputGraph.GetLength(0)) : new ButtonBar(InputGraph.GetLength(0) - 1, y, dir, InputGraph.GetLength(0));
            }

            return initBar;

        }

        private static void FillBar(ButtonBar btnBar, XYButton[,] InputGraph)
        {
            Console.WriteLine($"start=({btnBar.Index.X},{btnBar.Index.Y})");
            Console.WriteLine($"len={btnBar.Length} direction={btnBar.Direction}");


            if (btnBar.Direction == "left")
                for (int i = 0; i < btnBar.Length; i++)
                {
                    InputGraph[btnBar.Index.X - i, btnBar.Index.Y].ToggleWall();
                }

            if (btnBar.Direction == "right")
                for (int i = 0; i < btnBar.Length; i++)
                    InputGraph[btnBar.Index.X + i, btnBar.Index.Y].ToggleWall();

            if (btnBar.Direction == "up")
                for (int i = 0; i < btnBar.Length; i++)
                    InputGraph[btnBar.Index.X, btnBar.Index.Y - i].ToggleWall();

            if (btnBar.Direction == "down")
                for (int i = 0; i < btnBar.Length; i++)
                    InputGraph[btnBar.Index.X, btnBar.Index.Y + i].ToggleWall();
        }


        private static int _RNG(int min=0, int max=1)
        {
            return rand.Next(min, max);
        }

        private static string PickDirection(char direction)
        {
            switch (direction)
            {
                case 'h':
                    return _RNG(0, 100) > 50 ? "left" : "right";
                case 'v':
                    return _RNG(0, 100) > 50 ? "up" : "down";
                default:
                    throw new Exception("Unknown axis in directional parameter");
            }
        }
    }

    public static class RandomPointCloud
    {
        private static Random rand = new Random();
        
        public static void GeneratePointCloud(int numberOfPoints, XYButton[,] ButtonGrid)
        {
            int x;
            int y;
            for(int i = 0; i <= numberOfPoints; i++)
            {
                x = _RNG(39);
                y = _RNG();

                ButtonGrid[x, y].ToggleWall();
            }
        }

        private static int _RNG(int boundary=38)
        {    
            return rand.Next(boundary);
        }
    }
}
