using AlgoVisualizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoVisualizer
{
    public partial class Form1 : Form
    {
        // GLOBAL 
        private GroupBox groupBox1;

        public const int GRID_BOUNDARY_X = 39;
        public const int GRID_BOUNDARY_Y = 38;
        private Models.XYButton[,] ButtonGrid = new Models.XYButton[GRID_BOUNDARY_X, GRID_BOUNDARY_Y];

        public enum Fields {WALL,
                            END_POINT,
                            MARKED,
                            PATH,
                            EMPTY}

        public static Color MARKED_COLOR = Color.CornflowerBlue;
        public static Color PATH_COLOR = Color.Yellow;
        public static Color END_COLOR = Color.Red;
        public static Color WALL_COLOR = Color.Black;
        public static Color EMPTY_COLOR = Color.Transparent;


        public static Point START = new Point(-1, -1);
        public static Point END = new Point(-1, -1);

        public Form1()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            InitializeComponent();
            this.Text = "Algorithm Visualizer - Nrholm1";
            this.Size = new Size(800, 800);
            InitializeGrid();
        }

        private void ResetForm()
        {
            foreach (XYButton b in ButtonGrid)
            {
                b.InitValues(b.index.X, b.index.Y);
            }
            START = new Point(-1, -1);
            END = new Point(-1, -1);

        }

        private void Form1_Load(object sender, EventArgs e) { }


        // ------------------------------------------------------------------------------------------------------------------------ //
        // ------------------------------------------------------------------------------------------------------------------------ //
        // ------------------------------------------------------------------------------------------------------------------------ //


        private void InitializeGrid()
        {
            this.groupBox1 = new GroupBox();
            groupBox1.Size = new Size(this.Width, this.Height);
            groupBox1.Location = new Point(0, 0);

            const int fieldSize = 20;

            for (int i = 0; i < ButtonGrid.GetLength(0); i++)
            {
                for (int j = 0; j < ButtonGrid.GetLength(1); j++)
                {
                    ButtonGrid[i, j] = new Models.XYButton(i, j);
                    ButtonGrid[i, j].Size = new Size(fieldSize, fieldSize);
                    ButtonGrid[i, j].Location = new Point(fieldSize * i, fieldSize * j);
                    ButtonGrid[i, j].MouseDown += new MouseEventHandler(FieldClicked);


                    ButtonGrid[i, j].KeyPress += new KeyPressEventHandler(AlgorithmEvent);
                    this.groupBox1.Controls.Add(this.ButtonGrid[i, j]);
                }
            }

            this.Controls.Add(groupBox1);
        }


        public static void ChangeBackColor(XYButton b, Color color)
        {
            b.BackColor = color;
        }

        public static void FieldClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                MarkStartEnd(sender, e);
            if (e.Button == MouseButtons.Left)
                MarkWall(sender, e);
        }

        public static void MarkWall(object sender, MouseEventArgs e)
        {
            XYButton b = sender as XYButton;
            b.ToggleWall();
        }

        public static void MarkStartEnd(object sender, MouseEventArgs e)
        {
            XYButton b = sender as XYButton;
            bool changed = false;

            if (b.index == START)
            {
                changed = true;
                START = new Point(-1, -1);
            }
            if (b.index == END)
            {
                changed = true;
                END = new Point(-1, -1);
            }
            
            if (START.X == -1 && START.Y == -1 && !changed)
                START = b.index;
            else if (END.X == -1 && END.Y == -1 && !changed)
                END = b.index;


            b.shortestPathLength = b.index == START ? 0 : int.MaxValue;
            //b.shortestPathLength = b.index == START || b.index == END ? 0 : int.MaxValue;
            int fieldType = b.index == START || b.index == END ? (int)Fields.END_POINT : (int)Fields.EMPTY;
            Console.WriteLine($"b.index == START: {b.index == START} | START: {START} END: {END}");

            RenderField(b, fieldType);
        }


        public static void RenderField(XYButton b, int FieldType)
        {
            switch(FieldType)
            {
                case (int)Fields.MARKED:
                    Helpers.Render.MarkedField(b, START, END);
                    break;
                case (int)Fields.END_POINT:
                    Helpers.Render.EndField(b);
                    break;
                case (int)Fields.WALL:
                    Helpers.Render.WallField(b);
                    break;
                case (int)Fields.PATH:
                    Helpers.Render.PathField(b);
                    break;
                case (int)Fields.EMPTY:
                    Helpers.Render.EmptyField(b);
                    break;
            }
        }


        public void AlgorithmEvent(object sender, KeyPressEventArgs e)
        {
            // spacebar = Dijkstra Pathfinder
            if (e.KeyChar == (char)Keys.Space && START.X != -1 && END.X != -1)
            { 
                Algorithms.Pathfinder.CalculateShortestPathLengths(ButtonGrid, START, END);
                Console.WriteLine($"Shortest path between ({START.X},{START.Y}) and ({END.X},{END.Y}) is {ButtonGrid[END.X,END.Y].shortestPathLength}");
            }

            // m = Maze Generator recursive backtracker
            if (e.KeyChar == 'm')
            {
                Algorithms.RecursiveDivision.GenerateMaze(ButtonGrid);
                //Algorithms.RandomPointCloud.GeneratePointCloud(500, ButtonGrid);
            }

            // r = Reset grid and reinitialize values
            if (e.KeyChar == (char)'r')
            {
                ResetForm();
            }
        }


    }
}
