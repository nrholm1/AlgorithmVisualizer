using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoVisualizer.Models;

namespace AlgoVisualizer.Helpers
{
    class Render
    {
        private static Color MARKED_COLOR = Form1.MARKED_COLOR;
        private static Color PATH_COLOR = Form1.PATH_COLOR;
        private static Color END_COLOR = Form1.END_COLOR;
        private static Color WALL_COLOR = Form1.WALL_COLOR;
        private static Color EMPTY_COLOR = Form1.EMPTY_COLOR;
        public static void MarkedField(XYButton b, Point START, Point END)
        {
            if (b.index != START && b.index != END && b.BackColor != MARKED_COLOR)
            {
                Form1.ChangeBackColor(b, MARKED_COLOR);
                b.Update();
            }
        }
        public static void WallField(XYButton b)
        {
            Form1.ChangeBackColor(b, WALL_COLOR);
            b.Update();
        }
        public static void EndField(XYButton b)
        {
            Form1.ChangeBackColor(b, END_COLOR);
            b.Update();
        }
        public static void PathField(XYButton b)
        {
            Form1.ChangeBackColor(b, PATH_COLOR);
            b.Update();
        }
        public static void EmptyField(XYButton b)
        {
            Form1.ChangeBackColor(b, EMPTY_COLOR);
            b.Update();
        }
    }
}
